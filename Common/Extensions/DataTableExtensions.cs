using System;
using System.Data;

namespace Transit.Common.Extensions
{
    public static class DataTableExtensions
    {
        public static DataTable AddColumn(this DataTable table, string name, Type type = null)
        {
            type = type ?? typeof(string);
            table.Columns.Add(name, type);

            return table;
        }

        public static DataTable AddReadonlyColumn(this DataTable table, string name, Type type = null)
        {
            type = type ?? typeof(string);
            table.Columns.Add(name, type).ReadOnly = true;

            return table;
        }
    }
}
