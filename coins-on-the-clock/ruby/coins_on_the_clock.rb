def get_valid_sequences(num_hours, values, counts)
  sequences = []
  _get_valid_sequences(
    num_hours,
    values,
    counts,
    Array.new(num_hours, 0),
    Array.new(num_hours, false),
    sequences,
    0,
    0
  )

  sequences
end

def _get_valid_sequences(
  num_hours,
  values,
  counts,
  current_sequence,
  clock_state,
  return_values,
  current_value,
  current_sequence_index
)
  if current_sequence_index == num_hours
    return_values.push(current_sequence.map { |i| get_coin_name(i) }.join(''))
  else
    i = -1
    while ((i += 1) < counts.length) do
      next if counts[i] == 0

      next_value = (current_value + values[i]) % num_hours

      next if clock_state[next_value]

      clock_state[next_value] = true
      counts[i] -= 1
      current_sequence[current_sequence_index] = values[i]

      _get_valid_sequences(
        num_hours,
        values,
        counts,
        current_sequence,
        clock_state,
        return_values,
        next_value,
        current_sequence_index + 1
      )

      clock_state[next_value] = false
      counts[i] += 1
    end
  end
end

def get_coin_name(coin)
  if coin == 1
    "p"
  elsif coin == 5
    "n"
  elsif coin == 10
    "d"
  elsif coin == 25
    "q"
  else
    "?"
  end
end

modulo = 12
coins = [ 1, 5, 10 ]
counts = [ 4, 4, 4 ]

start = Time.now
1000.times { get_valid_sequences(modulo, coins, counts) }
stop = Time.now

# stop - start is time in seconds.
# Multiply by 1000 to get ms for 1000 calculations.
puts ((stop - start) * 1_000)
