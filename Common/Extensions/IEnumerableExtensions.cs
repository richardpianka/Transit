using System;
using System.Collections.Generic;

namespace Transit.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static IEnumerable<T> WithoutLast<T>(this IEnumerable<T> xs)
        {
            T lastX = default(T);
            bool first = true;

            foreach (T x in xs)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    yield return lastX;
                }
                lastX = x;
            }
        }
    }
}
