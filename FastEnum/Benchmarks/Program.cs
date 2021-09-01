using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using Library;

BenchmarkRunner.Run<Benchmarks>();

// [MemoryDiagnoser]
// [DisassemblyDiagnoser(exportHtml: true)] // uncomment and find html report in *.artifacts in the output folder
public class Benchmarks
{
    [Params(10_000)]
    public int Iterations { get; set; }

    private int Inc { get; set; } = 1; // by reading from field/property, the JIT compiler won't optimize it to inc instruction

    /*
     * in terms of perf, identical to i += inc variant
    [Benchmark]
    public int ForWithIncrementBy1()
    {
        var iters = Iterations;
        var a = 0;
        for (int i = 0; i < iters; i++)
            a += i;
        return a;
    }*/

    [Benchmark]
    public int ForWithCustomIncrement()
    {
        var iters = Iterations;
        var a = 0;
        var inc = Inc;
        for (int i = 0; i < iters; i += inc)
            a += i;
        return a;
    }

    [Benchmark]
    public int ForeachWithEnumerableRange()
    {
        var a = 0;
        foreach (var i in Enumerable.Range(0, Iterations))
            a += i;
        return a;
    }

    private static IEnumerable<int> MyRange(int from, int to, int inc)
    {
        for (int i = from; i <= to; i += inc)
            yield return i;
    }

    [Benchmark]
    public int ForeachWithYieldReturn()
    {
        var a = 0;
        foreach (var i in MyRange(0, Iterations - 1, 1))
            a += i;
        return a;
    }

    [Benchmark]
    public int ForeachWithRangeEnumerator()
    {
        var a = 0;
        foreach (var i in 0..(Iterations - 1))
            a += i;
        return a;
    }

    [Benchmark]
    public int ForeachWithRangeEnumeratorRaw()
    {
        var a = 0;
        var enumerator = (0..(Iterations - 1)).GetEnumerator();
        while (enumerator.MoveNext())
            a += enumerator.Current;
        return a;
    }

    [Benchmark]
    public int ForeachWithRangeEnumeratorRawWithLocal()
    {
        var enumerator = (0..(Iterations - 1)).GetEnumerator();        
        return EnumerateItAll(enumerator);

        static int EnumerateItAll(RangeEnumerator enumerator)
        {
            var a = 0;
            while (enumerator.MoveNext())
                a += enumerator.Current;
            return a;
        }
    }
}