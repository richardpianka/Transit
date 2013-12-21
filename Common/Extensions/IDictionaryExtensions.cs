using System.Collections.Generic;

namespace Transit.Common.Extensions
{
    public static class IDictionaryExtensions
    {
        public static TValue GetOrElse<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return defaultValue;
        }
    }
}
