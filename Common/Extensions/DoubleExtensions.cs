using System;

namespace Transit.Common.Extensions
{
    public static class DoubleExtensions
    {
        public static double ToRadian(this double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}
