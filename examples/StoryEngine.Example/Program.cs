using Microsoft.Extensions.DependencyInjection;
using StoryEngine.Core.Configuration;
using StoryEngine.Core;
using StoryEngine.Core.Extensions;
using StoryEngine.Example;

var engineConfiguration = new EngineConfiguration();

var serviceCollection = new ServiceCollection();
serviceCollection.AddStoryEngine(engineConfiguration, typeof(Program).Assembly);

using var serviceProvider = serviceCollection.BuildServiceProvider();
var engine = serviceProvider.GetRequiredService<Engine>();
engine.Run<EntryScene>();
