#include <string>
#include <iostream>
#include <vector>
#include <time.h>
using namespace std;

// Returns char representation of coin value
// value - value of coin to get char of
char getCoinName(int value)
{
  switch (value)
  {
  case 1:
    return 'p';
  case 5:
    return 'n';
  case 10:
    return 'd';
  case 25:
    return 'q';
  default:
    return '?';
  }
}

// Gets int arrays corresponding to valid coin sequences
// numHours             - number of hours on the clock
// values               - coin values we have (must match with counts)
// counts               - count of each coin value (must match with values)
// numValues            - number of different coin values we have
// currentSequence      - array of coin values we've placed
// clockState           - for each clock hour, true if a coin is on it
// currentValue         - current clock hour we're on
// currentSequenceIndex - index of currentSequence we're on
vector<int *> getValidSequences(
    int numHours,
    int *values,
    int *counts,
    int numValues,
    int *currentSequence,
    bool *clockState,
    int currentValue,
    int currentSequenceIndex)
{
  vector<int *> returnValues;

  // If we're on the last hour, we have a valid sequence
  if (currentSequenceIndex == numHours)
  {

    // Copy the array and push it onto the vector
    int *copiedSequence = new int[numHours];
    for (int i = 0; i < numHours; i += 1)
    {
      *(copiedSequence + i) = *(currentSequence + i);
    }
    returnValues.push_back(copiedSequence);
  }
  else
  {

    // For each coin value...
    for (int i = 0; i < numValues; i += 1)
    {

      // If we've run out of this coin value, continue
      if (*(counts + i) == 0)
        continue;

      // See where the next coin would go
      int value = *(values + i);
      int nextValue = (currentValue + value) % numHours;

      // If there's already a coin there, continue
      if (*(clockState + nextValue))
        continue;

      // Place the coin
      *(currentSequence + currentSequenceIndex) = value;
      *(clockState + nextValue) = true;
      *(counts + i) -= 1;

      // Recurse
      vector<int *> recursedValues = getValidSequences(
          numHours,
          values,
          counts,
          numValues,
          currentSequence,
          clockState,
          nextValue,
          currentSequenceIndex + 1);

      // Concatenate vectors
      returnValues.insert(returnValues.end(), recursedValues.begin(), recursedValues.end());

      // Remove coin
      *(clockState + nextValue) = false;
      *(counts + i) += 1;
    }
  }

  return returnValues;
}

// Gets vector of strings of valid coin sequences
// numHours  - number of hours on the clock
// values    - coin values we have (must match with counts)
// counts    - count of each coin value (must match with values)
// numValues - number of different coin values we have
vector<string> getValidSequences(
    int numHours,
    int *values,
    int *counts,
    int numValues)
{

  // Initialize clock state to all false (there are no coins after all!)
  bool *clockState = new bool[numHours]();
  int *currentSequence = new int[numHours];

  vector<int *> sequences = getValidSequences(
      numHours,
      values,
      counts,
      numValues,
      currentSequence,
      clockState,
      0,
      0);

  delete[] clockState;
  delete[] currentSequence;

  // Convert to char vector
  vector<string> results(sequences.size());
  for (int i = 0; i < sequences.size(); i += 1)
  {
    int *sequence = sequences[i];
    char *result = new char[numHours + 1];
    for (int j = 0; j < numHours; j += 1)
    {
      *(result + j) = getCoinName(*(sequence + j));
    }
    *(result + numHours) = '\0';

    string stringResult = result;
    results[i] = stringResult;
    delete[] result;
    delete[] sequence;
  }

  return results;
}

int main()
{
  int numHours = 12;
  int coins[] = {1, 5, 10};
  int counts[] = {4, 4, 4};
  int numValues = 3;

  // Get the number of ticks it takes for 1000 calculations.
  clock_t t;
  t = clock();
  for (int i = 0; i < 1000; i += 1)
  {
    getValidSequences(numHours, &coins[0], &counts[0], numValues);
  }
  t = clock() - t;

  // Divide by CLOCKS_PER_SEC to get seconds for 1000 calculations.
  // Multiple by 1000 to get ms for 1000 calculations.
  cout << ((double)t) * 1000 / CLOCKS_PER_SEC << endl;
}
