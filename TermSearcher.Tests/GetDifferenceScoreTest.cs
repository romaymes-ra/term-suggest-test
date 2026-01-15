namespace TermSearcher.Tests;

using TermSearcherAppCnsle;

public class GetDifferenceScoreTest
{

    private readonly TermSearcher _termSearcher;

    public GetDifferenceScoreTest()
    {
        _termSearcher = new TermSearcher();
    }

    [Fact]
    public void GetDifferenceScore_IdenticalStrings_ReturnsZero()
    {
        var result = _termSearcher.GetDifferenceScore("test", "test");


        Assert.Equal(0, result);
    }

    [Fact]
    public void GetDifferenceScore_DifferentStrings_ReturnsCorrectDifference()
    {
        var result = _termSearcher.GetDifferenceScore("test", "tent");

        Assert.Equal(1, result);
    }

    [Fact]
    public void GetDifferenceScore_StringsOfDifferentLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _termSearcher.GetDifferenceScore("test", "testing"));
    }

    [Fact]
    public void GetDifferenceScore_TotallyDifferentStrings_ReturnsNoSimilarity()
    {
        var result = _termSearcher.GetDifferenceScore("yes", "noo");

        Assert.Equal(int.MaxValue, result);
    }
}