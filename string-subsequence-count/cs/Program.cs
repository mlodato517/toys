using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Testing recursive");
    TestCountStringSubsequence(new Recursive());

    Console.WriteLine("Testing dynamic");
    TestCountStringSubsequence(new Dynamic());

    Console.WriteLine("Testing dynamic 1D");
    TestCountStringSubsequence(new Dynamic1D());

    TestDataSet(new Dynamic1D(), "C-small-practice.in");
    TestDataSet(new Dynamic1D(), "C-large-practice.in");
  }

  private static void TestDataSet(ISequenceCounter counter, string path)
  {
    string[] lines = File.ReadAllLines(path);
    string[] results = new string[int.Parse(lines[0])];
    string welcome = "welcome to code jam";
    for (int i = 1; i < lines.Length; i += 1) {
      results[i - 1] = "Case #" + i + ": " + counter.CountStringSubsequences(lines[i], welcome);
    }
    File.WriteAllLines(path + ".out", results);
  }

  private static void TestCountStringSubsequence(ISequenceCounter counter)
  {
    // Debug.Assert(counter.CountStringSubsequences("a", "d") == "0000", "a + d");
    // Debug.Assert(counter.CountStringSubsequences("aa", "aa") == "0001", "aa + aa");
    string welcome = "welcome to code jam";
    string[] codejamSources = { "elcomew elcome to code jam", "wweellccoommee to code qps jam", "lwelcome to codejam" };
    string[] expectations = { "0001", "0256", "0000" };

    for (int i = 0; i < codejamSources.Length; i += 1)
    {
      Debug.Assert(
        counter.CountStringSubsequences(codejamSources[i], welcome) == expectations[i],
        codejamSources[i]
      );
    }

    string superTest = "So you've registered. We sent you a welcoming email, to welcome you to code jam. But it's possible that you still don't feel welcomed to code jam. That's why we decided to name a problem \"welcome to code jam.\" After solving this problem, we hope that you'll feel very welcome. Very welcome, that is, to code jam.";

    // Warm up the JIT...
    for (int i = 0; i < 10000; i += 1)
    {
      counter.CountStringSubsequences(superTest, welcome);
    }

    Stopwatch watch = new Stopwatch();
    watch.Start();
    counter.CountStringSubsequences(superTest, welcome);
    watch.Stop();
    double elapsedMs = ((double)watch.ElapsedTicks / Stopwatch.Frequency) * 1000;
    Console.WriteLine("Rough time for super query: " + elapsedMs + " ms");
    Debug.Assert(counter.CountStringSubsequences(superTest, welcome) == "3727", "super");
  }
}
