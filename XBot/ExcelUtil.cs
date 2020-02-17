using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XlsFile;

namespace ExcelUtils
{
    class ExcelUtil
    {
        public static xlsf m_xls;
        public static DataTable ReadDataExcel(string filepath, string sheetname = "")
        {
            try
            {
                FileStream stream = File.Open(filepath, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader;

                string extension = System.IO.Path.GetExtension(filepath).ToLower();

                if (extension.Equals(".csv"))
                {
                    reader = ExcelReaderFactory.CreateCsvReader(stream);
                }
                else if (extension.Equals(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }

                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                };
                conf.UseColumnDataType = false;

                var dataSet = reader.AsDataSet(conf);
                DataTable dataTable;
                if (sheetname == "")
                    dataTable = dataSet.Tables[0];
                else
                    dataTable = dataSet.Tables[sheetname];
                stream.Close();
                return dataTable;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        public static void SaveDataExcel(DataTable dt, string filename, string sheetname)
        {
            try
            {
                m_xls = new xlsf();
                m_xls.NewFile();
                m_xls.SaveAs(filename);

                m_xls.NewSheet();
                m_xls.SelectSheet(1);
                string sh_name = sheetname;//.Replace(".", "");
                //m_xls.SetSheetName(sh_name);

                m_xls.Save();
                //m_xls.SelectSheet(sh_name);

                object[,] _records = new object[1, dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                    _records[0, j] = dt.Columns[j].ColumnName;
                Microsoft.Office.Interop.Excel.Worksheet gXlWs = (Microsoft.Office.Interop.Excel.Worksheet)m_xls.excelApp.ActiveWorkbook.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range range = gXlWs.get_Range("A1", IndexToColumn(dt.Columns.Count) + "1");
                range.Value2 = _records;

                // save it to m_xls
                object[,] records = new object[dt.Rows.Count, dt.Columns.Count];
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                        records[r, c] = "'" + dt.Rows[r][c].ToString();
                }

                string last = String.Format("{0}{1}", IndexToColumn(dt.Columns.Count), dt.Rows.Count + 1);
                range = gXlWs.get_Range("A2", last);
                range.Value2 = records;

                m_xls.Save();
                m_xls.CloseFile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                m_xls.CloseFile();
            }
        }

        const int ColumnBase = 26;
        const int DigitMax = 7; // ceil(log26(Int32.Max))
        const string Digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string IndexToColumn(int index)
        {
            if (index <= 0)
                throw new IndexOutOfRangeException("index must be a positive number");

            if (index <= ColumnBase)
                return Digits[index - 1].ToString();

            var sb = new StringBuilder().Append(' ', DigitMax);
            var current = index;
            var offset = DigitMax;
            while (current > 0)
            {
                sb[--offset] = Digits[--current % ColumnBase];
                current /= ColumnBase;
            }
            return sb.ToString(offset, DigitMax - offset);
        }
    }
}
