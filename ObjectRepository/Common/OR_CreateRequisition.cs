using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_CreateRequisition
    {
        //Top menu
        public string lnkInitiatorActions = "xpath://a[text()='Initiator Actions']";
        public string lnkCreateNewReq = "xpath://a[text()='Create New Requisition']";
        public string lnkManageRequisitions = "xpath://a[text()='Manage Requisitions']";
        public string cmbEntityName = "css:select#EntityName";
        public string btnSaveContinue = "css:input#btnEntitySubmit";

        //Screen 1
        public string txtProject = "projects";
        public string cmbProjectL1 = "ddlProjectL1";
        public string cmbProjectL1x = "xpath://body/main/div/div/form/div/section/div/div/div[1]/div[9]/div[1]/div[1]";
        public string cmbProjectL4 = "ddlProjectL4";
        public string cmbProjectL4x = "xpath://body/main/div/div/form/div/section/div/div/div[1]/div[10]/div[1]/div[1]";
        public string rdbIsOpportunity100kyes = "xpath://div[contains(@class,'radioQuestion radioButton Opportunity focused')]//label[contains(text(),'Yes')]";
        public string rdbIsOpportunity100kno = "xpath://div[contains(@class,'radioQuestion radioButton Opportunity focused')]//label[contains(text(),'No')]";
        public string txtProjectDescription = "xpath://input[@id='ProjectCatDesc']";
        public string rdbExclusive = "xpath://div[contains(@class, 'rbgPositionExclusive')]/ul[1]/li[1]";
        public string rdbNonExclusive = "xpath://div[contains(@class, 'rbgPositionExclusive')]/ul[1]/li[2]";
        public string rdbDemandTypeSOT = "xpath://input[@id='sot']/following::label[1]";
        public string rdbClientIntrvwYes = "xpath://div[contains(@class, 'showonClInter')]/ul[1]/li[1]";
        public string rdbClientIntrvwNo = "xpath://div[contains(@class, 'showonClInter')]/ul[1]/li[2]";
        //public string cmbRequestType = "RequisitionType";
        public string cmbRequestType = "xpath://input[@placeholder='--Select Requisition Type--']";
        public string cmbProgramType = "ProgramType";
        public string cmbxTransferTo = "xpath://button[@data-id='transferTo']";
        public string cmbTransferTo = "transferTo";
        public string cmbxProgramType = "xpath://button[@data-id='ProgramType']";
        public string cmbBatchYear = "BatchYear";
        public string lstReplacement = "xpath://a[@class='firstChild ng-binding'][contains(text(),'Replacement')]";
        public string lstNewResourceRequest = "xpath://a[@class='firstChild ng-binding'][contains(text(),'New Resource Request')]";
        public string lstFreshers = "xpath://a[@class='firstChild sub ng-binding'][contains(text(),'Freshers')]";
        public string lstOpportunity = "xpath://a[@class='firstChild ng-binding'][contains(text(),'Opportunity')]";
        public string subMenuNewResourceRequest = "xpath://*[@id='NewRequisitionType']/ul/li[2]/ul/li/a";
        public string cmbprogramType = "xpath://button[@data-id='ProgramType']";
        public string cmbcampusBatchYear = "xpath://button[@data-id='BatchYear']";
        public string cmbSRNumber = "SRNumber";
        public string btnTransferDemandPopUpYes = "xpath://div[contains(@class, 'sa-confirm-button-container')]/button[@class='confirm btn btn-lg btn-primary']";
        public string btnTransferDemandPopUpNo = "xpath://div[contains(@class, 'sa-button-container')]/button[@class='cancel btn btn-lg btn-default']";
        public string txtTransferDemandPopUp = "xpath://h2[contains(text(),'Demand is')]";
        public string txtSkill = "css:input#Skills";
        public string txtERSSkill = "css:div#txtaddskill";
        public string txtAccountId = "id:sfaAccountOppurtunity";
        public string txtOpprtnyId = "id:sfaOppurtunity";
        public string cmbJob = "job";
        public string txtWarngMsgJob = "xpath://small[contains(text(),'You have chosen Cybersecurity project, please sele')]";
        public string lnkRemoveJob = "xpath://form//div//div//div[2]//div[1]//i[1]";
        public string cmbXJob = "xpath://input[@id='JobID']";
        public string jobListItem = "xpath://div[@class='tt-dataset tt-dataset-jobs']//div[1]";
        public string icnfilterJob = "xpath://i[@id='ForBulkHiring']";
        public string cmbEmpGroup = "empG";
        public string cmbBand = "BandId";
        public string cmbSubBand = "subbandid";
        public string cmbCountry = "country";
        public string cmbxCountry = "xpath://div[5]//div[1]//div[1]//div[1]//button[1]";
        public string cmbCompanyName = "ddlCompanyCode";
        public string cmbPSA = "PersonalSubAreaID";
        public string cmbSecPSA = "SecondaryPersonalSubAreaID";
        public string cmbxSecPSA = "xpath://body//div[5]//div[5]//div[1]//div[1]//button[1]";
        public string txtNoOfPosition = "id:VacancyCount";
        public string rdbHirePanIndiaYes = "xpath://*[@id='divPANIndiaLocation']/div/ul[1]/li[1]/label[1]";
        public string rdbHirePanIndiaNo = "xpath://*[@id='divPANIndiaLocation']/div/ul[1]/li[2]/label[1]";
        public string btnSaveAsDraft = "xpath://button[@id='SaveAsDraft']";
        public string btnNext = "xpath://button[@id='btnSaveAsDraftNext']";
        public string btnSimilarDraftPopUp = "xpath://div[contains(@class, 'sa-confirm-button-container')]/button[@class='confirm btn btn-lg btn-danger']";
        public string txtSimilarDraft = "xpath://h2[contains(text(),'You already have back fill SR [New Project Already')]";
        public string cmbFresherType = "freshertype";
        public string cmbCohortMatrix = "cohortmatrix";
        public string icnfileUplod = "xpath://i[@class=' icon-upload prefix']";
        public string txtFIleUpload = "id:hdn_FilePath";
        //public string file = "xpath://div[@class='uploadFile focused']";
       
        
        //Bserv Specific
        public string cmbSalaryScale = "SalaryScalID";
        public string txMaximumSalary = "css:input#MaximumSalary";
        public string cmbTypeOfRequirement = "TypeOfRequirementID";
        public string txtAssessmentCriteria = "xpath://input[@class='asseC inputMaterial']";
        public string chkAssessCrt1 = "css:input#Chk_1";
        public string txtAssessCrt1 = "css:input#Txt_1";
        public string btnAssCrtUpdate = "xpath://button[text()='Update']";
        public string chkBulkHire = "xpath://input[@id='bulk']";
        public string cmbSource = "TypeOfSourceID";
        //C3i Language
        public string cmbLanguage1 = "ddlLanguage1";
        public string cmbLanguageGrade1 = "ddlLanguageGrade1";
        public string cmbLanguage2 = "ddlLanguage2";
        public string cmbLanguageGrade2 = "ddlLanguageGrade2";
        public string cmbLanguage3 = "ddlLanguage3";
        public string cmbLanguageGrade3 = "ddlLanguageGrade3";

        //Infra Specific
        public string cmbBU = "BUID";
        public string cmbSecBU = "SecondaryBUID";
        public string cmbDomain = "DomainID";
        public string cmbSecDomain = "SecondaryDomainID";
        public string cmbSubDomain = "SubDomainID";
        public string cmbSecSubDomain = "SecondarySubDomainID";
        public string cmbTRating = "TRatingID";
        public string cmbSecTRating = "SecondaryTRatingID";
        public string cmbFunction = "FunctionID";
        public string cmbSecFunction = "SecondaryFunctionID";
        public string cmbCustomerState = "CustomerState";
        public string icnInfraFileUpload = "xpath://i[@class=' icon-upload prefix']";
        public string txtInfraFileUpload = "xpath://input[@class='inputMaterial filename']";
        public string txtReleaseDate = "css:input#ReleaseDate";
        public string chkbxCustomerRehire = "xpath://input[@id='chkCustomerRehire']";
        public string txtCustomerRehire = "xpath://textarea[@id='TxtCustomerRehire']";
        public string txtResourceName = "xpath://textarea[@id='TxtResourceNameRehire']";

        //Screen 2
        public string cmbExperience = "ddlExperience";
        public string cmbQualification = "ddlQualification";
        public string txtJobDescription = "id:TxtjobDesription";
        public string txtAddtionalReq = "id:TxtAdditionalRequirement";
        public string cmbDesignation = "ddlDesignationCode";
        public string cmbPA = "ddlPA";
        public string cmbSecPA = "ddlSecondaryPA";
        public string cmbCWL = "ddlJoiningLocation";
        public string cmbCWLPopup = "xpath://html/body/div[8]";
        public string cmbCWLPopupYes = "xpath://html/body/div[8]/div[7]/div/button";
        public string cmbCWLPopupNo = "xpath://html/body/div[8]/div[7]/button";
        public string cmbCWLPopupOk = "xpath://html/body/div[8]/div[7]/div/button";
        public string btnCWLpopNobtn = "xpath://button[@class='cancel btn btn-lg btn-default']";
        public string txtCWLpop = "xpath://h2[text()='CWL/Joining location can be the given as NV']";
        public string txtNVReason = "xpath://input[@id='Nvlocation']";
        public string cmbSecCWL = "ddlSecondaryJoiningLocation";
        public string txtClientJL = "id:TxtClientJoiningLocation";
        public string txtSecClientJL = "xpath://input[@id='TxtSecondaryClientJoiningLocation']";
        public string cmbState = "ddlState";
        public string cmbCity = "ddlCity";
        public string txtBuyRate = "css:input#TxtBuyRate";
        public string txtSellRate = "css:input#TxtSellRate";
        public string cmbJoiningL1 = "ddlJoiningL1";
        public string cmbJoiningL1x = "xpath://div[3]//div[2]//div[1]//div[1]//button[1]";
        public string cmbJoiningL4 = "ddlJoiningL4";
        public string cmbJoiningL4x = "xpath://body//div[3]//div[3]//div[1]//div[1]//button[1]";
        public string rdbYConsentForTP = "xpath://div[contains(@class, 'constentRadio')]/ul[1]/li[1]/label[1]";
        public string rdbNConsentForTP = "xpath://div[contains(@class, 'constentRadio')]/ul[1]/li[2]/label[1]";
        public string txtTPEmpGrp = "css:input#TxtTPEmployeeGroup";
        public string cmbTPBand = "ddlTPBand";
        public string cmbTPSubBand = "ddlTPSubBand";
        public string cmbTPDesignation = "ddlTPDesignation";
        public string txtTPContractFrom = "css:input#TxtTPcontractFrom";
        public string txtTPContractTo = "css:input#TxtTPcontractTo";
        public string txtTPBuyRate = "id:TxtTPBuyRate";
        public string txtTPSellRate = "id:TxtTPSellRate";

        public string txtContractFromDate = "css:input#TxtcontractFrom";
        public string txtContractToDate = "css:input#TxtContractTo";

        //Screen 3
        public string cmbBillingType = "BillingTypeID";
        public string txtBillStartDate = "id:BillingStartDateID";
        public string txtOnBoardingDate = "id:OnBoardingDate";
        public string txtValidTillDate = "id:ValidTillDate";
        public string txtRM = "id:ReportingManagerID";
        public string lstRMnames = "xpath://*[@id='tab3']/div[3]/div[1]/span/div/div/div[1]";
        public string tpRm = "xpath://small[contains(text(),'Since demand created is for TP band, RM cannot be')]";
        public string txtGRAID = "id:GraID";
        public string cmbDemandProbability = "DemandProbability";
        public string txtInterviewer1TP1 = "id:interviewerid1TP1";
        public string txtInterviewer1TP2 = "id:interviewerid1TP2";
        public string txtInterviewer2TP1 = "id:interviewerid2TP1";
        public string txtInterviewer2TP2 = "id:interviewerid2TP2";
        public string txtApprover1 = "xpath://input[@id='Approver1']";
        public string txtApprover2 = "xpath://input[@id='Approver2']";
        public string txtApprover3 = "id:Approver3";
        public string txtApprover4 = "id:Approver4";
        public string btnReqSubmit = "xpath://button[@id='SaveSubmit']";
        public string btnSimilarPopUp = "xpath://div[contains(@class, 'sa-confirm-button-container')]/button[@class='confirm btn btn-lg btn-primary']";
        public string btnSimilarPopUpQA = "xpath://div[contains(@class, 'sa-confirm-button-container')]/button[@class='confirm btn btn-lg btn-danger']";
        public string lblReqNoText = "xpath://p[contains(@class, 'lead text-muted') and contains(text(),'Requisition')]";
        public string txtEditRemarks = "css:input#Remarks";
        public string txtSimilarPopUpMsg = "xpath://h2[contains(text(),'A similar requisition is already created in system')]";
        //public string txtQAPopUpMsg = "xpath://p[contains(@class,'lead text-muted')]";

        //Rotation
        public string txtNameOfResource = "xpath://input[@id='TPSAPCODE']";
        public string txtNameOfResrc = "xpath://input[@id='AttritionRotationSapCode']";

        //TP Replacement
        public string txtTPState = "css:input#TPState";


        //Manage Requisition Page
        public string txtReqNoSearch = "css:input#ReqNo";
        public string txtReqFromDate = "css:input#StartDate";
        public string txtReqToDate = "css:input#EndDate";
        public string btnReqSearch = "css:input#SearchReq";
        public string btnExportToExcel = "css:input#ExportToExcel";
        public string lnkReqNo = "css:a#viewReq";
        public string lnkPositionAndJobDetails = "xpath://a[contains(text(),' Position and Job details ')]";
        public string lnkInterviewerTab = "xpath://a[contains(text(),'Interviewer/Approver Details')]";
        public string lblApprover2 = "xpath://div[@id='Interviewer']/div[1]/table[1]/tbody[1]/tr[2]/td[2]";
        public string lblApprover3 = "xpath://div[@id='Interviewer']/div[1]/table[1]/tbody[1]/tr[3]/td[2]";
        public string lblApprover4 = "xpath://div[@id='Interviewer']/div[1]/table[1]/tbody[1]/tr[4]/td[2]";
        public string btnClose = "xpath://form[@id='ViewRequestions']//button[@class='btn-primary btn-sm'][contains(text(),'Close')]";
    }
}
