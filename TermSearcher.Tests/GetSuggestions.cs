namespace TermSearcher.Tests;

using TermSearcherAppCnsle;

[TestFixture]
public class GetSuggestionsTests
{
    private readonly ITermSearcher _termSearcher;

    public GetSuggestionsTests()
    {
        _termSearcher = new TermSearcher();
    }

    [Test]
    public void GetSuggestions_SortsByScoreWithSameLength()
    {

        var term = "abc";
        var choices = new List<string> { "azz", "aba", "zzz" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        Assert.That(result[0], Is.EqualTo("aba")); // 1
        Assert.That(result[1], Is.EqualTo("azz")); // 2
        Assert.That(result[2], Is.EqualTo("zzz")); // int.maxValue
    }

    [Test]
    public void GetSuggestions_SameScore_SortsByLength()
    {
        var term = "test";
        var choices = new List<string> { "testingverylong", "testing", "testinglong" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        Assert.That(result[0], Is.EqualTo("testing"));
        Assert.That(result[1], Is.EqualTo("testinglong"));
        Assert.That(result[2], Is.EqualTo("testingverylong"));
    }

    [Test]
    public void GetSuggestions_SameScoreAndLength_SortsAlphabetically()
    {
        var term = "abc";
        var choices = new List<string> { "ddd", "eee", "fff" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        Assert.That(result[0], Is.EqualTo("ddd"));
        Assert.That(result[1], Is.EqualTo("eee"));
        Assert.That(result[2], Is.EqualTo("fff"));
    }

    [Test]
    public void GetSuggestions_LimitsToNumberOfSuggestions()
    {
        var term = "test";
        var choices = new List<string> { "test", "best", "jest", "dest", "rest", "nest" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo("test")); // 0
        Assert.That(result[1], Is.EqualTo("best")); // 1 
        Assert.That(result[2], Is.EqualTo("dest")); // 1 
    }

    [Test]
    public void GetSuggestions_FewerCandidatesThanRequested_ReturnsAll()
    {
        var term = "test";
        var choices = new List<string> { "test", "best" };

        var result = _termSearcher.GetSuggestions(term, choices, 10).ToList();

        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void GetSuggestions_EmptyChoices_ReturnsEmpty()
    {
        var term = "test";
        var choices = new List<string>();

        var result = _termSearcher.GetSuggestions(term, choices, 5).ToList();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetSuggestions_ComplexSorting_ReturnsCorrectOrder()
    {
        var term = "gros";
        var choices = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };


        var result = _termSearcher.GetSuggestions(term, choices, 2).ToList();

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0], Is.EqualTo("gros"));  // 0 diff
        Assert.That(result[1], Is.EqualTo("gras"));  // 1 diff
    }

    // Case for sensitivity of the search
    // Should'nt be case insensitive
    [TestCase("GROS", "gros", true)]
    [TestCase("Gros", "GROS", true)]
    [TestCase("GrOs", "GRos", true)]
    public void GetSuggestions_CaseInsensitive_FindsMatches(
        string term, string choice, bool shouldMatch)
    {
        var choices = new List<string> { choice };
        var result = _termSearcher.GetSuggestions(term, choices, 1).ToList();

        Assert.That(result[0], Is.EqualTo(choice.ToLower()));
    }

    [Test]
    public void GetSuggestions_NullTermParameter_ThrowsArgumentException(){
        List<string> choices = new List<string> { "gris", "gras" }; 

        var exception = Assert.Throws<ArgumentException>(() => _termSearcher.GetSuggestions(null, choices, 2));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.StartWith("Term cannot be null or empty. Please try another term."));
    }

     [Test]
    public void GetSuggestions_NullTermP_ThrowsArgumentException(){
        List<string> choices = new List<string> { "gris", "gras" }; 

        var exception = Assert.Throws<ArgumentException>(() => _termSearcher.GetSuggestions(null, choices, 2));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.StartWith("Term cannot be null or empty. Please try another term."));
    }

     [Test]
    public void GetSuggestions_NullChoicesParameter_ThrowsArgumentException(){
        string term = "test";
        List<string> choices = null;

        var exception = Assert.Throws<ArgumentNullException>(() => _termSearcher.GetSuggestions(term, choices, 2));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.Contain("Choices cannot be null"));
    }

       [Test]
    public void GetSuggestions_NegativeCount_ThrowsArgumentException(){
        string term = "gros";
        List<string> choices = new List<string> { "gris", "gras" };

        var exception = Assert.Throws<ArgumentException>(() => _termSearcher.GetSuggestions(term, choices, -1));
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Does.Contain("Number of suggestions cannot be negative"));
    }
}