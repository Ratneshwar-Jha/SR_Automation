using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_InterviewerActions
    {
        //Top menu
        public string lnkChangeRole = "xpath://a[text()='Change Role']";
        public string lnkInterviwer = "xpath://a[contains(text(),'INTERVIEWER')]";
        public string tblHomeProfileOverview = "xpath://table[@class='global-dataTbl']";
        public string lnkHome = "xpath://li[contains(text(),'Home')]";
        public string lnkInternalProfiles = "xpath://li[contains(text(),'Internal Profiles')]";
        public string lnkInterviewerReq = "xpath://li[contains(text(),'Interviewer Requisitions')]";
        public string imgItap = "xpath://span[@id='lblHALink']/a/img";
        //Internal Profile Filter
        public string icnFilter = "css:img#img1.SR_CollapsableHide";
        public string txtReqNoSearch = "id:txtPrfId";
        public string btnReqSearch = "css:input#btnGO1";
        //Shortlist
        public string icnShortList = "css:input#imgBtnShortlist";
        public string txtEndDate = "id:txtEndDate";
        public string cmbBillingStatus = "Ddl_BillingStatus";
        public string cmbPIRole = "Ddl_role";
        public string cmbWBSComponent = "Ddl_WBS";
        public string txt_RoleRemarks = "css:textarea#txt_RoleRemarks";
        public string btnAddAssignation = "css:input#btnAddassignation";
        public string btnShortlist = "xpath://input[@value='Shortlist']";

        //Reject  
        public string icnReject = "css:input#imgBtnRejectShowRejectReason";
        public string btnRejectReason = "css:select#ddlRejectReason";
        public string txtRejectRemark = "css:textarea#txtRejectRemark";
        public string icnOk = "css:input#imgBtnOk";
        //Block
        public string icnBlock = "css:input#imgBtnBlockUnBlock";
        //UnBlock
        public string icnUnBlock = "css:input#imgBtnBlockUnBlock";
        //Filter
        public string icFilter = "css:img#img1.SR_CollapsableHide";
        public string txtReqNoSearch1 = "id:txtPrfId";
        public string btnReqSearch1 = "css:input#btnGO1";




    }
}
