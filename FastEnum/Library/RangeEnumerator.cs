using System;
using System.Collections.Generic;

namespace Library
{
    public static class Extensions
    {
        public static RangeEnumerator GetEnumerator(this Range @this)
            => (@this.Start, @this.End) switch
            {
                ({ IsFromEnd: true, Value: 0 }, { IsFromEnd: true, Value: 0 }) => new RangeEnumerator(0, int.MaxValue, 1),
                ({ IsFromEnd: true, Value: 0 }, { IsFromEnd: false, Value: var to }) => new RangeEnumerator(0, to + 1, 1),
                ({ IsFromEnd: false, Value: var from }, { IsFromEnd: true, Value: 0 }) => new RangeEnumerator(from, int.MaxValue, 1),
                ({ IsFromEnd: false, Value: var from }, { IsFromEnd: false, Value: var to })
                    => (from < to) switch
                    {
                        true => new RangeEnumerator(from, to, 1),
                        false => new RangeEnumerator(from, to, -1),
                    },
                _ => throw new InvalidOperationException("Invalid range")
            };
    }

    /// <summary>
    /// A struct enumerator
    /// </summary>
    public struct RangeEnumerator
    {
        private readonly int to;
        private readonly int step;

        private int curr;

        internal void Deconstruct(out int @from, out int to, out int step)
            => (@from, to, step) = (curr, this.to, this.step);

        /// <summary></summary>
        internal RangeEnumerator(int @from, int to, int step)
        {
            this.to = to + step;
            this.step = step;
            this.curr = @from - step;
        }

        /// <summary>
        /// Moves to the next element. There
        /// should be at least one call of this
        /// method before getting Current
        /// </summary>
        public bool MoveNext()
        {
            curr += step;
            return curr != to;
        }

        /// <summary>
        /// The current element (undefined if MoveNext
        /// is not called).
        /// </summary>
        public int Current => curr;
    }
}
