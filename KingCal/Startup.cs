using KingCal.Data;
using KingCal.Models;
using KingCal.Service.Implementations;
using KingCal.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace KingCal
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
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            // TODO
            // create DB User
            string connectionString = $"Data Source={appSettings.HOSTNAME};Initial Catalog={appSettings.DB_NAME};User ID={appSettings.USERNAME};Password={appSettings.PASSWORD};";

            //services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString, b => b.MigrationsAssembly("KingCal")));
            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

            //services.AddScoped<IFood, Food>();
            services.AddScoped<IAddress, Address>();
            services.AddScoped<IProperty, Property>();
            services.AddScoped<ICategory, Category>();
            services.AddScoped<ISubCategory, SubCategory>();
            services.AddScoped<IItem, Item>();
            services.AddScoped<ISubItem, SubItem>();
            services.AddScoped<IItemSubItem, ItemSubItem>();
            services.AddScoped<ISubItemProperty, SubItemProperty>();

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
