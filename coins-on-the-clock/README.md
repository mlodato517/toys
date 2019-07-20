# Problem definition:

You have 4 pennies, 4 nickels, and 4 dimes
You must place these coins on the clock in the following way:

1. Pick a starting hour (let's say 4)
1. Pick a coin (let's say a nickel)
1. Place the coin at the hour + its value (so 9)
1. Repeat (so let's now pick a Penny and place it at 10 (9+1) and then take a dime and place it at 8 (10+10 % 12))

Write a program to find all sequences which cover the clock completely (i.e. there are no coin "collisions").

## Setup

### C++

Install `g++`. It'll probably be installed on most unix based OSs. Probably have to jump through some hoops on windows or use Windows subsystem for Linux or a virtual box.

### CSharp

Install [dotnet core](https://dotnet.microsoft.com/download) version 2.x (I have 2.1.500 as of writing this).

### Ruby

Install [ruby](https://www.ruby-lang.org/en/documentation/installation/).

### Rust

Install [cargo](https://doc.rust-lang.org/cargo/getting-started/installation.html).

### Python

Install [python3](https://www.python.org/downloads/). Ensure there is an executable on your system called `python3` or update the `test-all.rb` script to use your executable for python.

### Node

Install [nodejs](https://nodejs.org/en/).

### Go

Install [go](https://golang.org/doc/install#install).

### Kotlin

Install [kotlin](https://kotlinlang.org/docs/tutorials/command-line.html).

## Testing all the languages

While in this directory, run `ruby test-all.rb`.

## Testing an individual language

See the code executed in `test-all.rb`.
