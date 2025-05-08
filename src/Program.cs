using System.Text.Json;
using solitare;
using Terminal.Gui;

public class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        try
        {
            // new Game(Difficulty.Easy, 123);

            // var json = File.ReadAllText("/home/krypek/home/Programming/repos/programming-exercises/gigathon/2025/solitare/state.json");
            // var state = GameState.FromJSON(json);
            new Game(new GameState(123, Difficulty.Easy));
            // Application.Run(new StartView());
        }
        finally
        {
            Application.Shutdown();
        }
    }
}


