import itertools
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
currentIndex    - Int. Current index of currentSequence we're on
coinLength      - Length of coins array
'''


def _GetValidSequences(
    numHours,
    coins,
    counts,
    clockState,
    currentSequence,
    currentValue,
    currentIndex,
    coinLength
):
    returnValues = []

    # If we have numHours coins in our sequence, we've
    # found a solution. Add it to returnValues.
    if (currentIndex == numHours):
        returnValues.append(currentSequence[:])
    else:
        for i in range(coinLength):

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
            currentSequence[currentIndex] = coinValue
            currentIndex += 1
            clockState[nextValue] = True
            counts[i] -= 1

            # Recurse
            returnValues.extend(_GetValidSequences(
                numHours,
                coins,
                counts,
                clockState,
                currentSequence,
                nextValue,
                currentIndex,
                coinLength
            ))

            # Remove the coin from the clock to try the next coin
            currentIndex -= 1
            clockState[nextValue] = False
            counts[i] += 1

    return returnValues


'''
GetCoinName
Returns character that is the name of the coin based on the given value
value - value of coin to get character of
'''


def GetCoinName(value):
    if value == 1:
        return 'p'
    if value == 5:
        return 'n'
    if value == 10:
        return 'd'
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
    currentSequence = [None] * numHours

    sequences = _GetValidSequences(
        numHours,
        coins,
        counts,
        clockState,
        currentSequence,
        0,
        0,
        len(coins)
    )

    # Convert list of list of ints to list of strings
    returnValue = [IntListToString(sequence) for sequence in sequences]

    return returnValue


def IntListToString(intList):
    return ''.join([GetCoinName(i) for i in intList])


def main():
    numHours = 12
    coins = [1, 5, 10]
    counts = [4, 4, 4]

    start = time.time()
    for _ in itertools.repeat(None, 1000):
        GetValidSequences(numHours, coins, counts)
    end = time.time()

    # time.time() gives us difference in seconds.
    # Multiply by 1000 to get ms
    total = 1000 * (end - start)
    print(total)


main()
