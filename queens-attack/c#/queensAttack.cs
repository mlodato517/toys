using System;
using System.Diagnostics;

namespace queensAttack
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(queensAttack(4, 0, 4, 4, new int[0][]) == 9);
            Debug.Assert(queensAttack(4, 0, 4, 4, new int[1][] {new [] { 4, 3 }}) == 6);
            Debug.Assert(queensAttack(4, 0, 4, 4, new int[1][] {new [] { 4, 2 }}) == 7);
        }

        // Complete the queensAttack function below.
        static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles) {

            // leftSpace is how many spaces there are to the left of the quen to the board.
            // rightSpace is to the right (and is negative);
            int leftSpace = c_q - 1;
            int rightSpace = c_q - n;

            // etc.
            int downSpace = r_q - 1;
            int upSpace = c_q - n;

            int downLeftSpace = remainingDiagSpace(n, r_q, c_q, -1, -1);
            int upRightSpace = remainingDiagSpace(n, r_q, c_q, 1, 1);

            int downRightSpace = remainingDiagSpace(n, r_q, c_q, -1, 1);
            int upLeftSpace = remainingDiagSpace(n, r_q, c_q, 1, -1);

            foreach (int[] r_c in obstacles) {
                int r_o = r_c[0];
                int c_o = r_c[1];

                int vDiff = r_q - r_o;
                int hDiff = c_q - c_o;

                // If they're on the same row, see if this obstacle is closer
                // than the closest left or right.
                if (vDiff == 0) {
                    if (hDiff > 0 && hDiff < leftSpace) leftSpace = hDiff - 1;
                    else if (hDiff < 0 && hDiff > rightSpace) rightSpace = hDiff + 1;
                }

                // Same for vertically.
                else if (hDiff == 0) {
                    if (vDiff > 0 && vDiff < downSpace) downSpace = vDiff - 1;
                    else if (vDiff < 0 && vDiff > upSpace) upSpace = vDiff + 1;
                }

                // This is on the upRight/downLeft diagonal.
                else if (vDiff == hDiff) {
                    if (vDiff > 0 && vDiff < downLeftSpace) downLeftSpace = vDiff - 1;
                    else if (vDiff < 0 && vDiff > upRightSpace) upRightSpace = vDiff + 1;
                }

                // This is the other diagonal.
                else if (vDiff == -1 * hDiff) {
                    if (vDiff > 0 && vDiff < downRightSpace) downRightSpace = vDiff - 1;
                    else if (vDiff < 0 && vDiff > upLeftSpace) upLeftSpace = vDiff + 1;
                }
            }

            return
                leftSpace
                + -1 * rightSpace
                + downSpace
                + -1 * upSpace
                + downLeftSpace
                + -1 * upRightSpace
                + downRightSpace
                + -1 * upLeftSpace;
        }

        static int remainingDiagSpace(int n, int r, int c, int rDir, int cDir) {
            int rSpace = rDir > 0 ? n - r : r - 1;
            int cSpace = cDir > 0 ? n - c : c - 1;

            return rSpace >= cSpace ? cSpace : rSpace;
        }
    }
}
