using System;
using System.Collections;
using System.Collections.Generic;

namespace Transit.Reporting.TimeBlocks
{
    public sealed class Hour : IEnumerable<WeightedSeconds>
    {
        public readonly int Offset;

        private readonly List<WeightedSeconds> _reads;

        private bool _dirty;

        private double _average;
        public double Average
        {
            get
            {
                Calculate();
                return _average;
            }
        }

        private double _standardDeviation;
        public double StandardDeviation
        {
            get
            {
                Calculate();
                return _standardDeviation;
            }
        }

        private double _max = Double.MinValue;
        public double Max
        {
            get
            {
                Calculate();
                return _max;
            }
        }

        private double _min = Double.MaxValue;
        public double Min
        {
            get
            {
                Calculate();
                return _min;
            }
        }

        public Hour(int offset)
        {
            Offset = offset;
            _reads = new List<WeightedSeconds>();
        }

        public void Add(WeightedSeconds seconds)
        {
            _dirty = true;
            _reads.Add(seconds);
        }

        private void Calculate()
        {
            if (!_dirty) return;
            _dirty = false;

            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (WeightedSeconds weightedValue in _reads)
            {
                double value = weightedValue.Scalar;

                if (value > _max) _max = value;
                if (value < _min) _min = value;

                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }

            _average = M;
            _standardDeviation = Math.Sqrt(S / (k - 1));
        }

        public IEnumerator<WeightedSeconds> GetEnumerator()
        {
            return _reads.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
