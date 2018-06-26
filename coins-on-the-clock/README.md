Problem definition:

You have 4 pennies, 4 nickels, and 4 dimes
You must place these coins on the clock in the following way:
1) Pick a starting hour (let's say 4)
2) Pick a coin (let's say a nickel)
3) Place the coin at the hour + its value (so 9)
4) Repeat (so let's now pick a Penny and place it at 10 (9+1) and then take a dime and place it at 8 (10+10 % 12))

Write a program to find all sequences which cover the clock completely (i.e. there are no coin "collisions").

My results:
1) c++ - 110,000ns
2) c# - 1,600,000ns
3) python 3 64 bit - 5,000,000ns
4) python 3 32 bit - 6,500,000ns