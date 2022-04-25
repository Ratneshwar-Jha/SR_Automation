using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SR_Automation.BusinessComponent.Common
{
    class BC_ApproverActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_CreateRequisition oCNR = new OR_CreateRequisition();
        OR_ApproverActions oAA = new OR_ApproverActions();
        

        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();
        BC_InitiatorActions bcIA = new BC_InitiatorActions();

        public void TakeApproverAction(string _action)
        {
            TW_Instance.WaitForObjectPresent(oAA.lnkApproverActions);
            TW_Instance.ClickElement(oAA.lnkApproverActions, "Approver Actions");
            TW_Instance.WaitForObjectPresent(oAA.lnkMyApprovals);
            TW_Instance.ClickElement(oAA.lnkMyApprovals, "My Approvals");
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(20000);
            //TW_Instance.ClickElement(oAA.lnkRequisition, "Requisition Number Link");
            //Thread.Sleep(350000);
            //if (TW_Instance.IsObjectExist(oAA.ConsentToHireFlag, "Consent to Hire Resource on PAN India location"))
            //{
            //    Thread.Sleep(90000);
            //    TW_Instance.GetElementText(oAA.ConsentToHireFlag, "Consent to Hire Resource on PAN India location");
            //    TW_Instance.GetElementText(oAA.ConsentToHireFlagStatus, "Consent to Hire Resource on PAN India location - Status");
            //    RG_Instance.ReportStepLog("Able to fetch the flag for Consent to Hire Resource on PAN India location successfully", "Pass", false);
            //    TW_Instance.ClickElement(oAA.btnClose, "Close button");
            //}
            TW_Instance.ClickElement(oAA.lnkActionByAppvr, "Action link");
            Thread.Sleep(350000);
            //TW_Instance.WaitForObjectPresent(oAA.txtApproverRemarks);
            TW_Instance.SelectDropdownValue(oAA.ddlApprvrSelectAction, _action, "Select Action dropdown");

            if (_action.ToLower().Replace(" ", "").Equals("referback"))
            {
                string rsn = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ReferBackReason");
                if (TW_Instance.IsObjectExist(oAA.ddlApprvrReason, "Reason"))
                {
                    TW_Instance.SelectDropdownValue(oAA.ddlApprvrReason, rsn, "Reason");
                }
            }
            else if (_action.ToLower().Replace(" ", "").Equals("close"))
            {
                string rsn = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "CloseReason");
                if (TW_Instance.IsObjectExist(oAA.ddlApprvrReason, "Reason"))
                {
                    TW_Instance.SelectDropdownValue(oAA.ddlApprvrReason, rsn, "Reason");
                }
            }

            TW_Instance.SetTextInElement(oAA.txtApproverRemarks, "By Automation", true, "Remarks");
            Thread.Sleep(60000);
            TW_Instance.ClickElement(oAA.btnApproverActionSubmit, "Submit button");
            Thread.Sleep(100000);
        }
        public void BulkApproverAction(string _action)
        {
            TW_Instance.WaitForObjectPresent(oAA.lnkApproverActions);
            TW_Instance.ClickElement(oAA.lnkApproverActions, "Approver Actions");
            TW_Instance.WaitForObjectPresent(oAA.lnkMyApprovals);
            TW_Instance.ClickElement(oAA.lnkMyApprovals, "My Approvals");
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            TW_Instance.ClickElement(oAA.chkbox, "Action link");
            TW_Instance.SelectDropdownValue(oAA.ddlBlkApprvAction, _action, "Select Action dropdown");

            if (_action.ToLower().Replace(" ", "").Equals("referback"))
            {
                string rsn = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ReferBackReason");
                if (TW_Instance.IsObjectExist(oAA.ddlBlkReason, "Reason"))
                {
                    TW_Instance.SelectDropdownValue(oAA.ddlBlkReason, rsn, "Reason");
                }
            }
            TW_Instance.SetTextInElement(oAA.txtBulkApprvRemark, "By Automation", true, "Remarks");
            TW_Instance.ClickElement(oAA.BtnBulkApproveButton, "Submit button");
            TW_Instance.GetPopupText();
        }
        public void MultiApprover()
        {
            bool bFlag = bcIA.GetApproverName();
            if (bFlag)
            {
                GlobalVariable.approverCount += 1;
                RG_Instance.ReportStepLog("Approver "+ Convert.ToString(GlobalVariable.approverCount) +" Approved Requisition", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("ApproverName"));
                Thread.Sleep(50000);               
                TakeApproverAction("Approved");
                bcIOA.AppLogin();
                MultiApprover();
            }
            else
            {
                return;
            }
        }
        public void BlkMultiApprover()
        {
            bool bFlag = bcIA.GetApproverName();
            if (bFlag)
            {
                GlobalVariable.approverCount += 1;
                RG_Instance.ReportStepLog("Approver " + Convert.ToString(GlobalVariable.approverCount) + " Approved Requisition", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("ApproverName"));
                BulkApproverAction("Approved");
                bcIOA.AppLogin();
                BlkMultiApprover();
            }
            else
            {
                return;
            }
        }
    }
}
