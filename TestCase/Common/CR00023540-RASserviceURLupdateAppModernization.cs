﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;
using SR_Automation.Library;

namespace SR_Automation.TestCase.Common
{
    class CR00023540_RASserviceURLupdateAppModernization
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

        public void RASserviceURLupdateAppModernization()
        {
            try
            {
                //RG_Instance.ReportStepLog("Initiator Creates Requisition", "Info", false);
                //bcIOA.AppLogin();
                //bcCR.CreateNewRequisition();

                //RG_Instance.ReportStepLog("Fetch Approver Name", "Info", false);
                //GlobalVariable.approverCount = 0;
                //bcAA.MultiApprover();

                //RG_Instance.ReportStepLog("Fetch TPG Manager Name", "Info", false);
                //bcIOA.AppLogin();
                //bcTPGA.GetTPGApproverName();

                Environment.SetEnvironmentVariable("TPGName", "40156996");
                Environment.SetEnvironmentVariable("GenerateReqNum", "3063213");

                RG_Instance.ReportStepLog("TPG Manager Final Select", "Info", false);
                bcIOA.LoginWithRandomUser(Environment.GetEnvironmentVariable("TPGName"));
                bcTPGA.TPGProfileAttach("finalselect");

            }
            catch (Exception ex)
            {
                RG_Instance.ReportStepLog(ex.Message.ToString(), "Fail", true);
            }

        }
    }
}
