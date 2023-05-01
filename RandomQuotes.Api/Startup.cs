using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RandomQuotes.Abstractions;
using RandomQuotes.Abstractions.Models;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Core;
using RandomQuotes.Core.Services;
using RandomQuotes.DataAccess;
using RandomQuotes.DataAccess.Repositories;

namespace RandomQuotes.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RandomQuotes", Version = "v1" });

            c.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });
        services.AddRouting(c => c.LowercaseUrls = true);

        RegisterMongoDbCollections(services);
        RegisterAutoMapper(services);

        services.AddScoped<IQuotesRepository, QuotesRepository>();
        services.AddScoped<IQuotesService, QuotesService>();

        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUsersService, UsersService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        AddAuth(services);
    }

    private void AddAuth(IServiceCollection services)
    {
        var authSettings = Configuration.GetSection(nameof(AuthorizationSettings))
            .Get<AuthorizationSettings>();
        services.AddSingleton(authSettings);

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RandomQuotes v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void RegisterMongoDbCollections(IServiceCollection services)
    {
        var mongoSection = Configuration.GetSection("RandomQuotesDatabase").Get<QuotesDatabaseSettings>();
        // It is recommended to store a MongoClient instance in a global place,
        // either as a static variable or in an IoC container with a singleton lifetime.
        // https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/connecting/#re-use
        services.AddSingleton(
            _ => new MongoClient(mongoSection.ConnectionString).GetDatabase(mongoSection.DatabaseName));
    }

    private static void RegisterAutoMapper(IServiceCollection services)
    {
        var mappings = new[]
        {
            typeof(ApiMappingProfile),
            typeof(CoreMappingProfile),
            typeof(DataAccessMappingProfile)
        };
        services.AddAutoMapper(mappings);
    }
}