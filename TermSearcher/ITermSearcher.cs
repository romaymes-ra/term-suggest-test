namespace TermSearcherAppCnsle;

public interface ITermSearcher
{
    IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions);
}