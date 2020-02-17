using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

namespace GPwdBot
{
    public class IGoogle
    {
        public int m_ID;
        public Guid m_guid;
        public object m_chr_data_dir = new object();
        public object m_selen_locker = new object();
        public string m_chr_user_data_dir = "";
        public string m_chr_extension_dir = Environment.CurrentDirectory + "\\ChromeExtension\\proxy helper";
        public string m_creat_time;
        public ChromeDriver Driver;
        public IJavaScriptExecutor m_js;
        public CookieContainer m_cookies;
        public bool m_screenshot;
        public System.Drawing.Point m_location = new System.Drawing.Point(0,0);
        public System.Drawing.Size m_size= new System.Drawing.Size(0, 0);
        object m_locker = new object();
        public bool m_incognito = false;
        public void DeleteCurrentChromeData()
        {
            try
            {
                Directory.Delete(m_chr_user_data_dir, true);
                return;
            }
            catch (Exception ex)
            {
                MainApp.log_info(ex.Message + "\n" + ex.StackTrace);
            }
        }

        public async Task<bool> Navigate(string target)
        {
            try
            {
            string url = Driver.Url;
            Driver.Navigate().GoToUrl(target);
            return await WaitUrlChange(url);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public void NewTab(string tabUrl)
        {
            lock (m_locker)
            {
                string newTabScript = "var d=document,a=d.createElement('a');"
                                + "a.target='_blank';a.href='{0}';"
                                + "a.innerHTML='new tab';"
                                + "d.body.appendChild(a);"
                                + "a.click();"
                                + "a.parentNode.removeChild(a);";
                if (String.IsNullOrEmpty(tabUrl))
                    tabUrl = "about:blank";

                m_js.ExecuteScript(String.Format(newTabScript, tabUrl));
            }
            //Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }
        public async Task<bool> Start()
        {
            lock (m_chr_data_dir)
            {
                m_guid = Guid.NewGuid();
                m_chr_user_data_dir = $"\\ChromeData\\checker_{Thread.CurrentThread.ManagedThreadId}" + m_guid.ToString();
                Directory.CreateDirectory(m_chr_user_data_dir);
            }

            try
            {
                ChromeDriverService defaultService = ChromeDriverService.CreateDefaultService();
                defaultService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();

                if(m_incognito)
                {
                    chromeOptions.AddArguments("--incognito");                    
                }

                chromeOptions.AddArgument("--system-developer-mode");
                chromeOptions.AddArgument("--no-first-run");
                //chromeOptions.AddArgument("--load-extension=" + m_chr_extension_dir);
                chromeOptions.AddArgument("--user-data-dir=" + m_chr_user_data_dir);
                string randomUserAgent = GetRandomUserAgent();
                chromeOptions.AddArgument(string.Format("--user-agent={0}", (object)randomUserAgent));
                //chromeOptions.AddUserProfilePreference("profile.managed_default_content_settings.images", 2);
                string chr_path;
                if (Environment.Is64BitOperatingSystem)
                    chr_path = "C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe";
                else
                    chr_path = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";

                if (!System.IO.File.Exists(chr_path))
                {
                    MainApp.log_info($"#{m_ID} - Not found chrome.exe. Perhaps the Google Chrome browser is not installed on this computer. The work of the transcriber is not possible.");                    
                    return false;
                }
                chromeOptions.BinaryLocation = chr_path;

                try
                {
                    Driver = new ChromeDriver(defaultService, chromeOptions);
                }
                catch (Exception ex)
                {
                    MainApp.log_info($"#{m_ID} - Fail to start chrome.exe. Please make sure any other chrome windows are not opened.");
                    return false;
                }

                m_js = (IJavaScriptExecutor)Driver;

                MainApp.log_info($"#{m_ID} - Successfully started.");

                //Driver.Manage().Window.Size = m_size;
                Driver.Manage().Window.Position = m_location;
                MainApp.log_info($"#{m_ID} - Cache and cookies are cleared. ");
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_info($"#{m_ID} - Failed to start. Exception:{ex.Message}");
                try
                {
                    Driver.Quit();
                }
                catch
                {
                    MainApp.log_info($"#{m_ID} - Failed to quit driver. Exception:{ex.Message}");
                }
                return false;
            }
        }
        public async Task<bool> WaitUrlChange(string url, int timeout = 7000)
        {
            try
            {
                Stopwatch wt = new Stopwatch();
                wt.Start();
                while (wt.ElapsedMilliseconds < timeout)
                {
                    if (Driver.Url != url)
                        return true;
                    await TaskDelay(100);
                }
            }
            catch (Exception ex)
            {
                MainApp.log_info($"#{m_ID} - Failed to wait for url change. Exception:{ex.Message}");
            }
            return false;
        }

        public async Task<bool> TrySelect(By list, By optionToVerify, string textToSelect, int timeout = 5000)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < timeout)
            {
                if (Driver.FindElement(optionToVerify).Text == textToSelect)
                    return true;
                Driver.FindElement(list).SendKeys(textToSelect[0].ToString());
                await TaskDelay(100) ;
            }
            return false;
        }

        public CookieContainer ConvertSeleniumCookieToCookieContainer(ICookieJar seleniumCookie)
        {
            CookieContainer cookieContainer = new CookieContainer();
            using (IEnumerator<OpenQA.Selenium.Cookie> enumerator = seleniumCookie.AllCookies.GetEnumerator())
            {
                while (((IEnumerator)enumerator).MoveNext())
                {
                    OpenQA.Selenium.Cookie current = enumerator.Current;
                    cookieContainer.Add(new Cookie(current.Name, current.Value, current.Path, current.Domain));
                }
            }
            return cookieContainer;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                Driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException ex)
            {
                return false;
            }
        }

        public async Task<bool> WaitToVisible(string xpath, int TimeOut = 1000)
        {
            return await WaitToVisible(By.XPath(xpath), TimeOut);
        }
        public async Task<bool> WaitToVisible(By by, int TimeOut = 1000)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < TimeOut)
            {
                if (await IsElementVisible(by))
                    return true;
                Thread.Sleep(100);
            }
            return false;
        }

        public async Task<bool> WaitToUnvisable(By by, int TimeOut = 1000)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < TimeOut)
            {
                try
                {
                    if (!await IsElementVisible(by))
                        return true;
                }
                catch
                {
                    return false;
                }
                await TaskDelay(100);
            }
            return false;
        }

        public async Task<bool> WaitToPresent(string xpath, int TimeOut = 1000)
        {
            return await WaitToPresent(By.XPath(xpath), TimeOut);
        }
        public async Task<bool> WaitToPresent(By by, int TimeOut = 5000)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            do
            {
                if (IsElementPresent(by))
                    return true;
                await Task.Delay(100);
            }
            while (wt.ElapsedMilliseconds < TimeOut);
            return false;
        }

        public async Task<By> WaitToPresent(List<By> by, int TimeOut = 1000)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < TimeOut)
            {
                using (List<By>.Enumerator enumerator = by.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        By current = enumerator.Current;
                        if (IsElementPresent(current))
                            return current;
                    }
                }
                await Task.Delay(100);
            }
            return null;
        }

        public async Task<bool> click_element(string xpath, int mode = 0, int timeout = 1000)
        {
            try
            {
                DateTime start = DateTime.Now;
                if (mode == 2) // move mouse on pixel
                {
                    string x_pos = "", y_pos = "";
                    int x = 0, y = 0;
                    while (x == 0 || y == 0)
                    {
                        //await Task.Run(() => { while (MainApp.g_paused) ; });
                        if (DateTime.Now.Subtract(start).TotalMilliseconds > timeout)
                        {
                            MainApp.log_info($"#{m_ID}: Mouse click on {xpath} cancelled due to timeout.");
                            return false;
                        }
                        try
                        {
                            string script_x =
                                    "(function()" +
                                        "{" +
                                            "bodyRect = document.body.getBoundingClientRect();" +
                                            "el = document.evaluate('" + xpath + "', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                                            "el.style.setProperty('display', '');" +
                                            "elemRect = el.getBoundingClientRect();" +
                                            "return elemRect.left - bodyRect.left;" +
                                    //"return elemRect.left;" +
                                    "})()";
                            x_pos = m_js.ExecuteScript(script_x).ToString();
                            x = (int)float.Parse(x_pos);
                            string script_y =
                                    "(function()" +
                                        "{" +
                                            "bodyRect = document.body.getBoundingClientRect();" +
                                            "el = document.evaluate('" + xpath + "', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                                            "el.style.setProperty('display', '');" +
                                            "elemRect = el.getBoundingClientRect();" +
                                            "return elemRect.top - bodyRect.top;" +
                                        //"return elemRect.top;" +
                                        "})()";
                            y_pos = m_js.ExecuteScript(script_y).ToString();
                            y = (int)float.Parse(y_pos);
                        }
                        catch (Exception ex)
                        {
                            MainApp.log_info($"#{m_ID}: Error occured while trying mouse click on {xpath}. {ex.Message}");
                            x = 0;
                            y = 0;
                        }
                    }

                    x += 1;
                    y += 1;

                    // Not implemented yet. Need further action.
                    var hnd = get_handle();
                    if(hnd != IntPtr.Zero)
                    {
                        //ClickOnPointTool.ClickOnPoint(hnd, new System.Drawing.Point(x, y));
                        Thread.Sleep(100);
                    }

                    return true;
                }
                else if (mode == 1)
                {
                    if (await WaitToPresent(xpath) == false)
                    {
                        MainApp.log_info($"#{m_ID}: Node not found. Mouse event(H+D+U) not sent to ({xpath})");
                        return false;
                    }
                    string script = @"(function(x) {
                    var el = document.evaluate('" + xpath + @"', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                    let hoverEvent = document.createEvent ('MouseEvents');
                    hoverEvent.initEvent ('mouseover', true, true);
                    el.dispatchEvent (hoverEvent);

                    let downEvent = document.createEvent ('MouseEvents');
                    downEvent.initEvent ('mousedown', true, true);
                    el.dispatchEvent (downEvent);

                    let upEvent = document.createEvent ('MouseEvents');
                    upEvent.initEvent ('mouseup', true, true);
                    el.dispatchEvent (upEvent);

                    let clickEvent = document.createEvent ('MouseEvents');
                    clickEvent.initEvent ('click', true, true);
                    el.dispatchEvent (clickEvent);
                    })();";

                    m_js.ExecuteScript(script);
                    Thread.Sleep(100);
                    //MainApp.log_info($"#{m_ID}: Mouse event(H+D+U) sent to ({xpath})");
                    return true;
                }
                else if (mode == 0)
                {
                    m_js.ExecuteScript($"document.evaluate('{xpath}', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click()");
                    Thread.Sleep(100);
                    //MainApp.log_info($"#{m_ID}: Javascript click function called {xpath}");
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                MainApp.log_info($"#{m_ID}: Click element failed. {xpath}. {ex.Message}");
                return false;
            }
        }

        public IntPtr get_handle()
        {
            string title = String.Format("{0} - Mozilla Firefox", Driver.Title);
            var process = Process.GetProcesses()
                .FirstOrDefault(x => x.MainWindowTitle == title);
            if (process != null)
            {
                return process.MainWindowHandle;
            }
            return IntPtr.Zero;
        }
        public async Task<bool> TryClick_All(string xpath)
        {
            bool ret = await TryClick(xpath, 0) || await TryClick(xpath, 1);
            if (ret == false)
            {
                try {
                    m_js.ExecuteAsyncScript($"document.evaluate('{xpath}', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click()");
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                }
            }
            if(ret == false)
                MainApp.log_info($"{m_ID} : Clicking all ways failed. {xpath}");
            return ret;
        }
        public async Task<bool> TryClick(string xpath, int mode = 0, int delay = 100)
        {
            try
            {
                await TryClick(By.XPath(xpath), mode);
                await TaskDelay(delay);
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
        public async Task<bool> TryClick(By by, int mode = 0)
        {
            try
            {
                if (mode == 0)
                {
                    Driver.ExecuteScript("arguments[0].click('');", ((RemoteWebDriver)Driver).FindElement(by));
                }
                else if (mode == 1)
                {
                    Driver.FindElement(by).Click();
                }
                else
                {
                    Actions action = new Actions(Driver);
                    action.MoveToElement(Driver.FindElement(by)).Perform();
                    action.Click(Driver.FindElement(by)).Perform();
                }
                return true;
            }
            catch (Exception) {

            }
            return false;
        }

        public async Task<bool> TryEnterText(string xpath, string textToEnter, string atributeToEdit = "value", int TimeOut = 10000, bool manualyEnter = false)
        {
            return await TryEnterText(By.XPath(xpath), textToEnter, atributeToEdit, TimeOut, manualyEnter);
        }
        public async Task<bool> TryEnterText(By by, string textToEnter, string atributeToEdit = "value", int TimeOut = 10000, bool manualyEnter = false)
        {
            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < TimeOut)
            {
                try
                {
                    if (IsElementPresent(by) && await IsElementVisible(by))
                    {
                        Driver.FindElement(by).SendKeys((string)OpenQA.Selenium.Keys.Control + "a");
                        if (manualyEnter)
                            Driver.FindElement(by).SendKeys(textToEnter);
                        else
                            Driver.ExecuteScript($"arguments[0].value = '{textToEnter}';", ((RemoteWebDriver) Driver).FindElement(by));

                        for (int index = 0; index < 11; ++index)
                        {
                            if ((string)Driver.ExecuteScript("return arguments[0].value;", Driver.FindElement(by)) == textToEnter)
                            {
                                return true;
                            }
                            await TaskDelay(100);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MainApp.log_info($"#{m_ID} - Failed to enter text. Exception:{ex.Message}");
                    return false;
                }
                await Task.Delay(100);
            }
            return false;
        }

        public async Task<bool> TryClickAndWait(By toClick, By toWait, int mode = 0, int TimeOut = 10000)
        {
            if (!await WaitToPresent(toClick, 3000))
            {
                MainApp.log_info($"#{m_ID} - Element to be clicked is not present! mode:{mode} By: {toClick}");
                return false;
            }

            Stopwatch wt = new Stopwatch();
            wt.Start();
            while (wt.ElapsedMilliseconds < TimeOut)
            {
                try
                {
                    if(mode == 1)
                    {
                        string script = @"(function(x) {
                            var el = document.evaluate('" + toClick + @"', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
                            let hoverEvent = document.createEvent ('MouseEvents');
                            hoverEvent.initEvent ('mouseover', true, true);
                            el.dispatchEvent (hoverEvent);

                            let downEvent = document.createEvent ('MouseEvents');
                            downEvent.initEvent ('mousedown', true, true);
                            el.dispatchEvent (downEvent);

                            let upEvent = document.createEvent ('MouseEvents');
                            upEvent.initEvent ('mouseup', true, true);
                            el.dispatchEvent (upEvent);

                            let clickEvent = document.createEvent ('MouseEvents');
                            clickEvent.initEvent ('click', true, true);
                            el.dispatchEvent (clickEvent);
                            })();";
                        Driver.ExecuteScript(script);
                        if (!await WaitToPresent(toWait) || !await WaitToVisible(toWait))
                        {
                            MainApp.log_info($"#{m_ID} - Click failed for waiting! mode:{mode} By: {toClick}");
                            return false;
                        }
                        MainApp.log_info($"#{m_ID} - Click success! mode:{mode} By: {toClick}");
                        return true;
                    }
                    else if (mode == 0)
                    {
                        Driver.ExecuteScript("arguments[0].click('');", Driver.FindElement(toClick));
                        if (!await WaitToPresent(toWait) || !await WaitToVisible(toWait))
                        {
                            MainApp.log_info($"#{m_ID} - Click failed for waiting! mode:{mode} By: {toClick}");
                            return false;
                        }

                        MainApp.log_info($"#{m_ID} - Click success! mode:{mode} By: {toClick}");
                        return true;
                    }
                    else if (mode == 2)
                    {
                        Driver.FindElement(toClick).Click();
                        if (!await WaitToPresent(toWait) || !await WaitToVisible(toWait))
                        {
                            MainApp.log_info($"#{m_ID} - Click failed for waiting! mode:{mode} By: {toClick}");
                            return false;
                        }

                        MainApp.log_info($"#{m_ID} - Click success! mode:{mode} By: {toClick}");
                        return true;
                    }
                    else if (mode == 3)
                    {
                        Actions action = new Actions(Driver);
                        action.MoveToElement(Driver.FindElement(toClick)).Perform();
                        action.Click(Driver.FindElement(toClick)).Perform();
                        if (!await WaitToPresent(toWait) || !await WaitToVisible(toWait))
                        {
                            MainApp.log_info($"#{m_ID} - Click failed for waiting! mode:{mode} By: {toClick}");
                            return false;
                        }
                        MainApp.log_info($"#{m_ID} - Click success! mode:{mode} By: {toClick}");
                        return true;
                    }
                }
                catch
                {
                    
                }
            }
            MainApp.log_info($"#{m_ID} - Click failed for waiting! mode:{mode} By: {toClick}");
            return false;
        }

        public async Task<bool> IsElementVisible(By by, int timeout = 0)
        {
            try
            {
                Stopwatch wt = new Stopwatch();
                wt.Start();
                do
                {
                    if (IsElementVisible(Driver.FindElement(by)))
                        return true;
                    await TaskDelay(100);
                } while (wt.ElapsedMilliseconds < timeout);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public void Quit()
        {
            Driver.Quit();
            Driver.Dispose();
            DeleteCurrentChromeData();
        }

        public IGoogle()
        {
        }

        public void SaveScreenshot(string screenshotName)
        {
            if (!m_screenshot)
                return;
            string path = $"screenshots/{m_ID}/screens/{Thread.CurrentThread.ManagedThreadId}_{m_creat_time}/";
            Directory.CreateDirectory(path);
            Driver.GetScreenshot().SaveAsFile(path + screenshotName);
        }
        

        public static string GetRandomUserAgent()
        {
            string[] strArray = new string[9]
            {
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
                "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36",
                "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36 OPR/43.0.2442.991",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 YaBrowser/17.10.0.2052 Yowser/2.5 Safari/537.36",
                "Mozilla/5.0 (Windows NT 5.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36 OPR/43.0.2442.991",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36"
            };
            return strArray[new Random().Next(0, strArray.Length)];
        }

        public async Task TaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

        public Size GetScreenSize()
        {
            Size[] sizeArray = new Size[]
            {
                new Size(800, 600),
            };
            return sizeArray[new Random().Next(0, sizeArray.Length)];
        }
    }
}
