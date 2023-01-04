using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RandomQuotes.Abstractions;
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
        });
        services.AddRouting(c => c.LowercaseUrls = true);
            
        RegisterMongoDbCollections(services);
        RegisterAutoMapper(services);
            
        services.AddScoped<IQuotesRepository, QuotesRepository>();
        services.AddScoped<IQuotesService, QuotesService>();
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

        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private void RegisterMongoDbCollections(IServiceCollection services)
    {
        var mongoSection = Configuration.GetSection("RandomQuotesDatabase").Get<QuotesDatabaseSettings>();
        // It is recommended to store a MongoClient instance in a global place,
        // either as a static variable or in an IoC container with a singleton lifetime.
        // https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/connecting/#re-use
        services.AddSingleton(_ => new MongoClient(mongoSection.ConnectionString).GetDatabase(mongoSection.DatabaseName));
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