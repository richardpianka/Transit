using System.Collections.Generic;

namespace Transit.Common.Extensions
{
    public static class IntExtensions
    {
        public static IEnumerable<int> To(this int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }
    }
}
