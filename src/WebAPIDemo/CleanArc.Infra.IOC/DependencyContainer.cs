using CleanArc.Application.Interfaces;
using CleanArc.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArc.Infra.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // services.Add
            services.AddScoped<ISpeechService, SpeechService>();
        }
    }
}
