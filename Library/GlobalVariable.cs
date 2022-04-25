/*************************************************************************************
 * About - This library contains all the varibles to be used throughout the framework
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 *************************************************************************************/

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SR_Automation.Library
{
    public class GlobalVariable
    {
        static ExcelFunction objEF = new ExcelFunction();
        public static string browserName = null;
        public static string mailFlag = null;
        public static string senderName = null;
        public static string receiverName = null;
        public static IWebDriver driver = null;
        public static string envName = null;
        public static int approverCount = 0;

        public static string currentUserDirectory = System.IO.Directory.GetCurrentDirectory().Split(new string[] { "\\bin\\Debug" }, StringSplitOptions.None)[0];
        public static string datetimestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
        public static string reportName = "Report" + datetimestamp + ".html";
        public static readonly string ProjectPath = currentUserDirectory;

        public static readonly string datapoolPath = ProjectPath + @"\DataPool";
        public static readonly string reportFolderPath = ProjectPath + @"\ExecutionReport";
        public static readonly string currentReportDirectory = ProjectPath + @"\ExecutionReport\" + datetimestamp;
        public static readonly string reportPath = currentReportDirectory + @"\Dashboard.html";
        public static readonly string zipReportName = reportFolderPath + @"\AutomationExecutionReport.zip";

        public static string dataFilePath = datapoolPath + @"\DataSheet\TestData.xls";
        public static string suiteFilePath = ProjectPath + @"\TestSuite\TestSuite.xls";
        public static string testReportPath = null;
        public static string logFileName = null;
        public static int currentIteration = 1;
        public static string dataSheetName = null;

        public static string elapsedTime = null;
        public static int totalCount = 0;
        public static int testPassCount = 0;
        public static int testFailCount = 0;
        public static int testSkipCount = 0;
        public static bool testCaseStatus = true;
        public static bool testStepStatus = true;
        public static bool testStepSkipStatus = false;
        public static int testStepCounter = 0;
        public static int stepTimeout = 10;
        public static Dictionary<string, int> skipDic = null;

        public GlobalVariable()
        {
            browserName = objEF.ReadValueFromExcel(GlobalVariable.suiteFilePath, "RunSetting", 1, "Browser Name");
            mailFlag = objEF.ReadValueFromExcel(GlobalVariable.suiteFilePath, "RunSetting", 1, "Send Mail");
            receiverName = objEF.ReadValueFromExcel(GlobalVariable.suiteFilePath, "RunSetting", 1, "Receiver Mail ID");
            string currentUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (browserName == "Chrome")
            {
                ChromeOptions CO = new ChromeOptions();
                CO.AddArguments("user-data-dir=" + currentUserProfile + @"\AppData\Local\Google\Chrome\User Data\Default");
                CO.AddArguments("--start-maximized");
                driver = new ChromeDriver(ProjectPath.ToString() + @"\packages\Selenium.WebDriver.ChromeDriver.99.0.4844.5100\driver\win32", CO, TimeSpan.FromMinutes(3));
                driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(90));
            }
            else if (browserName == "IE")
            {
                driver = new InternetExplorerDriver(ProjectPath.ToString() + @"\packages\Selenium.WebDriver.IEDriver.3.150.1\driver");
            }
            else if (browserName == "Edge")
            {
                Environment.SetEnvironmentVariable("webdriver.edge.driver", ProjectPath.ToString() + @"\packages\Selenium.WebDriver.MicrosoftWebDriver.10.0.17134\driver\msedgedriver.exe");
                driver = new EdgeDriver();
            }
            else if (browserName == "Firefox")
            {
                driver = new FirefoxDriver();
            }

        }
    }
}
