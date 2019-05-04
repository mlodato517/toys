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

	sequences := _getValidSequences(
		numHours,
		coinValues,
		coinCounts,
		currentSequence,
		clockState,
		0,
		0,
	)

	returnValues := make([]string, len(sequences))

	for i, sequence := range sequences {
		toBeString := make([]byte, len(sequence))

		for j, b := range sequence {
			toBeString[j] = getCoinName(b)
		}
		returnValues[i] = string(toBeString)
	}

	return returnValues
}

func _getValidSequences(
	numHours int,
	coinValues []int,
	coinCounts []int,
	currentSequence []int,
	clockState []bool,
	currentValue int,
	currentSequenceIndex int,
) [][]int {
	var returnValues [][]int
	if currentSequenceIndex == numHours {
		destination := make([]int, numHours)
		copy(destination, currentSequence)
		returnValues = append(returnValues, destination)
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

			sequences := _getValidSequences(
				numHours,
				coinValues,
				coinCounts,
				currentSequence,
				clockState,
				nextValue,
				currentSequenceIndex+1,
			)

			for _, sequence := range sequences {
				returnValues = append(returnValues, sequence)
			}

			clockState[nextValue] = false
			coinCounts[i]++
		}
	}

	return returnValues
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
