def get_valid_sequences(num_hours, values, counts)
  sequences = _get_valid_sequences(num_hours, values, counts, [], [], 0)

  sequences.map { |s| s.map { |i| get_coin_name(i) }.join('') }
end

def _get_valid_sequences(
  num_hours,
  values,
  counts,
  current_sequence,
  clock_state,
  current_value
)
  return [current_sequence.dup] if current_sequence.length == num_hours

  return_values = []
  (0...counts.length).each do |i|
    next if counts[i] == 0

    next_value = (current_value + values[i]) % num_hours

    next if clock_state[next_value]

    clock_state[next_value] = true
    counts[i] -= 1
    current_sequence.push values[i]

    return_values.concat(_get_valid_sequences(
      num_hours,
      values,
      counts,
      current_sequence,
      clock_state,
      next_value
    ))

    clock_state[next_value] = false
    counts[i] += 1
    current_sequence.pop
  end

  return_values
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
# Multipley by 1000 to get ms for 1000 calculations.
puts ((stop - start) * 1_000)
