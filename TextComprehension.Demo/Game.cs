using TextComprehension.Demo.Options;
using TextComprehension.ImageGeneration.Interface;
using TextComprehension.ImageGeneration.Models;
using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Models;
using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Models;

namespace TextComprehension.Demo;

public class Game
{
    private readonly IProvidedChoiceSelector _choiceSelector;
    private readonly IObserverMover _observerMover;
    private readonly ISceneDescriber _sceneDescriber;
    private readonly IImageGenerator _imageGenerator;
    
    private readonly Scene _scene;
    private ObserverState _observerState;

    public Game(Scene scene, 
        IProvidedChoiceSelector providedChoiceSelector,
        IObserverMover observerMover,
        ISceneDescriber sceneDescriber,
        IImageGenerator imageGenerator)
    {
        _choiceSelector = providedChoiceSelector;
        _observerMover = observerMover;
        _sceneDescriber = sceneDescriber;
        _imageGenerator = imageGenerator;
        
        _observerState = new ObserverState();
        _scene = scene;
    }
    
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        await PrintIntroAsync(cancellationToken);
        await RunGameLoopAsync(cancellationToken);
    }

    private Task PrintIntroAsync(CancellationToken cancellationToken)
    {
        var introMessage = new List<string>();
        
        introMessage.Add("Welcome to the game. Enter 'help' to list available actions.");

        var sceneIntroduction = _scene.Introduction ?? $"You're standing in {_scene.Description}.";
        introMessage.Add(sceneIntroduction);

        foreach (var line in introMessage) Console.WriteLine(line);
        
        return Task.CompletedTask;
    }

    private async Task RunGameLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested) 
            await GameLoopAsync(cancellationToken);
    }

    private async Task GameLoopAsync(CancellationToken cancellationToken)
    {
        var input = Input("What do you want to do?: ", cancellationToken);

        var result = _choiceSelector.GetChoices(input);
        
        var choice = result.Choices.FirstOrDefault();
        if (choice == null) Console.WriteLine("Invalid command. Enter 'help' for available commands.");
        else await EnactChoiceAsync(choice, cancellationToken);
    }

    private static string Input(string prompt, CancellationToken cancellationToken)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    private async Task EnactChoiceAsync(Choice choice, CancellationToken cancellationToken)
    {
        switch (choice.Option)
        {
            case HelpOption:
                await PrintHelpAsync(cancellationToken);
                break;
            case ForwardOption:
                _observerState = _observerMover.Move(_scene, _observerState, Movement.Forward);
                break;
            case LeftOption:
                _observerState = _observerMover.Move(_scene, _observerState, Movement.TurnLeft);
                break;
            case RightOption:
                _observerState = _observerMover.Move(_scene, _observerState, Movement.TurnRight);
                break;
            case TakePictureOption:
                var description = _sceneDescriber.DescribeScene(_scene, _observerState);
                var response = await _imageGenerator.GenerateImageAsync(new ImageGenerationRequest
                {
                    Prompt = description.Description
                });
                break;
        }
    }

    private async Task PrintHelpAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Available commands are: forward, left, right, take picture");
    }
}