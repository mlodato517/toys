const NUM_HOURS: usize = 12;
const PENNY: char = 'p';
const PENNY_VALUE: usize = 1;
const NICKEL: char = 'n';
const NICKEL_VALUE: usize = 5;
const DIME: char = 'd';
const DIME_VALUE: usize = 10;

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();

    _get_valid_sequences(
        4,
        4,
        4,
        &mut [' '; NUM_HOURS],
        &mut [false; NUM_HOURS],
        &mut sequences,
        0,
        0,
    );

    sequences
}

fn _get_valid_sequences(
    num_pennies: u8,
    num_nickels: u8,
    num_dimes: u8,
    current_sequence: &mut [char; NUM_HOURS],
    clock_state: &mut [bool; NUM_HOURS],
    return_values: &mut Vec<String>,
    current_value: usize,
    current_index: usize,
) {
    if num_pennies == 0 && num_nickels == 0 && num_dimes == 0 {
        return_values.push(current_sequence.iter().collect());
    } else {
        if num_pennies != 0 {
            let next_value = (current_value + PENNY_VALUE) % NUM_HOURS;
            if !clock_state[next_value] {
                clock_state[next_value] = true;
                current_sequence[current_index] = PENNY;

                _get_valid_sequences(
                    num_pennies - 1,
                    num_nickels,
                    num_dimes,
                    current_sequence,
                    clock_state,
                    return_values,
                    next_value,
                    current_index + 1,
                );

                clock_state[next_value] = false;
            }
        }
        if num_nickels != 0 {
            let next_value = (current_value + NICKEL_VALUE) % NUM_HOURS;
            if !clock_state[next_value] {
                clock_state[next_value] = true;
                current_sequence[current_index] = NICKEL;

                _get_valid_sequences(
                    num_pennies,
                    num_nickels - 1,
                    num_dimes,
                    current_sequence,
                    clock_state,
                    return_values,
                    next_value,
                    current_index + 1,
                );

                clock_state[next_value] = false;
            }
        }
        if num_dimes != 0 {
            let next_value = (current_value + DIME_VALUE) % NUM_HOURS;
            if !clock_state[next_value] {
                clock_state[next_value] = true;
                current_sequence[current_index] = DIME;

                _get_valid_sequences(
                    num_pennies,
                    num_nickels,
                    num_dimes - 1,
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
