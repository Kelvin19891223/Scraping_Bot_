using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCKLIB
{
    class CSVUtil
    {
        public static void dt2csv(DataTable dt, string strFilePath, bool header = true)
        {
            StringBuilder sb = new StringBuilder();

            if(header)
            {
                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));
            }

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field => field.ToString()).ToList();
                for(int i = 0; i < fields.Count; i ++)
                {
                    if (fields[i].Contains(","))
                        fields[i] = "\"" + fields[i] + "\"";
                }
                sb.AppendLine(string.Join(",", fields));
            }

            File.WriteAllText(strFilePath, sb.ToString());
        }
        public static DataTable csv2dt(string strFilePath)
        {
            try
            {
                DataTable dt = new DataTable();
                using (TextFieldParser parser = new TextFieldParser(strFilePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    bool first = true;
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (first)
                        {
                            first = false;
                            foreach (string field in fields)
                            {
                                dt.Columns.Add(field);
                            }
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < fields.Length; i++)
                            {
                                dr[i] = fields[i];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }

                foreach (DataColumn cl in dt.Columns)
                    cl.DataType = typeof(string);
                return dt;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }
    }
}
