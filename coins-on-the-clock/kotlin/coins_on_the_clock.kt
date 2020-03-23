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
  val sequences = mutableListOf<String>()
  getValidSequences(
    numHours,
    values,
    counts,
    IntArray(numHours),
    BooleanArray(numHours),
    sequences,
    0,
    0
  )

  return sequences
}

fun getValidSequences(
  numHours: Int,
  values: IntArray,
  counts: IntArray,
  currentSequence: IntArray,
  clockState: BooleanArray,
  returnValues: MutableList<String>,
  currentValue: Int,
  currentSequenceIndex: Int
) {
  if (currentSequenceIndex == numHours) {
    returnValues.add(currentSequence.map { getCoinName(it) }.joinToString(""))
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

      getValidSequences(
        numHours,
        values,
        counts,
        currentSequence,
        clockState,
        returnValues,
        nextValue,
        currentSequenceIndex + 1
      )

      clockState[nextValue] = false
      counts[i] += 1
    }
  }
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
