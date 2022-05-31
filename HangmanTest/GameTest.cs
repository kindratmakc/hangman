using Hangman;

namespace HangmanTest;

public class GameTest
{
    [Fact]
    public void init_the_game()
    {
        var game = CreateGame();

        Assert.False(game.IsWon());
        Assert.False(game.IsLost());
        Assert.False(game.IsFinished());
        Assert.Equal(0, game.GetFails());
        Assert.Equal(
            new Dictionary<char, bool>
            {
                {'a', true}, {'b', true}, {'c', true}, {'d', true}, {'e', true}, {'f', true}, {'g', true}, {'h', true},
                {'i', true}, {'j', true}, {'k', true}, {'l', true}, {'m', true}, {'n', true}, {'o', true}, {'p', true},
                {'q', true}, {'r', true}, {'s', true}, {'t', true}, {'u', true}, {'v', true}, {'w', true}, {'x', true},
                {'y', true}, {'z', true}
            }, game.GetAvailableLetters());
    }

    [Theory]
    [InlineData(0, "_____")]
    [InlineData(1, "____")]
    public void game_started_with_random_word(int index, string expectedWord)
    {
        var game = CreateFewWordGame(new List<string> {"lorem", "test"}, index);

        Assert.Equal(expectedWord, game.GetWord());
    }

    [Fact]
    public void guess_few_letters()
    {
        var game = CreateGame("godzilla");

        game.GuessLetter('l');

        Assert.Equal("_____ll_", game.GetWord());
        Assert.False(game.IsWon());
    }

    [Fact]
    public void letter_becomes_unavailable_after_guessing()
    {
        var game = CreateGame("test");

        game.GuessLetter('p');
        
        Assert.False(game.GetAvailableLetters()['p']);
    }
 
    [Fact]
    public void can_not_guess_same_letter_two_times()
    {
        var game = CreateGame("test");

        game.GuessLetter('p');

        Assert.Throws<LetterUnavailableException>(() => game.GuessLetter('p'));
    }

    [Fact]
    public void fails_count_increased_after_wrong_letter()
    {
        var game = CreateGame("test");
        
        game.GuessLetter('a');
        
        Assert.Equal(1, game.GetFails());
    }

    [Fact]
    public void fails_limited_to_the_number_of_distinct_letters()
    {
        var game = CreateGame("jazz");
        
        game.GuessLetter('b');
        game.GuessLetter('c');
        game.GuessLetter('d');
        
        Assert.True(game.IsLost());
        Assert.True(game.IsFinished());
    }

    [Fact]
    public void the_world_revealed_when_game_lost()
    {
        var game = CreateGame("jazz");
        
        game.GuessLetter('b');
        game.GuessLetter('c');
        game.GuessLetter('d');
        
        Assert.Equal("jazz", game.GetWord());
    }

    [Fact]
    public void game_won_when_all_chars_guessed()
    {
        var game = CreateGame("aaa");

        game.GuessLetter('a');

        Assert.True(game.IsWon());
        Assert.True(game.IsFinished());
    }

    private static Game CreateGame(string word = "beginning")
    {
        return new Game(new List<string> {word}, new TestRandom());
    }

    private static Game CreateFewWordGame(IList<string> words, int index)
    {
        return new Game(words, new TestRandom(index));
    }
}

internal class TestRandom : IRandom
{
    private readonly int _result;

    public TestRandom(int result = 0)
    {
        _result = result;
    }

    public int Next(int max)
    {
        return _result;
    }
}