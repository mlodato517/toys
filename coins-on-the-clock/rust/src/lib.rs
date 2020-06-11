const NUM_HOURS: usize = 12;
const COINS: [usize; 3] = [1, 5, 10];
const COIN_CHARS: [char; 3] = ['p', 'n', 'd'];

struct Node {
    counts: usize,
    current_sequence: [char; NUM_HOURS],
    clock_state: usize,
    current_value: usize,
}

pub fn get_valid_sequences() -> Vec<String> {
    let mut sequences = Vec::new();

    let mut stack = vec![Node {
        counts: 0b100100100,
        current_sequence: [' '; NUM_HOURS],
        clock_state: 0,
        current_value: 0,
    }];

    while let Some(node) = stack.pop() {
        let current_index = node.clock_state.count_ones() as usize;
        if current_index == NUM_HOURS {
            sequences.push(node.current_sequence.iter().collect());
        } else {
            for (i, coin) in COINS.iter().enumerate() {
                if (0b111 << (i * 3)) & node.counts != 0 {
                    let next_value = (node.current_value + coin) % NUM_HOURS;

                    let clock_mask = 1 << next_value;
                    if clock_mask & node.clock_state == 0 {
                        let mut next_node = Node {
                            current_value: next_value,
                            ..node
                        };
                        next_node.counts -= 1 << (i * 3);
                        next_node.clock_state = next_node.clock_state | clock_mask;
                        next_node.current_sequence[current_index] = COIN_CHARS[i];
                        stack.push(next_node);
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
        let mut val = get_valid_sequences();
        val.sort();
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
        assert_eq!(val, expected)
    }
}
