using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Debug.Assert(
            CountMorseDeletions(
                "&%$%&&&",
                new [] {"&%&"}
            ) == 2
        );
        Debug.Assert(
            CountMorseDeletions(
                "&&&&$&$&%&&$&%&&$%%%$$$&%%$%%%$&%&$&%&&$%&&",
                new [] {"&&&&$&$&%&&$&%%&"}
            ) == 1311
        );

        Debug.Assert(
            CountMorseDeletions(
                "&%$%&&&$%&%&$%&&",
                new [] { "&&&$%", "%%&&$%&" }
            ) == 5
        );
        Debug.Assert(
            CountMorseDeletions(
                "%$&&&&$&$$$&&&$%$&%$&%&$$$&%%$&%$&%&$&&&$$$&&&$&%$%%&$&%",
                new [] { "%&%%$%%%$%&&$&%", "&%&&$&$&&$&%" }
            ) == 11474
        );

        Debug.Assert(
            CountMorseDeletions(
                "$%&$%&",
                new [] { "$", "%", "&" }
            ) == 5
        );

        Stopwatch watch = new Stopwatch();
        watch.Start();
        CountMorseDeletions(
            "%$&&&&$&$$$&&&$%$&%$&%&$$$&%%$&%$&%&$&&&$$$&&&$&%$%%&$&%",
            new [] { "%&%%$%%%$%&&$&%", "&%&&$&$&&$&%" }
        );
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds); // 1061 ms

        // NEED MORE UNIT TESTS
    }

    // CountMorseDeletions
    // Gets number of unique strings resulting from removing each string
    // in delStrs from str.
    // str     - string to delete other strings from
    // delStrs - enumerable of strings to delete from str
    static int CountMorseDeletions(string str, IEnumerable<string> delStrs) {

        // Make enumerable of the single str
        IEnumerable<string> resultEnumerable = new List<string>(new [] { str });

        // Delete delStrs from str. That will result in an
        // enumerable of strings. Delete the next string in delStrs from
        // THAT enumerable. Repeat ad nauseam.
        foreach (string delStr in delStrs) {
            resultEnumerable = GetMorseDeletions(resultEnumerable, delStr);
        }

        // Once we've got our enumerable of all strings resulting from removing
        // all delStrs from str, we then have to weed out
        // duplicates and return the count.
        HashSet<string> uniqueDeletionStrings = new HashSet<string>();
        foreach (string resultString in resultEnumerable) {
            if (uniqueDeletionStrings.Contains(resultString)) continue;
            uniqueDeletionStrings.Add(resultString);
        }
        return uniqueDeletionStrings.Count;
    }

    // GetMorseDeletions
    // Gets the strings resulting from removing delStr
    // from each string in strs.
    // strs   - enumerable of strings to delete delStr from
    // delStr - string to delete from each string in strs
    static List<string> GetMorseDeletions(IEnumerable<string> strs, string delStr) {
        List<string> results = new List<string>();

        // Only find deletions for unique strs and only store unique results.
        HashSet<string> uniqueStrs = new HashSet<string>();
        HashSet<string> uniqueResults = new HashSet<string>();

        // Delete delStr from each unique str and store unique results.
        foreach (string str in strs) {
            if (uniqueStrs.Contains(str)) continue;
            uniqueStrs.Add(str);

            foreach (string result in GetDeletionStrings(str, delStr)) {
                if (uniqueResults.Contains(result)) continue;
                uniqueResults.Add(result);

                results.Add(result);
            }
        }
        return results;
    }

    // LastIndexToDeleteFrom
    // Gets the largest idx for which delStr can be
    // deleted from str.Substring(idx).
    // If delStr can't be deleted from str, returns -1.
    // str    - string to delete delStr from
    // delStr - string to delete from str
    static int LastIndexToDeleteFrom(string str, string delStr) {

        // Walk back from the ends of the strings
        int j = delStr.Length - 1;
        int i = str.Length - 1;
        for (; i >= 0 && j >= 0; i -= 1) {
            if (str[i] == delStr[j]) j -= 1;
        }

        // If j > -1, then we never got to the beginning of delStr,
        // so delStr can't be deleted from str at all.
        // Otherwise, i + 1 is the index in str corresponding
        // to where we "exhausted" all the characters in delStr.
        return j > -1 ? -1 : i + 1;
    }

    // GetDeletionStrings
    // Gets strings resulting from deleting delStr from str
    // str    - string to delete from
    // delStr - string to delete from str
    // result - result of deleting delStr from str
    static IEnumerable<string> GetDeletionStrings(string str, string delStr, string result = "") {

        // If there's nothing left to delete, we've found a result.
        // Append what's left of str first.
        if (delStr.Length == 0)  yield return result + str;

        else {

            // LastIndexToDeleteFrom tells us that if i > lastIndexToDeleteFrom,
            // then delStr can't be deleted from str.
            int lastIndexToDeleteFrom = LastIndexToDeleteFrom(str, delStr);

            // Walk through str. Try to see if str[i] equals delStr[0].
            // If so, "remove" that character, and try to remove the rest
            // of delStr from str.
            char? previouslyDeletedChar = null;
            for (int i = 0; i < str.Length && i <= lastIndexToDeleteFrom; i += 1) {

                // If we found a new match, find deeper deletions.
                if (
                    delStr[0] == str[i]

                    // If str is &&&& and delStr is &
                    // and we've compared delStr[0] to str[0]
                    // then there's no need to compare delStr[0]
                    // to str[1] because the resultant string will be the same.
                    && previouslyDeletedChar != str[i]
                ) {

                    // Record the current char.
                    previouslyDeletedChar = str[i];

                    // Return all deeper deletions.
                    foreach (string deletionString in GetDeletionStrings(
                        str.Substring(i + 1),
                        delStr.Substring(1),
                        result
                    )) {
                        yield return deletionString;
                    }
                }

                // We've found a new char, reset previouslyDeletedChar.
                else previouslyDeletedChar = null;

                // As we "walk" down str, we move chars onto result.
                result += str[i];
            }
        }
    }
}