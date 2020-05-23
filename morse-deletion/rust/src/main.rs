use std::collections::HashSet;
use std::time::Instant;

fn main() {
    let start = Instant::now();

    let count = count_morse_deletions(
        b"%$&&&&$&$$$&&&$%$&%$&%&$$$&%%$&%$&%&$&&&$$$&&&$&%$%%&$&%",
        &[b"%&%%$%%%$%&&$&%", b"&%&&$&$&&$&%"],
    );

    let elapsed = start.elapsed().as_millis();
    println!("Took {}ms to count {} options", elapsed, count);
}

// Counts the number of ways the sequences in delete_strs
// can be successively removed from the source string.
// Examples:
//
// source = b"&%&" and delete_strs = [b"&"]       -> 2 (b"%&" and b"&%")
// source = b"&%&" and delete_strs = [b"&", b"%"] -> 1 (just b"&")
//
// I don't think the original problem mentioned what to do if
// delete_strs is longer than source or if source is empty.
//
// Note that I've now re-read the problem description and I'm not
// sure if I've handled blank spaces correctly. Not sure if they need
// to be handled correctly. It's kind of hard since I've changed all
// the morse characters for some anonymity.
fn count_morse_deletions(source: &[u8], delete_strs: &[&[u8]]) -> usize {
    if source.is_empty() {
        return 0; // or 1? What is the expectation here? :shrug:
    }

    if delete_strs
        .iter()
        .map(|subtrahend| subtrahend.len())
        .sum::<usize>()
        > source.len()
    {
        return 0; // or 1? What is the expectation here? :shrug:
    }

    // First, we want to find all the ways we can delete
    // delete_strs[0] from sources. Then all those results become
    // the sources from which we delete delete_strs[1], etc.
    let mut sources: HashSet<Vec<u8>> = [source.to_vec()].iter().cloned().collect();
    for delete_str in delete_strs {
        sources = sources
            .iter()
            .flat_map(|source| {
                let result = Vec::with_capacity(source.len());
                deletion_options(source, delete_str, result)
            })
            .collect();
    }

    sources.len()
}

// Finds the unique set of strings resulting from removing the
// sequence delete_str from source.
fn deletion_options(source: &[u8], delete_str: &[u8], mut result: Vec<u8>) -> HashSet<Vec<u8>> {
    let mut results = HashSet::new();

    // Base case - we removed all of the characters and found a result.
    if delete_str.is_empty() {
        result.extend_from_slice(&source);
        results.insert(result);
        return results;
    }

    // Prevent us from matching twice in a row
    let mut matched = false;

    for i in 0..source.len() {
        if source[i] == delete_str[0] {
            if !matched {
                matched = true;
                let deep_results =
                    deletion_options(&source[i + 1..], &delete_str[1..], result.clone());
                deep_results.into_iter().for_each(|r| {
                    results.insert(r);
                });
            }
        } else {
            matched = false;
        }
        result.push(source[i]);
    }

    results
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn first_readme_example() {
        let count = count_morse_deletions(b"&%$%&&&", &[b"&%&"]);
        assert_eq!(count, 2);
    }

    #[test]
    fn second_readme_example() {
        let count = count_morse_deletions(
            b"&&&&$&$&%&&$&%&&$%%%$$$&%%$%%%$&%&$&%&&$%&&",
            &[b"&&&&$&$&%&&$&%%&"],
        );
        assert_eq!(count, 1311);
    }

    #[test]
    fn third_readme_example() {
        let count = count_morse_deletions(b"&%$%&&&$%&%&$%&&", &[b"&&&$%", b"%%&&$%&"]);
        assert_eq!(count, 5);
    }

    #[test]
    fn fourth_readme_example() {
        let count = count_morse_deletions(
            b"%$&&&&$&$$$&&&$%$&%$&%&$$$&%%$&%$&%&$&&&$$$&&&$&%$%%&$&%",
            &[b"%&%%$%%%$%&&$&%", b"&%&&$&$&&$&%"],
        );
        assert_eq!(count, 11474);
    }

    #[test]
    fn test_three_delete_strs() {
        let count = count_morse_deletions(b"$%&$%&", &[b"$", b"%", b"&"]);
        assert_eq!(count, 5);
    }
}
