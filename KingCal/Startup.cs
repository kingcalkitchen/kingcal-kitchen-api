using AutoMapper;
using KingCal.Data;
using KingCal.Data.Models;
using KingCal.Service.Helpers;
using KingCal.Service.Implementations;
using KingCal.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace KingCal
{
    public class Startup
    {
        public readonly AppSettings _appSettings;
        public readonly IConfigurationSection _appSettingsSection;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _appSettingsSection = Configuration.GetSection("AppSettings");
            _appSettings = _appSettingsSection.Get<AppSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(_appSettingsSection);

            string connectionString =
                $"Data Source={_appSettings.HOSTNAME};Initial Catalog={_appSettings.DB_NAME};User Id={_appSettings.USERNAME};Password={_appSettings.PASSWORD};";

            services.AddDbContext<DataContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<IUser, User>();
            services.AddScoped<IRole, Role>();
            services.AddScoped<IUserRoles, UserRoles>();
            
            services.AddScoped<IAddress, Address>();
            services.AddScoped<IProperty, Property>();
            services.AddScoped<ICategory, Category>();
            services.AddScoped<ISubCategory, SubCategory>();
            services.AddScoped<IItem, Item>();
            services.AddScoped<ISubItem, SubItem>();
            services.AddScoped<IItemSubItem, ItemSubItem>();
            services.AddScoped<ISubItemProperty, SubItemProperty>();

            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(_appSettings.SECRET);
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
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KingCal Kitchen API", Version = "v1" });
            });

            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMvc(x => 
            {
                x.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KingCal Kitchen APIs");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
