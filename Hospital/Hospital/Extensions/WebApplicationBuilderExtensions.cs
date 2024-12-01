using EnsureThat;
using Serilog;

namespace Hospital.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddSerilog(
            this WebApplicationBuilder webAppBuilder)
        {
            EnsureArg.IsNotNull(webAppBuilder, nameof(webAppBuilder));

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(webAppBuilder.Configuration)
                .CreateLogger();

            webAppBuilder.Logging.ClearProviders();
            webAppBuilder.Logging.AddSerilog(Log.Logger);
        }
    }
}
