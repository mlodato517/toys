use coins_on_the_clock::get_valid_sequences;
use std::time::Instant;

fn main() {
    println!("{:?}", get_valid_sequences());
    let start = Instant::now();
    for _ in 0..1000 {
        get_valid_sequences();
    }
    println!("{}", start.elapsed().as_millis());
}

