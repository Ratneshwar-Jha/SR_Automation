using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR_Automation.ObjectRepository.Common
{
    class OR_InOutApp
    {
        //Login Page
        public string txtLoginId = "css:input#EmployeeCode";
        public string txtPassword = "css:input#Password";
        //public string btnLogin = "css:input#SubmitButton";
        public string btnLogin = "xpath://input[@id='SubmitButton']";
        //Home Page
        public string lnkLogOut = "linktext:Logout";

        //Chrome Microsoft Login
        public string txtUserId = "xpath://input[@id='i0116']";
        public string btnNext = "xpath://input[@id='idSIButton9']";
        public string txtPassWrd = "xpath://input[@id='i0118']";
        public string btnSingIn = "xpath://input[@id='idSIButton9']";

        //Staging TPG Relogin
        public string txtEmp = "xpath://input[@id='txtEmp']";
        public string txtPwd = "xpath://input[@id='txtPwd']";
        public string btnSubmit = "xpath://input[@id='btnSubmit']";

    }
}
