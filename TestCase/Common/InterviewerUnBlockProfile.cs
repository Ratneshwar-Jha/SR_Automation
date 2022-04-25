using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.TestCase.Common

{
    class InterviewerUnBlockProfile
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();
        BC_InterviewerActions bcINA = new BC_InterviewerActions();
        BC_CreateRequistion bcCR = new BC_CreateRequistion();
        BC_InitiatorActions bcIA = new BC_InitiatorActions();
        BC_ApproverActions bcAA = new BC_ApproverActions();
        BC_TPGActions bcTPGA = new BC_TPGActions();

        public void InterviwerUnBlockTheProfile()
        {
            try
            {
                RG_Instance.ReportStepLog("Initiator Creates Requisition", "Info", false);
                bcIOA.AppLogin();
                bcCR.CreateNewRequisition();

                RG_Instance.ReportStepLog("Fetch Approver Name", "Info", false);
                GlobalVariable.approverCount = 0;
                bcAA.MultiApprover();

                RG_Instance.ReportStepLog("Fetch TPG Manager Name", "Info", false);
                bcIOA.AppLogin();
                bcTPGA.GetTPGApproverName();

                RG_Instance.ReportStepLog("TPG Manager Forward The attached Profile", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("TPGName"));
                bcTPGA.TPGProfileAttach(string.Empty);

                RG_Instance.ReportStepLog("Interviewer will block the Profile Attached", "Info", false);
                string interviewerName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP1");
                bcIOA.LoginWithRandomUser(interviewerName.Split('(')[1].Replace(")", ""));
                bcINA.TakeInterviwerAction("block");

                RG_Instance.ReportStepLog("Interviewer will unblock the Profile Attached", "Info", false);
                bcIOA.LoginWithRandomUser(interviewerName.Split('(')[1].Replace(")", ""));
                bcINA.TakeInterviwerAction("unblock");

            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }

        }


    }
}
   