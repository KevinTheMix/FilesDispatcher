using Dispatch.Domain;
using Dispatch.WebAPI.Models;

namespace Dispatch.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Initialize configuration provider
            Domain.ConfigurationProvider.Initialize(AppContext.BaseDirectory);

            // Add services to the container.

            // v1: read & inject a custom config section.
            //builder.Services.Configure<DispatchSection>(builder.Configuration.GetSection("Dispatch"));
            // v2: construct a BL with a custom config section, and inject it instead.
            DispatchSection? dispatchSection = builder.Configuration.GetSection("Dispatch").Get<DispatchSection>();  // See https://stackoverflow.com/a/70771643/3559724
            if (dispatchSection == null) return;

            // Validate directories from configuration
            string inDir = Domain.ConfigurationProvider.GetInDirectory();
            string outDir = Domain.ConfigurationProvider.GetOutDirectory();

            if (String.IsNullOrEmpty(inDir) || String.IsNullOrEmpty(outDir))
            {
                Console.Error.WriteLine("Error: InDirectory or OutDirectory from appsettings.json does not exist or is not configured.");
                return;
            }
            // builder.Services.AddSingleton<IEngine>(new Engine(dispatchSection.InDirectory, dispatchSection.OutDirectory));
            builder.Services.AddSingleton<IEngine>(new Engine(inDir, outDir));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Inspired by https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/web-host?view=aspnetcore-6.0
            // v1: Does not work:
            //WebHost.CreateDefaultBuilder(args).UseUrls("http://192.168.1.14:5000");
            // v2: Causes error:
            //builder.Host.ConfigureWebHost((configure) =>
            //{
            //    configure.UseUrls("http://192.168.1.14:5000");
            //});
            // v3: works!
            builder.WebHost.UseUrls("http://192.168.1.14:5000");
            // v4: "dynamic" IP.
            builder.WebHost.UseUrls($"http://{dispatchSection.LocalMachineIp}:5000");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            // Koko: first off, let's make it easy on ourselves...
            app.UseDeveloperExceptionPage();    // Does not seem to work..
            // Go to swagger/v1/swagger.json to check the error instead.

            //app.UseRouting();
            //GlobalConfiguration.Configure(WebApiConfig.Register);

            app.Run();
        }
    }

    //// See https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
    //public static class WebApiConfig
    //{
    //    public static void Register(HttpConfiguration config)
    //    {
    //        // Web API routes
    //        config.MapHttpAttributeRoutes();

    //        // Other Web API configuration not shown.
    //    }
    //}
}