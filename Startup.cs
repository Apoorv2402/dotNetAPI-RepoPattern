using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using StudyMash.API.DataAccess;
using StudyMash.API.Interfaces;
using StudyMash.API.Helpers;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StudyMashAPI
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
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("default")));
           
            services.AddControllers();
            services.AddCors();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            
            //services.AddScoped<ICityRepo, CityRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var SecretKey = Configuration.GetSection("AppSettings:Key").Value;
            //Authentication Service
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
               opt => opt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   IssuerSigningKey = Key
               }) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Production Mode Global Exception handler using Inbuilt middleware.
            else
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                var exeption = context.Features.Get<IExceptionHandlerFeature>();
                                if(exeption != null)
                                {
                                    await context.Response.WriteAsync(exeption.Error.Message);
                                }
                            });
                    }
                    );
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(m => m.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //Authentication Middleware
            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
