using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA;

namespace SR_Automation.BusinessComponent.Common
{
    class BC_TPGActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_CreateRequisition oCNR = new OR_CreateRequisition();
        OR_InitiatorActions oIA = new OR_InitiatorActions();
        OR_TPGActions oTPGA = new OR_TPGActions();
        OR_InOutApp oLP = new OR_InOutApp();

        BC_InOutApp bcIOA = new BC_InOutApp();

        public bool GetTPGApproverName()
        {
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.GetElementText(oIA.lnkPendingWith, "Pending With");
            TW_Instance.ClickElement(oIA.lnkPendingWith, "Pending With link");
            Thread.Sleep(15000);
            string pendingWith = TW_Instance.GetElementText(oIA.cellPendingWith, "Pending With");
            if (pendingWith.ToLower().Contains("pending"))
            {
                Thread.Sleep(15000);
                string TPGName = TW_Instance.GetElementText(oIA.cellPendingWithName, "TPG Approver Name");
                Environment.SetEnvironmentVariable("TPGName", TPGName.Split('[')[1].Replace("]", ""));
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TPGProfileAttach(string _action)
        {

            //click on ChangeRole
            TW_Instance.WaitForObjectPresent(oTPGA.lnkChangeRole);
            TW_Instance.ClickElement(oTPGA.lnkChangeRole, "Change Role");
            TW_Instance.WaitForObjectPresent(oTPGA.lnkTPGManager);
            TW_Instance.ClickElement(oTPGA.lnkTPGManager, "TPG Manager");
            TW_Instance.WaitForObjectPresent(oTPGA.txtTPGReqNum);
            //TW_Instance.CloseExtraWindow();
            //Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oTPGA.txtTPGReqNum, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
            if (demandType.ToLower().Equals("ras"))
            {
                TW_Instance.SelectDropdownValue(oTPGA.ddlTPGStatus, "TAG Manager Assigned", "Status");
            }
            TW_Instance.ClickElement(oTPGA.btnGo, "Go");
            Thread.Sleep(20000);
            TW_Instance.ClickElement(oTPGA.imgAttachProfile,"Attach Profile");
            Thread.Sleep(60000);
            TW_Instance.SwitchToNewWindow("searchcandidate");            
            Thread.Sleep(20000);
            TW_Instance.ClickElement(oTPGA.btnSCReset,"Reset");
            Thread.Sleep(2000);
            TW_Instance.SelectDropdownValue(oTPGA.ddlSCBand, "E2","Band");
            TW_Instance.ClickElement(oTPGA.btnSearchCandidate,"Search Candidate");
            Thread.Sleep(10000);
            //Attach employee to profile
            AttachEmployee();
            //validate selected profile
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oTPGA.btnSCClose, "Close button");
            Thread.Sleep(10000);
            TW_Instance.SwitchToParentWindow();
            Thread.Sleep(15000);
            //string attachProfileCounts = TW_Instance.GetLabelText(profileAttachedHyperlink);
            //TW_Instance.CompareValue(attachProfileCounts, "0", false);
            TW_Instance.ClickElement(oTPGA.cellProfileAttached, "Profile Attached");
            Thread.Sleep(15000);
            TW_Instance.SwitchToNewWindow("attachedprofile");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oTPGA.imgBtnForward, "Forward");
            Thread.Sleep(1000);
            TW_Instance.GetPopupText();
            Thread.Sleep(1000);
            TW_Instance.GetPopupText();
            Thread.Sleep(1000);
            if (_action.ToLower().Contains("block"))
            {
                Thread.Sleep(15000);
                TW_Instance.ClickElement(oTPGA.imgBtnBlock, "block");
                Thread.Sleep(1000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(1000);
                TW_Instance.GetPopupText();
                Thread.Sleep(1000);
            }
            if (_action.ToLower().Contains("finalselect"))
            {
                FinalSelect();
                TW_Instance.CloseExtraWindow();
            }
            else if (_action.ToLower().Contains("reject"))
            {
                FinalSelect();
                TW_Instance.SwitchToNewWindow("attachedprofile");
                Thread.Sleep(6000);
                TW_Instance.ClickElement(oTPGA.imgBtnReject,"Reject");
                Thread.Sleep(6000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(6000);
                TW_Instance.SelectDropdownValue(oTPGA.cmbRejectReason, 1,  "Reject");
                TW_Instance.SetTextInElement(oTPGA.txtTPGRejectRemark, "By Automation", false, "Remark");
                TW_Instance.ClickElement(oTPGA.imgRejectOK, "OK");
                Thread.Sleep(6000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(6000);
                string successMsg = TW_Instance.GetPopupText();
                Thread.Sleep(6000);
                TW_Instance.CloseExtraWindow();                
            }
            else
            {
                TW_Instance.ClickElement(oTPGA.btnSCClose, "Close button");
                TW_Instance.SwitchToParentWindow();                
            }
        }
        public void TPGManagerActions(string _action)
        {
            Thread.Sleep(5000);
            TW_Instance.WaitForObjectPresent(oTPGA.lnkChangeRole);
            TW_Instance.ClickElement(oTPGA.lnkChangeRole, "Change Role");
            TW_Instance.WaitForObjectPresent(oTPGA.lnkTPGManager);
            TW_Instance.ClickElement(oTPGA.lnkTPGManager, "TPG Manager");
            //TW_Instance.ClickElement(oTPGA.lnkChangeRole, "Change Role");
            ////Thread.Sleep(2000);            
            //TW_Instance.ClickElement(oTPGA.lnkTPGManager, "TPG Manager");
            Thread.Sleep(5000);
            //TW_Instance.CloseExtraWindow();
            //Thread.Sleep(2000);
            if (TW_Instance.IsObjectExist(oLP.txtPwd, "Password"))
            {
                bcIOA.LoginWithRandomUserRelogin(Environment.GetEnvironmentVariable("TPGName"));
            }
            TW_Instance.SetTextInElement(oTPGA.txtTPGReqNum, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            Thread.Sleep(5000);
            //TW_Instance.SetxDropdownValue(oTPGA.ddlTPGStatus, 2, false, "Tag Manager Assigned");
            TW_Instance.ClickElement(oTPGA.btnGo, "Req No Search Button");
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
            if (demandType.ToLower().Equals("ytocapprovalcng"))
            {
                TW_Instance.SelectDropdownValue(oTPGA.ddlTPGStatus, "Proactive SR", "ReferBack Reason");
            }
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oTPGA.btnGo, "Go");
            Thread.Sleep(10000);
            if (_action.ToLower().Contains("consenttohirepanindia"))
            {
                TW_Instance.ClickElement(oTPGA.lnkRequisitionNum, "Requisition Number Link");
                Thread.Sleep(20000);
                TW_Instance.SwitchToNewWindow("approverequisition");
                RG_Instance.ReportStepLog("Switched to new window successfully", "Pass", false);
                Thread.Sleep(2000);
                TW_Instance.WaitForObjectPresent(oTPGA.lnkRequisitionDetails);
                TW_Instance.ClickElement(oTPGA.lnkRequisitionDetails, "Requisition Details Link");
                Thread.Sleep(50000);
                if (TW_Instance.IsObjectExist(oTPGA.ConsentToHireFlagTPG, "Consent to Hire Resource on PAN India location"))
                {
                    Thread.Sleep(90000);
                    TW_Instance.GetElementText(oTPGA.ConsentToHireFlagTPG, "Consent to Hire Resource on PAN India location");
                    TW_Instance.GetElementText(oTPGA.ConsentToHireStatusTPG, "Consent to Hire Resource on PAN India location - Status");
                    RG_Instance.ReportStepLog("Able to fetch the flag for Consent to Hire Resource on PAN India location successfully", "Pass", false);
                    TW_Instance.ClickElement(oTPGA.btnCloseTPG, "Close button");
                }
            }
                if (_action.ToLower().Contains("finalselect"))
            {
                string prflAttachedVal = TW_Instance.GetElementText(oTPGA.cellProfileAttached, "Profile Attached Count");
                if (prflAttachedVal != "0")

                {
                    TW_Instance.ClickElement(oTPGA.cellProfileAttached, "Profile Attached link");
                    Thread.Sleep(2000);
                    TW_Instance.SwitchToNewWindow("attachedprofile");
                    Thread.Sleep(2000);
                    TW_Instance.ClickElement(oTPGA.imgbtnTPGShorlist, "ShortList");
                    TW_Instance.HandlePopupAlert("Accept");
                    Thread.Sleep(15000);
                    TW_Instance.SwitchToNewWindow("smartassignation");
                    TW_Instance.SetTextInElement(oTPGA.txtFSRemark, "Automation", false, "FS Remark");
                    TW_Instance.ClickElement(oTPGA.btnTPGFinalSelect, "TPG Final Select");
                    Thread.Sleep(40000);
                    string SuccessMsg = TW_Instance.GetPopupText();
                    Thread.Sleep(10000);
                    TW_Instance.CloseExtraWindow();
                }

            }
            else if (_action.ToLower().Contains("reject"))
            {
                string prflAttachedVal = TW_Instance.GetElementText(oTPGA.cellProfileAttached, "Profile Attached Count");
                if (prflAttachedVal != "0")

                {
                    TW_Instance.ClickElement(oTPGA.cellProfileAttached, "Profile Attached link");
                    TW_Instance.SwitchToNewWindow("attachedprofile");
                    Thread.Sleep(15000);
                    TW_Instance.ClickElement(oTPGA.imgBtnReject, "Reject");
                    TW_Instance.HandlePopupAlert("Accept");
                    TW_Instance.SwitchToNewWindow("smartassignation");
                    TW_Instance.SelectDropdownValue(oTPGA.cmbRejectReason, 1, "Reject");
                    TW_Instance.SetTextInElement(oTPGA.txtTPGRejectRemark, "Automation", false, "Remark");
                    TW_Instance.ClickElement(oTPGA.imgRejectOK, "OK");
                    TW_Instance.HandlePopupAlert("Accept");
                    Thread.Sleep(40000);
                    string SuccessMsg = TW_Instance.GetPopupText();
                    Thread.Sleep(10000);
                    TW_Instance.CloseExtraWindow();
                }
            }
            if (_action.ToLower().Contains("tpgreferback"))
            {
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oTPGA.chkSelectReq, "Requisition Number");
                TW_Instance.ClickElement(oTPGA.btnBulkReferback, "Refer Back");
                //Thread.Sleep(20000);
                //TW_Instance.SwitchToNewWindow("approverequisition");
                Thread.Sleep(10000);                
                string srReferBack = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ReferBackReason");
                TW_Instance.SelectDropdownValue(oTPGA.cmbReferbackReason, srReferBack,  "ReferBack Reason");
                TW_Instance.SetTextInElement(oTPGA.txtReferbackRemark, "Requisition referred back in Automation", true, "Remarks");
                TW_Instance.ClickElement(oTPGA.btnReferbackSubmit, "Submit Button");
                //TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(60000);
                string SuccessMsg = TW_Instance.GetPopupText();
                Thread.Sleep(10000);
                //TW_Instance.CloseExtraWindow();
            }

            if (_action.ToLower().Contains("tpgconfigurator"))
            {
                TW_Instance.ClickElement(oTPGA.lnkTpgConfigurator, "TPG Configurator");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oTPGA.lnkSearchReqNum, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                //TW_Instance.SetTextInElement(oTPGA.lnkSearchReqNum, "3011820", true, "Search Requisition Number");
                TW_Instance.ClickElement(oTPGA.lnkSearchReqNum, "Search Requisition Number");
                TW_Instance.ClickElement(oTPGA.btnApply, "Apply");
                RG_Instance.ReportStepLog("Apply button clicked successfully", "Pass", false);
                Thread.Sleep(10000);
                //TW_Instance.PerformMouseClick(oTPGA.imgAction);
                //Thread.Sleep(50000);
                TW_Instance.ClickElement(oTPGA.imgAction, "Action image");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oTPGA.txtJobFamily, "55065842", true, "Job Family");
                //TW_Instance.ClickElement(oTPGA.lstJobFamily, "Job Name");
                RG_Instance.ReportStepLog("Job selected successfully", "Pass", false);
                Thread.Sleep(10000);
                TW_Instance.SelectDropdownValue(oTPGA.ddlBand, 1, "Band");
                RG_Instance.ReportStepLog("Band selected successfully", "Pass", false);
                Thread.Sleep(10000);
                TW_Instance.SelectDropdownValue(oTPGA.ddlSubBand, 1, "SubBand");
                RG_Instance.ReportStepLog("SubBand selected successfully", "Pass", false);
                Thread.Sleep(10000);
                TW_Instance.SelectDropdownValue(oTPGA.ddlDesignation, 1, "Designation");
                RG_Instance.ReportStepLog("Designation selected successfully", "Pass", false);
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oTPGA.btnUpdate, "Update Button");
                RG_Instance.ReportStepLog("Update button clicked successfully", "Pass", false);
                Thread.Sleep(60000);
                TW_Instance.HandlePopupAlert("Accept");
                string SuccessMsg = TW_Instance.GetPopupText();
                RG_Instance.ReportStepLog("System is Prompting success popup", "Pass", false);
                Thread.Sleep(10000);
                
            }

            if (_action.ToLower().Contains("assigntotpgexecutive"))
            {
                TW_Instance.ClickElement(oTPGA.chkSelectReq,"Select Requisition");
                TW_Instance.ClickElement(oTPGA.btnAssignToTPGExe,"Assign To TPG Executive");
                Thread.Sleep(10000);
                TW_Instance.SelectDropdownValue(oTPGA.cmbTPGExecutive,1,"TPG Executive");
                string tpgExecutiveSAPID = TW_Instance.GetDropdownValue(oTPGA.cmbTPGExecutive, "SAP ID");
                tpgExecutiveSAPID = tpgExecutiveSAPID.Split('(')[1].Replace(")", "");
                Environment.SetEnvironmentVariable("TPGExecutiveName", tpgExecutiveSAPID);
                TW_Instance.ClickElement(oTPGA.imgBtnAssigntoTPGEx, "Assign To TPG Executive");
                TW_Instance.HandlePopupAlert("Accept");
            }
            if (_action.ToLower().Contains("block"))
            {
                TW_Instance.ClickElement(oTPGA.cellProfileAttached, "Profile Attached link");
                Thread.Sleep(2000);
                TW_Instance.SwitchToNewWindow("attachedprofile");
                Thread.Sleep(2000);
                TW_Instance.ClickElement(oTPGA.imgbtnTPGShorlist, "ShortList");
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(15000);
                TW_Instance.SwitchToNewWindow("smartassignation");
                TW_Instance.SetTextInElement(oTPGA.txtFSRemark, "Automation", false, "FS Remark");
                TW_Instance.ClickElement(oTPGA.btnTPGFinalSelect, "TPG Final Select");
                Thread.Sleep(40000);
                string SuccessMsg = TW_Instance.GetPopupText();
                Thread.Sleep(10000);
                TW_Instance.CloseExtraWindow();
            }
        }
        public void TPGExecutiveActions(string _action)
        {
            //click on ChangeRole
            Thread.Sleep(30000);
            TW_Instance.ClickElement(oTPGA.lnkChangeRole, "Change Role");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oTPGA.lnkTPGExecutive, "TPG Executive");
            Thread.Sleep(15000);
            TW_Instance.CloseExtraWindow();
            Thread.Sleep(2000);
            TW_Instance.SetTextInElement(oTPGA.txtTPGReqNum, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oTPGA.btnGo, "Go");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oTPGA.imgAttachProfile, "Attach Profile");
            Thread.Sleep(10000);
            TW_Instance.SwitchToNewWindow("searchcandidate");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oTPGA.btnSCReset, "Reset");
            Thread.Sleep(5000);
            TW_Instance.SelectDropdownValue(oTPGA.ddlSCBand, "E1", "Band");
            TW_Instance.ClickElement(oTPGA.btnSearchCandidate, "Search Candidate");
            Thread.Sleep(30000);

            //Attach employee to profile
            AttachEmployee();

            //validate selected profile
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oTPGA.btnSCClose, "Close button");
            Thread.Sleep(1000);
            TW_Instance.SwitchToParentWindow();
            Thread.Sleep(15000);
            //string attachProfileCounts = TW_Instance.GetLabelText(profileAttachedHyperlink);
            //TW_Instance.CompareValue(attachProfileCounts, "0", false);
            TW_Instance.ClickElement(oTPGA.cellProfileAttached, "Profile Attached");
            Thread.Sleep(15000);
            TW_Instance.SwitchToNewWindow("attachedprofile");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oTPGA.imgBtnForward, "Forward");
            Thread.Sleep(1000);
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(1000);
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(1000);
            if (_action.ToLower().Contains("finalselect"))
            {
                FinalSelect();
                TW_Instance.CloseExtraWindow();
            }
            else if (_action.ToLower().Contains("reject"))
            {
                FinalSelect();
                TW_Instance.SwitchToNewWindow("attachedprofile");
                Thread.Sleep(6000);
                TW_Instance.ClickElement(oTPGA.imgBtnReject, "Reject");
                Thread.Sleep(6000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(6000);
                TW_Instance.SelectDropdownValue(oTPGA.cmbRejectReason, 1, "Reject");
                TW_Instance.SetTextInElement(oTPGA.txtTPGRejectRemark, "By Automation", false, "Remark");
                TW_Instance.ClickElement(oTPGA.imgRejectOK, "OK");
                Thread.Sleep(6000);
                TW_Instance.HandlePopupAlert("Accept");
                Thread.Sleep(6000);
                string successMsg = TW_Instance.GetPopupText();
                Thread.Sleep(6000);
                TW_Instance.CloseExtraWindow();

            }
            else
            {
                TW_Instance.ClickElement(oTPGA.btnSCClose, "Close button");
                TW_Instance.SwitchToParentWindow();
            }
        }
        public void FinalSelect()
        {
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oTPGA.icnTPGShrotlist, "ShortList");
            Thread.Sleep(15000);
            TW_Instance.GetPopupText();
            Thread.Sleep(35000);
            TW_Instance.SwitchToNewWindow("smartassignation");
            Thread.Sleep(6000);
            TW_Instance.SetCalendarDate(oTPGA.txtAssignationEndDate, DateTime.Now.Date.AddDays(120).ToString("dd-MMM-yyyy"), "Assignation End Date");
            Thread.Sleep(10000);
            TW_Instance.SelectDropdownValue(oTPGA.cmbBillingStatus, 1, "Billing Status");
            Thread.Sleep(10000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbReasonNCS, 1, "ReasonNCS");
            //Thread.Sleep(10000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbPIRole, 3, "PI Role");            
            //Thread.Sleep(15000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbWBSComponent, 2, "WBS");
            
            TW_Instance.SelectDropdownValue(oTPGA.cmbPIRole, 1, "PI Role");
            TW_Instance.SetTextInElement(oTPGA.txtPIRole, "BY",false ,"PI Role");
            Thread.Sleep(15000);
            TW_Instance.SelectDropdownValue(oTPGA.cmbWBSComponent, 1, "WBS");
            TW_Instance.SetTextInElement(oTPGA.txtWBS, "BY", false, "WBS");
            //Thread.Sleep(6000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbLevel, 1, "Level");
            //Thread.Sleep(6000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbTransferType, 1, "Transfer Type");
            //TW_Instance.SelectDropdownValue(oTPGA.cmbCountry, 1, "Country");
            //TW_Instance.SelectDropdownValue(oTPGA.cmbPA, 1, "PA");
            //TW_Instance.SelectDropdownValue(oTPGA.cmbPSA, 1, "PSA");
            //TW_Instance.SelectDropdownValue(oTPGA.cmbNWL, 1, "NWL");
            //TW_Instance.SelectDropdownValue(oTPGA.cmbState, 1, "State");
            //Thread.Sleep(6000);
            //TW_Instance.SelectDropdownValue(oTPGA.cmbCity, 1, "City");
            TW_Instance.ClickElement(oTPGA.btnAddAssignation, "Add Assignation");
            Thread.Sleep(6000);
            TW_Instance.GetPopupText();
            Thread.Sleep(15000);
            TW_Instance.SetTextInElement(oTPGA.txtFSRemark, "By Automation", false, "FS Remark");
            TW_Instance.ClickElement(oTPGA.btnTPGFinalSelect, "TPG Final Select");
            Thread.Sleep(35000);
            string successMsg = TW_Instance.GetPopupText();
            Thread.Sleep(15000);
        }
        public void AttachEmployee()
        {
            bool breakFlag = false;
            for (int i = 3; i < 9; i++)
            {
                for (int j = 2; j < 9; j++)
                {
                    Thread.Sleep(15000);
                    TW_Instance.ClickElement("xpath://li[@pageindex='" + i.ToString() + "']//a[contains(text(),'" + i.ToString() + "')]", "Profile Page link");
                    string chkSCSelectEmp = "name:gvAttachCandidate$ctl0" + Convert.ToString(j + 1) + "$chk1";
                    Thread.Sleep(15000);
                    if (TW_Instance.IsObjectExist(chkSCSelectEmp, "Search Candidate CheckBox"))
                    {
                        Thread.Sleep(6000);
                        TW_Instance.ClickElement(chkSCSelectEmp, "Search Candidate CheckBox");
                        TW_Instance.ClickElement(oTPGA.btnSCAttachEmp, "Attach Emp");
                        Thread.Sleep(6000);
                        TW_Instance.ClickElement(oTPGA.btnAttachIcon, "Attach Emp Icon");
                        TW_Instance.HandlePopupAlert("Accept");
                        Thread.Sleep(35000);
                        string successMsg = TW_Instance.GetElementText(oTPGA.lblMessage, "Success Message");

                        if (successMsg.Contains("is successfully attached to requisition number"))
                        {
                            breakFlag = true;
                            break;
                        }
                    }
                }

                if (breakFlag)
                {
                    break;
                }
            }
        }
        public void TransferTPG(string _action)
        {
            TW_Instance.WaitForObjectPresent(oTPGA.lnkChangeRole);
            TW_Instance.ClickElement(oTPGA.lnkChangeRole, "Change Role");
            TW_Instance.WaitForObjectPresent(oTPGA.lnkTPGManager);
            TW_Instance.ClickElement(oTPGA.lnkTPGManager, "TPG Manager");
            TW_Instance.WaitForObjectPresent(oTPGA.txtTPGReqNum);
            //TW_Instance.CloseExtraWindow();
            //Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oTPGA.txtTPGReqNum, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            if (_action.ToLower().Contains("undo"))
            {
                TW_Instance.SelectDropdownValue(oTPGA.ddlTPGStatus, "TPG Transfer", "Status");
            }
            TW_Instance.ClickElement(oTPGA.btnGo, "Go");            
            Thread.Sleep(5000);
            if (_action.ToLower().Contains("transfer"))
            {
                TW_Instance.WaitForObjectPresent(oTPGA.imgTransfer);
                TW_Instance.ClickElement(oTPGA.imgTransfer, "Transfer");                
                GlobalVariable.driver.SwitchTo().Window(GlobalVariable.driver.WindowHandles[1]);
                TW_Instance.ClickElement(oTPGA.btnChangeTPG, "Transfer to TPG");
                TW_Instance.SelectDropdownValue(oTPGA.cmbTPG, 1, "Infra TPG");
                TW_Instance.SetTextInElement(oTPGA.txtRemark, "By Automation", true, "Remark");
                TW_Instance.ClickElement(oTPGA.btnChangTPG, "Change TPG");
                TW_Instance.HandlePopupAlert("Accept");
                GlobalVariable.driver.SwitchTo().Window(GlobalVariable.driver.WindowHandles[1]).Close();
                GlobalVariable.driver.SwitchTo().Window(GlobalVariable.driver.WindowHandles[0]);
            }
            else if (_action.ToLower().Contains("undo"))
            {
                TW_Instance.ClickElement(oTPGA.lnkUndo,"Undo");
                IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
                GlobalVariable.driver.SwitchTo().Window(wini.Last());
                TW_Instance.SetTextInElement(oTPGA.txUndoRemark, "By Automation", true, "Remark");
                TW_Instance.ClickElement(oTPGA.btnUndo, "Undo Transfer");
                TW_Instance.HandlePopupAlert("Accept");
                GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
                GlobalVariable.driver.SwitchTo().Window(wini.First());
            }
        }
    }
}
