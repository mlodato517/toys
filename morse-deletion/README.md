# Morse Deletion

## Step One

Given a morse string, find unique sequences of remaining tokens after removing a second string from the first. There are 3 Morse tokens:

Dot (&)
Dash (%)
Blank (\$)

Letters are separated by a blank character. Words are separated by 3 blanks.

Example:
Given: AB
`&%$%&&&`

Remove: R
`&%&`

This can be done 6 ways:

- `X X $ % X & &`
- `X X $ % & X &`
- `X X $ % & & X`
- `X % $ X X & &`
- `X % $ X & X &`
- `X % $ X & & X`

But only 2 are unique:

- `$%&&`
- `%$&&`

Thus the answer is 2.

Write a program that calculates all deletion paths.

Given Hello World:
`&&&&$&$&%&&$&%&&$%%%$$$&%%$%%%$&%&$&%&&$%&&`

Remove: Help
`&&&&$&$&%&&$&%%&`

ANSWER: 1311

## Step two

Remove a second string after removing the first. Tokens should remain in order to find a second phrase. Return all unique sequences of remaining tokens after removing both phrases.

Example:

Given: ABCD
`&%$%&&&$%&%&$%&&`

Remove: ST
`&&&$%`

Then Remove: ZN
`%%&&$%&`

One path is:

Start:
`& % $ % & & & $ % & % & $ % & &`
Remove ST:
`x % $ % x x & x x & % & $ % & &`
Remove ZN:
`x x $ x x x x x x x % & x x x &`
Remaining characters:
`$ % & &`

There are 5 unique sequences:

- `$%&&`
- `$&%&`
- `%$&&`
- `&$%&`
- `&%$&`

Have your code find all of possible sequences of remaining characters after removing 2 messages from the original.

Given: The Star Wars Saga
`%$&&&&$&$$$&&&$%$&%$&%&$$$&%%$&%$&%&$&&&$$$&&&$&%$%%&$&%`
Remove: Yoda
`%&%%$%%%$%&&$&%`
And Remove: Leia
`&%&&$&$&&$&%`
Expected Answer: 11474
