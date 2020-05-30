use std::time::Instant;

fn main() {
    let start = Instant::now();
    for _ in 0..1000 {
        get_valid_sequences();
    }
    println!("{}", start.elapsed().as_millis());
}

fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();
    _get_valid_sequences(4, 4, 4, 0, 0, 0, 0, &mut sequences);

    sequences
        .iter()
        .copied()
        .map(coin_sequence_from_u32)
        .collect()
}

// Recursively try putting pennies, nickels, and dimes on the clock.
// When a coin is placed, its value (see coin_sequence_from_u32)
// is masked onto current_sequence and the coin's count is decremented.
// Coins cannot be placed if they're spot is already taken (identified
// via the BitSet clock_state).
// When we have no pennies, nickels, or dimes, we've found a solution
// and we add it to the list.
fn _get_valid_sequences(
    num_pennies: u8,
    num_nickels: u8,
    num_dimes: u8,
    current_sequence: u32,
    clock_state: u16,
    current_value: u16,
    current_index: u8,
    return_values: &mut Vec<u32>,
) {
    if num_pennies == 0 && num_nickels == 0 && num_dimes == 0 {
        return_values.push(current_sequence);
    } else {
        if num_pennies != 0 {
            let next_value = (current_value + 1) % 12;
            if clock_state & (1 << next_value) == 0 {
                _get_valid_sequences(
                    num_pennies - 1,
                    num_nickels,
                    num_dimes,
                    current_sequence | (1 << current_index),
                    clock_state | (1 << next_value),
                    next_value,
                    current_index + 2,
                    return_values,
                );
            }
        }
        if num_nickels != 0 {
            let next_value = (current_value + 5) % 12;
            if clock_state & (1 << next_value) == 0 {
                _get_valid_sequences(
                    num_pennies,
                    num_nickels - 1,
                    num_dimes,
                    current_sequence | (2 << current_index),
                    clock_state | (1 << next_value),
                    next_value,
                    current_index + 2,
                    return_values,
                );
            }
        }
        if num_dimes != 0 {
            let next_value = (current_value + 10) % 12;
            if clock_state & (1 << next_value) == 0 {
                _get_valid_sequences(
                    num_pennies,
                    num_nickels,
                    num_dimes - 1,
                    current_sequence | (3 << current_index),
                    clock_state | (1 << next_value),
                    next_value,
                    current_index + 2,
                    return_values,
                );
            }
        }
    }
}

// A sequence is a 24 bit value.
// Every 2 bits is a space for a coin.
// 00 - no coin
// 01 - penny
// 10 - nickel
// 11 - dime
//
// A shortened example:
// sequence = 0b111001
//
// i = 0
// bit_index = 11
// coin_index = 01
// coin = 'p'
//
// i = 1
// bit_index = 1100
// coin_index = 10
// coin = 'n'
//
// i = 2
// bit_index = 110000
// coin_index = 11
// coin = 'd'
//
// Result is 'pnd'!
const COINS: [char; 4] = ['x', 'p', 'n', 'd'];
fn coin_sequence_from_u32(sequence: u32) -> String {
    (0..12)
        .map(|i| COINS[(sequence & (0b11 << (2 * i))) as usize >> (2 * i)])
        .collect()
}
