using EnsureThat;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Hospital.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHospitalClient(
            this IServiceCollection services, 
            Uri address)
        {
            EnsureArg.IsNotNull(services, nameof(services));
            EnsureArg.IsNotNull(address, nameof(address));

            services
                .AddRefitClient<IHospitalCient>()
                .ConfigureHttpClient(options => options.BaseAddress = address);
        }
    }
}
