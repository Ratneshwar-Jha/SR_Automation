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
    class BC_BulkActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_BulkAction oBKA = new OR_BulkAction();
        
        
        public void BulkActionForSRTransfer()
        {
            TW_Instance.WaitForObjectPresent(oBKA.lnkInitiatorActions);
            TW_Instance.ClickElement(oBKA.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.WaitForObjectPresent(oBKA.lnkBulkAction);
            TW_Instance.ClickElement(oBKA.lnkBulkAction, "Bulk Action");
            Thread.Sleep(9000);
            TW_Instance.WaitForObjectPresent(oBKA.chkRequistionNumber);
            TW_Instance.ClickElement(oBKA.chkRequistionNumber, "Select All");
            Thread.Sleep(3000);
            TW_Instance.IsObjectExist(oBKA.rbtnTransferReq, "Transfer Requisition");
            TW_Instance.SelectRadiobutton(oBKA.rbtnTransferReq, "Transfer Requisition");
            TW_Instance.ClickElement(oBKA.txtTransferee, "Transferee"); 
            string NameOfResource = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
            TW_Instance.SetTextInElement(oBKA.txtTransferee, NameOfResource, true, "NameOfResource");
            Thread.Sleep(5000);
            TW_Instance.WaitForObjectPresent(oBKA.setTransferee);
            TW_Instance.ClickElement(oBKA.setTransferee, "NameOfResource");
            Thread.Sleep(3000);
            TW_Instance.SelectDropdownValue(oBKA.ddSRTRReason, 2, "Transfer Reason");
            Thread.Sleep(3000);
            TW_Instance.SetTextInElement(oBKA.txtSRRemarks, "By Automation", true, "Remarks");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oBKA.btnBulkSRTransfer, "Bulk SR Transfer");
            Thread.Sleep(50000);
            TW_Instance.GetPopupText();
            RG_Instance.ReportStepLog("System is Prompting success popup", "Pass", false);

        }

        public void BulkActionForBSDUpdate()
        {
            TW_Instance.WaitForObjectPresent(oBKA.lnkInitiatorActions);
            TW_Instance.ClickElement(oBKA.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.WaitForObjectPresent(oBKA.lnkBulkAction);
            TW_Instance.ClickElement(oBKA.lnkBulkAction, "Bulk Action");
            Thread.Sleep(12000);
            TW_Instance.WaitForObjectPresent(oBKA.chkRequistionNumber);
            TW_Instance.ClickElement(oBKA.chkRequistionNumber, "Select All");
            Thread.Sleep(3000);
            TW_Instance.IsObjectExist(oBKA.rbtnBSDChange, "BSD Change");
            TW_Instance.ClickElement(oBKA.rbtnBSDChange, "BSD Change");
            TW_Instance.IsObjectExist(oBKA.txtBSDDate, "BSD Date");
            TW_Instance.SetTextInElement(oBKA.txtBSDDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), true, "BSD Date");
            Thread.Sleep(3000);
            TW_Instance.ClickElement(oBKA.ddBSDChangeReason, "BSD Change Reason");
            TW_Instance.SelectDropdownValue(oBKA.ddBSDChangeReason, 2, "BSD Change Reason");
            Thread.Sleep(5000);
            TW_Instance.WaitForObjectPresent(oBKA.btnBSD);
            TW_Instance.ClickElement(oBKA.btnBSD, "Bulk BSD Update");
            Thread.Sleep(60000);
            TW_Instance.GetPopupText();
            RG_Instance.ReportStepLog("System is Prompting success popup", "Pass", false);


        }

    }
}
