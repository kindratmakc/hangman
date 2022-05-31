namespace Hangman;

public static class Loop
{
    public static void PlayAGame()
    {
        do
        {
            StartANewGame();
        } while (AskIfPlayerWantToContinue());
    }

    private static void StartANewGame()
    {
        var game = new Game(WordsProvider.easy(), new PseudoRandom());

        while (!game.IsFinished())
        {
            Renderer.Draw(game);

            Console.WriteLine("Choose your letter:");
            try
            {
                game.GuessLetter(Console.ReadKey().KeyChar.ToString().ToLower()[0]);
            }
            catch (LetterUnavailableException)
            {
                Console.Beep();
            }
        }

        Renderer.Draw(game);
        Renderer.DrawFinish(game);
    }

    private static bool AskIfPlayerWantToContinue()
    {
        Console.WriteLine("Press any key to play again, press ESC to exit");

        return Console.ReadKey().Key != ConsoleKey.Escape;
    }
}