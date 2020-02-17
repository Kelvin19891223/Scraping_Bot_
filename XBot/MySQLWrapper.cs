using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBot
{
    class MySQLWrapper
    {
        private System.Object locker = new System.Object();
        public MySqlConnection sql_con;
        public MySqlCommand sql_cmd;

        public string server;
        public string database;
        public string uid;
        public string password;
        public string port;

        public bool log = false;


        public MySQLWrapper() { }

        public void OutLog(string str)
        {
            if (log == false)
                return;
            using (StreamWriter writer = new StreamWriter("mysql_log.txt", true))
            {
                writer.WriteLine(str);
            }
        }
        public bool ExecuteNonQuery(string txtQuery)
        {
            try
            {
                lock (locker)
                {
                    CloseConnection();
                    OutLog("\t\t#CENTRAL# Execute MySQL NonQuery: " + txtQuery);
                    OpenConnection();
                    sql_cmd = sql_con.CreateCommand();
                    sql_cmd.CommandText = txtQuery;
                    sql_cmd.ExecuteNonQuery();
                    CloseConnection();
                    return true;
                }
            }
            catch (Exception ex)
            {
                OutLog("Database query failed : " + ex.Message);
                return false;
            }
        }

        public DataTable ExecuteQuery(string txtQuery)
        {
            try
            {
                lock (locker)
                {
                    CloseConnection();
                    OpenConnection();
                    DataTable dt = new DataTable();
                    sql_cmd = sql_con.CreateCommand();
                    MySqlDataAdapter DB = new MySqlDataAdapter(txtQuery, sql_con);
                    DB.SelectCommand.CommandType = CommandType.Text;
                    DB.Fill(dt);
                    CloseConnection();

                    OutLog("\t\t#CENTRAL# Execute MySQL Query: " + txtQuery + " -> " + dt.Rows.Count.ToString());
                    return dt;
                }
            }
            catch (Exception ex)
            {
                OutLog("Database query failed : " + ex.Message);
                return null;
            }
        }

        public void CreateConnection()
        {
            OutLog(">>Create MySQL connection");
            string connectionString;
            if (port == "")
                connectionString = String.Format("server = {0}; user id = {1}; password ={2}; persistsecurityinfo = True; database ={3}; SslMode = none; Convert Zero Datetime=True;",
                                                server, uid, password, database);
            else
                connectionString = String.Format("server = {0}; user id = {1}; password ={2}; persistsecurityinfo = True; port ={3}; database ={4}; SslMode = none; Convert Zero Datetime=True;",
                                                server, uid, password, port, database);
            sql_con = new MySqlConnection(connectionString);
            OutLog("<<Create MySQL connection");
        }

        public static int GetExceptionNumber(MySqlException my)
        {
            if (my != null)
            {
                int number = my.Number;
                //if the number is zero, try to get the number of the inner exception
                if (number == 0 && (my = my.InnerException as MySqlException) != null)
                {
                    number = my.Number;
                }
                return number;
            }
            return -1;
        }
        public bool OpenConnection()
        {
            try
            {
                sql_con.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                OutLog(sql_con.ConnectionString);
                switch (GetExceptionNumber(ex))
                {
                    case 0:
                        OutLog("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        OutLog("Invalid username/password, please try again");
                        break;
                }
                OutLog(ex.Message);
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                if(sql_con != null)
                    sql_con.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                OutLog(ex.Message);
                return false;
            }
        }

        public bool is_connected()
        {
            lock (locker)
            {
                CloseConnection();
                bool ret = OpenConnection();
                if (ret)
                    CloseConnection();
                return ret;
            }
        }

        //public void CreateFile(string file_name)
        //{
        //    SQLiteConnection.CreateFile(file_name);
        //}

        public void SaveDataTable(DataTable DT, string tbl_name = "")
        {
            try
            {
                sql_cmd = sql_con.CreateCommand();
                if (tbl_name == "")
                    tbl_name = DT.TableName;
                if (tbl_name == "")
                    return;

                sql_cmd.CommandText = string.Format("SELECT * FROM {0}", tbl_name);
                var adapter = new MySqlDataAdapter(sql_cmd);
                
                adapter.AcceptChangesDuringFill = true;
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                adapter.Update(DT);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.{ex.Message}");
            }
        }


        public DataTable GetDataTable(string tablename, string where)
        {
            try
            {
                DataTable DT = new DataTable();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = string.Format("SELECT * FROM {0} {1}", tablename, where);
                var adapter = new MySqlDataAdapter(sql_cmd);
                adapter.AcceptChangesDuringFill = false;
                adapter.Fill(DT);

                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }
                return DT;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.\n {ex.Message}");
            }
            return null;
        }

        public int GetCountType(string tablename, string where)
        {
            try
            {
                DataTable DT = new DataTable();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = string.Format("SELECT count(DISTINCT(businesstypeinput)) as cnt FROM {0} {1}", tablename, where);
                var adapter = new MySqlDataAdapter(sql_cmd);
                adapter.AcceptChangesDuringFill = false;
                adapter.Fill(DT);

                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }

                if (DT != null)
                    return Convert.ToInt32(DT.Rows[0]["cnt"].ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.\n {ex.Message}");
                return 0;
            }
            return 0;
        }

        public int GetCountPostCode(string tablename, string where)
        {
            try
            {
                DataTable DT = new DataTable();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = string.Format("SELECT count(DISTINCT(postcodeinput)) as cnt FROM {0} {1}", tablename, where);
                var adapter = new MySqlDataAdapter(sql_cmd);
                adapter.AcceptChangesDuringFill = false;
                adapter.Fill(DT);

                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }

                if (DT != null)
                    return Convert.ToInt32(DT.Rows[0]["cnt"].ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.\n {ex.Message}");
                return 0;
            }
            return 0;
        }

        public int GetCount(string tablename)
        {
            try
            {
                DataTable DT = new DataTable();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = string.Format("SELECT count(*) as cnt FROM {0}", tablename);
                var adapter = new MySqlDataAdapter(sql_cmd);
                adapter.AcceptChangesDuringFill = false;
                adapter.Fill(DT);

                DT.TableName = tablename;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }

                if (DT != null)
                    return Convert.ToInt32(DT.Rows[0]["cnt"].ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.\n {ex.Message}");
                return 0;
            }
            return 0;
        }

        public void deleteData(String tableName, String where)
        {
            string query = string.Format("delete from {0} {1}", tableName, where);
            this.ExecuteNonQuery(query);
        }

        public void updateNBN(String nbn, String table, String address)
        {
            String query = "";
            if(address.IndexOf("\"") > -1)
                query = string.Format("update {2} set nbn='{0}' where address='{1}'", nbn, address, table);
            else
                query = string.Format("update {2} set nbn='{0}' where address=\"{1}\"", nbn, address, table);
            this.ExecuteNonQuery(query);
        }

        public int getCounts(string table)
        {
            try
            {
                DataTable DT = new DataTable();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = string.Format("SELECT count(*) as cnt FROM {0} where nbn=''", table);
                var adapter = new MySqlDataAdapter(sql_cmd);
                adapter.AcceptChangesDuringFill = false;
                adapter.Fill(DT);

                DT.TableName = table;
                foreach (DataRow row in DT.Rows)
                {
                    row.AcceptChanges();
                }

                if (DT != null)
                    return Convert.ToInt32(DT.Rows[0]["cnt"].ToString());
                return 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to execute Mysql query.\n {ex.Message}");
                return 0;
            }
            return 0;
        }
    }        
}
