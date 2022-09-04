using Microsoft.Extensions.DependencyInjection;
using StoryEngine;
using StoryEngine.Configuration;
using StoryEngine.Example;
using StoryEngine.Extensions;

var engineConfiguration = new EngineConfiguration();

var serviceCollection = new ServiceCollection();
serviceCollection.AddStoryEngine(engineConfiguration, typeof(Program).Assembly);

using var serviceProvider = serviceCollection.BuildServiceProvider();
var engine = serviceProvider.GetRequiredService<Engine>();
engine.Run<EntryScene>();
