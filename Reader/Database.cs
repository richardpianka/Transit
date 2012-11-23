using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using Transit.Common.Extensions;

namespace Transit.Reader
{
    public static class Database
    {
        private static SqlConnection _connection;
            
        public static SqlConnection GetDb()
        {
            if (_connection == null) {
                _connection = new SqlConnection(ConfigurationManager.AppSettings["connection"]);
                _connection.Open();
            }

            return _connection;
        }

        public static DataTable SelectQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, GetDb());
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            return table;
        }

        public static DataTable StoredProcedure(string name, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand("GetGpsCapture", GetDb());
            command.CommandType = CommandType.StoredProcedure;

            parameters.ForEach(x => command.Parameters.Add(x));

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());

            return table;
        }

        public static void TruncateTable(string table)
        {
            string query = string.Concat("TRUNCATE TABLE ", table);
            new SqlCommand(query, GetDb()).ExecuteNonQuery();
        }

        public static object ExtractValue(DataTable table, string column, List<string> fileColumns, string[] values)
        {
            int index = fileColumns.IndexOf(column);
            object value = values[index];

            if (table.Columns[column].DataType.Name.Equals("Boolean"))
            {
                value = (value.ToString().Equals("Y") || value.ToString().Equals("1")) ? true : value;
                value = (value.ToString().Equals("N") || value.ToString().Equals("0")) ? false : value;
            }
            else if (table.Columns[column].DataType.Name.Equals("Int32"))
            {
                value = Convert.ToInt32(value);
            }
            else if (table.Columns[column].DataType.Name.Equals("Decimal"))
            {
                value = Convert.ToDecimal(value);
            }

            return value;
        }

        public static DataTable MakeTable(string sqlTable)
        {
            string query = string.Format("SET FMTONLY ON; SELECT * FROM {0}; SET FMTONLY OFF;", sqlTable);
            SqlCommand fillSchema = new SqlCommand(query, GetDb());
            DataTable table = new DataTable();
            table.Load(fillSchema.ExecuteReader());
            return table;
        }

        public static void WriteToTable(string table, DataTable data)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(GetDb()))
            {
                bulkCopy.DestinationTableName = table;
                bulkCopy.WriteToServer(data);
            }
        }

        public static bool HasBeenImported(string fileName)
        {
            string query = string.Format("SELECT COUNT(0) FROM dbo.Imported WHERE FileName = '{0}'", fileName);
            SqlCommand command = new SqlCommand(query, GetDb());
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private static void SetFileImported(string fileName)
        {
            string query = string.Format("INSERT INTO dbo.Imported VALUES('{0}', GETDATE())", fileName);
            SqlCommand command = new SqlCommand(query, GetDb());
            command.ExecuteNonQuery();
        }

        public static void ImportFile(string fileName, IEnumerable<string> columns, IEnumerable<string> lines, string sqlTable, IEnumerable<string> expectedColumns)
        {
            DataTable table = MakeTable(sqlTable);

            foreach (string line in lines)
            {
                try
                {
                    string[] values = line.Split(',');
                    DataRow row = table.NewRow();

                    foreach (string column in expectedColumns)
                    {
                        object value = ExtractValue(table, column, columns.ToList(), values);
                        row[column] = value;
                    }

                    table.Rows.Add(row);
                } catch (Exception) { }
            }

            WriteToTable(sqlTable, table);
            SetFileImported(fileName);
        }
    }
}
