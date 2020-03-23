package main

import (
	"fmt"
	"time"
)

func main() {
	modulo := 12
	coinValues := []int{1, 5, 10}
	coinCounts := []int{4, 4, 4}

	start := time.Now()
	for i := 0; i < 1000; i++ {
		getValidSequences(modulo, coinValues, coinCounts)
	}
	elapsed := time.Since(start)
	fmt.Println(elapsed)
}

func getValidSequences(numHours int, coinValues []int, coinCounts []int) []string {
	clockState := make([]bool, numHours)
	currentSequence := make([]int, numHours)

	var sequences []string
	_getValidSequences(
		numHours,
		coinValues,
		coinCounts,
		currentSequence,
		clockState,
		sequences,
		0,
		0,
	)

	return sequences
}

func _getValidSequences(
	numHours int,
	coinValues []int,
	coinCounts []int,
	currentSequence []int,
	clockState []bool,
	returnValues []string,
	currentValue int,
	currentSequenceIndex int,
) {
	if currentSequenceIndex == numHours {
		coinNames := make([]byte, numHours)
		for i, v := range currentSequence {
			coinNames[i] = getCoinName(v)
		}
		returnValues = append(returnValues, string(coinNames))
	} else {
		for i := 0; i < len(coinValues); i++ {
			if coinCounts[i] == 0 {
				continue
			}

			nextValue := (currentValue + coinValues[i]) % numHours

			if clockState[nextValue] {
				continue
			}

			currentSequence[currentSequenceIndex] = coinValues[i]
			clockState[nextValue] = true
			coinCounts[i]--

			_getValidSequences(
				numHours,
				coinValues,
				coinCounts,
				currentSequence,
				clockState,
				returnValues,
				nextValue,
				currentSequenceIndex+1,
			)

			clockState[nextValue] = false
			coinCounts[i]++
		}
	}
}

func getCoinName(coin int) byte {
	if coin == 1 {
		return 'p'
	} else if coin == 5 {
		return 'n'
	} else if coin == 10 {
		return 'd'
	}
	return '?'
}
