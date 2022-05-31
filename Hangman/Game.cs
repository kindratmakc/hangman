using System.Text;

namespace Hangman;

public class Game
{
    private readonly Dictionary<char, bool> _availableLetters = new()
    {
        {'a', true}, {'b', true}, {'c', true}, {'d', true}, {'e', true}, {'f', true}, {'g', true}, {'h', true},
        {'i', true}, {'j', true}, {'k', true}, {'l', true}, {'m', true}, {'n', true}, {'o', true}, {'p', true},
        {'q', true}, {'r', true}, {'s', true}, {'t', true}, {'u', true}, {'v', true}, {'w', true}, {'x', true},
        {'y', true}, {'z', true}
    };

    private string _picked;
    private StringBuilder _guessed;
    private bool _isWon;
    private byte _fails;

    public Game(IList<string> words, IRandom random)
    {
        PickWord(words, random);
        FillGuessedWithPlaceholders();
    }

    public void GuessLetter(char letter)
    {
        EnsureThatLetterAvailable(letter);
        MakeLetterUnavailable(letter);
        CheckGuess(letter);
        CheckWon();
        RevealWordIfLost();
    }

    private void RevealWordIfLost()
    {
        if (IsLost())
        {
            _guessed = new StringBuilder(_picked);
        }
    }

    private void CheckWon()
    {
        if (!IsLost() && !_guessed.ToString().Contains('_'))
        {
            _isWon = true;
        }
    }

    private void CheckGuess(char letter)
    {
        var guessed = false;
        for (var i = 0; i < _picked.Length; i++)
        {
            if (_picked[i] == letter)
            {
                _guessed[i] = letter;
                guessed = true;
            }
        }

        if (!guessed)
        {
            _fails++;
        }
    }

    private void MakeLetterUnavailable(char letter)
    {
        _availableLetters[letter] = false;
    }

    private void EnsureThatLetterAvailable(char letter)
    {
        if (!_availableLetters[letter])
        {
            throw new LetterUnavailableException(letter);
        }
    }

    public string GetWord()
    {
        return _guessed.ToString();
    }

    private void PickWord(IList<string> words, IRandom random)
    {
        _picked = words[random.Next(words.Count)];
    }

    private void FillGuessedWithPlaceholders()
    {
        _guessed = new StringBuilder("".PadRight(_picked.Length, '_'));
    }

    public bool IsWon()
    {
        return _isWon;
    }

    public bool IsLost()
    {
        return _fails >= GetAllowedFailsCount();
    }

    public bool IsFinished()
    {
        return IsWon() || IsLost();
    }

    public IDictionary<char, bool> GetAvailableLetters()
    {
        return _availableLetters;
    }

    public byte GetFails()
    {
        return _fails;
    }

    private int GetAllowedFailsCount()
    {
        return _picked.Distinct().ToList().Count;
    }
}

public interface IRandom
{
    public int Next(int max);
}