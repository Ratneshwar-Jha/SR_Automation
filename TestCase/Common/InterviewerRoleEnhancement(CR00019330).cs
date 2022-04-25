using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.TestCase.Common
{
    class InterviewerRoleEnhancement_CR00019330_
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
        BC_InterviewerActions bcInA = new BC_InterviewerActions();
        public void InterviewerRoleEnhancement()
        {
            try
            {
                RG_Instance.ReportStepLog("User Navigate to Interviewer Home page", "Info", false);
                bcIOA.AppLogin();
                //bcCR.CreateNewRequisition();
                bcInA.InterviewerEnhancement();
            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }

        }
    }
}

