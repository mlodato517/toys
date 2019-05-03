python_ms = `python3 python/coins-on-the-clock.py`

cpp_compile = `g++ -O3 cpp/coins-on-the-clock.cpp -o "cpp/coins-on-the-clock"`
cpp_ms = `cpp/coins-on-the-clock`
cpp_cleanup = `rm cpp/coins-on-the-clock`

ruby_ms = `ruby ruby/coins_on_the_clock.rb`

cs_ms = `dotnet run -c Release --project c#/`

rust_ms = `cd rust && cargo run --release rust/src/main.rs && cd ../`

puts "OUTPUT (ms for 1000 iterations):"
[
  [ "python", python_ms ],
  [ "cpp", cpp_ms ],
  [ "ruby", ruby_ms ],
  [ "cs", cs_ms ],
  [ "rust", rust_ms ],
].sort_by { |_, ms| ms.to_f }.each { |lang, ms| puts "#{lang}:\t#{ms}" }
