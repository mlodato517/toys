using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
  delegate string SequenceCounter(string source, string target);

  static void Main(string[] args)
  {
    TestBinarySearch();
    TestCountStringSubsequence(new SequenceCounter(CountStringSubsequence));
    TestCountStringSubsequence(new SequenceCounter(CountStringSubsequenceDynamic));
    TestDataSet(new SequenceCounter(CountStringSubsequenceDynamic), "C-small-practice.in");
    TestDataSet(new SequenceCounter(CountStringSubsequenceDynamic), "C-large-practice.in");
  }

  private static void TestDataSet(SequenceCounter sequenceCounter, string path)
  {
    string[] lines = File.ReadAllLines(path);
    string[] results = new string[int.Parse(lines[0])];
    string welcome = "welcome to code jam";
    for (int i = 1; i < lines.Length; i += 1) {
      results[i - 1] = "Case #" + i + ": " + sequenceCounter(lines[i], welcome);
    }
    File.WriteAllLines(path + ".out", results);
  }

  private static void TestCountStringSubsequence(SequenceCounter sequenceCounter)
  {
    // string[] sources = {"abc", "aabc", "abbc", "abcc"};
    // string[] targets = {"abc", "", "a", "ab", "bc", "ac", "c"};
    // foreach (string source in sources) {
    //   foreach (string target in targets) {
    //     Console.WriteLine(
    //       "target: "
    //       + target
    //       + " + source: "
    //       + source
    //       + " = "
    //       + sequenceCounter(source, target)
    //     );
    //   }
    // }

    string welcome = "welcome to code jam";
    string[] codejamSources = { "elcomew elcome to code jam", "wweellccoommee to code qps jam", "lwelcome to codejam" };
    string[] expectations = { "0001", "0256", "0000" };

    for (int i = 0; i < codejamSources.Length; i += 1)
    {
      Console.WriteLine(sequenceCounter(codejamSources[i], welcome));
      Debug.Assert(
        sequenceCounter(codejamSources[i], welcome) == expectations[i],
        codejamSources[i]
      );
    }

    string superTest = "So you've registered. We sent you a welcoming email, to welcome you to code jam. But it's possible that you still don't feel welcomed to code jam. That's why we decided to name a problem \"welcome to code jam.\" After solving this problem, we hope that you'll feel very welcome. Very welcome, that is, to code jam.";
    Stopwatch watch = new Stopwatch();
    watch.Start();
    sequenceCounter(superTest, welcome);
    watch.Stop();
    double elapsedMs = ((double)watch.ElapsedTicks / Stopwatch.Frequency) * 1000;
    Console.WriteLine("Rough time for super query: " + elapsedMs + " ms");
    Debug.Assert(sequenceCounter(superTest, welcome) == "3727", "super");
  }

  private static void TestBinarySearch()
  {
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3 }), 1) == 0, "1,2,3/1");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3 }), 2) == 1, "1,2,3/2");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3 }), 3) == 2, "1,2,3/3");

    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 4 }), 1) == 0, "1,2,4/1");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 4 }), 2) == 1, "1,2,4/2");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 4 }), 4) == 2, "1,2,4/4");

    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 2, 4, 6 }), 1) == 0, "2,4,6/1");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 2, 4, 6 }), 3) == 1, "2,4,6/3");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 2, 4, 6 }), 5) == 2, "2,4,6/5");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 2, 4, 6 }), 7) == 3, "2,4,6/7");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 2, 3, 12, 13, 21 }), 0) == 0, "2,3,12,13,21/0");

    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3, 4 }), 1) == 0, "1,2,3,4/1");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3, 4 }), 2) == 1, "1,2,3,4/2");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3, 4 }), 3) == 2, "1,2,3,4/3");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 1, 2, 3, 4 }), 4) == 3, "1,2,3,4/4");

    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 0, 2, 3, 5 }), 0) == 0, "0,2,3,5/0");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 0, 2, 3, 5 }), 2) == 1, "0,2,3,5/2");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 0, 2, 3, 5 }), 3) == 2, "0,2,3,5/3");
    Debug.Assert(BinarySearch(new List<ushort>(new ushort[] { 0, 2, 3, 5 }), 5) == 3, "0,2,3,5/5");
  }

  static string CountStringSubsequence(string source, string target)
  {
    if (target.Length == 0) return "0000";

    Dictionary<char, List<ushort>> sourceIndices = GetSourceIndices(source, target);
    Dictionary<uint, ulong> memo = new Dictionary<uint, ulong>();

    ulong count = CountStringSubsequence(sourceIndices, source, target, 0, 0, memo);
    return (count % 10000).ToString("D4");
  }

  static ulong CountStringSubsequence(
    Dictionary<char, List<ushort>> sourceIndices,
    string source,
    string target,
    ushort sourcePosition,
    ushort targetPosition,
    Dictionary<uint, ulong> memo
  )
  {
    if (targetPosition >= target.Length) return 1;

    ulong count = 0;
    List<ushort> positionsOfTargetChar;
    if (sourceIndices.TryGetValue(
      target[targetPosition],
      out positionsOfTargetChar
    ))
    {
      ushort firstNextSourcePosition = BinarySearch(positionsOfTargetChar, sourcePosition);
      for (int i = firstNextSourcePosition; i < positionsOfTargetChar.Count; i += 1)
      {
        ushort nextSourcePosition = positionsOfTargetChar[i];

        ushort nextTargetPosition = (ushort)(targetPosition + 1);
        uint key = (uint)((nextSourcePosition << 16) | nextTargetPosition);

        ulong subCount;
        if (!memo.TryGetValue(key, out subCount))
        {
          subCount = CountStringSubsequence(
            sourceIndices,
            source,
            target,
            nextSourcePosition,
            nextTargetPosition,
            memo
          );
          memo[key] = subCount;
        }
        count += subCount;
      }
    }
    return count;
  }

  private static ushort BinarySearch(IList<ushort> list, ushort v)
  {
    int floor = 0;
    int ceil = list.Count - 1;

    while (floor < ceil)
    {
      int half = (ceil + floor) / 2;
      if (list[half] > v)
      {
        ceil = half - 1;
      }
      else if (list[half] < v)
      {
        floor = half + 1;
      }
      else
      {
        return (ushort)half;
      }
    }
    if (ceil < 0) return 0;
    return list[ceil] < v ? (ushort)(ceil + 1) : (ushort)ceil;
  }

  static Dictionary<char, List<ushort>> GetSourceIndices(string source, string target)
  {
    HashSet<char> targetCharacters = new HashSet<char>(target);
    Dictionary<char, List<ushort>> sourceCharacters = new Dictionary<char, List<ushort>>();
    for (ushort i = 0; i < source.Length; i += 1)
    {
      char c = source[i];
      if (targetCharacters.Contains(c))
      {
        List<ushort> list;
        if (!sourceCharacters.TryGetValue(c, out list))
        {
          sourceCharacters.Add(c, new List<ushort>());
        }

        sourceCharacters[c].Add(i);
      }
    }

    return sourceCharacters;
  }

  //   t h i s   i s   s r c   s t r i n g s
  // i F F F 6 6 6 0 0 0 0 0 0|0 0 0 0 0 0 0 << this row last.
  // s A A A A 6 6 6 3 3 1 1 1 1 0 0 0 0 0|0 << this row second.
  // s 5 5 5 5 4 4 4 3 3 2 2 2 2 1 1 1 1 1 1 << this row first.
  // Long story short, if there's a match at you, you become the sum
  // of the one to the right and the one to the right and down.
  // Start at the s_i one to the left of the previous row's first match (from the right).
  static string CountStringSubsequenceDynamic(string source, string target)
  {
    if (target.Length == 0 || source.Length == 0) return "0000";

    ulong[,] counts = new ulong[target.Length, source.Length];

    // Record location of first match. Future iterations can start to the left of this.
    int firstMatch = -1;

    // Fill bottom row with matches for the last char in target. Increment on match.
    ulong previousValue = 0;
    int lastTargetIndex = target.Length - 1;
    char lastTargetChar = target[lastTargetIndex];
    for (int s_i = source.Length - 1; s_i >= 0; s_i -= 1)
    {
      if (source[s_i] != lastTargetChar)
      {
        counts[lastTargetIndex, s_i] = previousValue;
        continue;
      }

      if (firstMatch == -1) firstMatch = s_i;
      counts[lastTargetIndex, s_i] = ++previousValue;
    }

    // Go through every other letter of target, building on the previous row.
    // Don't need to start further right than first match of previous row.
    for (int t_i = lastTargetIndex - 1; t_i >= 0; t_i -= 1)
    {
      bool foundMatch = false;
      previousValue = 0;
      for (int s_i = firstMatch - 1; s_i >= 0; s_i -= 1)
      {

        // If we don't have a match, we're the same value as the previous.
        if (source[s_i] != target[t_i])
        {
          counts[t_i, s_i] = previousValue;
          continue;
        }

        if (!foundMatch) {
          foundMatch = true;
          firstMatch = s_i;
        }

        // Add the previous to the one diagonally right and down.
        counts[t_i, s_i] = previousValue + counts[t_i + 1, s_i + 1];
        previousValue = counts[t_i, s_i];
      }
    }

    return (counts[0, 0] % 10000).ToString("D4");
  }
}
