using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(nonDivisibleSubset(1, new [] {1}) + " should be 1");
        Console.WriteLine(nonDivisibleSubset(1, new [] {1, 2}) + " should be 1");

        Console.WriteLine(nonDivisibleSubset(1, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9}) + " should be 1");
        Console.WriteLine(nonDivisibleSubset(1, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}) + " should be 1");

        Console.WriteLine(nonDivisibleSubset(2, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9}) + " should be 2");
        Console.WriteLine(nonDivisibleSubset(2, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}) + " should be 2");

        Console.WriteLine(nonDivisibleSubset(2, new [] {2, 4, 6, 8, 10}) + " should be 1");
        Console.WriteLine(nonDivisibleSubset(2, new [] {2, 4, 6, 8}) + " should be 1");

        Console.WriteLine(nonDivisibleSubset(7, new [] {1, 2, 3}) + " should be 3");
        Console.WriteLine(nonDivisibleSubset(7, new [] {1, 2}) + " should be 2");

        Console.WriteLine(nonDivisibleSubset(7, new [] {3, 4}) + " should be 1");
        Console.WriteLine(nonDivisibleSubset(7, new [] {4, 5}) + " should be 2");

        Console.WriteLine(nonDivisibleSubset(6, new [] {3, 6, 12, 18}) + " should be 2");
        Console.WriteLine(nonDivisibleSubset(6, new [] {3, 6, 12}) + " should be 2");

        Console.WriteLine(nonDivisibleSubset(5, new [] {1, 2, 3, 4, 5}) + " should be 3");
        Console.WriteLine(nonDivisibleSubset(5, new [] {2, 3, 4, 5}) + " should be 3");
    }

    // Complete the nonDivisibleSubset function below.
    static int nonDivisibleSubset(int k, int[] S) {
        int[] remainders = new int[k];
        foreach (int s in S) {
            remainders[s % k] += 1;
        }

        // remainders[0] has count of factors of k.
        // If we have any, we can safely take one from the bucket.
        int count = remainders[0] > 0 ? 1 : 0;
        bool kEven = (k & 1) == 0;

        // Start at 1 to get other numbers.
        for (int i = 1; i <= k / 2; i += 1) {

            // If we're dividing by, let's say 6
            // and we're looking at the bucket of things with remainder 3,
            // we know we can always take 1 but we can only take 1.
            if (kEven && i == k / 2 && remainders[i] > 0) count += 1;

            // Otherwise, compare remainders[i] to it's "compliment"
            // and take the larger
            else count += remainders[i] >= remainders[k - i]
                ? remainders[i]
                : remainders[k - i];
        }

        return count;
    }
}