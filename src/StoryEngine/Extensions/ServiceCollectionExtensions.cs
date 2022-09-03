using Microsoft.Extensions.DependencyInjection;
using StoryEngine.Configuration;
using StoryEngine.Graphics;
using System.Reflection;

namespace StoryEngine.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoryEngine(
            this IServiceCollection services,
            EngineConfiguration configuration,
            params Assembly[] assembliesToScan)
        {
            services.AddSingleton(_ => configuration);
            services.AddSingleton<IScenesManager, ScenesManager>();
            services.AddSingleton<IWindow, Window>();
            services.AddSingleton<Engine>();

            services.Scan(scan => scan
                .FromAssemblies(assembliesToScan)
                    .AddClasses(classes => classes
                        .AssignableTo<IScene>())
                    .AsSelf()
                    .WithTransientLifetime());

            return services;
        }
    }
}
