namespace TermSearcher.Tests;

using TermSearcherAppCnsle;

[TestFixture]
public class GetDifferenceScoreTest
{

    private readonly TermSearcher _termSearcher;

    public GetDifferenceScoreTest()
    {
        _termSearcher = new TermSearcher();
    }

    [Test]
    public void GetDifferenceScore_IdenticalStrings_ReturnsZero()
    {
        var result = _termSearcher.GetDifferenceScore("test", "test");


        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void GetDifferenceScore_DifferentStrings_ReturnsCorrectDifference()
    {
        var result = _termSearcher.GetDifferenceScore("test", "tent");

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void GetDifferenceScore_StringsOfDifferentLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _termSearcher.GetDifferenceScore("test", "testing"));
    }

    [Test]
    public void GetDifferenceScore_TotallyDifferentStrings_ReturnsNoSimilarity()
    {
        var result = _termSearcher.GetDifferenceScore("yes", "noo");

        Assert.That(result, Is.EqualTo(int.MaxValue));
    }
}