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
    class CR00019297_Configurator
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
        BC_ConfiguratorActions bcC = new BC_ConfiguratorActions();
        public void UpdateRM()
        {
            try
            {
                RG_Instance.ReportStepLog("Configurator Update RM", "Info", false);
                bcIOA.AppLogin();
                bcC.UpdateRm();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
        public void BulkUpdateRM()
        {
            try
            {
                RG_Instance.ReportStepLog("Configurator Bulk Update RM", "Info", false);
                bcIOA.AppLogin();
                bcC.UpdateBulkRM();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
        public void ProjectUpdate()
        {
            try
            {
                RG_Instance.ReportStepLog("Configurator Update Project", "Info", false);
                bcIOA.AppLogin();
                bcC.UpdateProject();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
        public void BulkUpdateProject()
        {
            try
            {
                RG_Instance.ReportStepLog("Configurator Bulk Update Project", "Info", false);
                bcIOA.AppLogin();
                bcC.UpdateBlukProject();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
        public void FilterFunction()
        {
            try
            {
                RG_Instance.ReportStepLog("Configurator Update RM", "Info", false);
                bcIOA.AppLogin();
                bcC.Filter();
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }
    }
}
