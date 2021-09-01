## Материалы

Статья на [хабре](https://habr.com/ru/post/575664/).

```
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1110 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100-preview.6.21355.2
  [Host]     : .NET 6.0.0 (6.0.21.35212), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.35212), X64 RyuJIT
```

|                                 Method | Iterations |      Mean |     Error |    StdDev |
|--------------------------------------- |----------- |----------:|----------:|----------:|
|                 ForWithCustomIncrement |      10000 |  3.498 us | 0.0658 us | 0.1153 us |
|             ForeachWithEnumerableRange |      10000 | 43.313 us | 0.8207 us | 1.0079 us |
|                 ForeachWithYieldReturn |      10000 | 56.963 us | 1.0638 us | 1.0448 us |
|             ForeachWithRangeEnumerator |      10000 | 17.931 us | 0.2047 us | 0.1915 us |
|          ForeachWithRangeEnumeratorRaw |      10000 | 17.932 us | 0.1486 us | 0.1390 us |
| ForeachWithRangeEnumeratorRawWithLocal |      10000 |  3.501 us | 0.0678 us | 0.0807 us |
