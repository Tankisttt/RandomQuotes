using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RandomQuotes.Abstractions.Repositories;
using RandomQuotes.Abstractions.Services;
using RandomQuotes.Core;
using RandomQuotes.Core.Services;
using RandomQuotes.DataAccess;
using RandomQuotes.DataAccess.Repositories;

namespace RandomQuotes.Api
{
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
            
            RegisterMongoDb(services);
            RegisterAutoMapper(services);
            
            // TODO add dependency injection in assemblies
            services.AddTransient<IQuotesRepository, QuotesRepository>();
            services.AddTransient<IQuotesService, QuotesService>();
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

        private void RegisterMongoDb(IServiceCollection services)
        {
            services.AddSingleton(_ =>
                new MongoClient(Configuration.GetConnectionString("MongoDb")).GetDatabase("RandomQuotesDatabase"));
            // Configuration["MongoDbDataBaseName"]));
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
}