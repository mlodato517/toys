python_ms = `python3 python/coins-on-the-clock.py`

cpp_compile = `g++ -O3 cpp/coins-on-the-clock.cpp -o "cpp/coins-on-the-clock"`
cpp_ms = `cpp/coins-on-the-clock`
cpp_cleanup = `rm cpp/coins-on-the-clock`

ruby_ms = `ruby --jit ruby/coins_on_the_clock.rb`

cs_ms = `dotnet run -c Release --project c#/`

rust_ms = `cd rust && cargo run --release rust/src/main.rs && cd ../`

js_ms = `node js/coins_on_the_clock.js`

go_ms = `go run go/coins_on_the_clock.go`

compile_kotlin = `kotlinc -include-runtime -d kotlin/coins_on_the_clock.jar kotlin/coins_on_the_clock.kt`
kotlin_ms = `java -jar kotlin/coins_on_the_clock.jar`
kotlin_cleanup = `rm kotlin/coins_on_the_clock.jar`

puts "OUTPUT (ms for 1000 iterations):"
[
  [ "python", python_ms ],
  [ "cpp", cpp_ms ],
  [ "ruby", ruby_ms ],
  [ "cs", cs_ms ],
  [ "rust", rust_ms ],
  [ "js", js_ms ],
  [ "go", go_ms ],
  [ "kotlin", kotlin_ms ],
].sort_by { |_, ms| ms.to_f }.each { |lang, ms| puts "#{lang}:\t#{ms}" }
