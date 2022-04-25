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
    class BC_InitiatorActions
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_CreateRequisition oCNR = new OR_CreateRequisition();
        OR_InitiatorActions oIA = new OR_InitiatorActions();
        OR_InOutApp oLP = new OR_InOutApp();

        public void EditRequisition()
        {
            string graID = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "GRDID");
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            Thread.Sleep(100000);
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(100000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Button");
            Thread.Sleep(90000);
            if (entityName.ToLower().Equals("tech"))
            {
                TW_Instance.SetTextInElement(oCNR.txtGRAID, graID, true, "Grade ID");
                string consentForTP = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ConsentForTP");
                if (consentForTP.ToLower() == "yes")
                {
                    TW_Instance.SelectDropdownValue(oIA.cmbEmpGrpRsn, 1, "Employee Group Reason");
                }
            }
            
            TW_Instance.SetTextInElement(oIA.txtPositionEndDate, DateTime.Now.Date.AddDays(120).ToString("dd-MMM-yyyy"), true, "Postion Date");
            TW_Instance.ClickElement(oIA.btnReqUpdate, "Edit Update Requisition");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(90000);
            string popUpMsg = TW_Instance.GetPopupText();
            TW_Instance.CompareValue(popUpMsg, "Requisition has been Updated Successfully.", true);
        }

        public void CopyRequisition()
        {
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            string job = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Job");
            string band = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Band");

            Thread.Sleep(15000);
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oIA.btnCopy, "Copy Button");
            Thread.Sleep(10000);

            string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if (positionExclsv.ToLower().Equals("exclusive"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                else
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");
            }

            string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "CopyNameOfResource");
            if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            {
                TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, true, "Name of Resource");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            {
                TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, true, "Name of Resource");
            }

            TW_Instance.SetValueInJobFilter(job, "Job");
            Thread.Sleep(2000);
            TW_Instance.SetDropdownValue(oCNR.cmbBand, 1, false, "Band");

            string subBand = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubBand");
            TW_Instance.SetDropdownValue(oCNR.cmbSubBand, 1, false, "Sub Band");

            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(20000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))

            {
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }

            TW_Instance.SetDropdownValue(oCNR.cmbQualification, 1, false, "Qualification");
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(20000);
            TW_Instance.SetDropdownValue(oCNR.cmbBillingType, 2, false, "Billing Type");
            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            Thread.Sleep(15000);

            if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            }
            else if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUpQA, "Similar Pop Up QA"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUpQA, "Similar Pop Up");
            }

            string generatedReqNum = string.Empty;

            if (TW_Instance.IsObjectExist(oCNR.lblReqNoText, "Similar Pop Up QA"))
            {
                Thread.Sleep(2000);
                generatedReqNum = TW_Instance.GetElementText(oCNR.lblReqNoText, "Pop Up Message");
            }
            else
            {
                Thread.Sleep(2000);
                generatedReqNum = TW_Instance.GetPopupText();
            }
            Environment.SetEnvironmentVariable("CopiedReqNum", generatedReqNum.Split(new String[] { "/2021/" }, StringSplitOptions.None)[1].Split('.')[0].Replace(" ", ""));            
            TW_Instance.CompareValue(Environment.GetEnvironmentVariable("CopiedReqNum"), Environment.GetEnvironmentVariable("GenerateReqNum"), false);
        }

        public void DropVacancy()
        {
            //Manage Requisition
            string dropVacancyReason = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DropReason");

            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oIA.btnDropVacancy, "Drop Vacancy Button");
            Thread.Sleep(90000);
            TW_Instance.SelectDropdownValue(oIA.cmbDropReason, dropVacancyReason, "Reason");
            TW_Instance.SetTextInElement(oIA.txtDropRemarks, "By Automation", true, "Remark");
            TW_Instance.ClickElement(oIA.btnDropReqSubmit, "Drop Vacancy Submit");
            string popUpMsg = TW_Instance.GetPopupText();
            TW_Instance.CompareValue(popUpMsg, "Vacancy has been dropped successfully!!", true);
        }

        public void CloseRequisition()
        {
            //Manage Requisition
            string closeReason = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "CloseReason");

            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oIA.btnReqClose, "Close Button");
            Thread.Sleep(90000);
            TW_Instance.SelectDropdownValue(oIA.cmbCloseReason, closeReason, "Reason");
            TW_Instance.SetTextInElement(oIA.txtCloseRemarks, "By Automation", true, "Remark");
            TW_Instance.ClickElement(oIA.btnCloseReqSubmit, "Close Req Submit");
            string popUpMsg = TW_Instance.GetPopupText();
            TW_Instance.CompareValue(popUpMsg, "Requisition has been Closed successfully.", true);
        }

        public void ReSubmitRequisition(bool bEditFlag)
        {
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Update Button");
            Thread.Sleep(40000);

            //Screen 1     
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            if (demandType.ToLower().Equals("ytocapprovalcng"))
            {               
                TW_Instance.SelectListBoxValue(oCNR.txtProject, "USAA-Documentum-CHN (C/150327)", "Project");
                Thread.Sleep(2000);
                if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                {
                    string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
                    if (positionExclsv.ToLower().Equals("exclusive"))
                        TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                    else
                        TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");
                }
                if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
                {
                    string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
                    if (clientIntrvw.ToLower().Equals("yes"))
                        TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
                }
                string subSubReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubSubRequestType");
                TW_Instance.SelectRequestType("New Resource Request", "New", subSubReqstType, false, "Requisition Type");
                Thread.Sleep(5000);
                //string skill = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill");
                //if (entityName.ToLower().Equals("infra"))
                //{
                //    if (TW_Instance.IsObjectEnabled(oCNR.txtSkill, "Skill"))
                //    {
                //        RG_Instance.ReportStepLog("Initiator Able to Edit skill", "Pass", false);
                //    }
                //    TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                //}
                    string country = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Country");
                TW_Instance.SetDropdownValue(oCNR.cmbCountry, "Show All Countries", true, "");
                TW_Instance.SetDropdownValue(oCNR.cmbCountry, country, true, "Country");
                TW_Instance.SetDropdownValue(oCNR.cmbPSA, 1, false, "PSA");

            }

            
            if (demandType.ToLower().Equals("secondary"))
            {
                if (TW_Instance.IsObjectEnabled(oCNR.cmbxSecPSA, "Secondary PSA"))
                {
                    RG_Instance.ReportStepLog("Initiator Able to Edit Secondary PSA", "Pass", false);
                }
            }
            
            if (entityName.ToLower().Equals("infra"))
            {
                string skill1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill1");
                if (TW_Instance.IsObjectEnabled(oCNR.txtSkill, "Skill1"))
                {
                    RG_Instance.ReportStepLog("Initiator is Able to Edit skill", "Pass", false);

                    if (TW_Instance.IsObjectExist(oCNR.txtSkill, "Skill1"))
                    {

                        TW_Instance.SetMultiValueInSkillPopUp(skill1, "Skill1");
                    }
                    else
                    {
                        TW_Instance.SetValueInSkillPopUp(skill1, "Skill1");
                    }
                }

                if (TW_Instance.IsObjectEnabled(oCNR.cmbBU, "Business Unit"))
                {
                    RG_Instance.ReportStepLog("Initiator Able to Edit Business Unit", "Pass", false);
                }
                TW_Instance.SetDropdownValue(oCNR.cmbBU, 1, false, "Business Unit");
                if (TW_Instance.IsObjectEnabled(oCNR.cmbDomain, "Domain"))
                {
                    RG_Instance.ReportStepLog("Initiator Able to Edit Domain", "Pass", false);
                }
                TW_Instance.SetDropdownValue(oCNR.cmbDomain, 1, false, "Domain");
                if (TW_Instance.IsObjectEnabled(oCNR.cmbSubDomain, "Sub Domain"))
                {
                    RG_Instance.ReportStepLog("Initiator Able to Edit Sub Domain", "Pass", false);
                }
                TW_Instance.SetDropdownValue(oCNR.cmbSubDomain, 1, false, "Sub Domain");
            }
                if (entityName.ToLower().Equals("infra"))
            {
                Thread.Sleep(10000);
                
                TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Doc.txt");
            }
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(10000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))

            {
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }

            Thread.Sleep(10000);
            //Screen 2          
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbState + "']", "Joining State"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbState, 2, false, "Joining State");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbCity, 1, false, "Joining City");
            }
            if (TW_Instance.IsObjectExist(oCNR.txtBuyRate, "SOT Buy Rate"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBuyRate, "500", true, "Buy Rate");
                TW_Instance.SetTextInElement(oCNR.txtSellRate, "1000", true, "Sell Rate");
            }
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");

            //Screen 3
            Thread.Sleep(10000);
            //if (bEditFlag)
            //{
            //    Thread.Sleep(20000);
            //    string billingType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ResubmitBillingType");
            //    TW_Instance.SetDropdownValue(oCNR.cmbBillingType, billingType, true, "Billing Type");
            //}
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbDemandProbability + "']", "Demand Probability"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbDemandProbability, 2, false, "Demand Probability");
            }
            TW_Instance.SetTextInElement(oCNR.txtEditRemarks, "Edit by Automation", true, "Edit Remarks");

            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            //Thread.Sleep(3000);
            //if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            //{
            //    TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            //}

            Thread.Sleep(1000);
            string generatedReqNum = "";
            
            if (TW_Instance.IsObjectExist(oCNR.lblReqNoText, "Similar Pop Up QA"))
            {
                Thread.Sleep(1000);
                generatedReqNum = TW_Instance.GetElementText(oCNR.lblReqNoText, "Pop Up Message");
            }
            else
            {
                Thread.Sleep(1000);
                generatedReqNum = TW_Instance.GetPopupText();
            }           
            Thread.Sleep(5000);
        }

        public void TPGRBResubmit()
        {
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Update Button");
            Thread.Sleep(90000);
            TW_Instance.SetTextInElement(oIA.txtPositionEndDate, DateTime.Now.Date.AddDays(120).ToString("dd-MMM-yyyy"), true, "Postion Date");
            Thread.Sleep(5000);
            TW_Instance.SetTextInElement(oIA.txtRemark, "Edit by Automation", true, "Edit Remarks");
            Thread.Sleep(5000);
            TW_Instance.ClickElement(oIA.btnSubmit, "TPG Submit button");
            Thread.Sleep(5000);
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(90000);
            string popUpMsg = TW_Instance.GetPopupText();
        }
           
        public bool GetApproverName()
        {
            //try
            //{
            //    Thread.Sleep(6000);
            //    GlobalVariable.driver.SwitchTo().Window(GlobalVariable.driver.WindowHandles[1]).Close();
            //    GlobalVariable.driver.SwitchTo().Window(GlobalVariable.driver.WindowHandles[0]); 
            //}
            //catch
            //{

            //}
            if (TW_Instance.IsObjectExist(oLP.txtLoginId, "Login ID"))
            {
                string userName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "UserName");
                string password = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "Password");

                TW_Instance.SetTextInElement(oLP.txtLoginId, userName, true, "Username");
                TW_Instance.SetTextInElement(oLP.txtPassword, password, true, "Password");
                IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Password']"));
                obj_p.SendKeys(Keys.Tab);
                TW_Instance.ClickElement(oLP.btnLogin, "Login");
            }
            TW_Instance.WaitForObjectPresent(oCNR.lnkInitiatorActions);
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.WaitForObjectPresent(oCNR.lnkManageRequisitions);
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            TW_Instance.WaitForObjectPresent(oCNR.txtReqNoSearch);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(40000);
            TW_Instance.GetElementText(oIA.lnkPendingWith, "Pending With");
            TW_Instance.ClickElement(oIA.lnkPendingWith, "Pending With link");
            Thread.Sleep(40000);
            string pendingWith = TW_Instance.GetElementText(oIA.cellPendingWith, "Pending With");
            if (pendingWith.ToLower().Contains("approver"))
            {
                string approverName = TW_Instance.GetElementText(oIA.cellPendingWithName, "Approver Name");
                Environment.SetEnvironmentVariable("ApproverName", approverName.Split('[')[1].Replace("]", ""));
                return true;
            }
            else
            {
                if (pendingWith.ToLower().Contains("tag"))
                {
                    RG_Instance.ReportStepLog("Requisition is Pending with TAG Manager" , "Pass", false);
                }
                return false;
            }
        }

        public void VerifyReqStatus(string _status)
        {
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(10000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            string currentStatus = TW_Instance.GetElementText(oIA.cellReqStatus, "Requisition Status");
            TW_Instance.CompareValue(currentStatus, _status, true);
            Thread.Sleep(10000);
        }

        public void TransferRequisition()
        {
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            string reqType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            if (entityName.ToLower().Equals("bserv"))
            {
                Thread.Sleep(300000);
            }
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            if (TW_Instance.IsObjectExist(oCNR.cmbEntityName, "Entity Name"))
            {
                TW_Instance.SelectDropdownValue(oCNR.cmbEntityName, 1, "Entity Name");
                TW_Instance.ClickElement(oCNR.btnSaveContinue, "Entity Name");
            }

            //Screen 1
            Thread.Sleep(15000);
            string projectName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
            TW_Instance.SelectListBoxValue(oCNR.txtProject, projectName, "Project");

            string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if (positionExclsv.ToLower().Equals("exclusive"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                else
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");

                if (entityName.ToLower().Equals("bserv"))
                    RG_Instance.ReportStepLog("Bserv SOT/NON-SOT validation check.", "Pass", false);
            }
            if (entityName.ToLower().Equals("bserv"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                    {
                        RG_Instance.ReportStepLog("Demand type validation check.", "Pass", false);
                    }
                }
            }
            //TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, "3", "Project L1");
            //TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, "3", "Project L4");
            //TW_Instance.SelectRadiobutton(oCNR.rdbDemandTypeSOT, "Demand Type");
            //Thread.Sleep(2000);

            string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
            if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
            {
                if (clientIntrvw.ToLower().Equals("yes"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
            }

            string reqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string subReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubRequestType");
            string subSubReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubSubRequestType");
            //TW_Instance.SetDropdownValue(oCNR.cmbRequestType, reqstType, false, "Requisition Type");
            TW_Instance.SelectRequestType(reqstType, subReqstType, subSubReqstType, false, "Requisition Type");
            Thread.Sleep(5000);
            string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
            if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            {
                TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, false, "Name of Resource");
                Thread.Sleep(5000);
                TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResource + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            {
                TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, false, "Name of Resource");
                Thread.Sleep(10000);
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResrc + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            Thread.Sleep(5000);
          
            string TransferPopUpMsg = TW_Instance.GetElementText(oCNR.txtTransferDemandPopUp, "Similar Draft POP UP");
            TW_Instance.CompareValue(TransferPopUpMsg, "Whether you want requisition to transfer to your console", true);
            Environment.SetEnvironmentVariable("TransferedReqNum", TransferPopUpMsg.Split(' ')[2]);
            //TW_Instance.ClickElement(oCNR.btnTransferDemandPopUpNo,"No Btn");
            TW_Instance.ClickElement(oCNR.btnTransferDemandPopUpYes, "Yes Btn");
 
            Thread.Sleep(150000);
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            Thread.Sleep(100000);
            TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("TransferedReqNum"), true, "Requisition Number Search");
            TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            Thread.Sleep(10000);
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Button");
            Thread.Sleep(90000);
            //string projectName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
            TW_Instance.SelectListBoxValue(oCNR.txtProject, projectName, "Project");

            //string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if (positionExclsv.ToLower().Equals("exclusive"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                else
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");

                if (entityName.ToLower().Equals("bserv"))
                    RG_Instance.ReportStepLog("Bserv SOT/NON-SOT validation check.", "Pass", false);
            }
            if (entityName.ToLower().Equals("bserv"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                    {
                        RG_Instance.ReportStepLog("Demand type validation check.", "Pass", false);
                    }
                }
            }
            //TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, "3", "Project L1");
            //TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, "3", "Project L4");
            //TW_Instance.SelectRadiobutton(oCNR.rdbDemandTypeSOT, "Demand Type");
            //Thread.Sleep(2000);

            //string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
            if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
            {
                if (clientIntrvw.ToLower().Equals("yes"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
            }

            //string reqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            //TW_Instance.SetDropdownValue(oCNR.cmbRequestType, reqstType, false, "Requisition Type");
            TW_Instance.SelectRequestType(reqstType, subReqstType, subSubReqstType, false, "Requisition Type");
            Thread.Sleep(5000);
            //string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
            //if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, false, "Name of Resource");
            //    Thread.Sleep(5000);
            //    TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
            //    //TW_Instance.ClickElement(oCNR.txtNameOfResource + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            //}
            //else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, false, "Name of Resource");
            //    Thread.Sleep(5000);
            //    TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
            //    //TW_Instance.ClickElement(oCNR.txtNameOfResrc + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            //}

            //string tpState = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "TPState");
            //if (TW_Instance.IsObjectExist(oCNR.txtTPState, "TP State"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtTPState, tpState, true, "TP State");
            //}

            //TW_Instance.SetTextInElement(oCNR.txtAccountId, "123456", "Account ID");
            //TW_Instance.SetTextInElement(oCNR.txtOpprtnyId, "456789", "Opportunity ID");

            string skill = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill");
            if (TW_Instance.IsObjectExist(oCNR.txtSkill, "Skill"))
            {
                TW_Instance.SetValueInSkillPopUp(skill, "Skill");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtERSSkill, "Skill"))
            {
                TW_Instance.SetValueInERSSkillPopUp(skill, "Skill");
            }
            Thread.Sleep(5000);
            string job = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Job");
            TW_Instance.SetValueInJobFilter(job, "Job");
            Thread.Sleep(5000);
            string empGroup = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "EmployeeGroup");
            TW_Instance.SetDropdownValue(oCNR.cmbEmpGroup, "Full time FTE", true, "Employee Group");
            TW_Instance.SetDropdownValue(oCNR.cmbEmpGroup, empGroup, true, "Employee Group");

            TW_Instance.SetDropdownValue(oCNR.cmbBand, 1, false, "Band");
            TW_Instance.SetDropdownValue(oCNR.cmbSubBand, 1, false, "Sub Band");
            Thread.Sleep(2000);
            TW_Instance.SetDropdownValue(oCNR.cmbCountry, 1, false, "Country");
            TW_Instance.SetDropdownValue(oCNR.cmbCompanyName, 1, false, "Company Name");
            Thread.Sleep(5000);
            TW_Instance.SetDropdownValue(oCNR.cmbPSA, 1, false, "PSA");

            if (!TW_Instance.IsObjectReadOnly(oCNR.txtNoOfPosition, "No Of Position"))
            {
                string noOfPosition = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NoOfPosition");
                TW_Instance.SetTextInElement(oCNR.txtNoOfPosition, noOfPosition, true, "No. of Position");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbLanguage1 + "']", "Language1"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbLanguage1, 2, false, "Language1");
                TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade1, 1, false, "LanguageGrade1");
                //TW_Instance.SetDropdownValue(oCNR.cmbLanguage1, 1, true, "Language1");
                TW_Instance.SetDropdownValue(oCNR.cmbLanguage2, 3, false, "Language2");
                TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade2, 2, false, "LanguageGrade2");
                //TW_Instance.SetDropdownValue(oCNR.cmbLanguage2, 2, true, "Language2");
                TW_Instance.SetDropdownValue(oCNR.cmbLanguage3, 4, false, "Language3");
                TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade3, 3, false, "LanguageGrade3");
                //TW_Instance.SetDropdownValue(oCNR.cmbLanguage3, 3, true, "Language3");
            }

            if (entityName.ToLower().Equals("infra"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBU, 1, false, "Business Unit");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbDomain, 1, false, "Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbSubDomain, 1, false, "Sub Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbTRating, 1, false, "T Rating");
                TW_Instance.SetDropdownValue(oCNR.cmbFunction, 1, false, "Function");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbCustomerState + "']", "Customer State"))
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbCustomerState, 1, false, "Customer State");
                }
                if (TW_Instance.IsObjectExist(oCNR.txtReleaseDate, "Release Date"))
                {
                    TW_Instance.SetTextInElement(oCNR.txtReleaseDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), true, "Release Date");
                }

                //TW_Instance.SetTextInElement(oCNR.txtInfraFileUpload, GlobalVariable.datapoolPath + @"\TextFile\InfraTextFile.txt", "File");
            }

            if (entityName.ToLower().Equals("bserv"))
            {
                //if (TW_Instance.IsObjectEnabled(oCNR.cmbSalaryScale, "Salary Scale"))
                //{
                //    string salaryScale = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SalaryScale");
                //    TW_Instance.SetDropdownValue(oCNR.cmbSalaryScale,1, false, "Salary Scale");
                //}

                TW_Instance.SetDropdownValue(oCNR.cmbTypeOfRequirement, 1, false, "Type of Requirement");
                string maxSalary = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "MaxSalary");
                TW_Instance.SetTextInElement(oCNR.txMaximumSalary, maxSalary, true, "Maximum Salary");

                //if (TW_Instance.IsObjectEnabled("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
                // {
                //     //if (!TW_Instance.IsObjectReadOnly("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
                //    // {
                //         string salaryScale = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SalaryScale");
                //         TW_Instance.SetDropdownValue(oCNR.cmbSalaryScale, 1, false, "Salary Scale");
                //     //}
                // }


                //TW_Instance.ClickElement(oCNR.txtAssessmentCriteria, "Assessment Criteria");
                //Thread.Sleep(2000);
                //TW_Instance.ClickElement(oCNR.chkAssessCrt1, "Skill");
                //TW_Instance.SetTextInElement(oCNR.txtAssessCrt1, "5", "Threshold level");
                //TW_Instance.ClickElement(oCNR.btnAssCrtUpdate, "Update button");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbFresherType + "']", "Customer State"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbFresherType, 1, false, "Fresher Type");
            }

            //if (entityName.ToLower().Equals("tech"))
            //{
            //    if (TW_Instance.IsObjectExist(oCNR.fileUplod, "File Upload"))
            //    {
            //        TW_Instance.ClickElement(oCNR.fileUplod, "File Upload");
            //        TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Doc.txt");
            //    }
            //}

            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(30000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))
            {
                string similarDraftPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarDraft, "Similar Draft POP UP");
                TW_Instance.CompareValue(similarDraftPopUpMsg.Split('[')[0], "You already have back fill SR ", true);
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }
            Thread.Sleep(80000);

            //Screen 2           
            TW_Instance.SetDropdownValue(oCNR.cmbExperience, 1, false, "Experience");
            TW_Instance.SetDropdownValue(oCNR.cmbQualification, 1, false, "Qualification");
            TW_Instance.SetTextInElement(oCNR.txtJobDescription,"Test", true,"Job Discription");
            TW_Instance.SetDropdownValue(oCNR.cmbDesignation, 1, false, "Designation");
            TW_Instance.SetDropdownValue(oCNR.cmbPA, 1, false, "PA");
            TW_Instance.SetDropdownValue(oCNR.cmbCWL, 1, false, "Joining Location");

            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbState + "']", "Joining State"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbState, 2, false, "Joining State");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbCity, 1, false, "Joining City");
            }

            if (TW_Instance.IsObjectExist(oCNR.txtContractFromDate, "Contract From Date"))
            {
                TW_Instance.SetTextInElement(oCNR.txtContractFromDate, DateTime.Now.Date.AddDays(10).ToString("dd-MMM-yyyy"), false, "Contract From Date");
                TW_Instance.SetTextInElement(oCNR.txtContractToDate, DateTime.Now.Date.AddDays(90).ToString("dd-MMM-yyyy"), false, "Contract To Date");
            }

            if (TW_Instance.IsObjectExist(oCNR.txtBuyRate, "SOT Buy Rate"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBuyRate, "500", true, "Buy Rate");
                TW_Instance.SetTextInElement(oCNR.txtSellRate, "1000", true, "Sell Rate");
            }
            TW_Instance.SetDropdownValue(oCNR.cmbJoiningL1,1,false,"L1");
            TW_Instance.SetDropdownValue(oCNR.cmbJoiningL4,1,false,"L4");

            string consentForTP = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ConsentForTP");
            if (TW_Instance.IsObjectExist(oCNR.rdbNConsentForTP, "Consent For TP"))
            {
                if (consentForTP.ToLower() == "no")
                    TW_Instance.SelectRadiobutton(oCNR.rdbNConsentForTP, "Consent For TP - No");
                else if (consentForTP.ToLower() == "yes")
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbYConsentForTP, "Consent For TP - Yes");

                    TW_Instance.SetDropdownValue(oCNR.cmbTPBand, 2, false, "TP Band");
                    TW_Instance.SetDropdownValue(oCNR.cmbTPSubBand, 1, false, "TP Sub Band");
                    TW_Instance.SetDropdownValue(oCNR.cmbTPDesignation, 1, false, "TP Designation");
                    TW_Instance.SetTextInElement(oCNR.txtTPContractFrom, DateTime.Now.Date.ToString("dd-MMM-yyyy"), true, "Contract From");
                    TW_Instance.SetTextInElement(oCNR.txtTPContractTo, DateTime.Now.Date.AddDays(10).ToString("dd-MMM-yyyy"), true, "Contract To");
                    TW_Instance.SetTextInElement(oCNR.txtTPBuyRate, "500", true, "Buy Rate");
                    TW_Instance.SetTextInElement(oCNR.txtTPSellRate, "1000", true, "Sell Rate");
                }
            }

            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");

            //Screen 3
            Thread.Sleep(20000);
            string billingType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "BillingType");
            TW_Instance.SetDropdownValue(oCNR.cmbBillingType, billingType, true, "Billing Type");

            if (TW_Instance.IsObjectExist(oCNR.txtBillStartDate, "Bill Start Date"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBillStartDate, DateTime.Now.Date.AddDays(90).ToString("dd-MMM-yyyy"), false, "Bill Start Date");
            }

            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbDemandProbability + "']", "Demand Probability"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbDemandProbability, 2, false, "Demand Probability");
            }

            string interview1TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP1");
            TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP1, interview1TP1, true, "Interview 1 TP1");

            //string interview1TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP2");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP2, interview1TP2, "Interview 1 TP2");

            string interview2TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP1");
            TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP1, interview2TP1, true, "Interview 2 TP1");

            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");

            //string interview2TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP2");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP2, interview2TP2, "Interview 2 TP2");
            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            //Thread.Sleep(5000);

            if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            }

            Thread.Sleep(100000);
            string generatedReqNum = TW_Instance.GetPopupText();
            Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] {"successfully -"}, StringSplitOptions.None )[1].Replace(" ", ""));            
            Thread.Sleep(20000);
        }
        public void TPGRBbulkHiring()
        {
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Update Button");
            Thread.Sleep(40000);
            if(TW_Instance.IsObjectEnabled(oCNR.chkBulkHire,"Bulk Hiring"))
            {
                RG_Instance.ReportStepLog("Bulk Hiring check box Object is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("Bulk Hiring check box Object is disabled", "Pass", false);
            }
        }
        public void InfraEdit()
        {
            TW_Instance.ClickElement(oIA.btnReqEdit, "Edit Update Button");
            Thread.Sleep(40000);
            if (TW_Instance.IsObjectEnabled(oCNR.cmbRequestType, "Request Type"))
            {
                RG_Instance.ReportStepLog("Request Type Field is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("Request Type Field is disabled", "Pass", false);
            }
            TW_Instance.SetDropdownValue(oCNR.cmbEmpGroup, "Full time hourly", true, "Employee Group");
            TW_Instance.SetDropdownValue(oCNR.cmbBand, 2, false, "Band");
            TW_Instance.SetDropdownValue(oCNR.cmbSubBand, 1, false, "Sub Band");
            if (TW_Instance.IsObjectEnabled(oCNR.cmbCountry, "Country"))
            {
                RG_Instance.ReportStepLog("Country Field is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("Country Field is disabled", "Pass", false);
            }
            if (TW_Instance.IsObjectEnabled(oCNR.cmbBU, "BU"))
            {
                RG_Instance.ReportStepLog("BU Field is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("BU Field is disabled", "Pass", false);
            }
            if (TW_Instance.IsObjectEnabled(oCNR.cmbDomain, "Domain"))
            {
                RG_Instance.ReportStepLog("Domain Field is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("Domain Field is disabled", "Pass", false);
            }
            if (TW_Instance.IsObjectEnabled(oCNR.cmbSubDomain, "Sub Domain"))
            {
                RG_Instance.ReportStepLog("Sub Field Object is enabled", "Fail", true);
            }
            else
            {
                RG_Instance.ReportStepLog("Sub Field Object is disabled", "Pass", false);
            }
            TW_Instance.SetDropdownValue(oCNR.cmbTRating, 2, false, "T Rating");
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(10000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))
            {
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }

            Thread.Sleep(10000);
            //Screen 2          
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbState + "']", "Joining State"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbState, 2, false, "Joining State");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbCity, 1, false, "Joining City");
            }
            if (TW_Instance.IsObjectExist(oCNR.txtBuyRate, "SOT Buy Rate"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBuyRate, "500", true, "Buy Rate");
                TW_Instance.SetTextInElement(oCNR.txtSellRate, "1000", true, "Sell Rate");
            }
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");

            //Screen 3
            Thread.Sleep(10000);
            //if (bEditFlag)
            //{
            //    Thread.Sleep(20000);
            //    string billingType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ResubmitBillingType");
            //    TW_Instance.SetDropdownValue(oCNR.cmbBillingType, billingType, true, "Billing Type");
            //}
            TW_Instance.SetDropdownValue(oCNR.cmbBillingType, "Billable", true, "Billing Type");

            if (TW_Instance.IsObjectExist(oCNR.txtBillStartDate, "Bill Start Date"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBillStartDate, DateTime.Now.Date.AddDays(90).ToString("dd-MMM-yyyy"), false, "Bill Start Date");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbDemandProbability + "']", "Demand Probability"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbDemandProbability, 2, false, "Demand Probability");
            }
            TW_Instance.SetTextInElement(oCNR.txtEditRemarks, "Edit by Automation", true, "Edit Remarks");

            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            //Thread.Sleep(3000);
            //if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            //{
            //    TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            //}

            Thread.Sleep(19000);
            string generatedReqNum = string.Empty;
            Thread.Sleep(4000);
            if (TW_Instance.IsObjectExist(oCNR.lblReqNoText, "Similar Pop Up QA"))
            {
                Thread.Sleep(4000);
                generatedReqNum = TW_Instance.GetElementText(oCNR.lblReqNoText, "Pop Up Message");
            }
            else
            {
                Thread.Sleep(4000);
                generatedReqNum = TW_Instance.GetPopupText();
            }
            Thread.Sleep(5000);
        }
        public void ObsoleteSkillValidation()
        {
            Thread.Sleep(25000);
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            string reqType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
            //if (entityName.ToLower().Equals("bserv"))
            //{
            //    Thread.Sleep(25000);
            //}
            try
            {
                IList<string> wini = GlobalVariable.driver.WindowHandles.ToList();
                GlobalVariable.driver.SwitchTo().Window(wini.Last()).Close();
                Thread.Sleep(10000);
                GlobalVariable.driver.SwitchTo().Window(wini.First());
            }
            catch (Exception ex)
            {

            }

            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(2000);
            if (TW_Instance.IsObjectExist(oCNR.cmbEntityName, "Entity Name"))
            {
                TW_Instance.SelectDropdownValue(oCNR.cmbEntityName, 2, "Entity Name");
                TW_Instance.ClickElement(oCNR.btnSaveContinue, "Entity Name");
            }

            //Screen 1
            Thread.Sleep(15000);
            string projectName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
            TW_Instance.SelectListBoxValue(oCNR.txtProject, projectName, "Project");
            //Thread.Sleep(2000);
            if (entityName.ToLower().Equals("infra"))
            {
                bool bFlag = TW_Instance.IsObjectReadOnly(oCNR.txtProjectDescription, "Project Description");
                if (bFlag)
                    RG_Instance.ReportStepLog("Initiator is Able to View Project Description ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Initiator is Not Able to View Project Description ", "Fail", true);
            }
            string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if (positionExclsv.ToLower().Equals("exclusive"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                else
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");

                if (entityName.ToLower().Equals("bserv"))
                    RG_Instance.ReportStepLog("Bserv SOT/NON-SOT validation check.", "Pass", false);
            }
            if (entityName.ToLower().Equals("bserv"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                    {
                        RG_Instance.ReportStepLog("Demand type validation check.", "Pass", false);
                    }
                }
            }
            if (!entityName.ToLower().Equals("infra"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!demandType.ToLower().Equals("bulkhiring"))
                    {
                        TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 1, false, "Project L1");
                        TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 1, false, "Project L4");
                    }
                }
            }
            string PL1 = "";
            string PL4 = "";
            if (demandType.ToLower().Equals("sales"))
            {

                PL1 = TW_Instance.GetcmbElementText(oCNR.cmbProjectL1, "Project L1");

                PL4 = TW_Instance.GetcmbElementText(oCNR.cmbProjectL4, "Project L4");
                RG_Instance.ReportStepLog("Initiator is able to Edit PL1 & PL4", "Pass", false);
            }

            //TW_Instance.SelectRadiobutton(oCNR.rdbDemandTypeSOT, "Demand Type");
            //Thread.Sleep(2000);

            if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
            {
                string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
                if (clientIntrvw.ToLower().Equals("yes"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
            }

            string reqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string subReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubRequestType");
            string subSubReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubSubRequestType");

            TW_Instance.SelectRequestType(reqstType, subReqstType, subSubReqstType, false, "Requisition Type");
            //Thread.Sleep(5000);

            if (TW_Instance.IsObjectExist(oCNR.cmbprogramType, "Program Type For Fresher"))
            {
                TW_Instance.SelectDropdownValue(oCNR.cmbprogramType, 1, "Program Type");
                TW_Instance.SelectDropdownValue(oCNR.cmbcampusBatchYear, 1, "Campus Batch Year");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbSRNumber + "']", "SR Number Against Buffer"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSRNumber, 1, true, "SR Number Against Buffer");
            }
            Thread.Sleep(5000);

            if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, false, "Name of Resource");
                Thread.Sleep(5000);
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResource + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, false, "Name of Resource");
                Thread.Sleep(5000);
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResrc + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            if (TW_Instance.IsObjectExist(oCNR.txtTPState, "TP State"))
            {
                string tpState = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "TPState");
                TW_Instance.SetTextInElement(oCNR.txtTPState, tpState, true, "TP State");
            }

            //TW_Instance.SetTextInElement(oCNR.txtAccountId, "123456", "Account ID");
            //TW_Instance.SetTextInElement(oCNR.txtOpprtnyId, "456789", "Opportunity ID");
            if (TW_Instance.IsObjectExist(oCNR.chkBulkHire, "BSERV Bulk Hiring"))
            {
                TW_Instance.ClickElement(oCNR.chkBulkHire, "BSERV Bulk Hiring");
            }
            string skill = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill");
            
            if (TW_Instance.IsObjectExist(oCNR.txtSkill, "Skill"))
            {

                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {
                    TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");
                }
                else
                {
                    string skill1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill1");
                    TW_Instance.SetValueInSkillPopUp4ObsoleteSkillValidation(skill, skill1, "Skill");
                }
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
            {

                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {
                    if (demandType.ToLower().Equals("mode2"))
                    {
                        TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");
                    }
                    if (demandType.ToLower().Equals("sales"))
                    {
                        string skill1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill1");
                        string skill2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill2");
                        string skill3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill3");
                        TW_Instance.SetMultiValueInSkillPopUpSales(skill1, skill2, skill3, "Skill");
                    }
                    else
                    {
                        TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                    }
                }
                else
                {
                    TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                }
            }

            Thread.Sleep(2000);
        }
        public void CreateNewBufferRequisition()
        {
            Thread.Sleep(35000);
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            string reqType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
           
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            Thread.Sleep(2000);
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(2000);
            if (TW_Instance.IsObjectExist(oCNR.cmbEntityName, "Entity Name"))
            {
                TW_Instance.SelectDropdownValue(oCNR.cmbEntityName, 1, "Entity Name");
                TW_Instance.ClickElement(oCNR.btnSaveContinue, "Entity Name");
            }

            //Screen 1
            Thread.Sleep(15000);
            string projectName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
            TW_Instance.SelectListBoxValue(oCNR.txtProject, projectName, "Project");
            //Thread.Sleep(2000);
            if (entityName.ToLower().Equals("infra"))
            {
                bool bFlag = TW_Instance.IsObjectReadOnly(oCNR.txtProjectDescription, "Project Description");
                if (bFlag)
                    RG_Instance.ReportStepLog("Initiator is Able to View Project Description ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Initiator is Not Able to View Project Description ", "Fail", true);
            }
            string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if (positionExclsv.ToLower().Equals("exclusive"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                else
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");

                if (entityName.ToLower().Equals("bserv"))
                    RG_Instance.ReportStepLog("Bserv SOT/NON-SOT validation check.", "Pass", false);
            }
            if (entityName.ToLower().Equals("bserv"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                    {
                        RG_Instance.ReportStepLog("Demand type validation check.", "Pass", false);
                    }
                }
            }
            if (!entityName.ToLower().Equals("infra"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!demandType.ToLower().Equals("bulkhiring"))
                    {
                        TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 1, false, "Project L1");
                        TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 1, false, "Project L4");
                    }
                }
            }
            string PL1 = "";
            string PL4 = "";
            if (demandType.ToLower().Equals("sales") || demandType.ToLower().Contains("tss"))
            {

                PL1 = TW_Instance.GetcmbElementText(oCNR.cmbProjectL1, "Project L1");
                TW_Instance.IsObjectEnabled(oCNR.cmbProjectL1x, "Project L1");
                PL4 = TW_Instance.GetcmbElementText(oCNR.cmbProjectL4, "Project L4");
                TW_Instance.IsObjectEnabled(oCNR.cmbProjectL4x, "Project L4");
                RG_Instance.ReportStepLog("Initiator is able to Edit PL1 & PL4", "Pass", false);
            }

            //TW_Instance.SelectRadiobutton(oCNR.rdbDemandTypeSOT, "Demand Type");
            //Thread.Sleep(2000);

            if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
            {
                string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
                if (clientIntrvw.ToLower().Equals("yes"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
            }

            string reqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string subReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubRequestType");
            string subSubReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubSubRequestType");

            TW_Instance.SelectRequestType(reqstType, subReqstType, subSubReqstType, false, "Requisition Type");

            if (TW_Instance.IsObjectExist(oCNR.cmbprogramType, "Program Type For Fresher"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbProgramType, 1, true, "Program Type");
                TW_Instance.SetDropdownValue(oCNR.cmbBatchYear, 1, true, "Campus Batch Year");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbSRNumber + "']", "SR Number Against Buffer"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSRNumber, 1, true, "SR Number Against Buffer");
            }
            Thread.Sleep(5000);

            if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, false, "Name of Resource");
                Thread.Sleep(5000);
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResource + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, false, "Name of Resource");
                Thread.Sleep(5000);
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResrc + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            if (TW_Instance.IsObjectExist(oCNR.txtTPState, "TP State"))
            {
                string tpState = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "TPState");
                TW_Instance.SetTextInElement(oCNR.txtTPState, tpState, true, "TP State");
            }

            //TW_Instance.SetTextInElement(oCNR.txtAccountId, "123456", "Account ID");
            //TW_Instance.SetTextInElement(oCNR.txtOpprtnyId, "456789", "Opportunity ID");
            if (TW_Instance.IsObjectExist(oCNR.chkBulkHire, "BSERV Bulk Hiring"))
            {
                TW_Instance.ClickElement(oCNR.chkBulkHire, "BSERV Bulk Hiring");
            }
            string skill = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill");

            if (TW_Instance.IsObjectExist(oCNR.txtSkill, "Skill"))
            {

                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {
                    TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");
                }
                else
                {
                    TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                }
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
            {

                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {
                    if (demandType.ToLower().Equals("mode2"))
                    {
                        TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");
                    }
                    if (demandType.ToLower().Equals("sales"))
                    {
                        string skill1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill1");
                        string skill2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill2");
                        string skill3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill3");
                        TW_Instance.SetMultiValueInSkillPopUpSales(skill1, skill2, skill3, "Skill");
                    }
                    else
                    {
                        TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                    }
                }
                else
                {
                    TW_Instance.SetValueInSkillPopUp(skill, "Skill");
                }
            }

            Thread.Sleep(2000);
            //string job = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Job");
            //if (TW_Instance.IsObjectReadOnly(oCNR.icnfilterJob, "Job"))
            //{
            //    TW_Instance.SetValueInJobFilter(job, "Job");
            //}

            Thread.Sleep(2000);

            //string empGroup = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "EmployeeGroup");
            //string band = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Band");
            //TW_Instance.SetDropdownValue(oCNR.cmbEmpGroup, empGroup, false, "Employee Group");
            //if (demandType.ToLower().Contains("tss") || demandType.ToLower().Contains("tpappchng"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbBand, band, false, "Band");
            //}
            //else
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbBand, 1, false, "Band");
            //}
            //if (demandType.ToLower().Contains("tpappchng"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSubBand, "E1", false, "Sub Band");
            //}
            //else
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSubBand, 1, false, "Sub Band");
            //}

            //Thread.Sleep(2000);
            //string country = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Country");
            //TW_Instance.SetDropdownValue(oCNR.cmbCountry, country, false, "Country");
            //if (demandType.ToLower().Contains("slfresher"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbCountry, "Sri Lanka", false, "Country");
            //}
            //else
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbCountry, "INDIA", false, "Country");
            //}            
            //TW_Instance.SetDropdownValue(oCNR.cmbCompanyName, 1, false, "Company Name");
            //Thread.Sleep(2000);
            //TW_Instance.SetDropdownValue(oCNR.cmbPSA, 1, false, "PSA");
            //if (!demandType.ToLower().Contains("slfresher"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSecPSA, 2, false, "Secondary PSA");
            //}

            //if (!TW_Instance.IsObjectReadOnly(oCNR.txtNoOfPosition, "No Of Position"))
            //{
            //    string noOfPosition = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NoOfPosition");
            //    TW_Instance.SetTextInElement(oCNR.txtNoOfPosition, noOfPosition, true, "No. of Position");
            //}
            //if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbLanguage1 + "']", "Language1"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguage1, 2, false, "Language1");
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade1, 1, false, "LanguageGrade1");
            //    //TW_Instance.SetDropdownValue(oCNR.cmbLanguage1, 1, true, "Language1");
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguage2, 3, false, "Language2");
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade2, 2, false, "LanguageGrade2");
            //    //TW_Instance.SetDropdownValue(oCNR.cmbLanguage2, 2, true, "Language2");
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguage3, 4, false, "Language3");
            //    TW_Instance.SetDropdownValue(oCNR.cmbLanguageGrade3, 3, false, "LanguageGrade3");
            //    //TW_Instance.SetDropdownValue(oCNR.cmbLanguage3, 3, true, "Language3");
            //}

            if (entityName.ToLower().Equals("infra"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBU, 2, false, "Business Unit");
                TW_Instance.SetDropdownValue(oCNR.cmbSecBU, 1, false, "Secondary Business Unit");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbDomain, 2, false, "Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbSecDomain, 1, false, "Secondary Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbSubDomain, 2, false, "Sub Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbSecSubDomain, 1, false, "Secondary Sub Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbTRating, 1, false, "T Rating");
                TW_Instance.SetDropdownValue(oCNR.cmbSecTRating, 1, false, "Secondary T Rating");
                TW_Instance.SetDropdownValue(oCNR.cmbFunction, 2, false, "Function");
                TW_Instance.SetDropdownValue(oCNR.cmbSecFunction, 1, false, "Secondary Function");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbCustomerState + "']", "Customer State"))
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbCustomerState, 1, false, "Customer State");
                }
                if (TW_Instance.IsObjectExist(oCNR.txtReleaseDate, "Release Date"))
                {
                    TW_Instance.SetTextInElement(oCNR.txtReleaseDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), true, "Release Date");
                }

                Thread.Sleep(10000);
                //TW_Instance.SetFileToUpload(oCNR.txtFIleUpload, GlobalVariable.datapoolPath + @"\TextFile\Doc.txt","File Upload");
                TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Doc.txt");
            }

            //if (entityName.ToLower().Equals("bserv"))
            //{
            //    //if (TW_Instance.IsObjectEnabled(oCNR.cmbSalaryScale, "Salary Scale"))
            //    //{
            //    //    string salaryScale = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SalaryScale");
            //    //    TW_Instance.SetDropdownValue(oCNR.cmbSalaryScale, 1, false, "Salary Scale");
            //    //}

            //    if (TW_Instance.IsObjectEnabled("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
            //    {
            //        if (!TW_Instance.IsObjectReadOnly("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
            //        {
            //            string salaryScale = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SalaryScale");
            //            TW_Instance.SetDropdownValue(oCNR.cmbSalaryScale, 1, false, "Salary Scale");
            //        }
            //    }
            //    string maxSalary = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "MaxSalary");
            //    TW_Instance.SetTextInElement(oCNR.txMaximumSalary, maxSalary, true, "Maximum Salary");
            //    TW_Instance.SetDropdownValue(oCNR.cmbTypeOfRequirement, 1, false, "Type of Requirement");
            //    TW_Instance.SetDropdownValue(oCNR.cmbSource, 1, false, "Source");
            //    //TW_Instance.ClickElement(oCNR.txtAssessmentCriteria, "Assessment Criteria");
            //    //Thread.Sleep(2000);
            //    //TW_Instance.ClickElement(oCNR.chkAssessCrt1, "Skill");
            //    //TW_Instance.SetTextInElement(oCNR.txtAssessCrt1, "5", "Threshold level");
            //    //TW_Instance.ClickElement(oCNR.btnAssCrtUpdate, "Update button");
            //}
            //if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbFresherType + "']", "Customer State"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbFresherType, 1, false, "Fresher Type");
            //}

            //if (entityName.ToLower().Equals("tech"))
            //{
            //    if (TW_Instance.IsObjectExist(oCNR.fileUplod, "File Upload"))
            //    {
            //        TW_Instance.ClickElement(oCNR.fileUplod, "File Upload");
            //        TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Doc.txt");
            //    }
            //}

            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(30000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))
            {
                string similarDraftPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarDraft, "Similar Draft POP UP");
                TW_Instance.CompareValue(similarDraftPopUpMsg.Split('[')[0], "You already have back fill SR ", true);
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }
            Thread.Sleep(30000);

            //Screen 2           
            //TW_Instance.SetDropdownValue(oCNR.cmbExperience, 1, false, "Experience");
            //TW_Instance.SetDropdownValue(oCNR.cmbQualification, 1, false, "Qualification");
            if (entityName.ToLower().Equals("infra") || entityName.ToLower().Equals("bserv"))
            {
                TW_Instance.SetTextInElement(oCNR.txtJobDescription, "Test", true, "Job Description");
            }
            //TW_Instance.SetDropdownValue(oCNR.cmbDesignation, 1, false, "Designation");
            //TW_Instance.SetDropdownValue(oCNR.cmbPA, 1, false, "PA");
            //if (!demandType.ToLower().Contains("slfresher"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSecPA, 2, false, "Secondary PA");
            //}
            //TW_Instance.SetDropdownValue(oCNR.cmbCWL, 1, false, "CWL");
            //string cwl = TW_Instance.GetcmbElementText(oCNR.cmbCWL, "CWL");
            //cwl = cwl.Replace(" ", "").Replace("\r\n", "");

            //if (cwl.ToLower().Equals("clientlocation"))
            //{
            //    TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
            //    TW_Instance.SetDropdownValue(oCNR.cmbCWL, 2, true, "CWL");
            //}
            //if (!demandType.ToLower().Contains("slfresher"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSecCWL, 1, false, "Secondary CWL");
            //}

            //if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbState + "']", "Joining State"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbState, 2, false, "Joining State");
            //    Thread.Sleep(10000);
            //    TW_Instance.SetDropdownValue(oCNR.cmbCity, 1, false, "Joining City");
            //}

            //if (TW_Instance.IsObjectExist(oCNR.txtContractFromDate, "Contract From Date"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtContractFromDate, DateTime.Now.Date.AddDays(10).ToString("dd-MMM-yyyy"), false, "Contract From Date");
            //    TW_Instance.SetTextInElement(oCNR.txtContractToDate, DateTime.Now.Date.AddDays(90).ToString("dd-MMM-yyyy"), false, "Contract To Date");
            //    TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
            //}

            //if (TW_Instance.IsObjectExist(oCNR.txtBuyRate, "SOT Buy Rate"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtBuyRate, "500", true, "Buy Rate");
            //    TW_Instance.SetTextInElement(oCNR.txtSellRate, "1000", true, "Sell Rate");
            //}

            //TW_Instance.SetDropdownValue(oCNR.cmbJoiningL1, 2, false, "Joining L1");
            //TW_Instance.SetDropdownValue(oCNR.cmbJoiningL4, 1, false, "Joining L4");
            //string JL1 = "";
            //string JL4 = "";
            //if (demandType.ToLower().Contains("tpappchng"))
            //{
            //    JL1 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL1, "Joining L1");
            //    JL4 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL4, "Joining L4");
            //}
            //if (demandType.ToLower().Equals("sales") || demandType.ToLower().Contains("tss"))
            //{
            //    TW_Instance.IsObjectEnabled(oCNR.cmbJoiningL1x, "Joining L1");
            //    TW_Instance.IsObjectEnabled(oCNR.cmbJoiningL4x, "Joining L4");
            //    JL1 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL1, "Joining L1");
            //    JL4 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL4, "Joining L4");
            //    PL1 = PL1.Split('(')[0].Replace(" ", "");
            //    PL4 = PL4.Split('(')[0].Replace(" ", "");
            //    JL1 = JL1.Replace(" ", "");
            //    JL4 = JL4.Replace(" ", "");
            //    TW_Instance.CompareValue(PL1, JL1, true);
            //    TW_Instance.CompareValue(PL4, JL4, true);
            //    RG_Instance.ReportStepLog("PL1 & PL4 values are same as JL1 & JL4", "Pass", false);
            //}

            //if (TW_Instance.IsObjectExist(oCNR.rdbNConsentForTP, "Consent For TP"))
            //{
            //    string consentForTP = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ConsentForTP");
            //    if (consentForTP.ToLower() == "no")
            //        TW_Instance.SelectRadiobutton(oCNR.rdbNConsentForTP, "Consent For TP - No");
            //    else if (consentForTP.ToLower() == "yes")
            //    {
            //        TW_Instance.SelectRadiobutton(oCNR.rdbYConsentForTP, "Consent For TP - Yes");

            //        TW_Instance.SetDropdownValue(oCNR.cmbTPBand, 2, false, "TP Band");
            //        TW_Instance.SetDropdownValue(oCNR.cmbTPSubBand, 1, false, "TP Sub Band");
            //        TW_Instance.SetDropdownValue(oCNR.cmbTPDesignation, 1, false, "TP Designation");
            //        TW_Instance.SetTextInElement(oCNR.txtTPContractFrom, DateTime.Now.Date.ToString("dd-MMM-yyyy"), true, "Contract From");
            //        TW_Instance.SetTextInElement(oCNR.txtTPContractTo, DateTime.Now.Date.AddDays(10).ToString("dd-MMM-yyyy"), true, "Contract To");
            //        TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
            //        TW_Instance.SetTextInElement(oCNR.txtTPBuyRate, "500", true, "Buy Rate");
            //        TW_Instance.SetTextInElement(oCNR.txtTPSellRate, "1000", true, "Sell Rate");
            //    }
            //}

            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");
            TW_Instance.HandlePopupAlert("Accept");

            //Screen 3
            Thread.Sleep(20000);
            //string billingType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "BillingType");
            //TW_Instance.SetDropdownValue(oCNR.cmbBillingType, billingType, true, "Billing Type");

            //if (TW_Instance.IsObjectExist(oCNR.txtBillStartDate, "Bill Start Date"))
            //{
            //    TW_Instance.SetTextInElement(oCNR.txtBillStartDate, DateTime.Now.Date.AddDays(90).ToString("dd-MMM-yyyy"), false, "Bill Start Date");
            //}
            //TW_Instance.SetTextInElement(oCNR.txtOnBoardingDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), false, "On Boarding Date");
            //if (entityName.ToLower().Equals("tech") || entityName.ToLower().Equals("infra"))
            //{
            //    TW_Instance.ClickElement(oCNR.txtGRAID, "GRA ID");
            //}
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbDemandProbability + "']", "Demand Probability"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbDemandProbability, 2, false, "Demand Probability");
            }

            //if (entityName.ToLower().Equals("tech"))
            //{
            //    TW_Instance.ClickElement(oCNR.txtRM, "Reporting Manager");
            //    Thread.Sleep(2000);
            //    TW_Instance.ClickElement(oCNR.lstRMnames, "RM Name");
            //}
            //TW_Instance.ClickElement(oCNR.txtInterviewer1TP2, "TP2");
            //string interview1TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP1");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP1, interview1TP1, true, "Interview 1 TP1");

            ////string interview1TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP2");
            ////TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP2, interview1TP2, "Interview 1 TP2");

            //string interview2TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP1");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP1, interview2TP1, true, "Interview 2 TP1");

            //string interview2TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP2");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP2, interview2TP2, "Interview 2 TP2");
            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            Thread.Sleep(5000);

            if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            }
            else if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUpQA, "Similar Pop Up QA"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUpQA, "Similar Pop Up");
            }

            string generatedReqNum = string.Empty;

            if (TW_Instance.IsObjectExist(oCNR.lblReqNoText, "Similar Pop Up QA"))
            {
                Thread.Sleep(2000);
                generatedReqNum = TW_Instance.GetElementText(oCNR.lblReqNoText, "Pop Up Message");
            }
            else
            {
                Thread.Sleep(2000);
                generatedReqNum = TW_Instance.GetPopupText();
            }

            Thread.Sleep(2000);
            //if (demandType.ToLower().Equals("sot"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog("Initiator is able to raise SOT demand and able enter buy & Sell rate.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Initiator is unable to raise SOT demand", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("bypass"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog("Initiator is able to raise a demand Without Interviewer Validation", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Initiator is unable to raise a demand Without Interviewer Validation", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("y"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog("Apps & SI Initiator is able to raise ProActive demand on Y code Projects.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Apps & SI Initiator is unable to raise ProActive demand on Y code Projects.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("apps"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog("Apps & SI Initiator is able to raise ProActive demand on Bench Projects.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Apps & SI Initiator is unable to raise ProActive demand on Bench Projects.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("others"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is able to raise TP Replacement demand on Others Projects.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is unable to raise TP Replacement demand on Others Projects.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("ftctofte"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is able to raise FTC to FTE Requisition.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is unable to raise FTC to FTE Requisition.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("c3i"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator is able to raise C3i Requisition.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator is unable to raise C3i Requisition.", "Fail", true);
            //}

            //if (demandType.ToLower().Equals("freshertagging"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator is able to select Fresher Type Field .", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator is unable to select Fresher Type Field.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("indbusinlob"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator is able to create proactive Requisition in India Business Lobs.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator is unable to create proactive Requisition in India Business Lobs.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("leaner"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator is able to create Requisition in Leaner SR.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator is unable to create Requisition in Leaner SR.", "Fail", true);
            //}
            //if (demandType.ToLower().Equals("mode2"))
            //{
            //    bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
            //    if (bFlag)
            //        RG_Instance.ReportStepLog(" Initiator is able to Select Mode2 Skill.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog(" Initiator is unable to Select Mode2 Skill.", "Fail", true);
            //}
            Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] { "/2020/" }, StringSplitOptions.None)[1].Split('.')[0].Replace(" ", ""));

            ////Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] { "requisition number is" }, StringSplitOptions.None)[1].Replace(" ", ""));
            //if (demandType.ToLower().Equals("itappopup"))
            //{
            //    TW_Instance.CompareValue(generatedReqNum, "Redirecting to iTAP EDGE", true);
            //    Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] { "/2020/" }, StringSplitOptions.None)[1].Replace(" ", ""));
            //    Thread.Sleep(35000);
            //    IList<string> winTab = GlobalVariable.driver.WindowHandles.ToList();
            //    GlobalVariable.driver.SwitchTo().Window(winTab.Last());
            //    RG_Instance.ReportStepLog("Initiator is able to See iTAP EDGE Page", "Pass", true);
            //    GlobalVariable.driver.SwitchTo().Window(winTab.Last()).Close();
            //    Thread.Sleep(10000);
            //    GlobalVariable.driver.SwitchTo().Window(winTab.First());
            //}
            //if (demandType.ToLower().Equals("bservapproval"))
            //{
            //    Thread.Sleep(100000);
            //    TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            //    TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            //    Thread.Sleep(10000);
            //    TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            //    TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            //    Thread.Sleep(10000);
            //    TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
            //    Thread.Sleep(50000);
            //    TW_Instance.ClickElement(oCNR.lnkInterviewerTab, "Interviewer TAB");
            //    Thread.Sleep(5000);
            //    string app3 = TW_Instance.GetElementText(oCNR.lblApprover3, "Approver3");
            //    string app4 = TW_Instance.GetElementText(oCNR.lblApprover4, "Approver4");
            //    TW_Instance.ClickElement(oCNR.btnClose, "Req Details");
            //    string approver3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver3");
            //    string approver4 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver4");
            //    bool bFlag = TW_Instance.CompareValue(app3, approver3, true);
            //    bool bFlag1 = TW_Instance.CompareValue(app4, approver4, true);
            //    if (bFlag & bFlag1)
            //        RG_Instance.ReportStepLog("Bserv Approval Hierarchy Working As Exepected.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Bserv Approval Hierarchy is Not Working As Exepected.", "Fail", true);
            //}

            //if (demandType.ToLower().Equals("bservapproval1"))
            //{
            //    Thread.Sleep(100000);
            //    TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            //    TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
            //    Thread.Sleep(10000);
            //    TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
            //    TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
            //    Thread.Sleep(10000);
            //    TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
            //    Thread.Sleep(50000);
            //    TW_Instance.ClickElement(oCNR.lnkInterviewerTab, "Interviewer TAB");
            //    Thread.Sleep(5000);
            //    string app3 = TW_Instance.GetElementText(oCNR.lblApprover2, "Approver3");
            //    string app4 = TW_Instance.GetElementText(oCNR.lblApprover3, "Approver4");
            //    TW_Instance.ClickElement(oCNR.btnClose, "Req Details");
            //    string approver3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver3");
            //    string approver4 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver4");
            //    bool bFlag = TW_Instance.CompareValue(app3, approver3, true);
            //    bool bFlag1 = TW_Instance.CompareValue(app4, approver4, true);
            //    if (bFlag & bFlag1)
            //        RG_Instance.ReportStepLog("Bserv Approval Hierarchy Working As Exepected.", "Pass", false);
            //    else
            //        RG_Instance.ReportStepLog("Bserv Approval Hierarchy is Not Working As Exepected.", "Fail", true);
            //}
            Thread.Sleep(40000);
        }

    }
}
