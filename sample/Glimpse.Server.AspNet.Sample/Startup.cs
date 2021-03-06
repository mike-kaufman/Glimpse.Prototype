﻿using Glimpse.Server;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Server.AspNet.Sample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                    .RunningServerWeb(settings => settings.AllowRemote = true); // Temp workaround for kestrel not implementing IHttpConnectionFeature;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();
            
            app.UseWelcomePage();
        }
    }
}
