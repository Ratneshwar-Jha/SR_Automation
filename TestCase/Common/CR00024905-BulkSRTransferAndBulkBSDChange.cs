using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;
using OpenQA.Selenium;


namespace SR_Automation.TestCase.Common
{
    class CR00024905_BulkSRTransferAndBulkBSDChange
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();
        BC_BulkActions bcBA = new BC_BulkActions();
        
        public void BulkSRTransfer()
        {
            try
            {
                RG_Instance.ReportStepLog("Bulk SR Transfer ", "Info", false);
                bcIOA.AppLogin();
                bcBA.BulkActionForSRTransfer();

               
            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }
        }
        public void BulkBSDChange()
        {
            try
            {
                RG_Instance.ReportStepLog("Bulk BSD Change ", "Info", false);
                bcIOA.AppLogin();
                bcBA.BulkActionForBSDUpdate();

            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }
        }

    }
}
