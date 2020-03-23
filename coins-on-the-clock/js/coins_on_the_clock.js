const performance = require("perf_hooks").performance;

function getValidSequences(numHours, values, counts) {
  const sequences = [];
  _getValidSequences(
    numHours,
    values,
    counts,
    Buffer.alloc(numHours),
    Buffer.alloc(numHours),
    sequences,
    0,
    0
  );
  return sequences
}

function _getValidSequences(
  numHours,
  values,
  counts,
  currentSequence,
  clockState,
  returnValues,
  currentValue,
  currentSequenceIndex
) {
  if (currentSequenceIndex === numHours) {
    returnValues.push(currentSequence.map(v => getCoinName(v)).join(""));
  }
  else {
    for (let i = 0; i < counts.length; i++) {
      if (counts[i] === 0) continue;

      const nextValue = (currentValue + values[i]) % numHours;

      if (clockState[nextValue]) continue;

      clockState[nextValue] = 1;
      counts[i] -= 1;
      currentSequence[currentSequenceIndex] = values[i];

      _getValidSequences(
        numHours,
        values,
        counts,
        currentSequence,
        clockState,
        returnValues,
        nextValue,
        currentSequenceIndex + 1
      );

      clockState[nextValue] = 0;
      counts[i] += 1;
    }
  }
}

function getCoinName(coin) {
  if (coin == 1) return "p";
  else if (coin == 5) return "n";
  else if (coin == 10) return "d";
  else if (coin == 25) return "q";
  else return "?";
}

const modulo = 12;
const coins = [1, 5, 10];
const counts = [4, 4, 4];

const start = performance.now();
for (let i = 1000; i--; ) {
  getValidSequences(modulo, coins, counts);
}
const stop = performance.now();
console.log(stop - start);
