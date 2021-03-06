using Glimpse.Initialization;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Glimpse.Agent
{
    public static class GlimpseAgentExtension
    {
        public static IApplicationBuilder UseGlimpseAgent(this IApplicationBuilder app)
        {
            var manager = app.ApplicationServices.GetRequiredService<IAgentStartupManager>();
            manager.Run(new StartupOptions(app));

            return app.UseMiddleware<GlimpseAgentMiddleware>(app);
        }
    }
}