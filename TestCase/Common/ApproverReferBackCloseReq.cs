using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.TestCase.Common
{
    class ApproverReferBackCloseReq
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();
        BC_CreateRequistion bcCR = new BC_CreateRequistion();
        BC_InitiatorActions bcIA = new BC_InitiatorActions();
        BC_ApproverActions bcAA = new BC_ApproverActions();

        public void ReferBackAndCloseReq()
        {
            try
            { 
                RG_Instance.ReportStepLog("Initiator Creates Requisition", "Info", false);
                bcIOA.AppLogin();
                bcCR.CreateNewRequisition();
                bcIA.GetApproverName();

                RG_Instance.ReportStepLog("Approver ReferBack Requisition", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("ApproverName"));
                bcAA.TakeApproverAction("Refer Back");

                RG_Instance.ReportStepLog("Initiator Checks ReferBack Status and Re-Submit", "Info", false);
                bcIOA.AppLogin();
                bcIA.VerifyReqStatus("Refer Back");
                bcIA.ReSubmitRequisition(false);
                bcIA.GetApproverName();

                RG_Instance.ReportStepLog("Approver Closes Requisition", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("ApproverName"));
                bcAA.TakeApproverAction("Close");

                RG_Instance.ReportStepLog("Initiator Checks Closed Status", "Info", false);
                bcIOA.AppLogin();
                bcIA.VerifyReqStatus("Closed");
            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }

        }


    }
}
