using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_TPGActions
    {
        public string lnkChangeRole = "xpath://a[text()='Change Role']";
        public string lnkTPGManager = "xpath://ul[@class='localNavData']//a[contains(text(),'TPG Manager')]";
        public string lnkRequisitionNum = "xpath://*[@id='ctl00_ContentPlaceHolder1_gvTPGHome']/tbody/tr[2]/td[2]/a";
        public string lnkRequisitionDetails = "xpath:/html/body/form/div[4]/table[1]/tbody/tr/td/table[2]/tbody/tr[3]/td/div/ul/li[1]";
        public string ConsentToHireFlagTPG = "xpath://*[@id='TabbedPanels3']/div/div[1]/table/tbody/tr[1]/td/table/tbody/tr[21]/td[1]";
        public string ConsentToHireStatusTPG = "xpath://*[@id='lblHiretoPANIndiaLocation']";
        public string btnCloseTPG = "xpath://*[@id='global-mainContent']/table[2]/tbody/tr[1]/td/div/input[2]";

        public string btnTPGReset = "xpath://input[@value='Reset']";
        public string ddlTPGStatus = "xpath://select[@name='ctl00$ContentPlaceHolder1$ddlStatus']"; // Pending (Self)
        public string txtTPGReqNum = "id:txtRequisition";
        public string ddlTPGLevel1 = "id:ddlL1";
        public string btnGo = "css:input#ContentPlaceHolder1_btnGo";

        //TPG Configurator
        public string lnkTpgConfigurator = "xpath://*[@id='ContentPlaceHolder1_lnktpgconfg']";
        public string lnkSearchReqNum = "xpath://*[@id='RequistionNo']";
        public string btnApply = "xpath://*[@id='btnApply']";
        public string imgAction = "xpath://*[@id='vReqNo']";
        public string txtJobFamily = "xpath://*[@id='job']";
        public string lstJobFamily = "xpath://*[@id='tpgData']/section[2]/div/div[1]/div[1]/span[1]/div";
        public string ddlBand = "xpath:/html/body/main/div/div[1]/form/section[2]/div/div[1]/div[2]/div/div[1]/select";
        public string ddlSubBand = "xpath:/html/body/main/div/div[1]/form/section[2]/div/div[1]/div[2]/div/div[2]/select";
        public string ddlDesignation = "xpath://*[@id='ddldsg']";
        public string linkSkills = "xpath://*[@id='txtaddskill']";
        public string btnUpdate = "xpath://*[@id='SaveSubmit']";

        //Req        
        public string cellRequisitionNo = "xpath://table[contains(@id,'_gvTPGHome')]/tbody/tr[2]/td[2]";
        public string cellProfileAttached = "xpath://tr[2]//td[15]//a[1]";
        public string imgAttachProfile = "xpath://img[@id='btnAttachProfile']";
       
        //TPG Transfer
        public string imgTransfer = "xpath://td[16]//a[1]//img[1]";
        public string btnChangeTPG = "xpath://input[@id='btnSubmitChangeTPG']";
        public string cmbTPG = "xpath://select[@id='ddlTPGManger']";
        public string txtRemark = "xpath://textarea[@id='txtTPGRemarks']";
        public string btnChangTPG = "xpath://input[@name='Button2']";

        //Undo
        public string lnkUndo = "xpath://body//td[27]";
        public string txUndoRemark = "xpath://textarea[@name='txtTPGRemarks']";
        public string btnUndo = "xpath://input[@name='btnUndoTPGManager']";

        //TPG Referback 
        public string btnBulkReferback = "xpath://input[@id='btnShowModal']";
        public string txtReferbackRemark = "xpath://textarea[@id='txtRemarks1']";
        public string cmbReferbackReason = "xpath://select[@id='ddlReason1']";
        public string btnReferbackSubmit = "xpath://input[@id='Submit']";

        //search candidates
        //public string btnSCReset = "xpath://input[@id='btnReset']";
        public string btnSCReset = "css:input#btnReset";
        public string ddlSCBand = "id:ddlBand";
        public string btnSearchCandidate = "css:input#btnSearch";
        public string chkSCSelectEmp = "id:chk1";
        public string btnSCAttachEmp = "id:btnAttachCandidate";
        public string btnAttachIcon = "id:iBtnAttach";
        public string lnkSCPageNo = "xpath://li[@pageindex='1']//a[contains(text(),'1')]";
        public string lblMessage = "css:span#lblMessage";
        public string btnSCClose = "xpath://input[@value='Close']";
        public string imgBtnForward = "css:input#imgBtnForward";
        public string imgBtnBlock = "xpath://input[@id='imgBtnBlockUnBlock']";

        //Smart Assignation
        public string imgbtnTPGShorlist = "css:input#imgBtnShortlist";
        public string icnTPGShrotlist = "xpath://input[@name='gvReqProfilesList$ctl03$imgBtnShortlist']";
        public string txtExpectedBillDate = "css:input#txtExpectedBillingDate";
        public string txtAssignationEndDate = "id:txtEndDate";/*"css:input#txtEndDate";*/
        public string cmbBillingStatus = "css:select#Ddl_BillingStatus";
        public string cmbReasonNCS = "css:select#ddl_ReasonNCS";
        public string cmbPIRole = "css:select#Ddl_role";
        public string txtPIRole = "xpath://textarea[@name='txt_RoleRemarks']";
        public string cmbWBSComponent = "css:select#Ddl_WBS";
        public string txtWBS = "xpath://textarea[@name='txt_WBSRemarks']";
        public string cmbLevel = "css:select#ddlLevel";
        public string cmbTransferType = "css:select#ddlTransfer";
        public string cmbCountry = "css:select#ddl_Country";
        public string cmbPA = "css:select#ddl_PA";
        public string cmbPSA = "css:select#ddl_PSA";
        public string cmbNWL = "css:select#ddl_NWL";
        public string cmbState = "css:select#ddl_State";
        public string cmbCity = "css:select#ddl_city";
        public string cmbDD = "css:select#ddl_dd";
        public string btnAddAssignation = "css:input#btnAddassignation";
        public string txtFSRemark = "css:textarea#txtRemarks";
        public string btnTPGFinalSelect = "css:input#btn_finalSubmit";

        //Reject
        public string imgBtnReject = "css:input#imgBtnReject";
        public string cmbRejectReason = "css:select#ddlRejectReason";
        public string txtTPGRejectRemark = "css:textarea#txtSelectRejectRemarkByTPG";
        public string imgRejectOK = "css:input#imgBtnOk";

        //Assign To TPG Executive
        public string btnAssignToTPGExe = "css:input#btnAssignTPGExec";
        public string chkSelectReq = "xpath://input[contains(@id, '_chk1')]";
        public string cmbTPGExecutive = "css:select#ddlTPGExec";
        public string imgBtnAssigntoTPGEx = "css:input#iBtnUpdateAssignTPGExec";
        public string lnkTPGExecutive = "xpath://a[contains(text(),'TPG Executive')]";

 

    }
}
