#include <string>
#include <iostream>
#include <vector>
#include <time.h>
using namespace std;

// Returns char representation of coin value
// value - value of coin to get char of
char getCoinName(int value) {
  switch (value) {
    case 1: return 'p';
    case 5: return 'n';
    case 10: return 'd';
    case 25: return 'q';
    default: return '?';
  }
}

// Gets int arrays corresponding to valid coin sequences
// TODO add other params
vector<int*> getValidSequences(
  int numHours,
  int *values,
  int *counts,
  int numValues,
  int *currentSequence,
  bool *clockState,
  int currentValue,
  int currentSequenceIndex
) {
  vector<int*> returnValues;

  if (currentSequenceIndex == numHours) {

    int *copiedSequence = new int[numHours];
    for (int i = 0; i < numHours; i += 1) {
      *(copiedSequence + i) = *(currentSequence + i);
    }
    returnValues.push_back(copiedSequence);
  }
  else {
    for (int i = 0; i < numValues; i += 1) {

      if (*(counts + i) == 0) continue;

      int value = *(values + i);
      int nextValue = (currentValue + value) % numHours;

      if (*(clockState + nextValue)) continue;

      *(currentSequence + currentSequenceIndex) = value;
      currentSequenceIndex += 1;
      *(clockState + nextValue) = true;
      *(counts + i) -= 1;

      vector<int*> recursedValues = getValidSequences(
        numHours,
        values,
        counts,
        numValues,
        currentSequence,
        clockState,
        nextValue,
        currentSequenceIndex
      );
      returnValues.insert(returnValues.end(), recursedValues.begin(), recursedValues.end());

      currentSequenceIndex -= 1;
      *(currentSequence + currentSequenceIndex) = 0;
      *(clockState + nextValue) = false;
      *(counts + i) += 1;
    }

    return returnValues;
  }
}

// Gets vector of strings of valid coin sequences
// TODO add other params
vector<string> getValidSequences(
  int numHours,
  int *values,
  int *counts,
  int numValues
) {

  // Would a packed bit vector peform better?
  bool *clockState = new bool[numHours];
  for (int i = 0; i < numHours; i += 1) {
    *(clockState + i) = false;
  }

  int* currentSequence  = new int[numHours];
  for (int i = 0; i < numHours; i += 1) {
    *(currentSequence + i) = 0;
  }

  vector<int*> sequences = getValidSequences(
    numHours,
    values,
    counts,
    numValues,
    currentSequence,
    clockState,
    0,
    0
  );

  delete[] clockState;

  // Convert to char vector
  vector<string> results (sequences.size());
  for(int i = 0; i < sequences.size(); i += 1) {
    int *sequence = sequences[i];
    char *result = new char[numHours + 1];
    for (int j = 0; j < numHours; j += 1) {
      *(result + j) = getCoinName(*(sequence + j));
    }
    *(result + numHours) = '\0';

    string stringResult = result;
    results[i] = stringResult;
    delete[] result;
  }

  return results;
}

int main() {
  int numHours= 12;
  int coins[] = {1, 5, 10};
  int counts[] = {4, 4, 4};
  int numValues = 3;

  vector<string> values;

  // Get the number of ticks it takes for 1000 calculations.
  clock_t t;
  t = clock();
  for (int i = 0; i < 1000; i += 1) {
    values = getValidSequences(numHours, &coins[0], &counts[0], numValues);
  }
  t = clock() - t;

  // Multiple by 1,000,000 to get ticks for 1e9 calculations.
  // Divide by ticks/sec to get ns/calculation.
  cout << "Time: " << ((double)t) * 1000 * 1000 / CLOCKS_PER_SEC << "ns" << endl;

  for(int i = 0; i < values.size(); i += 1) {
    cout << values[i] << endl; // Got ~200,000 ns
  }

  getchar();
}