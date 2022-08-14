using Dispatch.Domain;
using Dispatch.WebAPI.Models;

namespace Dispatch.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Koko: read & inject custom section
            //builder.Services.Configure<DispatchSection>(builder.Configuration.GetSection("Dispatch"));
            DispatchSection dispatchSection = builder.Configuration.GetSection("Dispatch").Get<DispatchSection>();
            builder.Services.AddSingleton<IEngine>(new Engine(dispatchSection.InDirectory, dispatchSection.OutDirectory));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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