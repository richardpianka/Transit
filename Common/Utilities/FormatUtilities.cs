using System;
using System.Text;

namespace Transit.Common.Utilities
{
    public static class FormatUtilities
    {
        public static string Duration(long ticks)
        {
            TimeSpan span = new TimeSpan(ticks * 10000);

            if (span.TotalSeconds == 0)
            {
                return "0s";
            }

            StringBuilder builder = new StringBuilder();

            Append(builder, span.Days, "d");
            Append(builder, span.Hours, "h");
            Append(builder, span.Minutes, "m");
            Append(builder, span.Seconds, "s");

            return builder.ToString().Trim();
        }

        private static void Append(StringBuilder builder, int value, string label)
        {
            if (value == 0) return;

            builder.Append(value);
            builder.Append(label);
            builder.Append(" ");
        }
    }
}
