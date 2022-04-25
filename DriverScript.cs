/*********************************************************************************
 * About - This is the driver code to start the execution of Test Case/Test Suite
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 *********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using SR_Automation.Library;
using System.Diagnostics;
using System.Reflection;


namespace SR_Automation
{
    public class DriverScript
    {
        public DriverScript()
        {
        }

        public static void Main(string[] args)
        {
            //Initializing library
            ReportGenerator RG_Instance = new ReportGenerator();
            ExcelFunction EF_Instance = new ExcelFunction();
            ToolWrapper TW_Instance = new ToolWrapper();
            GenericFunction GF_Instance = new GenericFunction();
            GlobalVariable GV_Instance = new GlobalVariable();

            //Local variable
            int testPassNum = 0;
            int testFailNum = 0;
            int totalIteration = 0;
            string moduleName = null;
            string testClassName = null;
            string classMethodName = null;

            //Dictionary variable initialization
            Dictionary<string, int> passDic = new Dictionary<string, int>();
            Dictionary<string, int> failDic = new Dictionary<string, int>();

            //To prevent windows lock
            //Process.Start(GlobalVariable.ProjectPath + @"\WindowLockPrevent.vbs");
            //GlobalVariable.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(GlobalVariable.stepTimeout);

            //Kill process
            //GF_Instance.KillProcess("EXCEL");

            //Timer initialization
            Stopwatch suiteStopWatch = new Stopwatch();
            Stopwatch moduleStopWatch = new Stopwatch();

            try
            {
                //Reading testcases from suite file             
                string testCaseString = EF_Instance.ReadSuiteFile(GlobalVariable.suiteFilePath);
                string[] testCaseNames = testCaseString.Split('#');

                //Initializing HTML reporting
                RG_Instance.InitiateHtmlReporting();

                //Suite timer begins
                suiteStopWatch.Start();

                int arrayLength = testCaseNames.Length;
                GlobalVariable.testSkipCount = (GlobalVariable.testSkipCount + arrayLength);

                for (int i = 0; i < testCaseNames.Length; i++)
                {
                    moduleName = testCaseNames[i].Split(':')[0].Replace(" ", "");
                    testClassName = testCaseNames[i].Split(':')[1].Replace(" ", "");
                    classMethodName = testCaseNames[i].Split(':')[2].Replace(" ", "");
                    int iterationStart = Convert.ToInt16(testCaseNames[i].Split(':')[3]);
                    int iterationEnd = Convert.ToInt16(testCaseNames[i].Split(':')[4]);
                    string testDescription = testCaseNames[i].Split(':')[5];
                    totalIteration = (iterationEnd - iterationStart) + 1;

                    if (!passDic.ContainsKey(moduleName) && !classMethodName.ToLower().Contains("login") && !classMethodName.ToLower().Contains("logout"))
                    {
                        passDic[moduleName] = 0;
                        failDic[moduleName] = 0;
                    }

                    for (int z = iterationStart; z <= iterationEnd; z++)
                    {
                        GF_Instance.KillProcess("EXCEL");
                        RG_Instance.CreateTestReportFile(testClassName, classMethodName, z, testDescription);
                        GlobalVariable.currentIteration = z;
                        GlobalVariable.logFileName = testClassName;
                        GlobalVariable.testStepSkipStatus = false;
                        GlobalVariable.testCaseStatus = true;
                        moduleStopWatch.Start();

                        string temp = "SR_Automation.TestCase." + testClassName;
                        Assembly testAssembly = Assembly.GetExecutingAssembly();
                        Type classType = testAssembly.GetType(temp);
                        var classInstance = Activator.CreateInstance(classType, null);
                        classType.InvokeMember(classMethodName, BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public,
                                                                    null, classInstance, null);

                        //Module timer
                        moduleStopWatch.Stop();
                        TimeSpan moduleTimeSpan = moduleStopWatch.Elapsed;                        

                        if (testClassName.ToLower() != "" && !classMethodName.ToLower().Contains("login") && !classMethodName.ToLower().Contains("logout"))
                        {

                            if (GlobalVariable.skipDic[moduleName] > 0)
                            {
                                GlobalVariable.testSkipCount -= 1;
                                GlobalVariable.skipDic[moduleName] -= 1;
                            }

                            string testCaseName = testClassName + " -> " + classMethodName;
                            string testCaseStatus = GlobalVariable.testCaseStatus.ToString();
                            string testCaseTime = String.Format("{0:00}:{1:00}:{2:00}", moduleTimeSpan.Hours, moduleTimeSpan.Minutes, moduleTimeSpan.Seconds);
                            string testCaseIteration = Convert.ToString(z);
                            string testCaseFilePath = testClassName + "_" + classMethodName + "_" + z.ToString();
                            if (testCaseStatus.ToLower() == "true")
                            {
                                passDic[moduleName] += 1;
                                testPassNum++;
                                RG_Instance.AddNewSuiteLine(GlobalVariable.currentReportDirectory + @"\HtmlFiles\PassReport.html", testPassNum, testCaseName, testCaseStatus, testCaseTime, testCaseIteration, testCaseFilePath);
                            }
                            else
                            {
                                failDic[moduleName] += 1;
                                testFailNum++;
                                RG_Instance.AddNewSuiteLine(GlobalVariable.currentReportDirectory + @"\HtmlFiles\FailReport.html", testFailNum, testCaseName, testCaseStatus, testCaseTime, testCaseIteration, testCaseFilePath);
                            }
                        }

                        moduleStopWatch.Reset();

                        //Delete Temp files
                        GF_Instance.DeleteFilesInFolder(GlobalVariable.ProjectPath + @"\bin\Debug", ".png");
                    }
                }
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog("Exception occurred : " + e.Message.ToString(), "Fail", true);
                moduleStopWatch.Stop();
                TimeSpan moduleTimeSpan = moduleStopWatch.Elapsed;
                string testCaseTime = String.Format("{0:00}:{1:00}:{2:00}", moduleTimeSpan.Hours, moduleTimeSpan.Minutes, moduleTimeSpan.Seconds);
                RG_Instance.AddNewSuiteLine(GlobalVariable.currentReportDirectory + @"\HtmlFiles\SkipReport.html", 1, testClassName + " -> " + classMethodName, "Abort", testCaseTime, totalIteration.ToString(), testClassName + "_" + classMethodName + "_" + GlobalVariable.currentIteration);
            }
            finally
            {
                suiteStopWatch.Stop();
                TimeSpan suiteTimeSpan = suiteStopWatch.Elapsed;
                RG_Instance.UpdateTestSuiteFile(suiteTimeSpan, passDic, failDic, GlobalVariable.skipDic);

                //Kill process
                GF_Instance.KillProcess("wscript");
                GF_Instance.KillProcess("EXCEL");
                GF_Instance.KillProcess("iexplore");
                GF_Instance.KillProcess("chrome");
                GF_Instance.KillProcess("conhost");
                GF_Instance.KillProcess("chromedriver");
                GF_Instance.KillProcess("IEDriverServer");
                GF_Instance.KillProcess("MicrosoftEdge");
                GF_Instance.KillProcess("MicrosoftWebDriver");

                GlobalVariable.driver.Quit();
                GlobalVariable.driver.Dispose();

                //Destroy objects
                suiteStopWatch = null;
                moduleStopWatch = null;

                //To Send mail
                if (GlobalVariable.mailFlag.ToLower() == "y")
                    //GF_Instance.SendReportOnMail();

                //To open execution report file
                Process.Start(GlobalVariable.reportPath);

                //Delete Temp files
                GF_Instance.DeleteFilesInFolder(GlobalVariable.ProjectPath + @"\bin\Debug", ".png");
            }
        }

    }
}