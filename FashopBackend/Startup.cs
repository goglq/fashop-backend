using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Core.Services;
using FashopBackend.Graphql;
using FashopBackend.Graphql.Types;
using FashopBackend.Infrastructure.Data;
using FashopBackend.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashopBackend
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(name: "AllowAll", builder => 
                builder
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                )
            );

            string dbConnectionString =
                $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" + 
                $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";

            services.AddDbContext<FashopContext>(opt => opt.UseNpgsql(dbConnectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<CategoryType>()
                .AddType<ProductType>()
                .AddFiltering()
                .AddSorting();

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FashopBackend", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FashopBackend v1"));
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapControllers();
            });

            app.UseGraphQLVoyager("/graphql-voyager/");
        }
    }
}
