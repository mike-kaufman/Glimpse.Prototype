﻿using System.Diagnostics.Tracing;
using Glimpse.Agent;
using Glimpse.Server;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.FunctionalTest.Website
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGlimpse()
                .RunningAgentWeb()
                .RunningServerWeb()
                .WithLocalAgent();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseGlimpseServer();
            app.UseGlimpseAgent();

            app.UseMvcWithDefaultRoute();
        }
    }
}
