namespace TermSearcherAppCnsle;

using System;
using System.Collections.Generic;
using System.Linq;


public class TermSearcher : ITermSearcher
{

    /*
        * A constant representing no similarity between two terms.
        * It is set to max int value to ensure that any valid difference score will be lower than this value.
    */
    private const int noSimilarity = int.MaxValue;

    /// <summary>
    /// Finds the terms most similar to term from choices
    /// </summary>
    /// <param name="term">The term we search for</param>
    /// <param name="choices">The list of candidates</param>
    /// <param name="numberOfSuggestions">The maximum number of suggestions to return</param>
    /// <returns>List of the most similar terms sorted by difference score, lenght and lexicographical order</returns>
    public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions)
    {
        if (string.IsNullOrWhiteSpace(term))
            throw new ArgumentException("Term cannot be null or empty. Please try another term.", nameof(term));

        if (choices == null)
            throw new ArgumentNullException("Choices cannot be null. Please provide a valid list of choices.", nameof(choices));

        if (numberOfSuggestions < 0)
            throw new ArgumentNullException("Number of suggestions cannot be negative. Please provide a valid number.", nameof(numberOfSuggestions));

        Dictionary<string, int> choicesDifference = new Dictionary<string, int>();

        foreach (string choice in choices)
        {
            int differenceScore = noSimilarity;

            if (choice.Length == term.Length)
            {
                differenceScore = GetDifferenceScore(choice, term);
            }
            else if (choice.Length > term.Length)
            {
                for (int substringIndex = 0; substringIndex <= choice.Length - term.Length; ++substringIndex)
                {
                    int currentDifferenceScore = GetDifferenceScore(choice.Substring(substringIndex, term.Length), term);

                    if (currentDifferenceScore < differenceScore)
                    {
                        differenceScore = currentDifferenceScore;
                    }
                }


            }

            choicesDifference.Add(choice, differenceScore);

        }

        List<string> suggestions = GetTopSuggest(choicesDifference, numberOfSuggestions, term.Length).ToList();
        return suggestions;

    }

    /// <summary>
    /// Calculates the difference between two strings
    /// </summary>
    /// <param name="dest">The first string</param>
    /// <param name="src">The second string</param>
    /// <returns>The number of different characters</returns>
    private int GetDifferenceScore(string dest, string src)
    {
        int differenceScore = 0;

        if (dest.Length != src.Length)
            throw new ArgumentException("Strings parameters must be of the same length");

        for (int index = 0; index < src.Length; ++index)
        {
            if (dest[index] != src[index])
            {
                differenceScore++;
            }
        }

        if (differenceScore == src.Length)
            differenceScore = noSimilarity;

        return differenceScore;
    }

    /// <summary>
    /// Sorts and selects the N best terms based on their score
    /// </summary>
    /// <param name="terms">Dictionary of terms|difference scores</param>
    /// <param name="numberOfSuggestions">Number of suggestions to return</param>
    /// <param name="termLength">Length of the searched term</param>
    /// <returns>The best sorted terms</returns>
    private IEnumerable<string> GetTopSuggest(Dictionary<string, int> terms, int numberOfSuggestions, int termLength)
    {
        // Sort by length
        return terms
            .OrderBy(t => t.Value)
            .ThenBy(t => Math.Abs(t.Key.Length - termLength))
            .ThenBy(t => t.Key)
            .Take(numberOfSuggestions)
            .Select(t => t.Key);
    }
}