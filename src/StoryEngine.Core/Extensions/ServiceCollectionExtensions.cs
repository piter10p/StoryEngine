using Microsoft.Extensions.DependencyInjection;
using StoryEngine.Core.Configuration;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;
using System.Reflection;

namespace StoryEngine.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStoryEngine(
            this IServiceCollection services,
            EngineConfiguration configuration,
            params Assembly[] assembliesToScan)
        {
            services.AddSingleton(_ => configuration);
            services.AddSingleton<IGameConsole, WindowsGameConsole>();
            services.AddSingleton<IScenesManager, ScenesManager>();
            services.AddSingleton<IWindow, Window>();
            services.AddSingleton<IGameSaver, GameSaver>();
            services.AddSingleton<IInputReader, InputReader>();
            services.AddSingleton<IButtonHandler, ButtonHandler>();
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
