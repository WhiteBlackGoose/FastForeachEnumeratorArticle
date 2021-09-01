using System;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        public Benchmarks GetBenchmarks()
            => new Benchmarks() { Iterations = 4 };

        public int Expected => 6;

        /*
         * in terms of perf, identical to i += inc variant
        [Fact]
        public void ForWithIncrementBy1()
        {
            Assert.Equal(Expected, GetBenchmarks().ForWithIncrementBy1());
        }*/

        [Fact]
        public void ForWithCustomIncrement()
        {
            Assert.Equal(Expected, GetBenchmarks().ForWithCustomIncrement());
        }

        [Fact]
        public void ForeachWithEnumerableRange()
        {
            Assert.Equal(Expected, GetBenchmarks().ForeachWithEnumerableRange());
        }

        [Fact]
        public void ForeachWithYieldReturn()
        {
            Assert.Equal(Expected, GetBenchmarks().ForeachWithYieldReturn());
        }

        [Fact]
        public void ForeachWithRangeEnumerator()
        {
            Assert.Equal(Expected, GetBenchmarks().ForeachWithRangeEnumerator());
        }

        [Fact]
        public void ForeachWithRangeEnumeratorRaw()
        {
            Assert.Equal(Expected, GetBenchmarks().ForeachWithRangeEnumeratorRaw());
        }

        [Fact]
        public void ForeachWithRangeEnumeratorRawWithLocal()
        {
            Assert.Equal(Expected, GetBenchmarks().ForeachWithRangeEnumeratorRawWithLocal());
        }
    }
}
