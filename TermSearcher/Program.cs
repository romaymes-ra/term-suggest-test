using TermSearcherAppCnsle;


/* 
* Exemple of TermSearcher usage
*/

try
{
    ITermSearcher termSearcher = new TermSearcher();
    IEnumerable<string> choices = new List<string>
    {
       "gros",
       "gras",
       "graisse",
        "agressif",
        "go",
        "ros",
        "gro"
    };

    IEnumerable<string> suggestions = termSearcher.GetSuggestions("gras", choices, 3);
    Console.WriteLine("Suggestions:");
    
    foreach (string suggestion in suggestions)
    {
        Console.WriteLine(suggestion);
    }
}
catch(Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}