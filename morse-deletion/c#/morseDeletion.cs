using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(GetMorseDeletions("&%$%&&&", new [] {"&%&"}));
        Console.WriteLine(GetMorseDeletions("&&&&$&$&%&&$&%&&$%%%$$$&%%$%%%$&%&$&%&&$%&&", new [] {"&&&&$&$&%&&$&%%&"}));
    }

    static int GetMorseDeletions(string originalString, string[] stringsToRemove) {
        int result = 0;
        foreach (string stringToRemove in stringsToRemove) {
            List<char> startStringList = new List<char>(originalString.ToCharArray());
            IEnumerable<string> deletionStrings = GetDeletionStrings(startStringList, stringToRemove);
            foreach (string deletionString in deletionStrings) {
                // Console.WriteLine(deletionString);
                result += 1;
            }
        }
        return result;
    }

    static IEnumerable<string> GetDeletionStrings(
        List<char> stringList,
        string stringToRemove,
        int index = 0,
        string finalString = ""
    ) {
        HashSet<string> returnStrings = new HashSet<string>();

        if (stringToRemove.Length == 0) {
            for (; index < stringList.Count; index += 1) finalString += stringList[index];
            returnStrings.Add(finalString);
        }
        else {
            for (; index < stringList.Count; index += 1) {
                if (stringToRemove[0] == stringList[index]) {
                    IEnumerable<string> deletionStrings = GetDeletionStrings(stringList, stringToRemove.Substring(1), index + 1, finalString);
                    foreach (string deletionString in deletionStrings) {
                        if (returnStrings.Contains(deletionString)) continue;
                        returnStrings.Add(deletionString);
                    }
                }
                finalString += stringList[index];
            }
        }

        foreach (string returnString in returnStrings) yield return returnString;
    }
}