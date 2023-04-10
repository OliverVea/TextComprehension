using TextComprehension.Level;
using TextComprehension.Demo;
using TextComprehension.Demo.ChoiceProviders;
using TextComprehension.ImageGeneration.DallE;
using TextComprehension.IO.Logic;
using TextComprehension.Level.Logic;

var cancellationToken = CancellationToken.None;

var choiceSelector = new ChoiceSelector();
var providedChoiceSelector = new ProvidedChoiceSelector(choiceSelector);

var helpProvider = new DefaultChoiceProvider();
providedChoiceSelector.AddOptionProvider(helpProvider);

var observerMover = new ObserverMover();
var sceneDescriber = new SceneDescriber();

var apiKey = Environment.GetEnvironmentVariable("DALLE_API_KEY");
if (apiKey == null) throw new ArgumentException("DALLE_API_KEY must be set.");

var imageGenerator = new DallEImageGenerator(apiKey);

var scene = Constants.DefaultScene;

var game = new Game(scene, providedChoiceSelector, observerMover, sceneDescriber, imageGenerator);
await game.RunAsync(cancellationToken);