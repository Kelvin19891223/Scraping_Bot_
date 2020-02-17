using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XBot
{
    static class MainApp
    {
        public static int first = 0;
        public static System.Object g_locker = new object();
        public static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static MainFrm m_main_frm = null;

        public static bool g_stopped = false;
        public static bool g_paused = false;

        public static int start_type = 0;
        public static int start_postcode = 0;

        public static string startUrl = "https://www.yellowpages.com.au";
        public static string[] bussinessTypesArray;
        public static string[] postCodeArray;

        public static MySQLWrapper m_sql = new MySQLWrapper();
        public static string dbName = "db.db";
        public static int count = 0;
        public static int totalCount = 0;
        public static int scrap_count = 0;
        public static int tt_scrap_count = 0;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false); 

            m_main_frm = new MainFrm();
            Application.Run(m_main_frm);
        }

        public static void createTable()
        {
            m_sql.ExecuteNonQuery("CREATE TABLE \"scrap\" (\"id\"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\"businesstypeinput\"  TEXT,\"postcodeinput\"  TEXT,\"businessname\"  TEXT,\"businesstype\"  TEXT,\"address\"  TEXT,\"phone\"  TEXT,\"nbn\"  TEXT,\"url\"  TEXT);");
            m_sql.ExecuteNonQuery("CREATE TABLE sqlite_sequence(name,seq);");
            m_sql.ExecuteNonQuery("INSERT INTO \"main\".\"sqlite_sequence\" VALUES ('scrap', null);");
        }
        public static void log_info(string msg)
        {
            lock (g_locker)
            {
                try
                {
                    logger.Info(msg);
                    if (m_main_frm != null)
                    {
                        string fname = "Log_shown.txt";
                        while (file_writable(fname) == false) ;
                        File.AppendAllLines(fname, new string[] { DateTime.Now.ToString("HH:mm:ss ") + msg });
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void log_error(string msg, bool msgbox = false)
        {
            lock (g_locker)
            {
                try
                {
                    logger.Error(msg);
                    if (m_main_frm != null)
                    {
                        string fname = "Log_shown.txt";
                        while (file_writable(fname) == false) ;
                        File.AppendAllLines(fname, new string[] { DateTime.Now.ToString("HH:mm:ss ") + msg });

                        if (msgbox)
                            MessageBox.Show(msg);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        

        public static bool file_writable(string file)
        {
            try
            {
                using (Stream stream = new FileStream(file, FileMode.Append))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
