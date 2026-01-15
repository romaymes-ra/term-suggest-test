namespace TermSearcher.Tests;

using TermSearcherAppCnsle;

public class GetSuggestionsTests
{
    private readonly ITermSearcher _termSearcher;

    public GetSuggestionsTests()
    {
        _termSearcher = new TermSearcher();
    }

    [Fact]
    public void GetSuggestions_SortsByScoreWithSameLength()
    {

        var term = "abc";
        var choices = new List<string> { "azz", "aba", "zzz" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        Assert.Equal("aba", result[0]); // 1
        Assert.Equal("azz", result[1]); // 2
        Assert.Equal("zzz", result[2]); // int.maxValue
    }

    [Fact]
    public void GetSuggestions_SameScore_SortsByLength()
    {
        var term = "test";
        var choices = new List<string> { "testingverylong", "testing", "testinglong" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        // Assert - closer to the length of "test" first
        Assert.Equal("testing", result[0]);       
        Assert.Equal("testinglong", result[1]);     
        Assert.Equal("testingverylong", result[2]); 
    }

    [Fact]
    public void GetSuggestions_SameScoreAndLength_SortsAlphabetically()
    {
        var term = "abc";
        var choices = new List<string> { "ddd", "eee", "fff" };
        // All have same length

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        // Assert - Alphabetical order
        Assert.Equal("ddd", result[0]);
        Assert.Equal("eee", result[1]);
        Assert.Equal("fff", result[2]);
    }

    [Fact]
    public void GetSuggestions_LimitsToNumberOfSuggestions()
    {
        var term = "test";
        var choices = new List<string> { "test", "best", "jest", "dest", "rest", "nest" };

        var result = _termSearcher.GetSuggestions(term, choices, 3).ToList();

        // Assert - Only 3 results
        Assert.Equal(3, result.Count);
        Assert.Equal("test", result[0]); // 0
        Assert.Equal("best", result[1]); // 1 
        Assert.Equal("dest", result[2]); // 1 
    }

    [Fact]
    public void GetSuggestions_FewerCandidatesThanRequested_ReturnsAll()
    {
        var term = "test";
        var choices = new List<string> { "test", "best" };

        var result = _termSearcher.GetSuggestions(term, choices, 10).ToList();

        // Assert - 10 results asked,  2 results available
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetSuggestions_EmptyChoices_ReturnsEmpty()
    {
        var term = "test";
        var choices = new List<string>();

        var result = _termSearcher.GetSuggestions(term, choices, 5).ToList();

        // Assert - No results available
        Assert.Empty(result);
    }

    [Fact]
    public void GetSuggestions_ComplexSorting_ReturnsCorrectOrder()
    {
        // Arrange - Example from the global test 
        var term = "gros";
        var choices = new List<string> { "gros", "gras", "graisse", "agressif", "go", "ros", "gro" };

        
        var result = _termSearcher.GetSuggestions(term, choices, 2).ToList();

        // Assert - Only top 2 results
        Assert.Equal(2, result.Count);
        Assert.Equal("gros", result[0]);  // 0 diff
        Assert.Equal("gras", result[1]);  // 1 diff
    }
}