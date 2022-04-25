/*******************************************************************************
 * About - This library contains all the functions related to general operations
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.Library;
//using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO.Compression;
using System.IO;

namespace SR_Automation.Library
{
    class GenericFunction
    {
        //Initializing classes
        //ExcelFunction EF_Instance = new ExcelFunction();

        public void KillProcess(string _processName)
        {
            try
            {
                foreach (Process clsProcess in Process.GetProcesses())
                    if (clsProcess.ProcessName.ToLower().Equals(_processName.ToLower()))
                    {
                        clsProcess.Kill();
                        clsProcess.Refresh();
                    }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message.ToString());
            }
        }

        //public void SendReportOnMail()
        //{
        //    try
        //    {
        //        //Delete existing zip file if any
        //        if (File.Exists(GlobalVariable.zipReportName))
        //            File.Delete(GlobalVariable.zipReportName);

        //        //Create zip file of execution report folder
        //        ZipFile.CreateFromDirectory(GlobalVariable.currentReportDirectory, GlobalVariable.zipReportName, CompressionLevel.Fastest, false);

        //        //Create the Outlook application.
        //        Outlook.Application oApp = new Outlook.Application();
        //        // Create a new mail item.
        //        Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
        //        oMsg.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;

        //        // Add the body of the email // 
        //        string bodyMsg = "<div style= 'float:left; color:#808080; background-color:#FFFFFF'>Hello, <br><br> Scheduled execution has been completed successfully.</div><br>";
        //        string statusMsg = "<tr><td colspan='2' align='center'><div style= 'float:Center; color:#FFFFFF; border:solid 2px #ccc; background-color:#0041C2;'><strong>Automation Execution Status</strong></div></td></tr>";
        //        string exeDate = "<tr><td width='75%' style= 'float:left; color:#0041C2;'>Execution Date: <font color='#616D7E'>" + System.DateTime.Now.ToString("d.M.yyyy") + "</font></td><td style = 'float:left; color:#000000;'>TC Scheduled: " + GlobalVariable.totalCount + "</td></tr>";
        //        int tcSkipCount = GlobalVariable.totalCount - (GlobalVariable.testPassCount + GlobalVariable.testFailCount);
        //        string exeTime = "<tr><td width='75%' style= 'float:left; color:#0041C2;'>Execution Time: <font color='#616D7E'>" + GlobalVariable.elapsedTime + "</font></td><td align='left' style= 'float:left; color:#0041C2;'>TC Skipped: " + Convert.ToString(tcSkipCount) + "</td></tr>";
        //        string exeBrw = "<tr><td width='75%' style= 'float:left; color:#0041C2;'>Browser Used: <font color='#616D7E'>" + GlobalVariable.browserName + "</font></td><td style = 'float:left; color:#80b849;'>TC Passed: " + GlobalVariable.testPassCount + "</td></tr>";
        //        string tcSkip = "<tr><td width='75%' style= 'float:left; color:#0041C2;'>Environment: <font color='#616D7E'>" + GlobalVariable.envName + "</font></td><td style = 'float:left; color:#d23039;'>TC Failed: " + GlobalVariable.testFailCount + "</td></tr>";
        //        string psScript = "<tr><td colspan='2'><br><br><div style= 'float:left; color:#808080; background-color:#FFFFFF'>Please download attachment to see detailed execution report !!!</div></td></tr>";

        //        oMsg.HTMLBody = "<html><body>"+ bodyMsg +"<table width='500'>"+ statusMsg + exeDate + exeTime + exeBrw + tcSkip + "<tr></tr>" + psScript + "</table></html></body>";

        //        // Add an attachment.
        //        Outlook.Attachment oAttach = oMsg.Attachments.Add(GlobalVariable.zipReportName);
        //        // Subject line
        //        oMsg.Subject = "Smart Recruit Automation Execution Report | " + System.DateTime.Now.ToString();
        //        // Add a recipient.
        //        Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
        //        // Change the recipient in the next line if necessary.
        //        if (GlobalVariable.receiverName.Contains(";"))
        //        {
        //            string[] arrReceiverNames = GlobalVariable.receiverName.Split(';');
        //            foreach (string name in arrReceiverNames)
        //            {
        //                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(name);
        //                oRecip.Resolve();
        //                oRecip = null;
        //            }
        //        }
        //        else
        //        {
        //            Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(GlobalVariable.receiverName);
        //            oRecip.Resolve();
        //        }
        //        // Send
        //        oMsg.Send();

        //        // Clean up.
        //        oRecips = null;
        //        oMsg = null;
        //        oApp = null;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.WriteLine(e.Message.ToString());
        //    }
        //}

        public void DeleteFilesInFolder(string folderPath, string fileExtension)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.Extension.Contains(fileExtension))
                        file.Delete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

    }
}