namespace Hangman;

public class LetterUnavailableException : Exception
{
    public LetterUnavailableException(char letter): base($"Letter '{letter}' is unavailable")
    {
    }
}