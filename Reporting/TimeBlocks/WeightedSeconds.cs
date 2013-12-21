namespace Transit.Reporting.TimeBlocks
{
    public class WeightedSeconds
    {
        public readonly int Seconds;
        public readonly double Weight;

        public WeightedSeconds(int seconds, double weight)
        {
            Seconds = seconds;
            Weight = weight;
        }

        public double Scalar { get { return Seconds / Weight; } }
    }
}
