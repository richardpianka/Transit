using System;
using System.Collections;
using System.Collections.Generic;
using Transit.Common.Extensions;

namespace Transit.Reporting.TimeBlocks
{
    public class HourBucket : IEnumerable<Hour>
    {
        private readonly Dictionary<int, Hour> _buckets;

        public Hour this[int offset]
        {
            get { return _buckets.GetOrElse(offset, null); }
        }

        public HourBucket()
        {
            _buckets = new Dictionary<int, Hour>();
        }

        public void Put(int offset, WeightedSeconds seconds)
        {
            if (offset < 0 || offset > 23)
            {
                throw new ArgumentException("The hour offset must be within 0 and 23, inclusive.");
            }

            //TODO: this is a filtering hack (bad)
            if (seconds.Seconds > 15*60 || seconds.Weight < 0.10) return;

            if (!_buckets.ContainsKey(offset))
            {
                _buckets.Add(offset, new Hour(offset));
            }

            //NOTE: if retrieval is slow, store reference at instantiation
            _buckets[offset].Add(seconds);
        }

        public IEnumerator<Hour> GetEnumerator()
        {
            return _buckets.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _buckets.Values.GetEnumerator();
        }
    }
}
