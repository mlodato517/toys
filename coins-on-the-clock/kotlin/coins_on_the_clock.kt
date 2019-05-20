import kotlin.system.measureTimeMillis

fun main(args: Array<String>) {
  val modulo = 12
  val coins = intArrayOf(1, 5, 10)
  val counts = intArrayOf(4, 4, 4)

  val time = measureTimeMillis {
    repeat(1000) {
      getValidSequences(modulo, coins, counts)
    }
  }
  println(time)
}

fun getValidSequences(numHours: Int, values: IntArray, counts: IntArray): List<String> {
  val sequences = getValidSequences(
    numHours,
    values,
    counts,
    IntArray(numHours),
    BooleanArray(numHours),
    0,
    0
  )

  return sequences.map { sequence -> sequence.map { getCoinName(it) }.joinToString("") }
}

fun getValidSequences(
  numHours: Int,
  values: IntArray,
  counts: IntArray,
  currentSequence: IntArray,
  clockState: BooleanArray,
  currentValue: Int,
  currentSequenceIndex: Int
): MutableList<IntArray> {
  val returnValues = mutableListOf<IntArray>()

  if (currentSequenceIndex == numHours) {
    returnValues.add(currentSequence.copyOf())
  } else {
    var i = counts.size
    while (--i > -1) {
      if (counts[i] == 0) continue

      val value = values[i]

      val nextValue = (currentValue + value) % numHours

      if (clockState[nextValue]) continue

      currentSequence[currentSequenceIndex] = value
      clockState[nextValue] = true
      counts[i] -= 1

      val sequences = getValidSequences(
          numHours,
          values,
          counts,
          currentSequence,
          clockState,
          nextValue,
          currentSequenceIndex + 1
      )
      for (sequence in sequences) {
        returnValues.add(sequence)
      }

      clockState[nextValue] = false
      counts[i] += 1
    }
  }

  return returnValues
}

fun getCoinName(i: Int): Char {
  return when (i) {
    1 -> 'p'
    5 -> 'n'
    10 -> 'd'
    25 -> 'q'
    else -> '?'
  }
}
