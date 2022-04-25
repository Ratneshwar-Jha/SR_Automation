using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;

namespace SR_Automation.TestCase.Common
{
    class CR00022331_ITAPEDGEHiring
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


        public void ITAPEDGEHiringInInterviewerPage()
        {
            try
            {
                RG_Instance.ReportStepLog("Checking for EDGE link option in SR for PM", "Info", false);
                string PMName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "UserName");
                bcIOA.LoginWithRandomUser(PMName);
                bcINA.iTABEdgeInterviewerEnhancement("itabedge");
                RG_Instance.ReportStepLog("Checking for EDGE link option in SR for PL4 head ", "Info", false);
                string PL4Name = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
                bcIOA.LoginWithRandomUser(PL4Name);
                bcINA.iTABEdgeInterviewerEnhancement("itabedge");
                //RG_Instance.ReportStepLog("Checking for Shortlist/Block option in SR for PM", "Info", false);
                //string PMName1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
                //bcIOA.LoginWithRandomUser(PMName1);
                //bcINA.iTABEdgeInterviewerEnhancement("softblock");
                RG_Instance.ReportStepLog("Checking for Shortlist/Block option in SR for PL4 head ", "Info", false);
                string PL4Name2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubRequestType");
                bcIOA.LoginWithRandomUser(PL4Name2);
                bcINA.iTABEdgeInterviewerEnhancement("softblock");
            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }
        }
    }
}
