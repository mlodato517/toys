class Dynamic1D : ISequenceCounter
{
  public string CountStringSubsequences(string source, string target)
  {
    if (target.Length == 0 || source.Length == 0) return "0000";

    // Create one array the length of source and fill it with 1s
    ulong[] counts = new ulong[source.Length];
    System.Array.Fill(counts, (ulong)1);

    // Keep track of the first index we got a match on the previous loop because
    // future iterations can start one to the left of that index.
    int firstMatchPreviousLoopIndex = source.Length;
    for (int t_i = target.Length - 1; t_i >= 0; t_i -= 1)
    {
      ulong previouslyRecordedValue = 0;

      int s_i = firstMatchPreviousLoopIndex - 1;
      firstMatchPreviousLoopIndex = 0;
      for (; s_i >= 0; s_i -= 1)
      {
        if (source[s_i] != target[t_i])
        {
          counts[s_i] = previouslyRecordedValue;
        }
        else
        {
          // Record a match if this is the first one
          if(firstMatchPreviousLoopIndex == 0) firstMatchPreviousLoopIndex = s_i;

          counts[s_i] += previouslyRecordedValue;
          previouslyRecordedValue = counts[s_i];
        }
      }
    }

    // Return the last four digits of the count
    return (counts[0] % 10000).ToString("D4");
  }
}
