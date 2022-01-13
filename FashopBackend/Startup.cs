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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.BrandImageAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.ProductImageAggregate;
using FashopBackend.Core.Aggregate.RoleAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Graphql.Errors;
using FashopBackend.SharedKernel.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using FashopBackend.Core.Aggregate.CartAggregate;

namespace FashopBackend
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AccessTokenSettings>(Configuration.GetSection("AccessTokenSettings"));
            services.Configure<RefreshTokenSettings>(Configuration.GetSection("RefreshTokenSettings"));
            
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            
            services.AddCors(options => options.AddPolicy(name: "AllowAll", builder => 
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials()
                )
            );
            
            services.AddHttpContextAccessor();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Configuration.GetSection("AccessTokenSettings").GetValue<string>("Issuer"),
                        ValidateIssuer = true,
                        ValidAudience = Configuration.GetSection("AccessTokenSettings").GetValue<string>("Audience"),
                        ValidateAudience = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AccessTokenSettings").GetValue<string>("Key"))),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                {
                    policy.RequireRole(new[] {"admin"});
                });
                options.AddPolicy("user", policy =>
                {
                    policy.RequireRole(new[] {"user", "admin"});
                });
            });

            string dbConnectionString =
                $"Server={Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"};" + 
                $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "5432"};" +
                $"Database={Environment.GetEnvironmentVariable("DB_NAME") ?? "fashop_db"};" +
                $"User Id={Environment.GetEnvironmentVariable("DB_USER") ?? "fashop_user"};" +
                $"Password={Environment.GetEnvironmentVariable("DB_PASS") ?? "123"}";

            services.AddDbContext<FashopContext>(opt => opt.UseNpgsql(dbConnectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandImageRepository, BrandImageRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddErrorFilter<GraphQLErrorFilter>();
            services
                .AddGraphQLServer()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddAuthorization()
                .AddType<UserType>()
                .AddType<BrandType>()
                .AddType<CategoryType>()
                .AddType<ProductType>()
                .AddFiltering()
                .AddSorting();

            services.AddControllers();

            services.AddRazorPages();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FashopBackend", Version = "v1" });
            //});
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FashopBackend v1"));
            }
            app.UseForwardedHeaders();
            
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager("/graphql-voyager");
        }
    }
}
