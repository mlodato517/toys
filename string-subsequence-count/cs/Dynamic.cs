class Dynamic : ISequenceCounter
{
  //   t h i s   i s   s r c   s t r i n g s
  // i F F F 6 6 6 0 0 0 0 0 0|0 0 0 0 0 0 0 << this row last.
  // s A A A A 6 6 6 3 3 1 1 1 1 0 0 0 0 0|0 << this row second.
  // s 5 5 5 5 4 4 4 3 3 2 2 2 2 1 1 1 1 1 1 << this row first.
  // Long story short, if there's a match at you, you become the sum
  // of the one to the right and the one to the right and down.
  // Start at the s_i one to the left of the previous row's first match (from the right).
  public string CountStringSubsequences(string source, string target)
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
