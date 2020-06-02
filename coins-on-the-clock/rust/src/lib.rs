const NUM_HOURS: usize = 12;
const COINS: [usize; 3] = [1, 5, 10];
const COIN_CHARS: [char; 3] = ['p', 'n', 'd'];
const COIN_IDS: [usize; 3] = [1, 2, 3];

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();

    _get_valid_sequences(
        &mut [4, 4, 4],
        0,
        &mut [false; NUM_HOURS],
        &mut sequences,
        0,
        0,
    );

    sequences
}

fn add_coin(current: usize, id: usize, offset: usize) -> usize {
    current | (id << (offset * 2))
}

fn translate(sequence: usize) -> String {
    (0..12)
        .map(|i| {
            let shift_amount = i * 2;
            let mask = 0b11 << shift_amount;
            let bits = sequence & mask;
            let value = bits >> shift_amount;
            COIN_CHARS[value - 1]
        })
        .collect()
}

fn _get_valid_sequences(
    counts: &mut [usize; 3],
    current_sequence: usize,
    clock_state: &mut [bool; NUM_HOURS],
    return_values: &mut Vec<String>,
    current_value: usize,
    current_index: usize,
) {
    if counts.iter().all(|&n| n == 0) {
        return_values.push(translate(current_sequence));
    } else {
        for i in 0..COINS.len() {
            if counts[i] == 0 {
                continue;
            }

            let coin = COINS[i];
            let next_value = (current_value + coin) % NUM_HOURS;

            if clock_state[next_value] {
                continue;
            }

            clock_state[next_value] = true;
            counts[i] -= 1;

            _get_valid_sequences(
                counts,
                add_coin(current_sequence, COIN_IDS[i], current_index),
                clock_state,
                return_values,
                next_value,
                current_index + 1,
            );

            clock_state[next_value] = false;
            counts[i] += 1;
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
