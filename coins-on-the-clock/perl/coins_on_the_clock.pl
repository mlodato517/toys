use Time::HiRes qw ( time );

sub get_valid_sequences {
  my (
    $num_hours,
    $coins,
    $counts,
  ) = @_;

  my $sequences = [];
  _get_valid_sequences(
    $num_hours,
    $coins,
    $counts,
    [],
    [],
    $sequences,
    0,
    0
  );

  return $sequences;
}

sub _get_valid_sequences {
  my (
    $num_hours,
    $coins,
    $counts,
    $current_sequence,
    $clock_state,
    $return_values,
    $current_value,
    $current_sequence_index,
  ) = @_;

  if ($current_sequence_index == $num_hours) {
    my $foo = join('', map { get_coin_name($_) } @$current_sequence);
    push(@$return_values, $foo);
  } else {
    for my $i (0..scalar @$counts) {
      if ($counts->[$i] == 0) {
        next;
      }

      my $next_value = ($current_value + $coins->[$i]) % $num_hours;

      if ($clock_state->[$next_value]) {
        next;
      }

      $clock_state->[$next_value] = 1;
      $counts->[$i] -= 1;
      $current_sequence->[$current_sequence_index] = $coins->[$i];

      _get_valid_sequences(
        $num_hours,
        $coins,
        $counts,
        $current_sequence,
        $clock_state,
        $return_values,
        $next_value,
        $current_sequence_index + 1
      );

      $clock_state->[$next_value] = 0;
      $counts->[$i] += 1;
    }
  }
}

sub get_coin_name {
  my $coin = shift @_;
  if ($coin == 1) {
    "p"
  } elsif ($coin == 5) {
    "n"
  } elsif ($coin == 10) {
    "d"
  } elsif ($coin == 25) {
    "q"
  } else {
    "?"
  }
}

my $modulo = 12;
my @coins = (1, 5, 10);
my @counts = (4, 4, 4);

my $start = time();
for (0..1000) {
  get_valid_sequences($modulo, \@coins, \@counts);
}
my $end = time();
print (($end-$start) * 1000);
