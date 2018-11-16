using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using eHospital.Diseases.DA;
using eHospital.Diseases.DA.Contracts;
using eHospital.Diseases.DA.Repository;
using eHospital.Diseases.DA.Entities;
using eHospital.Diseases.Domain.Contracts;
using eHospital.Diseases.Domain.Services;

namespace eHospital.Diseases.API
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
            string connection = @"Server=DESKTOP-PU90CNF;Database=eHealthDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<DiseaseDBContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IRepository<Disease>, Repository<Disease>>();
            services.AddScoped<IRepository<DiseaseCategory>, Repository<DiseaseCategory>>();
            services.AddScoped<IRepository<PatientDisease>, Repository<PatientDisease>>();
            services.AddScoped<IDisease, DiseaseService>();
            services.AddScoped<IDiseaseCategory, DiseaseCategoryService>();
            // TODO: create also PatientDisease service
            services.AddSingleton<IUniteOfWork, UnitOfWork>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                       
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "eHospital",
                    Description = "Diseases web api for eHospital Project",
                    TermsOfService = "Welcome everybody!",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact() { Name = "Alex Brylov", Email = "aksu@ukr.net" }
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eHospital");
            });
        }
    }
}
