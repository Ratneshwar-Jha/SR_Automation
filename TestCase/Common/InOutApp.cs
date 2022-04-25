using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SR_Automation.BusinessComponent.Common;

namespace SR_Automation.TestCase.Common
{
    class InOutApp
    {

        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing BC
        BC_InOutApp bcIOA = new BC_InOutApp();

        public void LoginApp()
        {
            try
            {
                bcIOA.AppLogin();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog("Exception occured : " + e.Message.ToString(), "Fail", true);
            }
        }

        public void LogoutApp()
        {
            try
            {
                bcIOA.AppLogout();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog("Exception occured : " + e.Message.ToString(), "Fail", true);
            }
        }
    }
}
