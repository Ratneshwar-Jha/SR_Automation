using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_InitiatorActions
    {
        //Edit Action
        public string btnReqEdit = "xpath://a[contains(@id, 'edit')]";
        public string txtGraId = "xpath://input[@id='GraID']";
        public string rdbTPConNo = "css:input#TPConversionNo";
        public string cmbEmpGrpRsn = "css:select#EmployeeGroupReasonID";
        public string txtPositionEndDate = "id:txtPositionEndDate";
        public string btnReqUpdate = "xpath://input[@id='Update']";

        //Copy Action
        public string btnCopy = "css:a#copy";

        //ReferBackEditResubmit
        public string txtRemark = "css:input#Remarks";
        public string btnSubmit = "css:input#Submit";

        //Drop Vacancy Action
        public string btnDropVacancy = "xpath://a[@class='DropVacency']";
        public string cmbDropReason = "xpath://select[@id='ddlreason']";
        public string cmdPositionToBeDropped = "xpath://select[@id='ddlPositionToBe']";
        public string txtDropRemarks = "xpath://textarea[@id='txtRemarks']";
        public string btnDropReqSubmit = "xpath://button[@id='btnSubmit']";

        //Close Requisition Action
        public string btnReqClose = "xpath://a[@name='close']";
        public string cmbCloseReason = "xpath://select[@id='Reason']";
        public string txtCloseRemarks = "xpath://textarea[@id='Remarks']";
        public string btnCloseReqSubmit = "xpath://input[@id='btnsubmit']";


        //Pending With Column
        public string lnkPendingWith = "css:a#pendWith";
        public string cellPendingWithName = "xpath://table[@class='table table-striped table-bordered table-hover example no-footer']/tbody/tr[1]/td[2]";
        public string cellPendingWith = "xpath://table[@class='table table-striped table-bordered table-hover example no-footer']/thead/tr[1]/th[2]/h4";
        public string btnPendingWithClose = "xpath://div[@class='modal-footer tc']/button[text()='Close']";


        //Status Column
        public string cellReqStatus = "xpath://td[text()='Refer Back' or text()='Closed']";


    }
}
