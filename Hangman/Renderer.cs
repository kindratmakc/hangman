namespace Hangman;

public static class Renderer
{
    public static void Draw(Game game)
    {
        Console.Clear();
        DrawWord(game.GetWord());
        Console.WriteLine();
        DrawAvailableLetters(game.GetAvailableLetters());
        Console.WriteLine();
        DrawHangman(game.GetFails());
    }

    public static void DrawFinish(Game game)
    {
        Console.WriteLine();
        Console.WriteLine(game.IsWon() ? "You WIN!!!" : "You LOSE :(");
        Console.WriteLine();
    }

    private static void DrawWord(string word)
    {
        Console.WriteLine($"Word of {word.Length} letters:");
        Console.WriteLine();
        Console.Write("         ");
        foreach (var letter in word)
        {
            Console.Write(' ');
            Console.Write(letter.ToString().ToUpper());
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    private static void DrawAvailableLetters(IDictionary<char, bool> availableLetters)
    {
        Console.WriteLine("Available letters:");
    
        foreach (var letter in availableLetters)
        {
            Console.Write(' ');

            if (!letter.Value)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            Console.Write(letter.Key.ToString().ToUpper());
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }

    private static void DrawHangman(byte fails)
    {
        Console.WriteLine($"Fails: {fails}");
        Console.WriteLine();
    }
}