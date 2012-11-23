namespace Transit.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string left, string right)
        {
            return left.ToUpper().Equals(right.ToUpper());
        }

        public static string IfEmpty(this string value, string defaultValue)
        {
            return value.Length == 0 ? defaultValue : value;
        }
    }
}
