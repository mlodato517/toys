const NUM_HOURS: usize = 12;
const COINS: [usize; 3] = [1, 5, 10];
const COIN_CHARS: [char; 3] = ['p', 'n', 'd'];

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();

    _get_valid_sequences(&mut [4, 4, 4], &mut [' '; NUM_HOURS], 0, &mut sequences, 0);

    sequences
}

fn _get_valid_sequences(
    counts: &mut [usize; 3],
    current_sequence: &mut [char; NUM_HOURS],
    clock_state: u16,
    return_values: &mut Vec<String>,
    current_value: usize,
) {
    if counts.iter().all(|&n| n == 0) {
        return_values.push(current_sequence.iter().collect());
    } else {
        for i in 0..COINS.len() {
            if counts[i] == 0 {
                continue;
            }

            let coin = COINS[i];
            let next_value = (current_value + coin) % NUM_HOURS;

            if clock_state & (1 << next_value) != 0 {
                continue;
            }

            counts[i] -= 1;
            current_sequence[clock_state.count_ones() as usize] = COIN_CHARS[i];

            _get_valid_sequences(
                counts,
                current_sequence,
                clock_state | (1 << next_value),
                return_values,
                next_value,
            );

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
