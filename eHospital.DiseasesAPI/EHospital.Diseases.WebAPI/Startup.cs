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
using EHospital.Diseases.Data;
using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.BusinessLogic.Services;
using EHospital.Diseases.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace EHospital.Diseases.WebAPI
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddDbContext<DiseaseDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EHospitalDatabase")));

            Mapper.Initialize(cfg => cfg.AddProfile<AutomapperProfileConfig>());
            services.AddScoped<IUniteOfWork, UnitOfWork>();

            services.AddScoped<IRepository<Disease>, Repository<Disease>>();
            services.AddScoped<IRepository<DiseaseCategory>, Repository<DiseaseCategory>>();
            services.AddScoped<IRepository<PatientDisease>, Repository<PatientDisease>>();
            services.AddScoped<IRepository<UsersData>, Repository<UsersData>>();

            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IDiseaseCategoryService, DiseaseCategoryService>();
            
            services.AddScoped<IPatientDiseaseService, PatientDiseaseService>();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            log.Info("Using Disease API");

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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));
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

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "eHospital");
            });
        }
    }
}
