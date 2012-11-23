using System.Collections.Generic;
using Transit.Common;
using Transit.Common.Model;

namespace Transit.Reader
{
    public interface IReader
    {
        IEnumerable<Route> Routes { get; }
        IEnumerable<Shape> Shapes { get; }
        IEnumerable<Stop> Stops { get; }

        IEnumerable<Capture> Captures { get; }
    }
}
