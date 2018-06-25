using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
      RunTests();

      Stopwatch timer = new Stopwatch();
      timer.Start();
      long rank = GetRanking("ZABCDEFGHIJKLMNOPQSR");
      timer.Stop();

      long nanosecPerTick = 1000L*1000L*1000L / Stopwatch.Frequency;

      Console.WriteLine("Elapsed " + (timer.ElapsedTicks * nanosecPerTick) + "ns"); // Got ~70,000 ns

      Debug.Assert(rank == 2311256907767808002);
      Console.WriteLine("Got " + rank + " for ZABCDEFGHIJKLMNOPQSR. Expected " + 2311256907767808002);
    }

    static void RunTests() {
      // Zero length string
      Debug.Assert(GetRanking("") == 1L);

      // One length string
      Debug.Assert(GetRanking("a") == 1L);

      // Two length strings
      Debug.Assert(GetRanking("aa") == 1L);
      Debug.Assert(GetRanking("ab") == 1L);
      Debug.Assert(GetRanking("ba") == 2L);

      // Three length strings with repitition
      Debug.Assert(GetRanking("aaa") == 1L);
      Debug.Assert(GetRanking("aab") == 1L);
      Debug.Assert(GetRanking("aba") == 2L);
      Debug.Assert(GetRanking("baa") == 3L);

      // Three length strings distinct
      Debug.Assert(GetRanking("abc") == 1L);
      Debug.Assert(GetRanking("acb") == 2L);
      Debug.Assert(GetRanking("bac") == 3L);
      Debug.Assert(GetRanking("bca") == 4L);
      Debug.Assert(GetRanking("cab") == 5L);
      Debug.Assert(GetRanking("cba") == 6L);

      // Four length strings with repitition
      Debug.Assert(GetRanking("aaaa") == 1L);
      Debug.Assert(GetRanking("aaab") == 1L);
      Debug.Assert(GetRanking("aaba") == 2L);
      Debug.Assert(GetRanking("abaa") == 3L);
      Debug.Assert(GetRanking("baaa") == 4L);
      Debug.Assert(GetRanking("aabb") == 1L);
      Debug.Assert(GetRanking("abab") == 2L);
      Debug.Assert(GetRanking("abba") == 3L);
      Debug.Assert(GetRanking("baab") == 4L);
      Debug.Assert(GetRanking("baba") == 5L);
      Debug.Assert(GetRanking("bbaa") == 6L);

      // Four length strings one repition
      // Start with aa's
      Debug.Assert(GetRanking("aabc") == 1L);
      Debug.Assert(GetRanking("aacb") == 2L);
      // ab's
      Debug.Assert(GetRanking("abac") == 3L);
      Debug.Assert(GetRanking("abca") == 4L);
      // ac's
      Debug.Assert(GetRanking("acab") == 5L);
      Debug.Assert(GetRanking("acba") == 6L);
      // ba's
      Debug.Assert(GetRanking("baac") == 7L);
      Debug.Assert(GetRanking("baca") == 8L);
      // bc
      Debug.Assert(GetRanking("bcaa") == 9L);
      // ca's
      Debug.Assert(GetRanking("caab") == 10L);
      Debug.Assert(GetRanking("caba") == 11L);
      // cb
      Debug.Assert(GetRanking("cbaa") == 12L);

      Debug.Assert(GetRanking("QUESTION") == 24572L);
      Debug.Assert(GetRanking("BOOKKEEPER") == 10743L);

      Debug.Assert(GetRanking("nonintuitiveness") == 8222334634L);
    }

    // GetCountsOfCharacters
    // Counts occurence of each unique character in the array of chars
    // chars - array of chars to count characters in
    // Returns dictionary with char keys and int occurrence values
    static Dictionary<char, int> GetCountsOfCharacters(char[] chars) {
      Dictionary<char, int> countOfCharacters = new Dictionary<char, int>();
      foreach (char c in chars) {
        if (!countOfCharacters.ContainsKey(c)) countOfCharacters.Add(c, 1);
        else countOfCharacters[c] += 1;
      }
      return countOfCharacters;
    }

    // Gets the sorted ranking of the passed word
    // amongst all its permuations
    // word - string to get ranking of
    // Returns 1 based ranking of word (e.g. GetRanking("ab") returns 1)
    static long GetRanking(string word) {

      // Sort the characters in word.
      char[] sortedChars = word.ToCharArray();
      Array.Sort(sortedChars);

      Dictionary<char, int> countOfCharacters = GetCountsOfCharacters(sortedChars);
      LinkedList<char> sortedCharList = new LinkedList<char>(sortedChars);

      long rank = 0;
      for (int i = 0; i < word.Length - 1; i += 1) {
        char wordChar = word[i];

        // Find unique characters less than wordChar.
        LinkedListNode<char> lastNode;
        HashSet<char> lesserChars = GetLesserChars(wordChar, sortedCharList, out lastNode);

        // Delete the node from the list.
        if (lastNode != null) sortedCharList.Remove(lastNode);

        // Add number of permutations of the rest of the word
        // (taking into consideration duplicate characters).
        rank += GetPermutationsAheadOfWord(lesserChars, word.Substring(i), countOfCharacters);

        // Decrement the count for that letter.
        countOfCharacters[wordChar] -= 1;
      }

      return rank + 1;
    }

    // GetLesserChars
    // Gets unique list of characters less than the given wordChar
    // wordChar       - character defining boundary of "lesser"
    // sortedCharList - linked list of char nodes sorted in descending
    //                  alphabetical order. This allows us to short circuit
    //                  when we've found the character matching wordChar.
    //                  (not that this method knows it, but it also allows
    //                  for quick deletion of that node too).
    // lastNode       - reference to node in sortedCharList that matches wordChar
    static HashSet<char> GetLesserChars(char wordChar, LinkedList<char> sortedCharList, out LinkedListNode<char> lastNode) {
      HashSet<char> lesserChars = new HashSet<char>();

      // Compare wordChar to each letter in the sorted list
      for (lastNode = sortedCharList.First; lastNode != null; lastNode = lastNode.Next) {
        // The rest of the characters will be >= wordChar.
        if (lastNode.Value == wordChar) break;

        // Every character here is < wordChar. Add it to the hash.
        if (!lesserChars.Contains(lastNode.Value)) lesserChars.Add(lastNode.Value);
      }

      return lesserChars;
    }

    // GetPermutationsAheadOfWord
    // Gets count of words generated by swapping chars in lesserChars
    // with the first character of wordEnding
    // lesserChars       - enumberable of chars less than the target char
    // wordEnding        - ending of word. We want to know how many permutations
    //                     of this there are.
    // countOfCharacters - count of each character. Used for "n choose m" math
    static long GetPermutationsAheadOfWord(IEnumerable<char> lesserChars, string wordEnding, Dictionary<char, int> countOfCharacters) {

      long total = 0;
      foreach (char lesserChar in lesserChars) {

        // Let's pretend we swapped lesserChar to the front of wordEnding.
        countOfCharacters[lesserChar] -= 1;
        long totalWaysWithoutThisChar = Factorial(wordEnding.Length - 1);

        // In "n choose m" math, we divide out duplicate items when
        // order doesn't matter. Do that here.
        long divisor = 1;
        HashSet<char> duplicateCharsHandled = new HashSet<char>();
        for (int j = 0 + 1; j < wordEnding.Length; j++) {

          // Don't process duplicate chars multiple times.
          char remainingChar = wordEnding[j];
          if (duplicateCharsHandled.Contains(remainingChar)) continue;

          duplicateCharsHandled.Add(remainingChar);

          // Need to check for > 0 because we decremented lesserChar
          // when we pretend swapped it to the front of wordEnding.
          int countOfChar = countOfCharacters[remainingChar];
          if (countOfChar > 0) divisor *= Factorial(countOfChar);
        }

        // Undo pretend swap.
        countOfCharacters[lesserChar] += 1;

        // Add result of "n choose m" math to total.
        totalWaysWithoutThisChar /= divisor;
        total += totalWaysWithoutThisChar;
      }

      return total;
    }

    // Factorial
    // Calculates factorial of a number.
    // number - number to factorialize
    // Maybe I should memoize these somehow?
    static long Factorial(long number) {
      if (number == 0L) return 1L;

      long returnValue = number;
      for (long i = number - 1; i > 0; i -= 1) returnValue *= i;

      return returnValue;
    }
}