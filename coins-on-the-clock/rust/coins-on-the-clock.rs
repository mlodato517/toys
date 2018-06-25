fn main() {
  let numHours = 12;
  let coins = [1, 5, 10];
  let counts = [4, 4, 4];

  let values = getValidSequences(numHours, &coins, &mut counts);

  for value in &values {
      println!("{}", value);
  }
}

fn getValidSequences(numHours: i32, coins: &[i32], counts &mut [i32]) ->