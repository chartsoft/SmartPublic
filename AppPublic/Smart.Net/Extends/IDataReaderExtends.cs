using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// IDataReader扩展方法
    /// </summary>
    public static class DataReaderExtends
    {
        /// <summary>
        /// IDataReader转DataSet
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(this IDataReader reader)
        {
            var dataSet = new DataSet();
            do
            {
                var schemaTable = reader.GetSchemaTable();
                var dataTable = new DataTable();
                if (schemaTable != null)
                {
                    for (var i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        var dataRow = schemaTable.Rows[i];
                        var columnName = (string)dataRow["ColumnName"];
                        var column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }
                    dataSet.Tables.Add(dataTable);
                    while (reader.Read())
                    {
                        var dataRow = dataTable.NewRow();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            dataRow[i] = reader.GetValue(i);
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    var column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    var dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }

        /// <summary>
        /// IDataReader转dataTable
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IDataReader reader)
        {
            return reader.ToDataSet().Tables[0];
        }

        /// <summary>
        /// IDataReader转IEnumerable<DataTable/>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<DataTable> ToDataTables(this IDataReader reader)
        {
            var dataTables = reader.ToDataSet().Tables.Cast<DataTable>().ToList();
            return dataTables;
        }
    }
}
