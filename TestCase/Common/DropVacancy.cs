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
    class DropVacancy
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

        public void DropVacancyOfAReq()
        {
            try
            {
                RG_Instance.ReportStepLog("Initiator Drop Vacancy of a Requisition", "Info", false);
                bcIA.DropVacancy();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
    }
}
