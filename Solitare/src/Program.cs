using Solitare;
using Terminal.Gui;

/// <summary>
/// Runs <c>StartView</c>.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        Application.Init();

        try
        {
            Application.Run(new StartView());
        }
        finally
        {
            Application.Shutdown();
        }
    }
}


