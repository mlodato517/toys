extern crate time;

fn main() {
    let num_hours = 12;
    let coins = [1, 5, 10];
    let counts = [4, 4, 4];

    let now = time::precise_time_ns();
    for _ in 0..1000 {
        get_valid_sequences(num_hours, &coins, &counts);
    }
    let now2 = time::precise_time_ns();
    let diff = (now2 - now) / 1000000;

    println!("{}", diff);
}

fn get_valid_sequences(num_hours: usize, coins: &[usize], counts: &[usize]) -> Vec<String> {
    let mut clock_state = vec![false; num_hours];
    let mut current_sequence = Vec::with_capacity(num_hours);

    let sequences = _get_valid_sequences_with_defaults(
        num_hours,
        coins,
        counts,
        &mut current_sequence,
        &mut clock_state,
    )
    .into_iter()
    .map(|value_vec| usize_vec_to_string(value_vec))
    .collect();

    return sequences;
}

fn usize_vec_to_string(values: Vec<usize>) -> String {
    values
        .into_iter()
        .map(|value| get_coin_name(value))
        .collect::<String>()
}

fn _get_valid_sequences_with_defaults(
    num_hours: usize,
    coins: &[usize],
    counts: &[usize],
    current_sequence: &mut Vec<usize>,
    clock_state: &mut Vec<bool>,
) -> Vec<Vec<usize>> {
    return _get_valid_sequences(
        num_hours,
        coins,
        counts,
        current_sequence,
        clock_state,
        0,
        &mut vec![0; counts.len()],
    );
}

fn _get_valid_sequences(
    num_hours: usize,
    coins: &[usize],
    counts: &[usize],
    current_sequence: &mut Vec<usize>,
    clock_state: &mut Vec<bool>,
    current_value: usize,
    current_counts: &mut Vec<usize>,
) -> Vec<Vec<usize>> {
    let mut return_values = Vec::new();

    if current_sequence.len() == num_hours {
        return_values.push(current_sequence.to_owned());
        return return_values;
    } else {
        for i in 0..coins.len() {
            let counts_remaining = counts[i] - current_counts[i];

            if counts_remaining == 0 {
                continue;
            }

            let coin = coins[i];
            let next_value = (current_value + coin) % num_hours;

            if clock_state[next_value] {
                continue;
            }

            clock_state[next_value] = true;
            current_counts[i] += 1;
            current_sequence.push(coin);

            let mut new_values = _get_valid_sequences(
                num_hours,
                coins,
                counts,
                current_sequence,
                clock_state,
                next_value,
                current_counts,
            );
            return_values.append(&mut new_values);

            clock_state[next_value] = false;
            current_counts[i] -= 1;
            current_sequence.pop();
        }
    }
    return return_values;
}

fn get_coin_name(coin_value: usize) -> char {
    return match coin_value {
        1 => 'p',
        5 => 'n',
        10 => 'd',
        25 => 'q',
        _ => 'x',
    };
}
