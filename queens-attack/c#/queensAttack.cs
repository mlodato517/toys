using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace queensAttack
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1x1 with 0 obstacles
            Debug.Assert(queensAttack(1, 0, 1, 1, new int[0][]) == 0);

            // 2x2 with zero obstacles
            Debug.Assert(queensAttack(2, 0, 1, 1, new int[0][]) == 3);
            Debug.Assert(queensAttack(2, 0, 1, 2, new int[0][]) == 3);
            Debug.Assert(queensAttack(2, 0, 2, 1, new int[0][]) == 3);
            Debug.Assert(queensAttack(2, 0, 2, 2, new int[0][]) == 3);

            // 2x2 with one obstacle
            Debug.Assert(queensAttack(2, 1, 1, 1, new int[][] { new [] { 1, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 1, 1, new int[][] { new [] { 2, 1 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 1, 1, new int[][] { new [] { 2, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 1, 2, new int[][] { new [] { 1, 1 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 1, 2, new int[][] { new [] { 2, 1 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 1, 2, new int[][] { new [] { 2, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 1, new int[][] { new [] { 1, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 1, new int[][] { new [] { 1, 1 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 1, new int[][] { new [] { 2, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 2, new int[][] { new [] { 1, 2 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 2, new int[][] { new [] { 2, 1 }}) == 2);
            Debug.Assert(queensAttack(2, 1, 2, 2, new int[][] { new [] { 1, 1 }}) == 2);

            // 2x2 with 2 obstacles
            Debug.Assert(queensAttack(2, 2, 1, 1,
                new int[][] { new [] { 1, 2 }, new int[] { 2, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 1, 1,
                new int[][] { new [] { 1, 2 }, new int[] { 2, 2 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 1, 1,
                new int[][] { new [] { 2, 2 }, new int[] { 2, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 1, 2,
                new int[][] { new [] { 1, 1 }, new int[] { 2, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 1, 2,
                new int[][] { new [] { 1, 1 }, new int[] { 2, 2 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 1, 2,
                new int[][] { new [] { 2, 2 }, new int[] { 2, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 1,
                new int[][] { new [] { 1, 2 }, new int[] { 1, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 1,
                new int[][] { new [] { 1, 2 }, new int[] { 2, 2 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 1,
                new int[][] { new [] { 2, 2 }, new int[] { 1, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 2,
                new int[][] { new [] { 1, 2 }, new int[] { 2, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 2,
                new int[][] { new [] { 1, 2 }, new int[] { 1, 1 }}
            ) == 1);
            Debug.Assert(queensAttack(2, 2, 2, 2,
                new int[][] { new [] { 1, 1 }, new int[] { 2, 1 }}
            ) == 1);

            // 2x2 with 3 obstacles
            Debug.Assert(queensAttack(2, 3, 1, 1,
                new int[][] { new [] { 1, 2 }, new [] { 2, 1 }, new [] { 2, 2 }}
            ) == 0);
            Debug.Assert(queensAttack(2, 3, 1, 2,
                new int[][] { new [] { 1, 1 }, new [] { 2, 1 }, new [] { 2, 2 }}
            ) == 0);
            Debug.Assert(queensAttack(2, 3, 2, 1,
                new int[][] { new [] { 1, 2 }, new [] { 1, 1 }, new [] { 2, 2 }}
            ) == 0);
            Debug.Assert(queensAttack(2, 3, 2, 2,
                new int[][] { new [] { 1, 2 }, new [] { 2, 1 }, new [] { 1, 1 }}
            ) == 0);

            // 3x3 with 1 obstacle
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 1, 2 }}) == 4);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 1, 3 }}) == 5);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 2, 1 }}) == 4);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 2, 2 }}) == 4);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 2, 3 }}) == 6);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 3, 1 }}) == 5);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 3, 2 }}) == 6);
            Debug.Assert(queensAttack(3,1,1,1, new int[][] { new [] { 3, 3 }}) == 5);

            // 4x4 0 obstacles
            Debug.Assert(queensAttack(4, 0, 4, 4, new int[0][]) == 9);

            // 4x4 1 obstacle
            Debug.Assert(queensAttack(4, 1, 4, 4, new int[][] {new [] { 4, 3 }}) == 6);
            Debug.Assert(queensAttack(4, 1, 4, 4, new int[][] {new [] { 4, 2 }}) == 7);

            // 4x4 2 obstacles
            Debug.Assert(queensAttack(4, 2, 4, 4, new int[][] {new [] { 4, 2 }, new int[] { 4, 2 }}) == 7);
        }

        // queensAttack
        // Gets the number of spaces a queen can attack given
        // the board size, queen coordinates, and obstacle coordinates.
        // n         - number of rows/columns on the board
        // k         - number of obstacles
        // r_q       - queen row index
        // c_q       - queen column index
        // obstacles - array of k 2-element arrays of obstacle row/column indecis
        static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) {

            int[,] gaps = getInitialGaps(n, r_q, c_q);

            HashSet<int> processedObstacleSquares = new HashSet<int>();
            foreach (int[] r_c in obstacles) {
                int r_o = r_c[0];
                int c_o = r_c[1];

                // Find out which square this is we don't bother
                // processing it twice.
                int square = n * (r_o - 1) + c_o;
                if (processedObstacleSquares.Contains(square)) continue;
                processedObstacleSquares.Add(square);

                // *Diff is the difference in position between the
                // queen and the obstacle.
                // Note that a difference of 1 means they are next
                // to each other which will result in a gap of 0.
                int vDiff = r_q - r_o;
                int hDiff = c_q - c_o;

                // If they're on the same row, see if this obstacle is closer
                // than the closest left or right.
                if (vDiff == 0) {
                    if (hDiff > 0 && hDiff <= gaps[1,0]) gaps[1,0] = hDiff - 1;
                    else if (hDiff < 0 && -hDiff <= gaps[1,2]) gaps[1,2] = -hDiff - 1;
                }

                // Same for vertically.
                else if (hDiff == 0) {
                    if (vDiff > 0 && vDiff <= gaps[2,1]) gaps[2,1] = vDiff - 1;
                    else if (vDiff < 0 && -vDiff <= gaps[0,1]) gaps[0,1] = -vDiff - 1;
                }

                // This is on the upRight/downLeft diagonal.
                else if (vDiff == hDiff) {
                    if (vDiff > 0 && vDiff <= gaps[2,0]) gaps[2,0] = vDiff - 1;
                    else if (vDiff < 0 && -vDiff <= gaps[0,2]) gaps[0,2] = -vDiff - 1;
                }

                // This is the other diagonal.
                else if (vDiff == -1 * hDiff) {
                    if (vDiff > 0 && vDiff <= gaps[2,2]) gaps[2,2] = vDiff - 1;
                    else if (vDiff < 0 && -vDiff <= gaps[0,0]) gaps[0,0] = -vDiff - 1;
                }
            }

            int returnValue = 0;
            foreach (int gap in gaps) {
                returnValue += gap;
            }
            return returnValue;
        }

        // getInitialGaps
        // Gets 3x3 matrix of gaps queen has
        // in each of 8 directions based on board size
        // and queen position
        // n   - board size
        // r_q - queen's row index
        // c_q - queen's column index
        static int[,] getInitialGaps(int n, int r_q, int c_q) {

            // Imagine the queen at the center of this 3x3 matrix.
            // In each non-center cell, we'll keep track of how many
            // spaces there are between the queen and the closest obstacle/board
            // edge.
            int[,] gaps = new int[3,3];

            gaps[1,0] = c_q - 1;                        // gap to the left
            gaps[1,2] = -(c_q - n);                     // gap to the right
            gaps[2,1] = r_q - 1;                        // gap below
            gaps[0,1] = -(r_q - n);                     // gap above
            gaps[2,0] = diagSpace(n, r_q, c_q, -1, -1); // gap downLeft
            gaps[0,2] = diagSpace(n, r_q, c_q, 1, 1);   // gap upRight
            gaps[2,2] = diagSpace(n, r_q, c_q, -1, 1);  // gap downRight
            gaps[0,0] = diagSpace(n, r_q, c_q, 1, -1);  // gap upLeft

            return gaps;
        }

        // diagSpace
        // Finds the positive number of spaces from a start space,
        // diagonally in some direction, to the edge of the board.
        // n    - board size
        // r    - row index
        // c    - column index
        // rDir - row direction for diagonal movement. 1 means up, -1 means down
        // cDir - column direction for diag movement. 1 means right, -1 means left
        static int diagSpace(int n, int r, int c, int rDir, int cDir) {

            // If we're moving up, then we have n - r spaces.
            // If we're moving down, then we have r - 1 spaces.
            // Ditto for left right.
            int rSpace = rDir > 0 ? n - r : r - 1;
            int cSpace = cDir > 0 ? n - c : c - 1;

            // Take the smaller of the two.
            return rSpace >= cSpace ? cSpace : rSpace;
        }
    }
}
