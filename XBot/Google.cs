using ExcelUtils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using XBot;
using Cookie = System.Net.Cookie;
using Size = System.Drawing.Size;
using PCKLIB;
using Mono.Web;

namespace GPwdBot
{
    public class Google : IGoogle
    {
        private string table_namme;
        private string m_initialAddress = "";        
        int interval = 15;
        System.Windows.Forms.Timer m_timer = new System.Windows.Forms.Timer();
        public Google(String startUrl, int interv, string _table_namme)
        {
            table_namme = _table_namme;
            m_timer = new System.Windows.Forms.Timer();
            m_initialAddress = startUrl;            
            interval = interv;
            m_timer.Start();
        }

        public void finishWork(String msg)
        {            
            try
            {      
                Quit();
                m_timer.Stop();
                MainApp.log_info(DateTime.Now.ToString("yyyy-MM-dd HH:ii:ss")+msg);
            }
            catch (Exception ex)
            {                
                MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
            }
        }
        public async void startNbn()
        {
            try
            {
                //Open browser
                if (!await Start())
                {
                    finishWork("Chrome Browser can not run");
                    return;
                }

                await Navigate(m_initialAddress);
                Driver.SwitchTo().DefaultContent();

                //start scraping
                
                while(true)
                {
                    DataTable dt = MainApp.m_sql.GetDataTable(table_namme, " where nbn='' group by address limit 0,50");
                    if (dt == null || dt.Rows.Count == 0)
                        break;
                    MainApp.m_main_frm.refreshGridTable(dt);
                    await ScrapNbn(dt);
                }                

            } catch (Exception ex)
            {
                MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
            }
            finishWork("Finish");
        }

        public async Task<bool> ScrapNbn(DataTable dt)
        {
            if(dt != null)
            {
                for (int i = 0; i <dt.Rows.Count; i++)
                {
                    await Navigate("https://www.nbnco.com.au/residential/learn/rollout-map");

                    String address = dt.Rows[i]["address"].ToString();
                    //String id = dt.Rows[i]["id"].ToString();
                    String nbn = "Ready to Connect";
                    address = address.Replace("\"", "");
                    if (address != null && address != "")
                    {
                        
                        await TryEnterText("//*/div[@id='mapContentDesktop']/*/input[@class='map-search-input']", address + "\n", "value", 3000, true);
                        //Driver.FindElementByXPath().SendKeys(address + '\n');


                        
                        if (Driver.Url == "https://www.nbnco.com.au/missingaddress")
                            nbn = "Error Address";
                        else
                        {
                            try
                            {
                                Driver.FindElements(By.XPath("//*/div[@class='nearby-addresses-list-holder']/ul/li/a"))[0].Click();
                                nbn = Driver.FindElements(By.XPath("//*/div[@class='media-body media-middle']"))[0].Text;
                                nbn = nbn.Substring(nbn.IndexOf("\n") + 1, nbn.Substring(nbn.IndexOf("\n") + 1).IndexOf("\n")).Replace("\r", "");
                            }
                            catch (Exception ex)
                            {
                                nbn = "Ready to Connect";
                                try
                                {
                                    nbn = Driver.FindElements(By.XPath("//*/div[@class='media-body media-middle']"))[0].Text;


                                    if (nbn.IndexOf("Contact a phone or internet provider and order an nbn™ powered plan.") > 0)
                                        nbn = "Ready to connect";
                                    else if (nbn.IndexOf("Technology used") > 0)
                                        nbn = "Ready to connect";
                                    else if (nbn.IndexOf("Find out more") > 0)
                                        nbn = "Ready to connect";
                                    else
                                    {
                                        nbn = nbn.Replace("\r\nThere’s still work to do before your premises is ready to connect.\r\n", "");
                                        
                                        nbn = nbn.Replace("\r\nThere’s still work to do before your premises is ready to connect.\r\n", "");
                                        nbn = nbn.Substring(nbn.IndexOf("\n") + 1, nbn.Substring(nbn.IndexOf("\n") + 1).IndexOf("\n")).Replace("\r", "");
                                        

                                        if(nbn.IndexOf("We expect to have more information about the availability and technology in your area soon")>=0)
                                        {
                                            nbn = "Not Ready";
                                        }

                                        if(nbn.IndexOf("There’s still work to do before your premises is ready to connect. This work may take approximately 6 to 12 months")>0)
                                        {
                                            nbn = "More work required";
                                        }

                                        if (nbn.IndexOf("For specific information on how to connect your premises contact the existing provider.") > 0)
                                        {
                                            nbn = "Pivit Pty Ltd delivers a fibre network to this area.";
                                        }
                                    }
                                        

                                }
                                catch (Exception eex)
                                {
                                    nbn = "Existing provider delivers";
                                }

                            }
                        }
                        
                    } else
                    {
                        nbn = "Empty Address";
                    }
                    MainApp.m_main_frm.setGrid(i);
                    MainApp.m_sql.updateNBN( nbn, table_namme, address);
                    MainApp.count = MainApp.m_sql.getCounts(table_namme);
                    MainApp.m_main_frm.setNBNCount();                    
                }
            }

            return true;
        }

        public async void startWork()
        {
            try
            {
                //Open browser
                if (!await Start())
                {
                    finishWork("Chrome Browser can not run");
                    return;
                }

                await Navigate(m_initialAddress);
                Driver.SwitchTo().DefaultContent();

                while (await WaitToPresent("//*/form[@name='captcha']"))
                {
                    if (await solve_captcha_manual("//*/form[@name='captcha']", "//*/button[text()='Submit']") == false)
                    {
                        finishWork("User did not solve captcha.");
                    }
                    await TaskDelay(1000);
                }

                //start scraping
                for (int i = MainApp.start_type; i < MainApp.bussinessTypesArray.Length; i++)
                {
                    int start = 0;
                    start = MainApp.start_postcode;
                    if (i != MainApp.start_type)
                        start = 0;
                    for (int k = start; k < MainApp.postCodeArray.Length; k++)
                    {
                        //start scraping
                        //MainApp.log_info(DateTime.Now.ToString("yyyy-MM-dd HH:ii:ss") + "Start Scraping");
                        //MainApp.log_info(DateTime.Now.ToString("yyyy-MM-dd HH:ii:ss") + String.Format("Business Type: {0}, PostCode: {1}",MainApp.bussinessTypesArray[i], MainApp.postCodeArray[k]));
                        int count = 0;
                        DataTable dt = MainApp.m_sql.GetDataTable(table_namme, String.Format(" where businesstypeinput='{0}' and postcodeinput='{1}'", MainApp.bussinessTypesArray[i].Replace("'", "\'"), MainApp.postCodeArray[k]));
                        if (dt != null && dt.Rows.Count != 0)
                            count = dt.Rows.Count;

                        if (count == 0)
                            await getData(MainApp.bussinessTypesArray[i], MainApp.postCodeArray[k]);
                        MainApp.count++;

                        try
                        {
                            MainApp.m_main_frm.setCount();
                            MainApp.m_main_frm.setGrid(k);
                        }
                        catch (Exception ex) { };
                    }
                    MainApp.start_type = i + 1;
                    try
                    {
                        MainApp.m_main_frm.fillGrid();
                    }
                    catch (Exception ex) { }
                }


            }
            catch (Exception ex)
            {
                MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
            }
            finishWork("Finish");
        }

        public async Task<bool> getData(String type, String postcode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("businesstypeinput"));
            dt.Columns.Add(new DataColumn("postcodeinput"));
            dt.Columns.Add(new DataColumn("businessname"));
            dt.Columns.Add(new DataColumn("businesstype"));
            dt.Columns.Add(new DataColumn("address"));
            dt.Columns.Add(new DataColumn("phone"));
            dt.Columns.Add(new DataColumn("url"));
            dt.Columns.Add(new DataColumn("nbn"));
            try
            {
                String type1 = HttpUtility.UrlEncode(type);
                await Navigate(String.Format("https://www.yellowpages.com.au/search/listings?clue={0}&locationClue={1}", type1, postcode));
                Driver.SwitchTo().DefaultContent();

                if (Driver.Url.IndexOf("https://www.yellowpages.com.au/dataprotection") >= 0)
                {
                    while (await WaitToPresent("//*/form[@name='captcha']"))
                    {
                        if (await solve_captcha_manual("//*/form[@name='captcha']", "//*/button[text()='Submit']") == false)
                        {
                            finishWork("User did not solve captcha.");
                        }
                        await TaskDelay(1000);
                    }
                }


                foreach (IWebElement item in Driver.FindElementsByClassName("search-contact-card"))
                {
                    String businessName = "";
                    String businessType = "";
                    String address = "";
                    String phone = "";
                    try
                    {
                        businessName = item.FindElement(By.ClassName("listing-name")).Text;
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        businessType = item.FindElement(By.ClassName("listing-heading")).FindElement(By.TagName("a")).Text;
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        address = item.FindElement(By.ClassName("poi-and-body")).FindElement(By.ClassName("body")).FindElement(By.TagName("p")).Text;
                    }
                    catch (Exception ex)
                    {

                    }

                    String url = "";

                    try
                    {
                        int count = item.FindElements(By.ClassName("call-to-action")).Count;
                        for (int k = 0; k < count; k++)
                        {
                            if (item.FindElements(By.ClassName("call-to-action"))[k].FindElement(By.TagName("a")).Text == "Website")
                                url = item.FindElements(By.ClassName("call-to-action"))[k].FindElement(By.TagName("a")).GetAttribute("href");
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                    try
                    {
                        phone = item.FindElements(By.ClassName("call-to-action"))[0].FindElement(By.TagName("a")).Text;
                        if (phone == "Request Quote")
                            phone = item.FindElements(By.ClassName("call-to-action"))[1].FindElement(By.TagName("a")).Text;

                        if (phone == "Send Email")
                            phone = "";
                    }
                    catch (Exception ex)
                    {

                    }

                    DataRow workRow = dt.NewRow();
                    workRow["businesstypeinput"] = type;
                    workRow["postcodeinput"] = postcode;
                    workRow["businessname"] = businessName;
                    workRow["businesstype"] = businessType;
                    workRow["address"] = address;
                    workRow["phone"] = phone;
                    workRow["url"] = url;
                    workRow["nbn"] = "";

                    dt.Rows.Add(workRow);
                    MainApp.tt_scrap_count++;
                    MainApp.m_main_frm.setScrapCount();
                    //workRow["nbn"] = nbn;
                    //Save database
                    //MainApp.m_sql.saveParameter(type, postcode, businessName, businessType, address, phone, url);
                }

                MainApp.m_sql.SaveDataTable(dt, table_namme);
            }
            catch (Exception ex)
            {
                MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
            }
            return true;
        }
        //public async void startWork()
        //{
        //    try
        //    {
        //        //Open browser
        //        if (!await Start())
        //        {
        //            finishWork("Chrome Browser can not run");
        //            return;
        //        }

        //        await Navigate(m_initialAddress);
        //        Driver.SwitchTo().DefaultContent();

        //        while (await WaitToPresent("//*/form[@name='captcha']"))
        //        {
        //            if (await solve_captcha_manual("//*/form[@name='captcha']", "//*/button[text()='Submit']") == false)
        //            {
        //                finishWork("User did not solve captcha.");
        //            }
        //            await TaskDelay(1000);
        //        }

        //        while (true)
        //        {
        //            DataTable dt = MainApp.m_sql.GetDataTable(table_namme, " where phone = 'Request Quote' limit 1");
        //            if (dt == null || dt.Rows.Count == 0)
        //                break;

        //            await getData(dt.Rows[0]["businesstypeinput"].ToString(), dt.Rows[0]["postcodeinput"].ToString(), dt.Rows[0]["address"].ToString(), dt.Rows[0]["businessname"].ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
        //    }
        //    finishWork("Finish");
        //}
        //fix
        //public async Task<bool> getData(String type, String postcode, String addresstype, String businessname)
        //{

        //    try
        //    {
        //        String type1 = HttpUtility.UrlEncode(type);
        //        await Navigate(String.Format("https://www.yellowpages.com.au/search/listings?clue={0}&locationClue={1}", type1, postcode));
        //        Driver.SwitchTo().DefaultContent();

        //        if (Driver.Url.IndexOf("https://www.yellowpages.com.au/dataprotection") >= 0)
        //        {
        //            while (await WaitToPresent("//*/form[@name='captcha']"))
        //            {
        //                if (await solve_captcha_manual("//*/form[@name='captcha']", "//*/button[text()='Submit']") == false)
        //                {
        //                    finishWork("User did not solve captcha.");
        //                }
        //                await TaskDelay(1000);
        //            }
        //        }


        //        foreach (IWebElement item in Driver.FindElementsByClassName("search-contact-card"))
        //        {
        //            String businessName = "";
        //            String businessType = "";
        //            String address = "";
        //            String phone = "";

        //            try
        //            {
        //                address = item.FindElement(By.ClassName("poi-and-body")).FindElement(By.ClassName("body")).FindElement(By.TagName("p")).Text;
        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //            if(addresstype == address)
        //            {
        //                try
        //                {
        //                    phone = item.FindElements(By.ClassName("call-to-action"))[0].FindElement(By.TagName("a")).Text;
        //                    if (phone == "Request Quote")
        //                        phone = item.FindElements(By.ClassName("call-to-action"))[1].FindElement(By.TagName("a")).Text;

        //                    if (phone == "Send Email")
        //                        phone = "";

        //                    MainApp.m_sql.ExecuteNonQuery(String.Format("update scrap set phone='{0}' where businessname=\"{1}\" and phone='Request Quote'", phone, businessname));
        //                }
        //                catch (Exception ex)
        //                {

        //                }
        //            }                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
        //    }
        //    return true;
        //}

        async Task<bool> wait_for_user_action(string xpath_container, string message, string xpath_submit)
        {
            try
            {
                if (await WaitToPresent(xpath_container) == false)
                    return true;

                string js = @"
                    var element = document.createElement('input');
                    element.id = 'note_btn';
                    element.type = 'button';
                    element.setAttribute('solved','false'); 
                    element.style.backgroundColor='tomato';
                    element.style.color='white';
                    element.style.height='40px';
                    element.onclick = function() { 
                        this.style.backgroundColor='green';
                    };
                    element.setAttribute('solved','false');
                    element.value ='" + message + @"';
                    x=document.evaluate(" + $"\"{xpath_container}\"" + @", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    x.appendChild(element);
                    y=document.evaluate(" + $"\"{xpath_submit}\"" + @", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    y.onclick = function(){element.setAttribute('solved','true');x.removeChild(element);};
                    ";
                m_js.ExecuteScript(js);

                Stopwatch w = new Stopwatch();
                w.Reset();
                w.Start();
                while (MainApp.g_stopped == false)
                {
                    try
                    {
                        if (w.ElapsedMilliseconds > 300000)
                        {
                            MainApp.log_error($" TimeOut waiting for user action.");
                            return false;
                        }
                        var elem = Driver.FindElementById("note_btn");
                        if (elem == null)
                            break;
                        string value = elem.GetAttribute("solved");
                        if (value == "true")
                            break;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        async Task<bool> solve_captcha_manual(string xpath_container, string xpath_submit, string message = "The bot is waiting for you to solve captcha!")
        {
            try
            {
                if (await WaitToPresent(xpath_container) == false)
                    return true;
                if (xpath_submit != "")
                {
                    string js = @"
                        var element = document.createElement('input');
                        element.id = 'captcha_btn';
                        element.type = 'button';
                        element.setAttribute('solved','false'); 
                        element.style.backgroundColor='tomato';
                        element.style.color='white';
                        element.style.height='50px';
                        element.onclick = function() { 
                            this.style.backgroundColor='green';
                        };
                        element.setAttribute('solved','false');
                        element.value ='" + message + @"';
                        x=document.evaluate(" + $"\"{xpath_container}\"" + @", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                        x.appendChild(element);
                        y=document.evaluate(" + $"\"{xpath_submit}\"" + @", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                        y.onclick = function(){element.setAttribute('solved','true');x.removeChild(element);};
                        ";
                    m_js.ExecuteScript(js);

                    Stopwatch w = new Stopwatch();
                    w.Reset();
                    w.Start();
                    while (MainApp.g_stopped == false)
                    {
                        try
                        {
                            if (w.ElapsedMilliseconds > 300000)
                            {
                                MainApp.log_error($" TimeOut in solving captcha.");
                                return false;
                            }
                            var elem = Driver.FindElementById("captcha_btn");
                            if (elem == null)
                                break;
                            string value = elem.GetAttribute("solved");
                            if (value == "true")
                                break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    return true;
                }
                else
                {
                    string js = @"
                        var element = document.createElement('input');
                        element.id = 'captcha_btn';
                        element.type = 'button';
                        element.setAttribute('solved','false'); 
                        element.style.backgroundColor='red';
                        element.style.color='white';
                        element.style.height='80px';
                        element.onclick = function() { 
                            this.style.backgroundColor='green';
                        };
                        element.setAttribute('solved','false');
                        element.value ='The bot is waiting for you to solve captcha!';
                        x=document.evaluate(" + $"\"{xpath_container}\"" + @", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                        x.appendChild(element);
                        ";
                    m_js.ExecuteScript(js);

                    Stopwatch w = new Stopwatch();
                    w.Reset();
                    w.Start();
                    while (MainApp.g_stopped == false)
                    {
                        try
                        {
                            if (w.ElapsedMilliseconds > 300000)
                            {
                                MainApp.log_error($"TimeOut in solving captcha.");
                                return false;
                            }
                            var elem = Driver.FindElementById("captcha_btn");
                            if (elem == null)
                                break;
                            string value = elem.GetAttribute("solved");
                            if (value == "true")
                                break;
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
