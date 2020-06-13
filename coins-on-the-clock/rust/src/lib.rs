const COINS: [u64; 3] = [1, 5, 10];
const COIN_CHARS: [char; 3] = ['p', 'n', 'd'];
const COIN_IDS: [u64; 3] = [0b01, 0b10, 0b11];

// All state is stored in a 64 bit number:
// 0000000000000000000000000000000000000000000000000000000000000000
// ^         ^^          ^^          ^^   ^^                      ^
// |         ||          ||          ||   | holds sequence of coins
// |         ||          ||          ||   | 11 - dime
// |         ||          ||          ||   | 10 - nickel
// |         ||          ||          ||   | 01 - penny
// |         ||          ||          ||   | 00 - no coin
// |         ||          ||          ||   |
// |         ||          ||          | hour of clock where
// |         ||          ||          | last coin was placed
// |         ||          ||          |
// |         ||          | clock state
// |         ||          | bit is 1 if hour is taken
// |         ||          | and 0 otherwise
// |         ||          |
// |         | coin counts
// |         | right-most three bits are penny count
// |         | next three bits are nickel count
// |         | left-most three bits are dime count
// |         |
//  padding
const NUM_HOURS: u64 = 12;
const COIN_BIT_LENGTH: u64 = 2;
const COIN_MASK: u64 = 0b11;
const CURRENT_VALUE_OFFSET: u64 = COIN_BIT_LENGTH * NUM_HOURS;
const CURRENT_VALUE_BIT_LENGTH: u64 = 5;
const CURRENT_VALUE_MASK: u64 = 0b11111 << CURRENT_VALUE_OFFSET;
const CLOCK_STATE_OFFSET: u64 = CURRENT_VALUE_OFFSET + CURRENT_VALUE_BIT_LENGTH;
const CLOCK_STATE_MASK: u64 = 0b111111111111 << CLOCK_STATE_OFFSET;
const CLOCK_STATE_BIT_LENGTH: u64 = NUM_HOURS;
const COIN_COUNT_OFFSET: u64 = CLOCK_STATE_OFFSET + CLOCK_STATE_BIT_LENGTH;
const COIN_COUNT_MASK: u64 = 0b111111111111 << COIN_COUNT_OFFSET;

// Extract string sequence of coins from u64 state.
// There are 12 coins so we loop 12 times.
// Each coin gets two bits so we have to shift by 2 * i.
// We shift the mask to the proper place and extract the coin value.
// 0b01 == penny, 0b10 == nickel, 0b11 = dime.
// Subtract one to get the index for the COIN_CHARS array.
fn extract_sequence(state: u64) -> String {
    (0..12)
        .map(|i| {
            let shift_amount = i * COIN_BIT_LENGTH;
            let coin_index = ((state & (COIN_MASK << shift_amount)) >> shift_amount) - 1;
            COIN_CHARS[coin_index as usize]
        })
        .collect()
}

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();
    let mut stack = vec![0b100100100 << COIN_COUNT_OFFSET];

    while let Some(state) = stack.pop() {
        if state & COIN_COUNT_MASK == 0 {
            sequences.push(extract_sequence(state));
        } else {
            for (i, coin) in COINS.iter().enumerate() {
                let this_coin_count_offset = COIN_COUNT_OFFSET + i as u64 * 3;
                let this_coin_count_mask = 0b111 << this_coin_count_offset;
                if state & this_coin_count_mask != 0 {
                    let current_value = (state & CURRENT_VALUE_MASK) >> CURRENT_VALUE_OFFSET;
                    let next_value = (current_value + coin) % NUM_HOURS;
                    let this_hour_clock_mask = 1 << (CLOCK_STATE_OFFSET + next_value);

                    if state & this_hour_clock_mask == 0 {
                        let coin_count_subtrahend = 1 << this_coin_count_offset;
                        let num_placed_coins = (state & CLOCK_STATE_MASK).count_ones() as u64;
                        let coin_id = COIN_IDS[i] << (num_placed_coins * COIN_BIT_LENGTH);

                        let mut new_state = state;
                        new_state = new_state | this_hour_clock_mask; // mark clock-hour as taken
                        new_state = new_state | coin_id; // add coin to sequence
                        new_state -= coin_count_subtrahend; // decrement count of coin taken

                        // update current value by clearing out current value and adding in next_value
                        new_state =
                            (new_state & !CURRENT_VALUE_MASK) | next_value << CURRENT_VALUE_OFFSET;
                        stack.push(new_state);
                    }
                }
            }
        }
    }

    sequences
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_valid_sequences() {
        let mut received = get_valid_sequences();
        received.sort();
        let mut expected = [
            String::from("ppddnnpddpnn"),
            String::from("pnpddnpnddpn"),
            String::from("pnnpddpnnddp"),
            String::from("pdpdnnpnndpd"),
            String::from("nppppnddnddn"),
            String::from("npppnddnddnp"),
            String::from("nppnddpnpddn"),
            String::from("nppnddnddnpp"),
            String::from("npnddpnpddnp"),
            String::from("npnddnddnppp"),
            String::from("nnpddnpppndd"),
            String::from("nnddpnpddnpp"),
            String::from("nnddnddnpppp"),
            String::from("nddnpppnddpn"),
        ];
        expected.sort();
        assert_eq!(received, expected);
    }
}
