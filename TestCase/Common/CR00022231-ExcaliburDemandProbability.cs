using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;

namespace SR_Automation.TestCase.Common
{
    class CR00022231_ExcaliburDemandProbability
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
        BC_TPGActions bcTPGA = new BC_TPGActions();

        public void Excalibur_DemandProbabilitye()
        {
            try
            {
                RG_Instance.ReportStepLog("Initiator Creates Requisition ", "Info", false);
                bcIOA.AppLogin();
                bcCR.CreateNewRequisition();
                RG_Instance.ReportStepLog("Initiator is able to Submit Requisition", "Pass", false);
                GlobalVariable.approverCount = 0;
                bcAA.MultiApprover();
            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }
        }
    }
}
