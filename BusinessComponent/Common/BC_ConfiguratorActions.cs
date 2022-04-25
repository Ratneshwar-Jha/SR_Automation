using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA;

namespace SR_Automation.BusinessComponent.Common
{
    class BC_ConfiguratorActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_CreateRequisition oCNR = new OR_CreateRequisition();
        OR_Configurator oCR = new OR_Configurator();

        public void UpdateRm()
        {
            Thread.Sleep(25000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oCR.lnkConfigurator, "Configurator");
            Thread.Sleep(50000);
            string oRMName = TW_Instance.GetElementText(oCR.txtRMName, "RM Name");
            oRMName = oRMName.Split('(')[0].Replace(" ", "");
            TW_Instance.ClickElement(oCR.icnUpdateRM, "Update RM ");
            Thread.Sleep(5000);
            string sRMName = TW_Instance.GetElementText(oCR.txtRMnamePopup, "RM Name");
            sRMName = sRMName.Replace(" ", "");
            if (oRMName.Equals(sRMName))
            {
                TW_Instance.SelectRadiobutton(oCR.rbtn1RMname, "Update RM");
            }
            else
            {
                TW_Instance.SelectRadiobutton(oCR.rbtnRMname, "Update RM");
            }           
            TW_Instance.ClickElement(oCR.btnConfirm, "Click Confirm");
            Thread.Sleep(3000);
            string popUpMessage = TW_Instance.GetPopupText();
            TW_Instance.CompareValue("Saved Successfully!!", popUpMessage, true);
            Thread.Sleep(50000);
            string nRMName = TW_Instance.GetElementText(oCR.txtRMName, "RM Name");
            nRMName = nRMName.Split('(')[0].Replace(" ", "");
            Thread.Sleep(3000);
            bool result = TW_Instance.CompareValue(oRMName, nRMName, false);
            if(result)
            {
                RG_Instance.ReportStepLog("Configurator is able to update RM", "Pass", false);               
            }
            else
            {
                RG_Instance.ReportStepLog("Configurator is Not able to update RM", "Fail", true);
            }
        }
        public void UpdateBulkRM()
        {
            Thread.Sleep(25000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oCR.lnkConfigurator, "Configurator");
            Thread.Sleep(50000);
            string oRMName = TW_Instance.GetElementText(oCR.txtRMName, "RM Name");
            oRMName = oRMName.Split('(')[0].Replace(" ","");
            Thread.Sleep(1000);
            TW_Instance.ClickElement(oCR.chkbox1, "Select First Requistion");
            Thread.Sleep(1000);
            TW_Instance.ClickElement(oCR.chkbox2, "Select second Requistion");
            Thread.Sleep(1000);
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//tr[2]//td[1]//input[1]"));
            obj_p.SendKeys(Keys.ArrowUp);
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnBulkRM, "Bulk Update RM");
            Thread.Sleep(5000);            
            string sRMName = TW_Instance.GetElementText(oCR.txtRMnamePopup, "RM Name");
            sRMName = sRMName.Replace(" ", "");
            if (oRMName.Equals(sRMName))
            {
                TW_Instance.SelectRadiobutton(oCR.rbtn1RMname, "Update RM");
            }
            else
            {
                TW_Instance.SelectRadiobutton(oCR.rbtnRMname, "Update RM");
            }
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oCR.btnConfirm, "Click Confirm");
            string popUpMessage = TW_Instance.GetPopupText();
            TW_Instance.CompareValue("Saved Successfully!!", popUpMessage, true);
            Thread.Sleep(50000);
            string nRMName = TW_Instance.GetElementText(oCR.txtRMName, "RM Name");
            nRMName = nRMName.Split('(')[0].Replace(" ", "");
            Thread.Sleep(3000);
            bool result = TW_Instance.CompareValue(oRMName, nRMName, false);
            if (result)
            {
                RG_Instance.ReportStepLog("Configurator is able to update RM", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Configurator is Not able to update RM", "Fail", true);
            }
        }
        public void UpdateProject()
        {
            Thread.Sleep(25000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oCR.lnkConfigurator, "Configurator");
            Thread.Sleep(50000);
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//tr[2]//td[1]//input[1]"));
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.txtPageNo,"Page");
            Thread.Sleep(5000);
            IWebElement obj_q = GlobalVariable.driver.FindElement(By.XPath("//a[contains(text(),'2')]"));
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            Thread.Sleep(2000);
            string FetchReqNo = TW_Instance.GetElementText(oCR.txtFetchReqNumber, "Requisition Number");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnfilter, "Filter Icon");
            Thread.Sleep(2000);
            TW_Instance.SetTextInElement(oCR.txtRequestionNumber, FetchReqNo, true, "Requistion Number");
            TW_Instance.ClickElement(oCR.btnApply, "Apply");
            Thread.Sleep(50000);
            TW_Instance.IsObjectExist(oCR.txtFetchReqNumber, "Requisition Number");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnUpdateProject,"Update Project");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oCR.rbtnProject,"Project");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.btnConfirmP, "Click Confirm");
            Thread.Sleep(3000);
            string popUpMessage = TW_Instance.GetPopupText();
            TW_Instance.CompareValue("Saved Successfully!!", popUpMessage, true);
            Thread.Sleep(8000);
        }
        public void UpdateBlukProject()
        {
            Thread.Sleep(25000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(15000);
            TW_Instance.SelectRadiobutton(oCR.lnkConfigurator, "Configurator");
            Thread.Sleep(50000);
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//tr[2]//td[1]//input[1]"));
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            obj_p.SendKeys(Keys.ArrowDown);
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.txtPageNo, "Page");
            Thread.Sleep(5000);
            IWebElement obj_q = GlobalVariable.driver.FindElement(By.XPath("//a[contains(text(),'2')]"));
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            obj_q.SendKeys(Keys.ArrowUp);
            Thread.Sleep(2000);
            string FetchReqNo = TW_Instance.GetElementText(oCR.txtFetchReqNumber1, "Requisition Number");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnfilter, "Filter Icon");
            Thread.Sleep(2000);
            TW_Instance.SetTextInElement(oCR.txtRequestionNumber, FetchReqNo, true, "Requistion Number");
            TW_Instance.ClickElement(oCR.btnApply, "Apply");
            Thread.Sleep(50000);
            TW_Instance.IsObjectExist(oCR.txtFetchReqNumber, "Requisition Number");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.chkbox1, "Select First Requistion");
            Thread.Sleep(1000);
            TW_Instance.ClickElement(oCR.chkbox2, "Select second Requistion");
            Thread.Sleep(1000);
            IWebElement obj_s = GlobalVariable.driver.FindElement(By.XPath("//tr[1]//td[1]//input[1]"));
            obj_s.SendKeys(Keys.ArrowUp);
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnBulkProject, "Update Project");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oCR.rbtnProject, "Project");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.btnConfirmP, "Click Confirm");
            Thread.Sleep(3000);
            string popUpMessage = TW_Instance.GetPopupText();
            TW_Instance.CompareValue("Saved Successfully!!", popUpMessage, true);
            Thread.Sleep(8000);
        }
        public void Filter()
        {
            Thread.Sleep(25000);
            //IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
            //GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
            //Thread.Sleep(10000);
            //GlobalVariable.driver.SwitchTo().Window(wini.First());
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(15000);
            TW_Instance.ClickElement(oCR.lnkConfigurator, "Configurator");
            Thread.Sleep(50000);
            string FetchReqNo = TW_Instance.GetElementText(oCR.txtFetchReqNumber, "Requisition Number");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCR.icnfilter, "Filter Icon");
            Thread.Sleep(2000);
            if(TW_Instance.IsObjectExist(oCR.txtRequestionNumber, "Requisition Number"))
            {
                RG_Instance.ReportStepLog("Requisition Number field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Requisition Number field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.txtCandidateName, "Candidate Name"))
            {
                RG_Instance.ReportStepLog("Candidate Name field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Candidate Name field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.cmbProjectModalL1, "Project L1"))
            {
                RG_Instance.ReportStepLog("Project L1 field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Project L1 field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.cmbProjectModalL4, "Project L4"))
            {
                RG_Instance.ReportStepLog("Project L4 field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Project L4 field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.cmbJoiningModalL1, "Joining L1"))
            {
                RG_Instance.ReportStepLog("Joining L1 field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Joining L1 field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.cmbJoiningModalL4, "Joining L4"))
            {
                RG_Instance.ReportStepLog("Joining L4 field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Joining L4 field not displayed in filter page", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCR.cmbCountryModal, "Country"))
            {
                RG_Instance.ReportStepLog("Country field displayed successfully in filter page", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Country field not displayed in filter page", "Fail", true);
            }
            
            Thread.Sleep(2000);
            TW_Instance.SetTextInElement(oCR.txtRequestionNumber, FetchReqNo,true,"Requistion Number");
            TW_Instance.ClickElement(oCR.btnApply,"Apply");
            Thread.Sleep(50000);
            
            if (TW_Instance.IsObjectExist(oCR.txtFetchReqNumber, "Requisition Number"))
            {
                RG_Instance.ReportStepLog("Requisition Number displayed successfully", "Pass", false);
            }
            else
            {
                RG_Instance.ReportStepLog("Requisition Number field not displayed", "Fail", true);
            }
        }
    }
}
