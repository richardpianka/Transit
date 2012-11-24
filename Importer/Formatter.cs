using System.Globalization;
using System.Threading;

namespace Transit.Importer
{
    public static class Formatter
    {
        public static string CleanColumn(string column)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            string columnClean = column;

            if (column.Contains(" ") || column.Contains("_"))
            {
                string noUnderScores = column.Replace("_", " ");
                string lowerCase = noUnderScores.ToLower();
                string titleCase = textInfo.ToTitleCase(lowerCase);
                columnClean = titleCase.Replace(" ", "");
            }
            else if (column.ToLower().Equals(column))
            {
                columnClean = textInfo.ToTitleCase(column);
            }
            return columnClean;
        }
    }
}
