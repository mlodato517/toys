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
          int[] coins = {1, 5, 10};
          int[] counts = {4, 4, 4};
          Console.WriteLine("Modulo=" + modulo + "\ncoins=" + string.Join(',', coins) + "\n");

          Stopwatch timer = new Stopwatch();
          timer.Start();
          List<string> sequences = GetValidSequences(modulo, coins, counts);
          timer.Stop();

          long nanosecPerTick = 1000L*1000L*1000L / Stopwatch.Frequency;

          Console.WriteLine("Elapsed " + (timer.ElapsedTicks * nanosecPerTick) + "ns"); // Got ~1,600,000ns.

          foreach (string sequence in sequences) {
              System.Console.WriteLine(sequence);
          }
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
            List<int[]> sequences = GetValidSequences(
                numHours,
                values,
                counts,
                currentSequence,
                clockState
            );

            // Convert to strings.
            List<string> results = new List<string>();
            foreach (int[] sequence in sequences) {
                char[] result = new char[sequence.Length];
                for (int i = 0; i < result.Length; i += 1) {
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
        // currentValue         - current clock hour we're on
        // currentSequenceIndex - Current index of sequence we're on
        static List<int[]> GetValidSequences(
            int numHours,
            int[] values,
            int[] counts,
            int[] currentSequence,
            bool[] clockState,
            int currentValue = 0,
            int currentSequenceIndex = 0
        ) {
            // Store all the sequences we find
            List<int[]> returnValues = new List<int[]>();

            // If we've added the last coin, record the sequence
            if (currentSequenceIndex == numHours) {
                int[] copiedSequence = new int[currentSequence.Length];
                System.Array.Copy(currentSequence, copiedSequence, currentSequence.Length);
                returnValues.Add(copiedSequence);
            }
            else {

                // We need to build on our current sequence. Get the next coin
                for (int i = 0; i < values.Length; i += 1) {

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
                    currentSequenceIndex += 1;
                    clockState[nextValue] = true;
                    counts[i] -= 1;

                    returnValues.AddRange(GetValidSequences(
                        numHours,
                        values,
                        counts,
                        currentSequence,
                        clockState,
                        nextValue,
                        currentSequenceIndex
                    ));

                    // Remove the coin
                    currentSequenceIndex -= 1;
                    currentSequence[currentSequenceIndex] = 0;
                    clockState[nextValue] = false;
                    counts[i] += 1;
                }
            }

            return returnValues;
        }

        // GetCoinName
        // Gets char name of coin value
        // coin - value of coin to get name for
        // Throws argument exception if coin name is not found
        public static char GetCoinName(int coin) {
            switch(coin) {
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