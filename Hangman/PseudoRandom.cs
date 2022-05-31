namespace Hangman;

public class PseudoRandom : IRandom
{
    private Random _random;

    public PseudoRandom()
    {
        _random = new Random();
    }

    public int Next(int max)
    {
        return _random.Next(max);
    }
}