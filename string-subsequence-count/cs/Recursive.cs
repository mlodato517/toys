using System;
using System.Collections.Generic;

class Recursive : ISequenceCounter
{
  public string CountStringSubsequences(string source, string target)
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
}
