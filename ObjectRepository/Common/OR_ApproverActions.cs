using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_ApproverActions
    {
        //Top menu
        public string lnkApproverActions = "xpath://a[text()='Approver Actions']";
        public string lnkMyApprovals = "xpath://a[text()='My Approvals']";
        public string lnkRequisition = "xpath://a[@class='modal-link btn btn-sm']";
        public string lnkPositionAndJobDetails = "xpath://a[text()=' Position and Job details ']";
        public string ConsentToHireFlag = "xpath://*[@id='Requisitions']/div/table/tbody/tr[14]/td[1]";
        public string ConsentToHireFlagStatus = "xpath://*[@id='Requisitions']/div/table/tbody/tr[14]/td[2]";
        public string btnClose = "xpath://*[@id='ViewRequestions']/div/div/div[3]/button";
        public string DemandProbibility = "xpath://*[@id='Job']/div/table[1]/tbody/tr[9]/td[1]";
        public string DemandProbibilityValue = "xpath://*[@id='Job']/div/table[1]/tbody/tr[9]/td[2]";
        


        public string chkbox = "xpath://tr[1]//td[1]//input[1]";
        public string ddlBlkApprvAction = "xpath://select[@name='Action1']";
        public string ddlBlkReason = "xpath://select[@name='Reason1']";
        public string txtBulkApprvRemark = "xpath://input[@name='Remarks']";
        public string BtnBulkApproveButton = "xpath://input[@name='btn-Submit']";

        public string lnkActionByAppvr = "xpath://a[@id='edit' and @class='modal-link btn']/span[1]";
        public string ddlApprvrSelectAction = "css:select#Action";
        public string ddlApprvrReason = "css:select#Reason";
        public string txtApproverRemarks = "css:textarea#Remarks";
        public string btnApproverActionSubmit = "css:input#btnsubmit";
    }
}
