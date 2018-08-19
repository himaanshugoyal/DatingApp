using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Order of services is not so important
        public void ConfigureServices(IServiceCollection services)
        {
            
           services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddTransient<Seed>();
            /*Note:
                //Inside the start up class we are going to add this as a service.
                //When we do this, it will available for injection throughout the rest of our application
                3 options to include Auhorization as service    
                1. AddSingleton - We create single instance of our repository throughout application
                It creates the instance for the first time and then we use the same object in all of the calls
                this particular one can cause issues, when it comes to concurrent requests.
                2. AddTransient- This one is useful for lightweight stateless services.
                Because these are created each time they  are requested.
                3. AddScoped - The service is created once per request within the scope.
                It is equivalent to Singleton but within the current scope itself
                For eg: It creates one instance for each http requests. It uses the same instance within the course of the same web requests.
             */
             //This will available for injection
            services.AddScoped<IAuthRepository,AuthRepository>();
            //Authentication Scheme that our application is going to use
            /* Telling ASP.net what type of authentication are we using */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //Configuration
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //validate if our key is valid
                    ValidateIssuerSigningKey = true,
                    //Need to convert the key into byte array
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSettings:Token").Value )),
                    //currently our issuer is localhost and validator is audience host, so currently we are nog
                    //going to validate that.
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Order is really important in this
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
        {
            if (env.IsDevelopment())
            {
                // This is the developer exception page, this is the page that we see
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // If we are not in developement mode,
                // Adds a middleware to the pipeline that will catch exceptions, log them, and re-execute the request in an alternate pipeline. The request will not be re-executed if the response has already started
                app.UseExceptionHandler(builder => 
                {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            //We are going to add extension method
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
               // app.UseHsts();
            }

            // Commented as the user data seed is completed now, if seed is required again, we can run the application again.
            // seeder.SeedUsers();
            
            //  app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            //This routes our request to the correct controller
            app.UseMvc();
        }
    }
}
