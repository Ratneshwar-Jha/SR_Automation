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
    class BC_CreateRequistion
    {
        //Initializing Library
        ReportGenerator RG_Instance = new ReportGenerator();
        ExcelFunction EF_Instance = new ExcelFunction();
        ToolWrapper TW_Instance = new ToolWrapper();
        GenericFunction GF_Instance = new GenericFunction();

        //Initializing OR
        OR_CreateRequisition oCNR = new OR_CreateRequisition();
        OR_InOutApp oLP = new OR_InOutApp();
        BC_InOutApp bcIOA = new BC_InOutApp();
        OR_ApproverActions oAA = new OR_ApproverActions();
        public void CreateNewRequisition()
        {
            string entityName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Entity");
            string reqType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
                
            TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
            TW_Instance.ClickElement(oCNR.lnkCreateNewReq, "Create New Requisition");
            Thread.Sleep(50000);
            if (TW_Instance.IsObjectExist(oCNR.cmbEntityName, "Entity Name"))
            {
                TW_Instance.SelectDropdownValue(oCNR.cmbEntityName, 1, "Entity Name");
                TW_Instance.ClickElement(oCNR.btnSaveContinue, "Entity Name");
            }
            if (TW_Instance.IsObjectExist(oLP.txtLoginId, "Login ID"))
            {
                string userName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "UserName");
                string StagingSRpassword = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Login", 1, "StagingSRpassword");

                TW_Instance.SetTextInElement(oLP.txtLoginId, userName, true, "Username");
                TW_Instance.SetTextInElement(oLP.txtPassword, StagingSRpassword, true, "StagingSRpassword");
                IWebElement obj_p = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Password']"));
                obj_p.SendKeys(Keys.Tab);
                TW_Instance.ClickElement(oLP.btnLogin, "Login");
                Thread.Sleep(50000);
            }
            //Screen 1
            string projectName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ProjectName");
            TW_Instance.SelectListBoxValue(oCNR.txtProject, projectName, "Project");
            if (entityName.ToLower().Equals("infra"))
            {
                Thread.Sleep(5000);
                bool bFlag = TW_Instance.IsObjectReadOnly(oCNR.txtProjectDescription, "Project Description");
                if (bFlag)
                    RG_Instance.ReportStepLog("Initiator is Able to View Project Description ", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Initiator is Not Able to View Project Description ", "Fail", true);
            }
            if (TW_Instance.IsObjectExist(oCNR.rdbIsOpportunity100kyes, "100k?"))
            {
                if (demandType.ToLower().Equals("exdpy"))
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbIsOpportunity100kyes, "Is opportunity 100k? yes");
                }
                else if (demandType.ToLower().Equals("exdpn"))
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbIsOpportunity100kno, "Is opportunity 100k? no");
                }
                else 
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbIsOpportunity100kyes, "Is opportunity 100k? yes");
                }
            }
            string positionExclsv = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "PositionExclusive");
            if (TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
            {
                if(positionExclsv.ToLower().Equals("exclusive"))
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbExclusive, "Exclusive");
                    RG_Instance.ReportStepLog("System is prompt for Exclusive question", "Pass", false);
                }
                    
                else
                {
                    TW_Instance.SelectRadiobutton(oCNR.rdbNonExclusive, "Non-Exclusive");
                    RG_Instance.ReportStepLog("System is prompt for Exclusive question", "Pass", false);
                }                    

                if (entityName.ToLower().Equals("bserv"))
                    RG_Instance.ReportStepLog("Bserv SOT/NON-SOT validation check.", "Pass", false);
            }
            if (entityName.ToLower().Equals("bserv"))
            {
                if(projectName.ToLower().Equals("others (6863)"))
                {
                    if(! TW_Instance.IsObjectExist(oCNR.rdbExclusive, "Exclusive"))
                    {
                        RG_Instance.ReportStepLog("Demand type validation check.", "Pass", false);
                    }
                }
            }
            if (!entityName.ToLower().Equals("infra"))
            {
                if (projectName.ToLower().Equals("others (6863)"))
                {
                    if (!demandType.ToLower().Equals("bulkhiring") && !demandType.ToLower().Equals("hdhiring"))
                    {
                        if (demandType.ToLower().Equals("sappractmode2"))
                        {
                            TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 53, false, "Project L1");
                            TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 2, false, "Project L4");
                        }
                        else if (!demandType.ToLower().Equals("slschool"))
                        {
                            TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 1, false, "Project L1");
                            TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 1, false, "Project L4");
                        }
                    }
                }
            }
            if (projectName.ToLower().Equals("others (6863)"))
            {
                if (demandType.ToLower().Equals("hdhiring") || demandType.ToLower().Equals("showbutoinfra"))
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 1, false, "Project L1");
                    TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 2, false, "Project L4");
                }
                else if (demandType.ToLower().Equals("conversiontofte"))
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbProjectL1, 2, false, "Project L1");
                    TW_Instance.SetDropdownValue(oCNR.cmbProjectL4, 2, false, "Project L4");
                }
            }           
            if (TW_Instance.IsObjectExist(oCNR.rdbClientIntrvwYes, "Client Interview"))
            {
                string clientIntrvw = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ClientInterview");
                if (clientIntrvw.ToLower().Equals("yes"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbClientIntrvwYes, "Client Interview - Yes");
            }

            string reqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "RequestType");
            string subReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubRequestType");
            string subSubReqstType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SubSubRequestType");
            
            TW_Instance.SelectRequestType(reqstType, subReqstType, subSubReqstType, false , "Requisition Type");

            if (TW_Instance.IsObjectExist(oCNR.cmbxTransferTo, "Transfer To"))
            {
                string transferTo = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "TransferTo");
                TW_Instance.SetDropdownValue(oCNR.cmbTransferTo, transferTo, true, "Transfer To");
            }

            if (TW_Instance.IsObjectExist(oCNR.cmbxProgramType, "Program Type For Fresher"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbProgramType, 1, true, "Program Type");
                TW_Instance.SetDropdownValue(oCNR.cmbBatchYear, "2023", false, "Campus Batch Year");
            }
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbSRNumber + "']", "SR Number Against Buffer"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSRNumber, 1, true, "SR Number Against Buffer");
            }
            
            if (TW_Instance.IsObjectExist(oCNR.txtNameOfResource, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResource, resName, false, "Name of Resource");
                
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResource + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtNameOfResrc, "Name of Resource"))
            {
                string resName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NameOfResource");
                TW_Instance.SetTextInElement(oCNR.txtNameOfResrc, resName, false, "Name of Resource");
                
                //TW_Instance.PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//input[@id='TPSAPCODE']/following::div[@class='tt-menu tt-open']/div[1]/div[1]")));
                //TW_Instance.ClickElement(oCNR.txtNameOfResrc + "/following::div[@class='tt-menu tt-open']/div[1]/div[1]", "SAP Name");
            }           
            if (TW_Instance.IsObjectExist(oCNR.txtTPState, "TP State"))
            {
                string tpState = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "TPState");
                TW_Instance.SetTextInElement(oCNR.txtTPState, tpState, true, "TP State");
            }
            if (TW_Instance.IsObjectExist(oCNR.txtAccountId, "Account ID"))
            {
                TW_Instance.SetTextInElement(oCNR.txtAccountId, "Google Inc (001U000000XY9vjIAD )", false, "Account ID");
                TW_Instance.SetTextInElement(oCNR.txtOpprtnyId, "Google enterprise support (OHCLP080283 )", false, "Opportunity ID");
            }
            if (TW_Instance.IsObjectExist(oCNR.chkBulkHire, "BSERV Bulk Hiring"))
            {
                TW_Instance.ClickElement(oCNR.chkBulkHire, "BSERV Bulk Hiring");
            }
            string skill = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill");
            string skill1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill1");
            string skill2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill2");
            string skill3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Skill3");
            if (TW_Instance.IsObjectExist(oCNR.txtSkill, "Skill"))
            {
                
                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {                                       
                        TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");

                }
                else
                {
                    if (demandType.ToLower().Contains("sappractmode2"))
                    {
                        TW_Instance.SetMultiValueInSkillPopUpAppsSI(skill, skill1, skill2, skill3, "Skill");
                    }
                    else
                    {
                        TW_Instance.SetValueInSkillPopUp(skill, "Skill");

                    }

                }
            }
            else if (TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
            {
               
                if (entityName.ToLower().Equals("tech") && TW_Instance.IsObjectExist(oCNR.txtERSSkill, "ERS Skill"))
                {                 
                    if(demandType.ToLower().Equals("mode2"))
                    {
                        TW_Instance.SetMultiValueInSkillPopUp(skill, "Skill");
                    }
                    if (demandType.ToLower().Equals("sales") || demandType.ToLower().Equals("hdhiring"))
                    {                        
                        TW_Instance.SetMultiValueInSkillPopUpSales(skill1, skill2, skill3, "Skill");
                    }
                    if (demandType.ToLower().Equals("appsiskill") || demandType.ToLower().Equals("sappractmode2"))
                    {
                        TW_Instance.SetMultiValueInSkillPopUpAppsSI(skill, skill1, skill2, skill3, "Skill");
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
            if(demandType.ToLower().Equals("hdhiring"))
            {
                TW_Instance.ClickElement(oCNR.cmbXJob, "Job");
                TW_Instance.ClickElement(oCNR.jobListItem, "Job");
            }
            else if(demandType.ToLower().Equals("cyberjob"))
            {
                string job = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Job");
                TW_Instance.SetValueInJobFilter("Junior Developer", "Job");
                if(TW_Instance.IsObjectExist(oCNR.txtWarngMsgJob,"Warning Msg CrberSec Project"))
                {
                    RG_Instance.ReportStepLog("Initiator is able to See Warning Msg to Select Only CyberSec Job", "Pass", false);
                    TW_Instance.ClickElement(oCNR.lnkRemoveJob, "Remove Job Field");
                    TW_Instance.SetValueInJobFilter(job, "Job");
                }
            }
            else
            {
                string job = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Job");
                TW_Instance.SetValueInJobFilter(job, "Job");
                RG_Instance.ReportStepLog("Job selected successfully", "Pass", false);
            }
            Thread.Sleep(2000);
            string empGroup = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "EmployeeGroup");
            string band = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Band");
            Thread.Sleep(8000);
            TW_Instance.SetDropdownValue(oCNR.cmbEmpGroup, empGroup, false, "Employee Group");
            RG_Instance.ReportStepLog("Employee Group selected successfully", "Pass", false);
            if (demandType.ToLower().Equals("tss") || demandType.ToLower().Equals("tpappchng"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBand, band, false, "Band");
            }
            else if(demandType.ToLower().Equals("tpres"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBand, band, false, "Band");
            }
            else if (empGroup.ToLower().Equals("contract"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBand, band, false, "Band");
                RG_Instance.ReportStepLog("Band selected successfully", "Pass", false);
            }
            else
            {
                TW_Instance.SetDropdownValue(oCNR.cmbBand, 1, false, "Band");
            }
            if(demandType.ToLower().Equals("tpappchng"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSubBand, "E1", false, "Sub Band");
            }
            else
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSubBand, 1, false, "Sub Band");
            }
                        
            string country = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Country");
            
            TW_Instance.SetDropdownValue(oCNR.cmbCountry, country, true, "Country");
            if (demandType.ToLower().Contains("vietnamtss"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbCompanyName, 1, false, "Company Name");
            }
            else
            {
                TW_Instance.SetDropdownValue(oCNR.cmbCompanyName, 1, false, "Company Name");
            }
            Thread.Sleep(2000);
            TW_Instance.SetDropdownValue(oCNR.cmbPSA, 1, true, "PSA");
            if (!demandType.ToLower().Contains("slfresher") && !demandType.ToLower().Contains("slschool"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSecPSA, 2, false, "Secondary PSA");
            }
           
            
            string noOfPosition = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "NoOfPosition");
            TW_Instance.SetTextInElement(oCNR.txtNoOfPosition, noOfPosition, true, "NoOfPosition");
            
            if (TW_Instance.IsObjectExist(oCNR.rdbHirePanIndiaNo, "Hire PAN India"))
            {
                string HirePanIndia = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "HirePanIndia");
                if (HirePanIndia.ToLower().Equals("no"))
                    TW_Instance.SelectRadiobutton(oCNR.rdbHirePanIndiaNo, "Consent to Hire Resource on PAN India location - No");
                RG_Instance.ReportStepLog("Initiator is able to Select flag for PAN India successfully", "Pass", false);
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
                TW_Instance.SetDropdownValue(oCNR.cmbBU, 2, false, "Business Unit");
                TW_Instance.SetDropdownValue(oCNR.cmbSecBU, 1, false, "Secondary Business Unit");
                Thread.Sleep(10000);
                TW_Instance.SetDropdownValue(oCNR.cmbDomain, 1, false, "Domain");
                //TW_Instance.SetDropdownValue(oCNR.cmbSecDomain, 1, false, "Secondary Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbSubDomain, 1, false, "Sub Domain");
                //TW_Instance.SetDropdownValue(oCNR.cmbSecSubDomain, 1, false, "Secondary Sub Domain");
                TW_Instance.SetDropdownValue(oCNR.cmbTRating, 1, false, "T Rating");
                //TW_Instance.SetDropdownValue(oCNR.cmbSecTRating, 1, false, "Secondary T Rating");
                TW_Instance.SetDropdownValue(oCNR.cmbFunction, 1, false, "Function");
                //TW_Instance.SetDropdownValue(oCNR.cmbSecFunction, 1, false, "Secondary Function");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbCustomerState + "']", "Customer State"))
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbCustomerState, 1, false, "Customer State");
                }
                if (TW_Instance.IsObjectExist(oCNR.txtReleaseDate, "Release Date"))
                {
                    TW_Instance.SetTextInElement(oCNR.txtReleaseDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), true, "Release Date");
                }
                //TW_Instance.ClickElement(oCNR.chkbxCustomerRehire, "Customer Rehire");
                //string cutomerRehire = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "CustmerRehire");
                //TW_Instance.SetTextInElement(oCNR.txtCustomerRehire, cutomerRehire, true, "Customer Rehire");
                //string resourceName = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ResourceName");
                //TW_Instance.SetTextInElement(oCNR.txtResourceName, resourceName, true, "Resource Name");
                //Thread.Sleep(10000);
                //TW_Instance.SetFileToUpload(oCNR.txtFIleUpload, GlobalVariable.datapoolPath + @"\TextFile\Doc.txt","File Upload");
                //TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Doc.txt");
            }
          
            if (entityName.ToLower().Equals("bserv"))
            {  
                if (TW_Instance.IsObjectEnabled("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
                {
                    if (!TW_Instance.IsObjectReadOnly("css:button[data-id = '" + oCNR.cmbSalaryScale + "']", "Salary Scale"))
                    {
                        string salaryScale = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "SalaryScale");
                        TW_Instance.SetDropdownValue(oCNR.cmbSalaryScale, 1, false, "Salary Scale");
                    }
                }                
                string maxSalary = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "MaxSalary");
                TW_Instance.SetTextInElement(oCNR.txMaximumSalary, maxSalary, true, "Maximum Salary");
                TW_Instance.SetDropdownValue(oCNR.cmbTypeOfRequirement, 1, false, "Type of Requirement");
                TW_Instance.SetDropdownValue(oCNR.cmbSource, 1, false, "Source");
                //TW_Instance.ClickElement(oCNR.txtAssessmentCriteria, "Assessment Criteria");
                //Thread.Sleep(2000);
                //TW_Instance.ClickElement(oCNR.chkAssessCrt1, "Skill");
                //TW_Instance.SetTextInElement(oCNR.txtAssessCrt1, "5", "Threshold level");
                //TW_Instance.ClickElement(oCNR.btnAssCrtUpdate, "Update button");
            }

            if (!entityName.ToLower().Equals("tech") || entityName.ToLower().Equals("tech"))
            {
                if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbFresherType + "']", "FresherType"))
                {
                    if (demandType.ToLower().Equals("vietnamtss"))
                    {
                        TW_Instance.SetDropdownValue(oCNR.cmbFresherType, 2, false, "Fresher Type");
                        RG_Instance.ReportStepLog("Fresher Type selected successfully", "Pass", false);
                        if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbCohortMatrix + "']", "Cohort Matrix"))
                        {
                            TW_Instance.SetDropdownValue(oCNR.cmbCohortMatrix, 1, false, "Cohort Matrix");
                            RG_Instance.ReportStepLog("Cohort Matrix selected successfully", "Pass", false);
                        }
                    }
                    else
                    {
                        TW_Instance.SetDropdownValue(oCNR.cmbFresherType, 1, false, "Fresher Type");
                        RG_Instance.ReportStepLog("Fresher Type selected successfully", "Pass", false);
                        Thread.Sleep(10000);
                    }
                }
            }
            if (entityName.ToLower().Equals("infra") && projectName.ToLower().Equals("others (6863)"))
            {
                TW_Instance.FileUpload(GlobalVariable.datapoolPath + @"\TextFile\Upload.docx");
            }
            TW_Instance.ClickElement(oCNR.btnSaveAsDraft, "Save As Draft Button");           
            TW_Instance.HandlePopupAlert("Accept");
            Thread.Sleep(30000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up"))
            {
                string similarDraftPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarDraft,"Similar Draft POP UP");
                TW_Instance.CompareValue(similarDraftPopUpMsg.Split('[')[0], "You already have back fill SR ", true);
                TW_Instance.ClickElement(oCNR.btnSimilarDraftPopUp, "Similar Draft Pop Up");
            }
            Thread.Sleep(30000);

            //Screen 2           
            TW_Instance.SetDropdownValue(oCNR.cmbExperience, 1, false, "Experience");
            TW_Instance.SetDropdownValue(oCNR.cmbQualification, 1, false, "Qualification");
            //if (entityName.ToLower().Equals("infra") || entityName.ToLower().Equals("bserv"))
            //{
                TW_Instance.SetTextInElement(oCNR.txtJobDescription,"Test",true,"Job Description");
            //}
            TW_Instance.SetDropdownValue(oCNR.cmbDesignation, 1, false, "Designation");
            RG_Instance.ReportStepLog("Designation selected successfully", "Pass", false);
            TW_Instance.SetDropdownValue(oCNR.cmbPA, 1, false, "PA");
            if (!demandType.ToLower().Contains("slfresher") && !demandType.ToLower().Contains("slschool"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbSecPA, 2, false, "Secondary PA");
            }
            TW_Instance.SetDropdownValue(oCNR.cmbCWL, 1, false, "CWL");
            if (TW_Instance.IsObjectExist(oCNR.btnCWLpopNobtn, "CWL Pop Up QA"))
            {
                Thread.Sleep(6000);
                string cwlPopUpMsg = TW_Instance.GetElementText(oCNR.txtCWLpop, "CWL POP UP");
                TW_Instance.CompareValue(cwlPopUpMsg, "CWL/Joining location can be the given as NV", true);
                TW_Instance.ClickElement(oCNR.btnCWLpopNobtn, "CWL Pop Up no");
            }
            if(TW_Instance.IsObjectExist(oCNR.txtNVReason, "NV Reason"))
            {
                TW_Instance.SetTextInElement(oCNR.txtNVReason, "Test", true, "NV Reason");
            }
            //string cwl = TW_Instance.GetcmbElementText(oCNR.cmbCWL, "CWL");
            //cwl = cwl.Replace(" ", "").Replace("\r\n", "");

            //if(cwl.ToLower().Equals("clientlocation"))
            //{
            //    TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
            //    TW_Instance.SetDropdownValue(oCNR.cmbCWL, 2, true, "CWL");
            //}
            //if (!demandType.ToLower().Contains("slfresher") && !demandType.ToLower().Contains("slschool"))
            //{
            //    TW_Instance.SetDropdownValue(oCNR.cmbSecCWL, 1, false, "Secondary CWL");
            //}

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
                TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
            }

            if (TW_Instance.IsObjectExist(oCNR.txtBuyRate, "SOT Buy Rate"))
            {
                TW_Instance.SetTextInElement(oCNR.txtBuyRate, "500", true, "Buy Rate");
                TW_Instance.SetTextInElement(oCNR.txtSellRate, "1000", true, "Sell Rate");
            }

            TW_Instance.SetDropdownValue(oCNR.cmbJoiningL1, 2, false, "Joining L1");
            TW_Instance.SetDropdownValue(oCNR.cmbJoiningL4, 1, false, "Joining L4");
            if (demandType.ToLower().Contains("dachanges"))
            {
                try
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbJoiningL1, "Digital & Analytics", false, "Joining L1");
                    TW_Instance.SetDropdownValue(oCNR.cmbJoiningL4, "D&A", false, "Joining L4");
                }
                catch
                {
                    TW_Instance.SetDropdownValue(oCNR.cmbJoiningL1, 2, false, "Joining L1");
                    TW_Instance.SetDropdownValue(oCNR.cmbJoiningL4, 1, false, "Joining L4");
                    RG_Instance.ReportStepLog("D&A LOB's are not visible in JL1 & JL4", "Pass", false);
                    TW_Instance.ClickElement(oCNR.txtSecClientJL, "Client JL");
                }
                
            }
            string JL1 = "";
            string JL4 = "";
            if(demandType.ToLower().Contains("tpappchng"))
            {
                JL1 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL1, "Joining L1");
                JL4 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL4, "Joining L4");
            }
            if (demandType.ToLower().Equals("sales") || demandType.ToLower().Contains("tss"))
            {
                TW_Instance.IsObjectEnabled(oCNR.cmbJoiningL1x, "Joining L1");
                TW_Instance.IsObjectEnabled(oCNR.cmbJoiningL4x, "Joining L4");
                JL1 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL1, "Joining L1");
                JL4 = TW_Instance.GetcmbElementText(oCNR.cmbJoiningL4, "Joining L4");
                //PL1 = PL1.Split('(')[0].Replace(" ","");
                //PL4 = PL4.Split('(')[0].Replace(" ", "");
                //JL1 = JL1.Replace(" ", "");
                //JL4 = JL4.Replace(" ", "");
                //TW_Instance.CompareValue(PL1, JL1, true);
                //TW_Instance.CompareValue(PL4, JL4, true);
                //RG_Instance.ReportStepLog("PL1 & PL4 values are same as JL1 & JL4", "Pass", false);
            }

            if (TW_Instance.IsObjectExist(oCNR.rdbNConsentForTP, "Consent For TP"))
            {
                string consentForTP = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "ConsentForTP");
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
                    TW_Instance.ClickElement(oCNR.txtClientJL, "Client JL");
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
            TW_Instance.SetTextInElement(oCNR.txtOnBoardingDate, DateTime.Now.Date.AddDays(5).ToString("dd-MMM-yyyy"), false, "On Boarding Date");
            if (entityName.ToLower().Equals("tech") || entityName.ToLower().Equals("infra"))
            {
                TW_Instance.ClickElement(oCNR.txtGRAID, "GRA ID");
            }
            
            if (TW_Instance.IsObjectExist("css:button[data-id='" + oCNR.cmbDemandProbability + "']", "Demand Probability"))
            {
                TW_Instance.SetDropdownValue(oCNR.cmbDemandProbability, 2, false, "Demand Probability");
            }
            if (entityName.ToLower().Equals("tech"))
            {
                if (demandType.ToLower().Equals("tpres"))
                {
                    TW_Instance.SetTextInElement(oCNR.txtRM, "40804860", true, "Reporting Manager");
                    TW_Instance.ClickElement(oCNR.lstRMnames, "RM Name");
                    bool TP_RM = TW_Instance.IsObjectExist(oCNR.tpRm,"TP RM");
                    if (TP_RM)
                    {
                        RG_Instance.ReportStepLog("Since demand created is for TP band, RM cannot be TP Band", "Pass", false);
                        TW_Instance.DeleteText(oCNR.txtRM);
                        TW_Instance.SetTextInElement(oCNR.txtRM, "40164402", true, "Reporting Manager");
                        TW_Instance.ClickElement(oCNR.lstRMnames, "RM Name");
                    }                       
                    else
                        RG_Instance.ReportStepLog("Able Enter TP Band RM", "Fail", true);
                }
                //TW_Instance.SetDropdownValue(oCNR.txtRM, 2, false, "Reporting Manager");

                //TW_Instance.ClickElement(oCNR.txtRM, "Reporting Manager");                               
                //TW_Instance.ClickElement(oCNR.lstRMnames, "RM Name");                             
            }
            TW_Instance.ClickElement(oCNR.txtInterviewer1TP2, "TP2");
            string interview1TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP1");
            TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP1, interview1TP1, true, "Interview 1 TP1");

            //string interview1TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview1TP2");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer1TP2, interview1TP2, "Interview 1 TP2");

            string interview2TP1 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP1");
            TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP1, interview2TP1, true, "Interview 2 TP1");

            //string interview2TP2 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Interview2TP2");
            //TW_Instance.SetTextInElement(oCNR.txtInterviewer2TP2, interview2TP2, "Interview 2 TP2");
            //if(demandType.ToLower().Equals("infraapplogic"))
            //{
                //TW_Instance.GetElementText(oCNR.txtApprover1, "Approver 1");
                //TW_Instance.GetElementText(oCNR.txtApprover2, "Approver 2");
           // }
           
            TW_Instance.ClickElement(oCNR.btnReqSubmit, "Submit button");
            Thread.Sleep(50000);
            if (TW_Instance.IsObjectExist(oCNR.btnSimilarPopUp, "Similar Pop Up"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUp, "Similar Pop Up");
            }
            else if(TW_Instance.IsObjectExist(oCNR.btnSimilarPopUpQA, "Similar Pop Up QA"))
            {
                string similarPopUpMsg = TW_Instance.GetElementText(oCNR.txtSimilarPopUpMsg, "Similar POP UP");
                TW_Instance.CompareValue(similarPopUpMsg.Split('.')[0], "A similar requisition is already created in system", true);
                TW_Instance.ClickElement(oCNR.btnSimilarPopUpQA, "Similar Pop Up");
            }             
            string generatedReqNum = string.Empty;           
            if (TW_Instance.IsObjectExist(oCNR.lblReqNoText, "Similar Pop Up QA"))
            {
                Thread.Sleep(60000);
                generatedReqNum = TW_Instance.GetElementText(oCNR.lblReqNoText, "Pop Up Message");                
            }
            else
            {
                Thread.Sleep(60000);
                generatedReqNum = TW_Instance.GetPopupText();
            }
            Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] { "/2022/" }, StringSplitOptions.None)[1].Split('.')[0].Replace(" ", ""));
            
            if (demandType.ToLower().Equals("itappopup"))
            {
                TW_Instance.CompareValue(generatedReqNum, "Redirecting to iTAP EDGE", true);
                Environment.SetEnvironmentVariable("GenerateReqNum", generatedReqNum.Split(new String[] { "/2021/" }, StringSplitOptions.None)[1].Replace(" ", ""));
                Thread.Sleep(35000);
                IList<string> winTab = GlobalVariable.driver.WindowHandles.ToList();
                GlobalVariable.driver.SwitchTo().Window(winTab.Last());
                RG_Instance.ReportStepLog("Initiator is able to See iTAP EDGE Page", "Pass", true);
                GlobalVariable.driver.SwitchTo().Window(winTab.Last()).Close();
                Thread.Sleep(10000);
                GlobalVariable.driver.SwitchTo().Window(winTab.First());
            }

            if (demandType.ToLower().Equals("excalibur"))
            {
                Thread.Sleep(1000);
                TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
                TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
                Thread.Sleep(10000);
                TW_Instance.WaitForObjectPresent(oCNR.lnkPositionAndJobDetails);
                TW_Instance.ClickElement(oCNR.lnkPositionAndJobDetails, "Position and job details Link");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist(oAA.DemandProbibility, "Demand Probability field"))
                {
                    Thread.Sleep(1000);
                    TW_Instance.GetElementText(oAA.DemandProbibility, "Demand Probability field");
                    TW_Instance.GetElementText(oAA.DemandProbibilityValue, "Demand Probability field value");
                    RG_Instance.ReportStepLog("Able to fetch Demand Probability value successfully", "Pass", false);
                    TW_Instance.ClickElement(oAA.btnClose, "Close button");
                }
            }

            if (demandType.ToLower().Equals("exdpy"))
            {
                Thread.Sleep(1000);
                TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
                TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
                Thread.Sleep(100000);
                TW_Instance.WaitForObjectPresent(oCNR.lnkPositionAndJobDetails);
                TW_Instance.ClickElement(oCNR.lnkPositionAndJobDetails, "Position and job details Link");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist(oAA.DemandProbibility, "Demand Probability field"))
                {
                    Thread.Sleep(1000);
                    TW_Instance.GetElementText(oAA.DemandProbibility, "Demand Probability field");
                    TW_Instance.GetElementText(oAA.DemandProbibilityValue, "Demand Probability field value");
                    RG_Instance.ReportStepLog("Able to fetch Demand Probability value successfully", "Pass", false);
                    TW_Instance.ClickElement(oAA.btnClose, "Close button");
                }
            }

            if (demandType.ToLower().Equals("exdpn"))
            {
                Thread.Sleep(1000);
                TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
                TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
                Thread.Sleep(100000);
                TW_Instance.WaitForObjectPresent(oCNR.lnkPositionAndJobDetails);
                TW_Instance.ClickElement(oCNR.lnkPositionAndJobDetails, "Position and job details Link");
                Thread.Sleep(10000);
                if (TW_Instance.IsObjectExist(oAA.DemandProbibility, "Demand Probability field"))
                {
                    Thread.Sleep(1000);
                    TW_Instance.GetElementText(oAA.DemandProbibility, "Demand Probability field");
                    TW_Instance.GetElementText(oAA.DemandProbibilityValue, "Demand Probability field value");
                    RG_Instance.ReportStepLog("Able to fetch Demand Probability value successfully", "Pass", false);
                    TW_Instance.ClickElement(oAA.btnClose, "Close button");
                }
            }

            if (demandType.ToLower().Equals("bservapproval"))
            {
                Thread.Sleep(100000);
                TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
                TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oCNR.lnkReqNo,"Requisition No");
                Thread.Sleep(50000);
                TW_Instance.ClickElement(oCNR.lnkInterviewerTab, "Interviewer TAB");
                Thread.Sleep(5000);
                string app3 = TW_Instance.GetElementText(oCNR.lblApprover3, "Approver3");
                string app4 = TW_Instance.GetElementText(oCNR.lblApprover4, "Approver4");
                TW_Instance.ClickElement(oCNR.btnClose, "Req Details");
                string approver3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver3");
                string approver4 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver4");
                bool bFlag = TW_Instance.CompareValue(app3, approver3, true);
                bool bFlag1 = TW_Instance.CompareValue(app4, approver4, true);
                if (bFlag & bFlag1)
                    RG_Instance.ReportStepLog("Bserv Approval Hierarchy Working As Exepected.", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Bserv Approval Hierarchy is Not Working As Exepected.", "Fail", true);
            }
            if (demandType.ToLower().Equals("bservapproval1"))
            {
                Thread.Sleep(100000);
                TW_Instance.ClickElement(oCNR.lnkInitiatorActions, "Initiator Actions");
                TW_Instance.ClickElement(oCNR.lnkManageRequisitions, "Manage Requisition");
                Thread.Sleep(10000);
                TW_Instance.SetTextInElement(oCNR.txtReqNoSearch, Environment.GetEnvironmentVariable("GenerateReqNum"), true, "Requisition Number Search");
                TW_Instance.ClickElement(oCNR.btnReqSearch, "Req No Search Button");
                Thread.Sleep(10000);
                TW_Instance.ClickElement(oCNR.lnkReqNo, "Requisition No");
                Thread.Sleep(50000);
                TW_Instance.ClickElement(oCNR.lnkInterviewerTab, "Interviewer TAB");
                Thread.Sleep(5000);
                string app3 = TW_Instance.GetElementText(oCNR.lblApprover2, "Approver3");
                string app4 = TW_Instance.GetElementText(oCNR.lblApprover3, "Approver4");
                TW_Instance.ClickElement(oCNR.btnClose, "Req Details");
                string approver3 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver3");
                string approver4 = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "Approver4");
                bool bFlag = TW_Instance.CompareValue(app3, approver3, true);
                bool bFlag1 = TW_Instance.CompareValue(app4, approver4, true);
                if (bFlag & bFlag1)
                    RG_Instance.ReportStepLog("Bserv Approval Hierarchy Working As Exepected.", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Bserv Approval Hierarchy is Not Working As Exepected.", "Fail", true);
            }
            if (demandType.ToLower().Equals("sot"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog("Initiator is able to raise SOT demand and able enter buy & Sell rate.", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Initiator is unable to raise SOT demand", "Fail", true);
            }
            if (demandType.ToLower().Equals("bypass"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog("Initiator is able to raise a demand Without Interviewer Validation", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Initiator is unable to raise a demand Without Interviewer Validation", "Fail", true);
            }
            if (demandType.ToLower().Equals("y"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog("Apps & SI Initiator is able to raise ProActive demand on Y code Projects.", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Apps & SI Initiator is unable to raise ProActive demand on Y code Projects.", "Fail", true);
            }
            if (demandType.ToLower().Equals("apps"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog("Apps & SI Initiator is able to raise ProActive demand on Bench Projects.", "Pass", false);
                else
                    RG_Instance.ReportStepLog("Apps & SI Initiator is unable to raise ProActive demand on Bench Projects.", "Fail", true);
            }
            if (demandType.ToLower().Equals("others"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is able to raise TP Replacement demand on Others Projects.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is unable to raise TP Replacement demand on Others Projects.", "Fail", true);
            }
            if (demandType.ToLower().Equals("ftctofte"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is able to raise FTC to FTE Requisition.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator(Tech & Infra) is unable to raise FTC to FTE Requisition.", "Fail", true);
            }
            if (demandType.ToLower().Equals("c3i"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator is able to raise C3i Requisition.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator is unable to raise C3i Requisition.", "Fail", true);
            }
            if (demandType.ToLower().Equals("freshertagging"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator is able to select Fresher Type Field .", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator is unable to select Fresher Type Field.", "Fail", true);
            }
            if (demandType.ToLower().Equals("indbusinlob"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator is able to create proactive Requisition in India Business Lobs.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator is unable to create proactive Requisition in India Business Lobs.", "Fail", true);
            }
            if (demandType.ToLower().Equals("leaner"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator is able to create Requisition in Leaner SR.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator is unable to create Requisition in Leaner SR.", "Fail", true);
            }
            if (demandType.ToLower().Equals("mode2"))
            {
                bool bFlag = TW_Instance.CompareValue(generatedReqNum.Split('-')[0], "Requisition has been submitted successfully", true);
                if (bFlag)
                    RG_Instance.ReportStepLog(" Initiator is able to Select Mode2 Skill.", "Pass", false);
                else
                    RG_Instance.ReportStepLog(" Initiator is unable to Select Mode2 Skill.", "Fail", true);
            }
            Thread.Sleep(10000);
        }        
    }
}

