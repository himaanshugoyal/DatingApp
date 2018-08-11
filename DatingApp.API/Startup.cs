using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Order is really important in this
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseHsts();
            }

            //  app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //This routes our request to the correct controller
            app.UseMvc();
        }
    }
}
