class Dynamic1D : ISequenceCounter
{
  public string CountStringSubsequences(string source, string target)
  {
    if (target.Length == 0 || source.Length == 0) return "0000";

    ulong[] counts = new ulong[source.Length];

    // Future iterations can start one to the left of the first match on the previous loop
    int firstMatchPreviousLoopIndex = -1;

    // Initialize row with incrementing counts on matches against the last char of target
    ulong firstLoopCounter = 0;
    char lastTargetChar = target[target.Length - 1];
    for (int s_i = source.Length - 1; s_i >= 0; s_i -= 1)
    {
      bool match = source[s_i] == lastTargetChar;
      if(firstMatchPreviousLoopIndex == -1 && match) firstMatchPreviousLoopIndex = s_i;
      counts[s_i] = match ? ++firstLoopCounter : firstLoopCounter;
    }

    // Edge case: initialization pass found no matches
    if (firstMatchPreviousLoopIndex == -1) return "0000";

    for (int t_i = target.Length - 2; t_i >= 0; t_i -= 1)
    {
      bool foundMatch = false;
      ulong replacedValue = counts[firstMatchPreviousLoopIndex];
      ulong previouslyRecordedValue = 0;
      for (int s_i = firstMatchPreviousLoopIndex - 1; s_i >= 0; s_i -= 1)
      {
        if (source[s_i] == target[t_i])
        {
          // Record the first match
          if(!foundMatch)
          {
            firstMatchPreviousLoopIndex = s_i;
            foundMatch = true;
          }

          ulong newValue = previouslyRecordedValue + replacedValue;
          replacedValue = counts[s_i];
          counts[s_i] = newValue;
          previouslyRecordedValue = counts[s_i];
        }
        else
        {
          // Else, no match, carry the previous value
          replacedValue = counts[s_i];
          counts[s_i] = previouslyRecordedValue;
        }
      }
    }

    // Return the last four digits of the count
    return (counts[0] % 10000).ToString("D4");
  }
}
