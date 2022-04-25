using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SR_Automation.Library;
using SR_Automation.ObjectRepository.Common;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA;

namespace SR_Automation.BusinessComponent.Common
{
    class BC_InOutApp
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_InOutApp oLP = new OR_InOutApp();

        public void AppLogin()
        {
            string urlToOpen = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "QAURL");
            string userName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "UserName");
            string password = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "QAPassword");

            Environment.SetEnvironmentVariable("UserName", userName);
           
            string browserName = EF_Instance.ReadValueFromExcel(GlobalVariable.suiteFilePath, "RunSetting", 1, "Browser Name");

            TW_Instance.OpenURL(urlToOpen);
            TW_Instance.SetTextInElement(oLP.txtLoginId, userName, true, "Username");
            TW_Instance.SetTextInElement(oLP.txtPassword, password, true, "QAPassword");
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Password']"));
            obj_p.SendKeys(Keys.Tab);
            TW_Instance.ClickElement(oLP.btnLogin, "Login");
            
        }

        public void StagingAppLogin()
        {
            string StagingurlToOpen = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "StagingURL");
            string userName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "UserName");
            string Stagingpassword = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "Stagingpassword");

            Environment.SetEnvironmentVariable("UserName", userName);

            string browserName = EF_Instance.ReadValueFromExcel(GlobalVariable.suiteFilePath, "RunSetting", 1, "Browser Name");

            TW_Instance.OpenURL(StagingurlToOpen);
            TW_Instance.SetTextInElement(oLP.txtLoginId, userName, true, "Username");
            TW_Instance.SetTextInElement(oLP.txtPassword, Stagingpassword, true, "Stagingpassword");
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Password']"));
            obj_p.SendKeys(Keys.Tab);
            TW_Instance.ClickElement(oLP.btnLogin, "Login");
            Thread.Sleep(10000);
        }

        public void AppLogout()
        {
            TW_Instance.ClickElement(oLP.lnkLogOut, "LogOut");
        }

        public void HandleSecurityPopUp()
        {
            #region Variable Declaration
            #endregion

            #region Data Read
            string tpgSecurityUserName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, GlobalVariable.dataSheetName, GlobalVariable.currentIteration, "TPGSecurityUserName");
            string tpgSecurityPwd = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, GlobalVariable.dataSheetName, GlobalVariable.currentIteration, "TPGSecurityPwd");
            #endregion

        }

        public void LoginWithRandomUser(string _userName)
        {
            string urlToOpen = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "QAURL");
            string password = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "QAPassword");
            
            TW_Instance.OpenURL(urlToOpen);
            TW_Instance.SetTextInElement(oLP.txtLoginId, _userName, true, "Username");
            TW_Instance.SetTextInElement(oLP.txtPassword, password, true, "QAPassword");
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Password']"));
            obj_p.SendKeys(Keys.Tab);
            TW_Instance.ClickElement(oLP.btnLogin, "Login");

        }

        public void LoginWithRandomUserRelogin(string _userName)
        {
            string password = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "Password");

            TW_Instance.SetTextInElement(oLP.txtEmp, _userName, true, "Username");
            TW_Instance.SetTextInElement(oLP.txtPwd, password, true, "Password");
            IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtPwd']"));
            obj_p.SendKeys(Keys.Tab);
            TW_Instance.ClickElement(oLP.btnSubmit, "Submit");

        }
    }
}
