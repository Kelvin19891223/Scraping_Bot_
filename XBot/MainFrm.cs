using ExcelUtils;
using GPwdBot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using PCKLIB;
using Mono.Web;
using CsvHelper;
namespace XBot
{
    public partial class MainFrm : Form
    {
        public string m_main_csv;
        public DataTable m_main_dt;
        public bool isOpenType = false;
        public bool isOpenPostCode = false;
        
        public MainFrm()
        {
            InitializeComponent();
        }

        //open the business type url
        private void btn_open_main_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Text file";
                dlg.Filter = "Text files|*.txt";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();
                if (dlg.ShowDialog() == DialogResult.OK)
                {                    
                    txt_business_type.Text = dlg.FileName;                    
                }
            }
            catch(Exception ex)
            {
                MainApp.log_error("Can not open the business type. {ex.Message}");                
            }
        }

        //open the postcode
        private void btn_open_postcode_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Text file";
                dlg.Filter = "Text files|*.txt";
                dlg.InitialDirectory = Directory.GetCurrentDirectory();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txt_postcode.Text = dlg.FileName;                    
                }
            }
            catch (Exception ex)
            {
                MainApp.log_error("Can not open the postcode. {ex.Message}");
            }
        }

        public void setGrid(int index)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                var obj = grid_main.Rows[index].Cells[3].Value = "OK";                
            });
        }

        public void setCount()
        {
            this.InvokeOnUiThreadIfRequired(() =>
            { 
                remain.Text = String.Format("{0}", MainApp.count);
            });
        }

        public void setScrapCount()
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                scrap_count.Text = String.Format("{0}", MainApp.scrap_count + MainApp.tt_scrap_count);
            });
        }
        public void fillGrid()
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                try
                {
                    if (MainApp.start_type >= MainApp.bussinessTypesArray.Length)
                        return;
                    grid_main.Rows.Clear();


                    for (int i = 0; i < MainApp.postCodeArray.Length; i++)
                    {
                        if(i < MainApp.start_postcode)
                            grid_main.Rows.Add((i + 1 + MainApp.start_type * MainApp.postCodeArray.Length), MainApp.bussinessTypesArray[MainApp.start_type], MainApp.postCodeArray[i], "OK");
                        else
                            grid_main.Rows.Add((i + 1 + MainApp.start_type * MainApp.postCodeArray.Length), MainApp.bussinessTypesArray[MainApp.start_type], MainApp.postCodeArray[i]);
                        grid_main.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10);
                    }
                    //MainApp.start_type ++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Master XLS file format is not valid. {ex.Message}");
                }
            });
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (!checkTable())
                {
                    MessageBox.Show("Can not connect to the database");
                    return;
                }

                grid_main.Columns[1].HeaderText = "BusinessType";
                MainApp.scrap_count = MainApp.m_sql.GetCount(txt_table.Text);
                btn_startnbn.Enabled = false;
                btn_save.Enabled = false;
                btn_start.Enabled = false;
                btn_delete.Enabled = false;
                bool contFlag = chk_continue.Checked;
                MainApp.postCodeArray = System.IO.File.ReadAllLines(txt_postcode.Text);
                MainApp.bussinessTypesArray = System.IO.File.ReadAllLines(txt_business_type.Text);
                if(MainApp.postCodeArray == null || MainApp.postCodeArray.Length == 0 || MainApp.bussinessTypesArray == null || MainApp.bussinessTypesArray.Length == 0)
                {
                    MessageBox.Show("Can not read the postcode or businessType. Please select others.");
                    return;
                }

                if(!contFlag)
                {
                    MainApp.start_type = 0;
                    MainApp.start_postcode = 0;
                } else
                {
                    MainApp.start_type = MainApp.m_sql.GetCountType(txt_table.Text, "");
                    MainApp.start_type--;
                    if (MainApp.start_type < 0)
                        MainApp.start_type = 0;
                    MainApp.start_postcode = MainApp.m_sql.GetCountPostCode(txt_table.Text, String.Format(" where businesstypeinput = '{0}'",MainApp.bussinessTypesArray[MainApp.start_type]));
                }

                btn_startnbn.Enabled = true;
                btn_save.Enabled = true;
                btn_start.Enabled = true;
                btn_delete.Enabled = true;
            } catch(Exception ex)
            {
                MessageBox.Show("Can not read the postcode or businessType. Please select others.");
                return;
            }

            //set grid
            fillGrid();
            MainApp.log_info(DateTime.Now.ToString("yyyy-MM-dd HH:ii:ss") + " Start scraping");
            Thread t = new Thread(new ThreadStart(startScrap));
            t.Start();
            //start thread
            
        }        

        //start scraping
        public void startScrap()
        {
            GPwdBot.Google goo = new GPwdBot.Google(MainApp.startUrl, 1, txt_table.Text);
            goo.startWork();
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!checkTable())
            {
                MessageBox.Show("Can not connect to the database");
                return;
            }
            DataTable dt = MainApp.m_sql.GetDataTable(txt_table.Text, "");
            if(dt != null)
            {
                using (SaveFileDialog svdlg = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
                {
                    if(svdlg.ShowDialog() == DialogResult.OK)
                    {
                        CSVUtil.dt2csv(dt, svdlg.FileName);
                    }
                }
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (!checkTable())
            {
                MessageBox.Show("Can not connect to the database");
                return;
            }
            //start
            btn_startnbn.Enabled = false;
            btn_save.Enabled = false;
            btn_start.Enabled = false;
            btn_delete.Enabled = false;
            grid_main.Columns[1].HeaderText = "Address";            

            MainApp.totalCount = MainApp.m_sql.getCounts(txt_table.Text);
            MainApp.count = 0;

            Thread t = new Thread(new ThreadStart(startNbn));
            t.Start();            

            btn_startnbn.Enabled = true;
            btn_save.Enabled = true;
            btn_start.Enabled = true;
            btn_delete.Enabled = true;
        }

        public bool checkTable()
        {
            MainApp.m_sql.server = txt_server.Text;
            MainApp.m_sql.database = txt_database.Text;
            MainApp.m_sql.uid = txt_user.Text;
            MainApp.m_sql.password = txt_pass.Text;
            MainApp.m_sql.port = txt_port.Text;

            MainApp.m_sql.CreateConnection();
            
            if (MainApp.m_sql.is_connected() == false)
            {
                return false;
            }

            DataTable dt = MainApp.m_sql.ExecuteQuery("show tables");

            bool flag = false;
            if (dt == null || dt.Rows.Count == 0)
                flag = false;
            else
            {
                for(int i=0; i<dt.Rows.Count;i++)
                {
                    if (dt.Rows[i][0].ToString() == txt_table.Text)
                        flag = true;
                }
            }

            if(!flag)
            {
                MainApp.m_sql.ExecuteNonQuery(String.Format("CREATE TABLE `{0}` (`id` int(11) NOT NULL AUTO_INCREMENT,`businesstypeinput`  text,`postcodeinput`  text,`businessname`  text,`businesstype`  text,`address`  TEXT,`phone`  TEXT,`nbn`  TEXT,`url`  TEXT,  PRIMARY KEY(`id`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;", txt_table.Text));
            }
            return true;
        }

        public void startNbn()
        {
            GPwdBot.Google goo = new GPwdBot.Google("https://www.nbnco.com.au/residential/learn/rollout-map", 1, txt_table.Text);
            goo.startNbn();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (!checkTable())
            {
                MessageBox.Show("Can not connect to the database");
                return;
            }

            MainApp.m_sql.ExecuteNonQuery("drop table " + txt_table.Text);
        }

        public void refreshGridTable(DataTable dt)
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                grid_main.Rows.Clear();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {                        
                        grid_main.Rows.Add((i + 1 ), dt.Rows[i]["address"].ToString(), dt.Rows[i]["postcodeinput"].ToString(), "");                        
                        grid_main.Rows[i].DefaultCellStyle.Font = new Font("Segoe UI", 10);
                    }
                }
            });
        }

        public void setNBNCount()
        {
            this.InvokeOnUiThreadIfRequired(() =>
            {
                remain.Text = String.Format("{0}", MainApp.count);
            });
        }
    }
}
