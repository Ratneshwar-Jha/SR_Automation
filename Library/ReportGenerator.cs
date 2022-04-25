/*******************************************************************************
 * About - This library contains all the functions related to reporting and logs
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenQA.Selenium;

namespace SR_Automation.Library
{
    class ReportGenerator
    {
        public void InitiateHtmlReporting()
        {
            try
            {
                string colorBoxPath = GlobalVariable.currentReportDirectory + @"\Colorbox";
                string imagesPath = GlobalVariable.currentReportDirectory + @"\Images";
                string snapFolderPath = GlobalVariable.currentReportDirectory + @"\Snapshot";
                string logFolderPath = GlobalVariable.currentReportDirectory + @"\TextLog";
                string htmlFileFolderPath = GlobalVariable.currentReportDirectory + @"\HtmlFiles";

                System.IO.Directory.CreateDirectory(colorBoxPath);
                System.IO.Directory.CreateDirectory(imagesPath);
                System.IO.Directory.CreateDirectory(snapFolderPath);
                System.IO.Directory.CreateDirectory(logFolderPath);
                System.IO.Directory.CreateDirectory(htmlFileFolderPath);

                CopyFolderContents(GlobalVariable.datapoolPath + @"\ReportTemplate\Colorbox", colorBoxPath);
                CopyFolderContents(GlobalVariable.datapoolPath + @"\ReportTemplate\Images", imagesPath);

                string suiteFile = GlobalVariable.currentReportDirectory + @"\Dashboard.html";
                System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Dashboard.txt", suiteFile, true);

                string passReportFile = GlobalVariable.currentReportDirectory + @"\HtmlFiles\PassReport.html";
                System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Suite.txt", passReportFile, true);

                string failReportFile = GlobalVariable.currentReportDirectory + @"\HtmlFiles\FailReport.html";
                System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Suite.txt", failReportFile, true);

                string skipReportFile = GlobalVariable.currentReportDirectory + @"\HtmlFiles\SkipReport.html";
                System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Suite.txt", skipReportFile, true);

                string headerText = "<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0'><td width='10%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Serial #</td><td width='50%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>TestCase Name</td><td width='10%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Iteration #</td><td width='15%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Execution Time</td><td width='15%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Status</td></table></td></tr>";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(passReportFile, true))
                {
                    file.WriteLine(headerText);
                    file.Close();
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(failReportFile, true))
                {
                    file.WriteLine(headerText);
                    file.Close();
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(skipReportFile, true))
                {
                    file.WriteLine(headerText);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        private bool CopyFolderContents(string SourcePath, string DestinationPath)
        {
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }
                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
                return false;
            }
        }

        public void AddNewSuiteLine(string suiteFile, int serialNum, string testCaseName, string status, string executionTime, string count, string testFile)
        {
            try
            {
                string suiteLineText = null;

                if (status.ToLower() == "true")
                {
                    GlobalVariable.testPassCount += 1;
                    suiteLineText = "<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><td width='10%' align='center' style='color:#000000;'>" + serialNum.ToString() + "</td><td width='50%' align='center' style='color:#000000;'><a href='./" + testFile + ".html" + "'><font color='#0000FF'>" + testCaseName + "</font></a></td><td width='10%' align='center' style='color:#000000;'>" + count + "</td><td width='15%' align='center' style='color:#000000;'>" + executionTime + "</td><td width='15%' bgcolor='#FFFFFF' align='center' style='color:#80b849;'><b>" + "Passed" + "</b></td></table></td></tr>";
                }
                else if (status.ToLower() == "false")
                {
                    GlobalVariable.testFailCount += 1;
                    suiteLineText = "<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><td width='10%' align='center' style='color:#000000;'>" + serialNum.ToString() + "</td><td width='50%' align='center' style='color:#000000;'><a href ='./" + testFile + ".html" + "'><font color='#0000FF'>" + testCaseName + "</font></a></td><td width='10%' align='center' style='color:#000000;'>" + count + "</td><td width='15%' align='center' style='color:#000000;'>" + executionTime + "</td><td width='15%' bgcolor='#FFFFFF' align='center' style='color:#d23039;'><b>" + "Failed" + "</b></td></table></td></tr>";
                }
                else
                {
                    if (GlobalVariable.testSkipCount > 0)
                    {
                        suiteLineText = "<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0' bgcolor='#FFFFFF'><td width='10%' align='center' style='color:#000000;'>" + serialNum.ToString() + "</td><td width='50%' align='center' style='color:#000000;'><a href ='./" + testFile + ".html" + "'><font color='#0000FF'>" + testCaseName + "</font></a></td><td width='10%' align='center' style='color:#000000;'>" + count + "</td><td width='15%' align='center' style='color:#000000;'>" + executionTime + "</td><td width='15%' bgcolor='#FFFFFF' align='center' style='color:#FF8C00;'><b>" + "Aborted" + "</b></td></table></td></tr>";
                    }
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(suiteFile, true))
                {
                    file.WriteLine(suiteLineText);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        public void UpdateTestSuiteFile(TimeSpan ts, Dictionary<string, int> passDict, Dictionary<string, int> failDict, Dictionary<string, int> skipDict)
        {
            try
            {
                GlobalVariable.totalCount = GlobalVariable.testPassCount + GlobalVariable.testFailCount + GlobalVariable.testSkipCount;
                string systemDate = System.DateTime.Now.ToString("dd-MMM-yyyy, hh:mm tt");
                ReplaceTextInFile("{TCUNT}", GlobalVariable.reportPath, GlobalVariable.totalCount.ToString());
                ReplaceTextInFile("{PASS}", GlobalVariable.reportPath, GlobalVariable.testPassCount.ToString());
                ReplaceTextInFile("{FAIL}", GlobalVariable.reportPath, GlobalVariable.testFailCount.ToString());
                ReplaceTextInFile("{Date}", GlobalVariable.reportPath, systemDate);
                ReplaceTextInFile("{SKIPPED}", GlobalVariable.reportPath, Convert.ToString(GlobalVariable.testSkipCount));
                ReplaceTextInFile("{BrowserName}", GlobalVariable.reportPath, GlobalVariable.browserName);
                ReplaceTextInFile("{EnvUrl}", GlobalVariable.reportPath, GlobalVariable.envName);

                //Update elapsed time as a TimeSpan value.
                GlobalVariable.elapsedTime = String.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
                ReplaceTextInFile("{ExecutionTime}", GlobalVariable.reportPath, GlobalVariable.elapsedTime);

                int i = 0, j = 0;
                string detailedMatrix = string.Empty;
                for (i = 0; i < passDict.Count; i++)
                {
                    detailedMatrix += "[\"" + passDict.Keys.ElementAt(i).ToString() + "\", " + passDict.Values.ElementAt(i) + ", " + failDict.Values.ElementAt(i) + ", " + skipDict.Values.ElementAt(i) + "], ";
                }
                for (j = i; j < skipDict.Count; j++)
                {
                    detailedMatrix += "[\"" + skipDict.Keys.ElementAt(j).ToString() + "\", 0, 0," + skipDict.Values.ElementAt(j) + "], ";
                }
                ReplaceTextInFile("{DetailedMatrix}", GlobalVariable.reportPath, detailedMatrix);

                //Update footer section
                AddFooter(GlobalVariable.reportPath);               
                
            }
            catch (Exception e) { Console.Out.WriteLine(e.ToString()); }
        }

        private void ReplaceTextInFile(string searchText, string filePath, string replaceText)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                string content = reader.ReadToEnd();
                reader.Close();
                content = Regex.Replace(content, searchText, replaceText);
                StreamWriter writer = new StreamWriter(filePath);
                writer.Write(content);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        private void AddFooter(string suiteFile)
        {
            try
            {
                string footerText;
                footerText = "<tr><td width='1000' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'> Copyright &copy; 2018 HCL Technologies Ltd. All Rights Reserved.</td></tr></table></body></html>";
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(suiteFile, true))
                {
                    file.WriteLine(footerText);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        public void OpenHtmlFile(string fileToOpen)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Verb = "RunAs";
                startInfo.Arguments = fileToOpen;
                startInfo.ErrorDialog = true;
                Process.Start(startInfo);
            }
            catch (Exception e) { Console.Out.WriteLine(e.ToString()); }
        }

        public void CreateTestReportFile(string testName, string methodName, int iteration, string testDescription)
        {
            GlobalVariable.testStepCounter = 0;

            string testFile = GlobalVariable.currentReportDirectory + @"\HtmlFiles\" + testName + "_" + methodName + "_" + iteration.ToString() + ".html";
            if (System.IO.File.Exists(testFile) == false)
            {
                System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Test.txt", testFile, true);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(testFile, true))
            {
                file.WriteLine("<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0'>");
                file.WriteLine("<tr><td valign='top' background='../Images/bg1.jpg' bgcolor='#E6F3FC'>");
                file.WriteLine("<table width='972' border='0' cellpadding='0' cellspacing='0'><tr><td align='center' valign='top' class='headings'>");
                file.WriteLine("<span align='center' style='color:#fff;'>" + methodName + "</span><br/>");
                file.WriteLine("<span align='center' style='color:#fff;'>" + testDescription + "</span><br/></td></tr></table></td></tr>");
                file.WriteLine("<tr><td><table width='100%' border='1' cellspacing='0' cellpadding='0'>");
                file.WriteLine("<td width='8%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Serial #</td>");
                file.WriteLine("<td width='48%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Test Step</td>");
                file.WriteLine("<td width='6%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Status</td>");
                file.WriteLine("<td width='6%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>SnapShot</td>");
                file.WriteLine("<td width='7%' align='center' bgcolor='#A6AAAB' style='color:#FFFFFF;'>Detailed Log</td>");
                file.WriteLine("</table></td></tr>");
            }
            GlobalVariable.testReportPath = testFile;
        }

        public void ReportStepLog(string logMsg, string passFailStatus, bool snapshotFlag)
        {
            string snapFilePath = string.Empty;
            GlobalVariable.testStepCounter += 1;

            try
            {
                if (snapshotFlag == true)
                {
                    string snapshotName = "screenshot_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".png";
                    string screenshotFilePath = Path.Combine(GlobalVariable.currentReportDirectory + @"\Snapshot", snapshotName);
                    Screenshot scrShot = ((ITakesScreenshot)GlobalVariable.driver).GetScreenshot();
                    scrShot.SaveAsFile(screenshotFilePath);
                    snapFilePath = "<a href='../Snapshot/" + snapshotName + "'>" + "<font color='#FF8C00'>Snap</font></a>";
                }
                else
                {
                    snapFilePath = "No Snap";
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(GlobalVariable.testReportPath, true))
                {
                    if (passFailStatus.ToLower() == "info")
                    {
                        GlobalVariable.testStepCounter -= 1;
                        file.WriteLine("<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        file.WriteLine("<td width='100%' align='center' bgcolor='#FFFFFF' style='color:#0000FF;'><b>" + logMsg + "</b></td>");
                    }
                    else
                    {
                        file.WriteLine("<tr><td><table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        file.WriteLine("<td width='8%' align='center' bgcolor='#FFFFFF' style='color:#000000;'>" + GlobalVariable.testStepCounter + "</td>");
                        file.WriteLine("<td width='48%' align='left' bgcolor='#FFFFFF' style='color:#000000;'>" + logMsg + "</td>");

                        if (passFailStatus.ToLower() == "pass")
                        {
                            file.WriteLine("<td width='6%' bgcolor='#FFFFDC' valign='middle' align='center'><b><font color='#32cd32' face='Tahoma' size='2'>Pass</font></b></td>");
                        }
                        else if (passFailStatus.ToLower() == "warn")
                        {
                            GlobalVariable.testCaseStatus = false;
                            file.WriteLine("<td width='6%' bgcolor='#FFFFDC' valign='middle' align='center'><b><font color='#FF8C00' face='Tahoma' size='2'>Warn</font></b></td>");
                        }
                        else if (passFailStatus.ToLower() == "done")
                        {
                            file.WriteLine("<td width='6%' bgcolor='#FFFFDC' valign='middle' align='center'><b><font color='#6D7F92' face='Tahoma' size='2'>Done</font></b></td>");
                        }
                        else
                        {
                            GlobalVariable.testCaseStatus = false;
                            file.WriteLine("<td width='6%' bgcolor='#FFFFDC' valign='middle' align='center'><b><font color='Red' face='Tahoma' size='2'>Fail</font></b></td>");
                        }
                        file.WriteLine("<td width='6%' align='center' bgcolor='#FFFFFF' style='color:#000000;'>" + snapFilePath + "</td>");
                        file.WriteLine("<td width='7%' align='center' bgcolor='#FFFFFF' style='color:#000000;'>" + "<a href='../TextLog/" + GlobalVariable.logFileName + ".txt" + "'>" + "<font color='#FF8C00'>View</font></a>" + "</td>");
                    }
                    file.WriteLine("</table></td></tr>");
                    file.Close();

                    UpdateLogFile(logMsg);
                }
            }
            catch (Exception e)
            { Console.Out.WriteLine(e.ToString()); }
        }

        public void UpdateLogFile(string logMsg)
        {
            try
            {
                string filepath = GlobalVariable.currentReportDirectory + @"\TextLog\" + GlobalVariable.logFileName + ".txt";
                if (!System.IO.File.Exists(filepath))
                {
                    System.IO.File.Copy(GlobalVariable.datapoolPath + @"\ReportTemplate\Log.txt", filepath, true);
                }

                using (System.IO.StreamWriter logFile = new System.IO.StreamWriter(filepath, true))
                {
                    logFile.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "       " + logMsg.Replace("<b>", "").Replace("</b>", ""));
                    logFile.Close();
                }
            }
            catch (Exception e) { Console.Out.WriteLine(e.ToString()); }
        }
        
        private string FormatTextString(string textString)
        {
            string newTextString = null;

            for (int i = 0; i < textString.Length; i++)
            {
                char letter = textString[i];
                if (newTextString == null)
                    newTextString = letter.ToString();
                else
                {
                    if (char.IsUpper(letter) && !char.IsUpper(textString[i - 1]))
                        newTextString += " " + letter.ToString();
                    else
                        newTextString += letter.ToString();
                }
            }
            return newTextString;
        }

    }
}