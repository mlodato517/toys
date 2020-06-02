const NUM_HOURS: usize = 12;
const COINS: [usize; 3] = [1, 5, 10];
const COIN_CHARS: [char; 3] = ['p', 'n', 'd'];

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();

    _get_valid_sequences(
        0b100100100,
        &mut [' '; NUM_HOURS],
        &mut [false; NUM_HOURS],
        &mut sequences,
        0,
        0,
    );

    sequences
}

fn has_coin(count: usize, offset: usize) -> bool {
    count & (0b111 << offset * 3) != 0
}

fn take_coin(count: usize, offset: usize) -> usize {
    count - (1 << offset * 3)
}

fn _get_valid_sequences(
    counts: usize,
    current_sequence: &mut [char; NUM_HOURS],
    clock_state: &mut [bool; NUM_HOURS],
    return_values: &mut Vec<String>,
    current_value: usize,
    current_index: usize,
) {
    if counts == 0 {
        return_values.push(current_sequence.iter().collect());
    } else {
        for i in 0..COINS.len() {
            if !has_coin(counts, i) {
                continue;
            }

            let coin = COINS[i];
            let next_value = (current_value + coin) % NUM_HOURS;

            if clock_state[next_value] {
                continue;
            }

            clock_state[next_value] = true;
            current_sequence[current_index] = COIN_CHARS[i];

            _get_valid_sequences(
                take_coin(counts, i),
                current_sequence,
                clock_state,
                return_values,
                next_value,
                current_index + 1,
            );

            clock_state[next_value] = false;
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn test_valid_sequences() {
        assert_eq!(
            get_valid_sequences(),
            [
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
            ]
        )
    }
}
