using AutoMapper;
using EHospital.Diseases.BusinessLogic.Contracts;
using EHospital.Diseases.BusinessLogic.Services;
using EHospital.Diseases.Data;
using EHospital.Diseases.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

      // Add Cors policy.
      services.AddCors(o => o.AddPolicy("MyCors Policy", builder =>
      {
        builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      }));

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

      //// Register Cors authorization filter.
      services.Configure<MvcOptions>(options =>
      {
        options.Filters.Add(new CorsAuthorizationFilterFactory("MyCors Policy"));
      });

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

      // Enable Cors.
      app.UseCors("MyCors Policy");

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
