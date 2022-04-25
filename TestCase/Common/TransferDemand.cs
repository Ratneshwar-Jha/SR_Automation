using System;
using System.Collections.Generic;
using System.Linq;
using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;

namespace SR_Automation.TestCase.Common
{
    class TransferDemand
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

        public void TransferReq()
        {
            try
            {
                RG_Instance.ReportStepLog("Initiator Transfer the Auto Created Requisition", "Info", false);
                bcIOA.AppLogin();
                bcIA.TransferRequisition();
                GlobalVariable.approverCount = 0;
                bcAA.MultiApprover();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
    }
}
