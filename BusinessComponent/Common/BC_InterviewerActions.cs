using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using OpenQA;

namespace SR_Automation.BusinessComponent.Common
{
    class BC_InterviewerActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_InterviewerActions oINA = new OR_InterviewerActions();
        
        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();

        public void TakeInterviwerAction(string _action)
        {
            TW_Instance.ClickElement(oINA.lnkChangeRole, "Change Role");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oINA.lnkInterviwer, "Interveiwer");
            Thread.Sleep(35000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oINA.lnkInternalProfiles, "Internal Profile");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oINA.icnFilter, "Filter");
            Thread.Sleep(5000);
            TW_Instance.SetTextInElement(oINA.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oINA.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);

            if (_action.ToLower().Replace(" ", "").Equals("shortlist"))
            {
                TW_Instance.ClickElement(oINA.icnShortList, "ShortList");
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(60000);
                IList<string> winx = GlobalVariable.driver.WindowHandles.ToList();
                GlobalVariable.driver.SwitchTo().Window(winx.Last());
                Thread.Sleep(45000);
                if (TW_Instance.IsObjectExist(oINA.txtEndDate, "Bill End Date"))
                {
                   TW_Instance.SetTextInElement(oINA.txtEndDate, DateTime.Now.Date.AddDays(120).ToString("dd-MMM-yyyy"), false, "Bill End Date");
                }
                TW_Instance.SelectDropdownValue(oINA.cmbBillingStatus, 1, "BillingStatus");
                TW_Instance.SelectDropdownValue(oINA.cmbPIRole, 1, "PI Role");
                TW_Instance.SetTextInElement(oINA.txt_RoleRemarks, "By Automation", true, "Role Remarks");
                TW_Instance.SelectDropdownValue(oINA.cmbWBSComponent, 1, "WBS Component");
                TW_Instance.ClickElement(oINA.btnAddAssignation, "Proposed Assignation Details button");
                Thread.Sleep(1000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(15000);
                TW_Instance.ClickElement(oINA.btnShortlist, "Shortlist button");
                Thread.Sleep(1000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(1000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(45000);
                IList<string> wind = GlobalVariable.driver.WindowHandles.ToList();
                GlobalVariable.driver.SwitchTo().Window(wind.Last());
            }
            else if (_action.ToLower().Replace(" ", "").Equals("reject"))
            {
                TW_Instance.ClickElement(oINA.icnReject, "Reject");
                TW_Instance.HandlePopupAlert("Accept");
                TW_Instance.SelectDropdownValue(oINA.btnRejectReason,1,"Reason");
                TW_Instance.SetTextInElement(oINA.txtRejectRemark, "By Automation", true, "Remarks");
                TW_Instance.ClickElement(oINA.icnOk, "Reject OkButton");
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(10000);
                //string popUpMsg = TW_Instance.GetPopupText();
               //TW_Instance.CompareValue(popUpMsg, "has been rejected successfully.", true);
                TW_Instance.HandlePopupAlert("Accept");
            }
            else if (_action.ToLower().Replace(" ", "").Equals("block"))
            {
                TW_Instance.ClickElement(oINA.icnBlock, "Block");
                TW_Instance.HandlePopupAlert("Accept");
                TW_Instance.GetPopupText();
                
            }
            else if (_action.ToLower().Replace(" ", "").Equals("unblock"))
            {
                TW_Instance.ClickElement(oINA.icnBlock, "UnBlock");
                TW_Instance.HandlePopupAlert("Accept");
                TW_Instance.HandlePopupAlert("Accept");
            }
        }

        public void InterviewerEnhancement()
        {
            Thread.Sleep(80000);
            TW_Instance.ClickElement(oINA.lnkChangeRole, "Change Role");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oINA.lnkInterviwer, "Interveiwer");
            Thread.Sleep(115000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            bool bFlag = TW_Instance.IsObjectExist(oINA.lnkHome, "HOME Tab");
            if (bFlag)
                RG_Instance.ReportStepLog("Interviewer is Able to View HOME Tab ", "Pass", false);
            else
                RG_Instance.ReportStepLog("Interviewer is Not Able to View HOME Tab", "Fail", true);
            bool bFlag1 = TW_Instance.IsObjectExist(oINA.lnkInternalProfiles, "InternalProfiles Tab");
            if (bFlag1)
                RG_Instance.ReportStepLog("Interviewer is Able to View InternalProfiles Tab ", "Pass", false);
            else
                RG_Instance.ReportStepLog("Interviewer is Not Able to View InternalProfiles Tab", "Fail", true);
            bool bFlag2 = TW_Instance.IsObjectExist(oINA.lnkInterviewerReq, "Interviewer Requisition Tab");
            if (bFlag2)
                RG_Instance.ReportStepLog("Interviewer is Able to View Interviewer Requisition Tab ", "Pass", false);
            else
                RG_Instance.ReportStepLog("Interviewer is Not Able to View Interviewer Requisition Tab", "Fail", true);
            bool bFlag3 = TW_Instance.IsObjectExist(oINA.tblHomeProfileOverview, "Overview");
            if(bFlag3)
                RG_Instance.ReportStepLog("Interviewer is Able to View Requisitions In HOME Tab ", "Pass", false);
            else
                RG_Instance.ReportStepLog("Interviewer is Not Able to View Requisitions In HOME Tab", "Fail", true);
            bool bFlag4 = TW_Instance.IsObjectExist(oINA.imgItap, "ITap");
            if (bFlag3)
                RG_Instance.ReportStepLog("Interviewer is Able to View ITAP Link In HOME Tab ", "Pass", false);
            else
                RG_Instance.ReportStepLog("Interviewer is Not Able to View ITAP Link In HOME Tab", "Fail", true);
        }
        public void iTABEdgeInterviewerEnhancement(string _action)
        {
            Thread.Sleep(50000);
            TW_Instance.ClickElement(oINA.lnkChangeRole, "Change Role");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oINA.lnkInterviwer, "Interveiwer");
            Thread.Sleep(50000);
            if (_action.ToLower().Replace(" ", "").Equals("itabedge"))
            {
                bool bFlag3 = TW_Instance.IsObjectExist(oINA.tblHomeProfileOverview, "Overview");
                if (bFlag3)
                    RG_Instance.ReportStepLog("Interviewer is Able to View Requisitions In HOME Tab ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Interviewer is Not Able to View Requisitions In HOME Tab", "Fail", true);
                bool bFlag4 = TW_Instance.IsObjectExist(oINA.imgItap, "ITap");
                if (bFlag4)
                    RG_Instance.ReportStepLog("Interviewer is Able to View ITAP Link In HOME Tab ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Interviewer is Not Able to View ITAP Link In HOME Tab", "Fail", true);
            
            }
            if (_action.ToLower().Replace(" ", "").Equals("softblock"))
            {
                TW_Instance.ClickElement(oINA.lnkInternalProfiles, "Internal Profile");
                Thread.Sleep(5000);
                bool bFlag5 = TW_Instance.IsObjectExist(oINA.icnShortList, "Shrot List");
                if (bFlag5)
                    RG_Instance.ReportStepLog("Interviewer is Able to View ShrotList/Block In Internal Profile Tab ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Interviewer is Not Able to View ShrotList/Block In Internal Profile Tab", "Fail", true);
            }
        }
    }
}