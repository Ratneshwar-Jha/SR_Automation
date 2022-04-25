using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_BulkAction
    {
        //BulkAction Console

        public string lnkInitiatorActions = "xpath://a[text()='Initiator Actions']";
        public string lnkBulkAction = "xpath://a[text()='Bulk SR (Transfer/BSD Change)']"; 
        public string chkRequistionNumber = "xpath://input[@id='SelectAll']";


        //BulkSRTransfer
        public string rbtnTransferReq = "xpath://div[1]/form/div[2]/input[1]"; 
        public string txtTransferee = "xpath://input[@id='SRTransferSAPCode']";
        public string setTransferee = "xpath://*[@id='Transferee']/td[2]/ul";
        public string ddSRTRReason = "css:select#ddlSRTreason"; 
        public string txtSRRemarks = "xpath://input[@id='SRRemarks']";
        public string btnBulkSRTransfer = "xpath://input[@id='btnSR']";

        //BulkBSDChange
        public string rbtnBSDChange = "xpath://html/body/div[1]/form/div[2]/input[2]";
        public string txtBSDDate = "xpath://input[@id='BSDDate']";
        public string ddBSDChangeReason = "css:select#BSDChangeReason";
        public string btnBSD = "xpath://input[@id='btnBSD']";


    }
}
