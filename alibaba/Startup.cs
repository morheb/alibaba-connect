using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using alibaba.Common;
using alibaba.interfaces;
using alibaba.Mapper;
using alibaba.Repos;
using alibaba.Sql.Data;

namespace alibaba
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().

                   AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).
                   AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSwaggerGen();
            services.AddTransient<UserService>();
            services.AddTransient<IDbSettings, SqlDbSettings>();
            services.AddTransient<TalabakService>();
            services.AddTransient<ITalabakRepo, TalabakRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            services.AddTransient<OrderService>();
            services.AddTransient<ICommentRepo, CommentRepo>();
            services.AddTransient<AddonService>();
            services.AddTransient<IAddonRepo, AddonRepo>();
            services.AddTransient<CommentService>();
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<CategoryService>();
            services.AddTransient<IConstantRepo, ConstantRepo>();
            services.AddTransient<ConstantService>();
            services.AddTransient<IBannerRepo, BannerRepo>();
            services.AddTransient<BrandService>();
            services.AddTransient<IBrandRepo, BrandRepo>();
            services.AddTransient<BannerService>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<RestaurantService>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<ProductService>();
            services.AddTransient<IRestaurantRepo, RestaurantRepo>();
            services.AddTransient<RatingService>();
            services.AddTransient<IRatingRepo, RatingRepo>();
            services.AddLogging();
            services.AddSwaggerGen();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            var connectionString = Configuration.GetSection("ConnectionStrings");
            if (connectionString != null)
            {
                services.Configure<DbSettings>(connectionString);
            }



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName.Production;


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpMethodOverride();
            app.UseForwardedHeaders();
            app.UseRouting();
            app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
