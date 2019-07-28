using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace coinsOnTheClock
{
  class Program
  {
    static void Main(string[] args)
    {
      int modulo = 12;
      int[] coins = { 1, 5, 10 };
      int[] counts = { 4, 4, 4 };

      Stopwatch timer = new Stopwatch();
      timer.Start();
      for (int i = 0; i < 1000; i += 1) GetValidSequences(modulo, coins, counts);
      timer.Stop();

      Console.WriteLine(timer.ElapsedMilliseconds);
    }

    // Gets valid coin sequences
    // numHours - number of hours on our clock
    // values   - int array of coin values to add to clock
    // counts   - count of each coin value. Related to values
    public static List<string> GetValidSequences(int numHours, int[] values, int[] counts)
    {
      bool[] clockState = new bool[numHours];
      int[] currentSequence = new int[numHours];

      // Get all the sequences
      List<int[]> sequences = new List<int[]>();
      GetValidSequences(
          numHours,
          values,
          counts,
          currentSequence,
          clockState,
          sequences
      );

      // Convert to strings.
      List<string> results = new List<string>();
      foreach (int[] sequence in sequences)
      {
        char[] result = new char[sequence.Length];
        for (int i = 0; i < result.Length; i += 1)
        {
          result[i] = GetCoinName(sequence[i]);
        }
        results.Add(new string(result));
      }
      return results;
    }

    // Recursive function to get valid coin sequences
    // numHours             - number of hours on our clock
    // values               - int array of coin values to add to clock
    // counts               - count of each coin value. Related to values
    // currentSequence      - Array of coin values we've placed on the clock
    // clockState           - Array recording which clock hours have coins
    // returnValues         - List of sequence results (int[]s)
    // currentValue         - current clock hour we're on
    // currentSequenceIndex - Current index of sequence we're on
    static void GetValidSequences(
        int numHours,
        int[] values,
        int[] counts,
        int[] currentSequence,
        bool[] clockState,
        List<int[]> returnValues,
        int currentValue = 0,
        int currentSequenceIndex = 0
    )
    {
      // If we've added the last coin, record the sequence
      if (currentSequenceIndex == numHours)
      {
        int[] copiedSequence = new int[currentSequence.Length];
        Array.Copy(currentSequence, copiedSequence, currentSequence.Length);
        returnValues.Add(copiedSequence);
      }
      else
      {

        // We need to build on our current sequence. Get the next coin
        for (int i = 0; i < values.Length; i += 1)
        {

          // If we've used all coins of this type, continue
          if (counts[i] == 0) continue;

          // Get coin value
          int value = values[i];

          // Otherwise, see where this next coin put us
          int nextValue = (currentValue + value) % numHours;

          // If there's already a coin there, move on
          if (clockState[nextValue]) continue;

          // Add the coin to our clock
          currentSequence[currentSequenceIndex] = value;
          clockState[nextValue] = true;
          counts[i] -= 1;

          GetValidSequences(
              numHours,
              values,
              counts,
              currentSequence,
              clockState,
              returnValues,
              nextValue,
              currentSequenceIndex + 1
          );

          // Remove the coin
          clockState[nextValue] = false;
          counts[i] += 1;
        }
      }
    }

    // GetCoinName
    // Gets char name of coin value
    // coin - value of coin to get name for
    // Throws argument exception if coin name is not found
    public static char GetCoinName(int coin)
    {
      switch (coin)
      {
        case 1:
          return 'p';
        case 5:
          return 'n';
        case 10:
          return 'd';
        default:
          throw new ArgumentException("Invalid US Coin value of " + coin);
      }
    }
  }
}
