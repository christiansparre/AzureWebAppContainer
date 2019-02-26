using System;
using System.Collections;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureWebAppContainer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var version = "00010101.001-local";

                var path = Path.Combine(env.ContentRootPath, "version.txt");
                logger.LogInformation("Looking for version.txt in {path}", path);
                if (File.Exists(path))
                {
                    version = File.ReadAllText(path).Trim();
                }

                var builder = new StringBuilder();

                builder.AppendLine($"Hello World! I'm version '{version}'");
                builder.AppendLine();
                builder.AppendLine("Environment variables");

                foreach (DictionaryEntry kv in Environment.GetEnvironmentVariables())
                {
                    builder.AppendLine($"{kv.Key} = '{kv.Value}'");
                }

                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(builder.ToString());
            });
        }
    }
}
