using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using log4net;
using System.Reflection;
using log4net.Config;
using System.IO;

namespace EHospital.Diseases.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logRepo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepo, new FileInfo("log4net.config"));
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
