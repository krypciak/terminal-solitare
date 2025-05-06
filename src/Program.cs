using myproj;
using Terminal.Gui;

public class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        try
        {
            new Game(Difficulty.Easy);
            // Application.Run(new StartView());
        }
        finally
        {
            Application.Shutdown();
        }
    }
}


