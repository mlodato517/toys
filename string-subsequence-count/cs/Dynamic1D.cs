class Dynamic1D : ISequenceCounter
{
  public string CountStringSubsequences(string source, string target)
  {
    if (target.Length == 0 || source.Length == 0) return "0000";

    ulong[] counts = new ulong[source.Length];
    System.Array.Fill(counts, (ulong)1);

    int previousMatchIndex = source.Length - 1;
    for (int t_i = target.Length - 1; t_i >= 0; t_i -= 1)
    {
      bool foundMatch = false;
      ulong previouslyRecordedValue = 0;
      for (int s_i = previousMatchIndex; s_i >= 0; s_i -= 1)
      {
        if (source[s_i] != target[t_i])
        {
          if (!foundMatch) previousMatchIndex -= 1;
          counts[s_i] = previouslyRecordedValue;
        }
        else
        {
          foundMatch = true;

          counts[s_i] += previouslyRecordedValue;
          previouslyRecordedValue = counts[s_i];
        }
      }
    }

    return (counts[0] % 10000).ToString("D4");
  }
}
