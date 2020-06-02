use coins_on_the_clock::get_valid_sequences;
use criterion::{criterion_group, criterion_main, Criterion};

fn criterion_benchmark(c: &mut Criterion) {
    c.bench_function("coins on the clock", |b| b.iter(|| get_valid_sequences()));
}

criterion_group!(benches, criterion_benchmark);
criterion_main!(benches);
