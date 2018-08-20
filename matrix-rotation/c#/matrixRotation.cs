using System;

class Solution {

    // Complete the matrixRotation function below.
    static int[][] matrixRotation(int[][] matrix, int r) {

        // Create new matrix because mutating args makes me sad.
        int[][] newMatrix = new int[matrix.Length][];
        for (int i = 0; i < matrix.Length; i += 1) {
            newMatrix[i] = new int[matrix[i].Length];
        }

        // Get the top left and bottom right corners of the outer band.
        int rStart = 0;
        int cStart = 0;
        int rEnd = matrix.Length - 1;
        int cEnd = matrix[0].Length - 1;

        // Start at the outer band and move in
        while (rStart <= rEnd && cStart <= cEnd) {

            // Find out how many numbers are in this band
            int count = countNumbersInBand(rStart, cStart, rEnd, cEnd);

            // Reduce rotations so no element passes itself as it rotates.
            int rModulo = r % count;

            // Get the new position for the top left corner
            int x = cStart;
            int y = rStart;
            int[] destination = rModulo > 0 ?
                getDestination(
                    x,
                    y,
                    rStart,
                    cStart,
                    rEnd,
                    cEnd,
                    rModulo,
                    false
                )
                : new [] {x, y};
            int destX = destination[0];
            int destY = destination[1];

            // Now walk pointers clockwise until
            // x == cStart and y = rStart again
            do {
                newMatrix[destY][destX] = matrix[y][x];

                // Move the start once clockwise
                int[] newStart = getDestination(
                    x,
                    y,
                    rStart,
                    cStart,
                    rEnd,
                    cEnd,
                    1,
                    true
                );
                x = newStart[0];
                y = newStart[1];

                // Move the destination once clockwise
                int[] newDest = getDestination(
                    destX,
                    destY,
                    rStart,
                    cStart,
                    rEnd,
                    cEnd,
                    1,
                    true
                );
                destX = newDest[0];
                destY = newDest[1];
            // Repeat until we get back to the beginning
            } while (!(x == cStart && y == rStart));

            // Move the corners in and repeat with the next band.
            rStart += 1;
            cStart += 1;
            rEnd -= 1;
            cEnd -= 1;
        }

        return newMatrix;
    }

    // Given a starting position and the corners of a band,
    // determines destination after taking steps in a direction.
    static int[] getDestination(
      int x,
      int y,
      int rStart,
      int cStart,
      int rEnd,
      int cEnd,
      int steps,
      bool clockwise
    ) {

        // These will help later.
        int[] ends = new [] {cEnd, rEnd};
        int[] starts = new [] {cStart, rStart};
        int[] pos = new [] {x, y};
        while (steps > 0) {
            int[] direction = getDirection(
                pos[0],
                pos[1],
                rStart,
                cStart,
                rEnd,
                cEnd,
                clockwise
            );

            // Find out if we're moving in x or y.
            int index = direction[0] != 0 ? 0 : 1;

            // Find out how much longer x or y has until an end or start.
            int remainingDistance = direction[index] > 0
                ? ends[index] - pos[index]
                : pos[index] - starts[index];

            // If we have too many steps, move to the start/end,
            // decrement steps, and repeat
            if (steps > remainingDistance) {
                steps -= remainingDistance;
                pos[index] = direction[index] > 0 ? ends[index] : starts[index];
            }
            // Otherwise, move the position the number of steps and
            // return the new position.
            else {
                pos[index] = pos[index] + (direction[index] * steps);
                return pos;
            }
        }

        // If we had zero steps, we didn't go anywhere.
        return pos;
    }

    // Gets a pair of ints whose first value is the direction of x
    // and whose second value is the direction of y.
    // Direction is positive if moving right or down.
    // Direction is negative if moving left or up.
    // Direction is 0 if not moving.
    // IS THERE A BETTER WAY TO DO THIS!?
    static int[] getDirection(
        int x,
        int y,
        int rStart,
        int cStart,
        int rEnd,
        int cEnd,
        bool clockwise
    ) {
        int xDir;
        if (clockwise) {
            if (y == rStart && x < cEnd) xDir = 1;
            else if (y == rEnd && x > cStart) xDir = -1;
            else xDir = 0;
        }
        else {
            if (y == rStart && x > cStart) xDir = -1;
            else if (y == rEnd && x < cEnd) xDir = 1;
            else xDir = 0;
        }

        int yDir;
        if (clockwise) {
            if (x == cEnd && y < rEnd) yDir = 1;
            else if (x == cStart && y > rStart) yDir = -1;
            else yDir = 0;
        }
        else {
            if (x == cEnd && y > rStart) yDir = -1;
            else if (x == cStart && y < rEnd) yDir = 1;
            else yDir = 0;
        }
        return new [] {xDir, yDir};
    }

    // Counts the number of numbers in a band of a matrix
    static int countNumbersInBand(int rStart, int cStart, int rEnd, int cEnd) {

      // If we're looking at a band that's 4 columns wide,
      // then cEnd - cStart will be 3, so we want 2 * (3 + 1).
      int colCount = 2 * (cEnd - cStart + 1);

      // If we're looking at a band that's 4 rows high,
      // then rEnd - rStart will be 3. But we've already counted
      // some elements when we counted the columns, so we count
      // one less on either side so we want 2 * (3 - 1)
      int rowCount = 2 * (rEnd - rStart - 1);
      return colCount + rowCount;
    }

    // Prints out a 2 dimensional matrix as space separated elements.
    static void printMatrix(int[][] matrix) {
        for (int i = 0; i < matrix.Length; i += 1) {
            Console.WriteLine(string.Join(" ", matrix[i]));
        }
    }

    static void Main(string[] args) {

        int[][] matrix = new int[4][];
        matrix[0] = new [] { 1, 2, 3, 4, 5 };
        matrix[1] = new [] { 6, 7, 8, 9, 10 };
        matrix[2] = new [] { 11, 12, 13, 14, 15 };
        matrix[3] = new [] { 16, 17, 18, 19, 20 };
        printMatrix(matrix);

        for (int r = 0; r < 15; r += 1) {
            Console.WriteLine("\n--------\nr = " + r + "\n---------\n");
            printMatrix(matrixRotation(matrix, r));
        }
    }
}
