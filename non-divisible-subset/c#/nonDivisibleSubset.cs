using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args) {
        Debug.Assert(nonDivisibleSubset(1, new [] {1}) == 1);
        Debug.Assert(nonDivisibleSubset(1, new [] {1, 2}) == 1);

        Debug.Assert(nonDivisibleSubset(1, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9}) == 1);
        Debug.Assert(nonDivisibleSubset(1, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}) == 1);

        Debug.Assert(nonDivisibleSubset(2, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9}) == 2);
        Debug.Assert(nonDivisibleSubset(2, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}) == 2);

        Debug.Assert(nonDivisibleSubset(2, new [] {2, 4, 6, 8, 10}) == 1);
        Debug.Assert(nonDivisibleSubset(2, new [] {2, 4, 6, 8}) == 1);

        Debug.Assert(nonDivisibleSubset(7, new [] {1, 2, 3}) == 3);
        Debug.Assert(nonDivisibleSubset(7, new [] {1, 2}) == 2);

        Debug.Assert(nonDivisibleSubset(7, new [] {3, 4}) == 1);
        Debug.Assert(nonDivisibleSubset(7, new [] {4, 5}) == 2);

        Debug.Assert(nonDivisibleSubset(6, new [] {3, 6, 12, 18}) == 2);
        Debug.Assert(nonDivisibleSubset(6, new [] {3, 6, 12}) == 2);

        Debug.Assert(nonDivisibleSubset(5, new [] {1, 2, 3, 4, 5}) == 3);
        Debug.Assert(nonDivisibleSubset(5, new [] {2, 3, 4, 5}) == 3);
    }

    // nonDivisibleSubset
    // Given a non-factor, k, and a set of unique ints, S,
    // finds the magnitude of the largest subset of S, S',
    // Such that for each x, y in S', k does not divide (x + y).
    // k - non-factor. Sums of pairs of elements of S' must not be divisible by this
    // S - array of unique ints.
    static int nonDivisibleSubset(int k, int[] S) {

        // Okay here's the idea. (a + b) % c is congruent
        // to (a % c) + (b % c). Therefore, (assuming positive remainders)
        // c | (a + b) <=> (a % c) + (b % c) == 0 or c.
        // First, get the count of each possible modulus of k.
        int[] remainders = new int[k];
        foreach (int s in S) {
            remainders[s % k] += 1;
        }

        // Now, the interesting thing here is that, because we're
        // only adding pairs of numbers, (a % c) + (b % c) will only be
        // 0 or c if both are 0 or one is n and the other is (c - n).
        // That means we can greedily compare each c and (c - n) and
        // just take the larger count.

        // We can always take one from the bucket of factors of k
        // because if (a % c) == 0, then k | (a + b) iff
        // (b % c) == 0 also.
        int count = remainders[0] > 0 ? 1 : 0;

        // If k is even, we can take one from the middle bucket
        // because if (a % c) == k / 2, then k | (a + b) iff
        // (b % c) == k/2 also.
        if ((k & 1) == 0 && remainders[k / 2] > 0) count += 1;

        // Compare n and k - n until we get to the midpoint.
        // Note that if k is odd, then (k-1) / 2 == k / 2.
        int midpoint = (k - 1) / 2;
        for (int i = 1; i <= midpoint; i += 1) {

            // Compare each n with its k-n and greedily
            // take the larger
            count += remainders[i] >= remainders[k - i]
                ? remainders[i]
                : remainders[k - i];
        }

        return count;
    }
}