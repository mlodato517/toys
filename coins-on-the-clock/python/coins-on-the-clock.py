from collections import deque
import time

'''
_GetValidSequences
Gets list of list of ints
Each list of ints represents a valid coin sequence on the clock
numHours        - Int. Number of hours on the clock
coins           - Array of ints. Must be matched with counts. Gives value of each coin
counts          - Array of ints. Must be matched with coins. Gives count of each coin
clockState      - Array of bools. Lists which clock hours have coins
currentSequence - List of ints. Current sequence of coins placed
currentValue    - Int. Current value of clock we're on
'''
def _GetValidSequences(
  numHours,
  coins,
  counts,
  clockState,
  currentSequence,
  currentValue
):
  returnValues = []

  # If we have numHours coins in our sequence, we've
  # found a solution. Add it to returnValues.
  if (len(currentSequence) == numHours):
    returnValues.append(currentSequence[:])
  else:
    for i in range(len(coins)):

      # If we ran out of this coin, continue
      if counts[i] == 0:
        continue

      # Determine where the next coin would go
      coinValue = coins[i]
      nextValue = (currentValue + coinValue) % numHours

      # If there's already a coin there, continue
      if clockState[nextValue]:
        continue

      # Place the coin on the clock
      currentSequence.append(coinValue)
      clockState[nextValue] = True
      counts[i] -= 1

      # Recurse
      returnValues.extend(_GetValidSequences(
        numHours,
        coins,
        counts,
        clockState,
        currentSequence,
        nextValue
      ))

      # Remove the coin from the clock to try the next coin
      currentSequence.pop()
      clockState[nextValue] = False
      counts[i] += 1

  return returnValues

'''
GetCoinName
Returns character that is the name of the coin based on the given value
value - value of coin to get character of
'''
def GetCoinName(value):
  if value == 1: return 'p'
  if value == 5: return 'n'
  if value == 10: return 'd'
  return '?'

'''
GetValidSequences
Gets list of strings representing valid clock coin sequences
numHours        - Int. Number of hours on the clock
coins           - Array of ints. Must be matched with counts. Gives value of each coin
counts          - Array of ints. Must be matched with coins. Gives count of each coin
'''
def GetValidSequences(numHours, coins, counts):
  clockState = [False] * numHours
  currentSequence = []

  sequences = _GetValidSequences(
    numHours,
    coins,
    counts,
    clockState,
    currentSequence,
    0
  )

  # Convert list of list of ints to list of strings
  # The list generator takes the list of ints and generates
  # a list of coin names. ''.join() joins those into a single string
  for i in range(len(sequences)):
    sequences[i] = ''.join([GetCoinName(coinValue) for coinValue in sequences[i]])

  return sequences


def main ():
  numHours = 12
  coins = [1, 5, 10]
  counts = [4, 4, 4]
  print(f'Number of hours = {numHours}\nCoins = {coins}\nCounts = {counts}')

  start = time.time()
  sequences = GetValidSequences(numHours, coins, counts)
  end = 1000 * 1000 * 1000 * (time.time() - start)
  print(f'{end} ns') # ~5,000,000 ns Python 3 64 bit
                     # ~6,500,000 ns Python 3 32 bit
  print(sequences)

main()