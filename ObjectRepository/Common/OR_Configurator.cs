using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_Configurator
    {
        //Configurator Console

        public string lnkConfigurator = "xpath://a[contains(text(),'Configurator')]";

        public string chkRequistionNumber = "xapth://input[@id='selectall']";

        public string icnBulkProject = "xpath://*[@id='ActionBulkProject']/i";

        public string icnBulkRM = "xpath://*[@id='ActionBulkRM']/i";

        public string icnUpdateProject = "xpath://tr[1]//td[14]//div[1]//a[1]";

        public string icnUpdateRM = "xpath://tr[1]//td[14]//div[1]//a[2]";

        public string chkbox1 = "xpath://tr[1]//td[1]//input[1]";

        public string chkbox2 = "xpath://tr[2]//td[1]//input[1]";

        public string txtRMName = "xpath://tr[1]//td[6]";

        public string rbtnRMname = "xpath://tr[1]//td[3]//input[1]";

        public string txtRMnamePopup = "xpath://table[@id='tblRm']//tbody[1]//tr[1]//td[2]";

        public string rbtn1RMname = "xpath://tr[2]//td[3]//input[1]";

        public string txtSearchRM = "xpath://input[@id='searchRM']";

        public string btnConfirm = "xpath://button[@id='btnRM']";

        public string btnConfirmP = "xpath://button[@id='btnProject']";

        public string btnClose = "xpath://div[@class='modal-footer btnFooter']//button[@class='btn secondry-button'][contains(text(),'CLOSE')]";

        public string txtPageNo = "xpath://a[contains(text(),'2')]";

        public string rbtnProject = "xpath://div[@id='projectModal']//tr[1]//td[3]//input[1]";


        //Filter in Configurator Console

        public string icnfilter = "xpath://i[@class='icon-nfilter']";

        public string txtRequestionNumber = "xpath://input[@id='RequisitionNo']";

        public string txtCandidateName = "xpath://input[@id='CandidateName']";

        public string cmbProjectModalL1 = "xpath://select[@ng-model='ProjectL1Modal']";

        public string cmbProjectModalL4 = "xpath://select[@ng-model='ProjectL4Modal']";

        public string cmbJoiningModalL1 = "xpath://select[@ng-model='JoiningL1Modal']";

        public string cmbJoiningModalL4 = "xpath://select[@ng-model='JoiningL4Modal']";

        public string cmbCountryModal = "xpath://select[@id='CountryCode']";

        public string btnApply = "xpath://span[@onclick='ApplyFilter();']";

        public string btnRemoveFilter = "xpath://span[@onclick='RemoveFilter();']";

        public string txtFetchReqNumber = "xpath://tr[1]//td[@class='sorting_1']";

        public string txtFetchReqNumber1 = "xpath://tr[2]//td[@class='sorting_1']";




    }
}
