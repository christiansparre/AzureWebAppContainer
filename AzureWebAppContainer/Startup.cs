using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AzureWebAppContainer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var version = "00010101.001-local";

                var path = Path.Combine(typeof(Startup).Assembly.Location, "version.txt");
                if (File.Exists(path))
                {
                    version = File.ReadAllText(path).Trim();
                }

                await context.Response.WriteAsync($"Hello World! I'm version '{version}'");
            });
        }
    }
}
