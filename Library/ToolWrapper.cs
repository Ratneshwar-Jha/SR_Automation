/***********************************************************************************
 * About - This library contains all the functions to be performed over UI controls
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 ***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using AutoIt;


namespace SR_Automation.Library
{
    class ToolWrapper
    {
        //Initialzing class
        ReportGenerator RG_Instance = new ReportGenerator();
        GenericFunction GF_Instance = new GenericFunction();
        ExcelFunction EF_Instance = new ExcelFunction();
        //ToolWrapper TW_Instance = new ToolWrapper();

        public IWebElement TestObject(string _objProp)
        {
            string _locatorType = _objProp.Split(':')[0];
            string _objectProp = _objProp.Split(':')[1];

            IWebElement element = null;
            try
            {
                if (_locatorType.ToLower() == "id")
                {
                    element = GlobalVariable.driver.FindElement(By.Id(_objectProp));
                }
                else if (_locatorType.ToLower() == "name")
                {
                    element = GlobalVariable.driver.FindElement(By.Name(_objectProp));
                }
                else if (_locatorType.ToLower() == "xpath")
                {
                    element = GlobalVariable.driver.FindElement(By.XPath(_objectProp));
                }
                else if (_locatorType.ToLower() == "css")
                {
                    element = GlobalVariable.driver.FindElement(By.CssSelector(_objectProp));
                }
                else if (_locatorType.ToLower() == "linktext")
                {
                    element = GlobalVariable.driver.FindElement(By.LinkText(_objectProp));
                }

                return element;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        internal void PerformMouseClick(string chkRequistionNumber)
        {
            throw new NotImplementedException();
        }

        public void OpenURL(string _url)
        {
            GlobalVariable.envName = _url;
            GlobalVariable.driver.Navigate().GoToUrl(_url);
            GlobalVariable.driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(5);
            GlobalVariable.driver.Manage().Window.Maximize();
            RG_Instance.ReportStepLog(_url + " : url successfully open in browser", "Done", false);
        }

        public void CloseBrowser()
        {
            GlobalVariable.driver.Close();
            RG_Instance.ReportStepLog("Browser closed successfully", "Done", false);
        }

        public void SetTextInElement(string _objProp, string _valueToSet, bool _forceFlag, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                if (string.IsNullOrEmpty(obj.Text) || _forceFlag == true)
                {
                    obj.Clear();
                    obj.SendKeys(_valueToSet);
                    RG_Instance.ReportStepLog("<b>" + _valueToSet + "</b> : set successfully in the field : " + _objDesc, "Done", false);
                }
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : field not exists on the screen", "Fail", true);
            }
        }

        public void ClickElement(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                obj.Click();
                RG_Instance.ReportStepLog(_objDesc + " : control clicked successfully", "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

        }

        public string GetElementText(string _objProp, string _objDesc)
        {
            string textValue = null;

            IWebElement obj = TestObject(_objProp);
            if (obj.Displayed)
            {
                textValue = obj.Text;
                RG_Instance.ReportStepLog("<b>" + textValue + "</b> : label text fetched successfully", "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

            return textValue;
        }
        public string GetcmbElementText(string _btnDataId, string _objDesc)
        {
            string textValue = null;

            IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnDataId + "']"));
            if (obj_button.Displayed)
            {
                textValue = obj_button.Text;
                RG_Instance.ReportStepLog("<b>" + textValue + "</b> : label text fetched successfully", "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

            return textValue;
        }

        public void SelectRadiobutton(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                obj.Click();
                RG_Instance.ReportStepLog(_objDesc + " : radio button clicked successfully", "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }
        }

        public void SelectDropdownValue(string _objProp, string _valToSelect, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                SelectElement se = new SelectElement(obj);
                se.SelectByText(_valToSelect);
                RG_Instance.ReportStepLog("<b>"+ _valToSelect + "</b> : value selected successfully in dropdown : "+ _objDesc, "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

        }

        public void SelectDropdownValue(string _objProp, int _valueIndex, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                SelectElement se = new SelectElement(obj);
                se.SelectByIndex(_valueIndex);
                //string s = se.SelectedOption.Text;
                RG_Instance.ReportStepLog("Value at index : <b>" + _valueIndex.ToString() + "</b> selected successfully in dropdown : "+ _objDesc, "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

        }
        //public void SetDropdownValue(string _btnId, int _valIndex, bool _forceFlag, string _objDesc)
        //{
        //    IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnId + "']"));
        //    WaitForObjectToAppear(obj_button);
        //    if (obj_button.Displayed)
        //    {
        //        if (obj_button.Text.Contains("--Select") || _forceFlag == true)
        //        {
        //            obj_button.Click();

        //            IWebElement obj_list = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnId + "']/following::div[1]/ul[1]/li[@data-original-index='" + _valIndex + "']/a[1]/span[1]"));
        //            IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
        //            js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);

        //            if (obj_list.Displayed)
        //            {
        //                PerformMouseClick(obj_list);
        //                RG_Instance.ReportStepLog("<b>" + obj_button.Text.ToString() + "</b> : selected successfully in dropdown control : " + _objDesc, "Done", false);
        //            }
        //            else
        //            {
        //                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
        //            }
        //        }
        //    }
        //}
        public void SetDropdownValue(string _btnDataId, int _valIndex, bool _forceFlag, string _objDesc)
        {
            IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnDataId + "']"));
            WaitForObjectToAppear(obj_button);
            if (obj_button.Displayed)
            {
                if (obj_button.Text.Contains("--Select") || _forceFlag == true)
                {
                    obj_button.Click();

                    IWebElement obj_list = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnDataId + "']/following::div[1]/ul[1]/li[@data-original-index='" + _valIndex + "']/a[1]/span[1]"));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);

                    if (obj_list.Displayed)
                    {
                        PerformMouseClick(obj_list);
                        RG_Instance.ReportStepLog("<b>" + obj_button.Text.ToString() + "</b> : selected successfully in dropdown control : " + _objDesc, "Done", false);
                    }
                    else
                    {
                        RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
                    }
                }
            }
        }
        public void SetxDropdownValue(string _btnDataId, int _valIndex, bool _forceFlag, string _objDesc)
        {
            IWebElement obj_button = TestObject(_btnDataId);
            WaitForObjectToAppear(obj_button);
            if (obj_button.Displayed)
            {
                if (obj_button.Text.Contains("--Select") || _forceFlag == true)
                {
                    obj_button.Click();

                    IWebElement obj_list = TestObject("" + _btnDataId + "/following::div[1]/ul[1]/li[@data-original-index='" + _valIndex + "']/a[1]/span[1]");
                    //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                    //js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);

                    if (obj_list.Displayed)
                    {
                        PerformMouseClick(obj_list);
                        RG_Instance.ReportStepLog("<b>" + obj_button.Text.ToString() + "</b> : selected successfully in dropdown control : " + _objDesc, "Done", false);
                    }
                    else
                    {
                        RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
                    }
                }
            }
        }
        //public void SelectRequestType(string _ReqType, bool _forceFlag, string _objDesc)
        //{
        //    IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//input[@placeholder='--Select Requisition Type--']"));
        //    WaitForObjectToAppear(obj_button);
        //    if (obj_button.Displayed)
        //    {
        //        if (obj_button.Displayed || _forceFlag == true )
        //        {
        //            obj_button.Click();
        //            //IWebElement obj_list = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Replacement')]"));
        //            //IWebElement obj_list1 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'New Resource Request')]"));
        //            //IWebElement obj_list2 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild sub ng-binding'][contains(text(),'Freshers')]"));
        //            //IWebElement obj_list3 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Opportunity')]"));
        //            //IWebElement obj_list4 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Rebadging')]"));
        //            //IWebElement obj_list5 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Buffer')]"));
        //            IWebElement obj_list = null;
        //            IWebElement obj_list1 = null;
        //            IWebElement obj_list2 = null;
        //            IWebElement obj_list3 = null;
        //            IWebElement obj_list4 = null;
        //            IWebElement obj_list5 = null;

        //            try
        //            {
        //                obj_list = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Replacement')]"));
        //                obj_list1 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'New Resource Request')]"));
        //                obj_list2 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild sub ng-binding'][contains(text(),'Freshers')]"));
        //            }
        //            catch
        //            {

        //            }
        //            try
        //            {
        //                obj_list = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Replacement')]"));
        //                obj_list1 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'New Resource Request')]"));                       
        //            }
        //            catch
        //            {

        //            }
        //            try
        //            {
        //                obj_list1 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'New Resource Request')]"));
        //                obj_list3 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding'][contains(text(),'Opportunity')]"));
        //            }
        //            catch
        //            {

        //            }
        //            //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
        //            //js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);

        //            if (_ReqType.Contains("New Resource Request"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);

        //                try
        //                {
        //                    hover.MoveToElement(obj_list1).Perform();
        //                    Thread.Sleep(5000);
        //                    IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[2]/ul/li/a"));
        //                    IWebElement obj_subMenu1 = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[1]/ul/li/a"));
        //                    if (obj_subMenu.Displayed)
        //                    {
        //                        PerformMouseClick(obj_subMenu);
        //                    }
        //                    else
        //                    {
        //                        PerformMouseClick(obj_subMenu1);
        //                    }

        //                    RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //                }
        //                catch
        //                {

        //                }                                                                                       
        //            }
        //            else if (_ReqType.Contains("Proactive SR"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);                       
        //                hover.MoveToElement(obj_list4).Perform();
        //                Thread.Sleep(3000);
        //                IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[2]/ul/li/a"));
        //                PerformMouseClick(obj_subMenu);
        //                RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //            }
        //            else if (_ReqType.Contains("Rotation"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);
        //                hover.MoveToElement(obj_list).Perform();
        //                Thread.Sleep(3000);
        //                IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[1]/ul/li[1]/a"));
        //                PerformMouseClick(obj_subMenu);
        //                RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //            }
        //            else if (_ReqType.Contains("Attrition"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);
        //                hover.MoveToElement(obj_list).Perform();
        //                Thread.Sleep(3000);
        //                IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[1]/ul/li[2]/a"));
        //                PerformMouseClick(obj_subMenu);
        //                RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //            }
        //            else if (_ReqType.Contains("TP Replacement"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);
        //                hover.MoveToElement(obj_list).Perform();
        //                Thread.Sleep(3000);
        //                IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[1]/ul/li[3]/a"));
        //                PerformMouseClick(obj_subMenu);
        //                RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //            }
        //            else if (_ReqType.Contains("Conversion to FTE"))
        //            {
        //                Actions hover = new Actions(GlobalVariable.driver);
        //                hover.MoveToElement(obj_list).Perform();
        //                Thread.Sleep(3000);
        //                IWebElement obj_subMenu = GlobalVariable.driver.FindElement(By.XPath("//*[@id='NewRequisitionType']/ul/li[1]/ul/li[4]/a"));
        //                PerformMouseClick(obj_subMenu);
        //                RG_Instance.ReportStepLog("<b>" + obj_subMenu.Text.ToString() + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
        //            }
        //            else
        //            {
        //                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
        //            }
        //        }
        //    }
        //}
        public void SelectRequestType(string _ReqType, string _subReqType, string _subSubReqType, bool _forceFlag, string _objDesc)
        {
            IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//input[@placeholder='--Select Requisition Type--']"));
            WaitForObjectToAppear(obj_button);
            if (obj_button.Displayed)
            {
                if (obj_button.Displayed || _forceFlag == true)
                {
                    obj_button.Click();
                    Thread.Sleep(5000);
                    if (String.IsNullOrEmpty(_subSubReqType))
                    {
                        IWebElement objL1 = GlobalVariable.driver.FindElement(By.XPath("//a[contains(@class,'firstChild') and contains(text(),'" + _ReqType + "')]"));
                        Actions hover = new Actions(GlobalVariable.driver);
                        hover.MoveToElement(objL1).Perform();
                        Thread.Sleep(2000);
                        IWebElement objL2 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='ng-binding' and contains(text(),'" + _subReqType + "')]"));
                        hover.MoveToElement(objL2).Click().Perform();
                        //PerformMouseClick(objL2); 
                        //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                        //js.ExecuteScript("arguments[0].click();", objL2);
                        //hover.MoveToElement(objL2).Click().Build().Perform();
                        Thread.Sleep(3000);
                        RG_Instance.ReportStepLog("<b>" + _ReqType + "--" + _subReqType + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);
                    }
                    else
                    {
                        IWebElement objL1 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild sub ng-binding' and contains(text(),'" + _ReqType + "')]"));
                        Actions hover = new Actions(GlobalVariable.driver);
                        hover.MoveToElement(objL1).Perform();
                        Thread.Sleep(2000);
                        IWebElement objL2 = GlobalVariable.driver.FindElement(By.XPath("//a[@class='firstChild ng-binding' and contains(text(),'" + _subReqType + "')]"));                       
                        hover.MoveToElement(objL2).Perform();
                        Thread.Sleep(2000);
                        
                        if(_subReqType.Equals("Campus"))
                        {
                            if(_subSubReqType.Equals("B School"))
                            { 
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[1]//ul[1]//li[1]//a[1]"));
                                hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("E School"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[1]//ul[1]//li[2]//a[1]"));
                                hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("Others"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[1]//ul[1]//li[3]//a[1]"));
                                hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("In School Grads"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[1]//ul[1]//li[4]//a[1]"));
                                hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }

                        }
                        else
                        {
                            if (_subSubReqType.Equals("B School"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[2]//ul[1]//li[1]//a[1]"));
                                //hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("E School"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[2]//ul[1]//li[2]//a[1]"));
                                //hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("Others"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[2]//ul[1]//li[3]//a[1]"));
                                //hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                            if (_subSubReqType.Equals("In School Grads"))
                            {
                                IWebElement objL3 = GlobalVariable.driver.FindElement(By.XPath("//li//li[2]//ul[1]//li[4]//a[1]"));
                                //hover.MoveToElement(objL3).Perform();
                                PerformMouseClick(objL3);
                            }
                        }
                        
                        Thread.Sleep(2000);
                        RG_Instance.ReportStepLog("<b>" + _ReqType + "--" + _subReqType +"--"+ _subSubReqType + "</b> : selected successfully in Menu control : " + _objDesc, "Done", false);

                    }
                }
            }
        }
        public void SetDropdownValue(string _btnDataId, string _valToSelect, bool _forceFlag, string _objDesc)
        {
            IWebElement obj_button = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnDataId + "']"));
            WaitForObjectToAppear(obj_button);
            if (obj_button.Displayed)
            {
                if (obj_button.Displayed || _forceFlag == true)
                {
                    obj_button.Click();

                    IWebElement obj_list = GlobalVariable.driver.FindElement(By.XPath("//button[@data-id='" + _btnDataId + "']/following::div[1]/ul[1]/li/a[1]/span[text()='"+ _valToSelect +"']"));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);
                    if (obj_list.Displayed)
                    {
                        PerformMouseClick(obj_list);
                        RG_Instance.ReportStepLog("<b>" + obj_button.Text.ToString() + "</b> : selected successfully in dropdown control : " + _objDesc, "Done", false);
                    }
                    else
                    {
                        RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
                    }
                }
            }
        }
        public void SetxDropdownValue(string _btnDataId, string _valToSelect, bool _forceFlag, string _objDesc)
        {
            IWebElement obj_button = TestObject(_btnDataId);
            WaitForObjectToAppear(obj_button);
            if (obj_button.Displayed)
            {
                if (obj_button.Text.Contains("--Select") || _forceFlag == true)
                {
                    obj_button.Click();

                    IWebElement obj_list = TestObject("" + _btnDataId + "/following::div[1]/ul[1]/li/a[1]/span[text()='" + _valToSelect + "']");
                    //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                    //js.ExecuteScript("arguments[0].scrollIntoView(true);", obj_list);
                    
                    if (obj_list.Displayed)
                    {
                        PerformMouseClick(obj_list);
                        RG_Instance.ReportStepLog("<b>" + obj_button.Text.ToString() + "</b> : selected successfully in dropdown control : " + _objDesc, "Done", false);
                    }
                    else
                    {
                        RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
                    }
                }
            }
        }
        public void SelectListBoxValue(string _divId, string _valueToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//div[@id='" + _divId + "']/span[1]/input[2]"));
            WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                IWebElement clearIcon = GlobalVariable.driver.FindElement(By.XPath("//div[@id='" + _divId + "']/i[1]"));
                if (clearIcon.Displayed)
                {
                    clearIcon.Click();
                    Thread.Sleep(2000);
                }

                obj_Input.SendKeys(_valueToType);
            }

            IWebElement obj_list = GlobalVariable.driver.FindElement(By.XPath("//div[@id='" + _divId + "']/span[1]/div/div/div"));
            WaitForObjectToAppear(obj_list);
            if (obj_list.Displayed)
            {
                PerformMouseClick(obj_list);
                //obj_list.Click();
                RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : selected successfully in : " + _objDesc, "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }
        }

        public void DeleteText(string _objProp)
        {
            try
            {
                IWebElement obj = TestObject(_objProp);
                WaitForObjectToAppear(obj);
                if (obj.Displayed)
                {
                    Actions act = new Actions(GlobalVariable.driver);
                    //act.MoveToElement(obj).SendKeys(OpenQA.Selenium.Keys.Control + "a").SendKeys(OpenQA.Selenium.Keys.Delete).Perform();
                    //act.MoveToElement(obj).KeyDown(OpenQA.Selenium.Keys.Control).SendKeys("a").KeyUp(OpenQA.Selenium.Keys.Control).Perform();
                    act.MoveToElement(obj).DoubleClick().SendKeys(OpenQA.Selenium.Keys.Control+"A").SendKeys(OpenQA.Selenium.Keys.Delete).Perform();
                    
                    //obj.SendKeys(OpenQA.Selenium.Keys.Control + "a");
                    //obj.SendKeys(OpenQA.Selenium.Keys.Delete);
                    RG_Instance.UpdateLogFile( " pressed successfully");
                }
                else
                {
                    RG_Instance.UpdateLogFile( " failed to press");
                    GlobalVariable.testStepStatus = false;
                }
            }
            catch (Exception e)
            {
                RG_Instance.UpdateLogFile( " : " + e.Message.ToString());
                GlobalVariable.testStepStatus = false;
            }
        }
        public void SetValueInSkillPopUp4ObsoleteSkillValidation(string _valueToType, string _value1ToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//*[(@id='Skills' and @class='skill inputMaterial')]"));

            //or @id='txtaddskill')and(@class='skillTag' or
            //IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Skills' or @id='txtaddskill'] or //div[@id='Skills' or @id='txtaddskill']"));
            //WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }
                obj_Input.Click();
            }
            else
            {
                IWebElement obj_Inpt = GlobalVariable.driver.FindElement(By.XPath("//*[(@id='txtaddskill' and @class='skillTag')]"));
                if (!string.IsNullOrEmpty(obj_Inpt.Text))
                {
                    return;
                }
                obj_Inpt.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtskill' or @id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.Clear();
                obj_Input1.SendKeys(_valueToType);
                Thread.Sleep(2000);
                RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);

                if (IsObjectExist("xpath://input[@id='txtskill' or @id='txtERSskill']/following::div[1]/div[1]/div[1]", "Skill Search"))
                {
                    GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtskill' or @id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                }
                else
                {
                    RG_Instance.ReportStepLog("Value not displayed in dropdown to select : " + _objDesc, "Pass", true);
                }
                DeleteText("xpath://input[@id='txtskill' or @id='txtERSskill']");
                obj_Input1.SendKeys(_value1ToType);
                Thread.Sleep(2000);
                RG_Instance.ReportStepLog("<b>" + _value1ToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
                if (IsObjectExist("xpath://input[@id='txtskill' or @id='txtERSskill']/following::div[1]/div[1]/div[1]", "Skill Search"))
                {
                    GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtskill' or @id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                }
                else
                {
                    RG_Instance.ReportStepLog("Value not displayed in dropdown to select : " + _objDesc, "Pass", true);
                }
            }
            //Thread.Sleep(2000);
            //GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addSkillicon' or @class='icon-add addIcon openNextCol']")).Click();
            //Thread.Sleep(2000);
            //GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            //Thread.Sleep(2000);
            //GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();

            //RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
        }
        public void SetValueInSkillPopUp(string _valueToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//*[(@id='Skills' and @class='skill inputMaterial')]"));

            //or @id='txtaddskill')and(@class='skillTag' or
            //IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//input[@id='Skills' or @id='txtaddskill'] or //div[@id='Skills' or @id='txtaddskill']"));
            //WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }
                obj_Input.Click();
            }
            else
            {
                IWebElement obj_Inpt = GlobalVariable.driver.FindElement(By.XPath("//*[(@id='txtaddskill' and @class='skillTag')]"));
                if (!string.IsNullOrEmpty(obj_Inpt.Text))
                {
                    return;
                }
                obj_Inpt.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtskill' or @id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.Clear();
                obj_Input1.SendKeys(_valueToType);
                Thread.Sleep(2000);
                GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtskill' or @id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            }
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addSkillicon' or @class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }

            RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
        }

        public void SetMultiValueInSkillPopUp(string _valueToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//div[@id='txtaddskill']"));
            WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }
                obj_Input.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.SendKeys(OpenQA.Selenium.Keys.Control+"A");
                obj_Input1.SendKeys(OpenQA.Selenium.Keys.Delete);
                obj_Input1.SendKeys(_valueToType);
                Thread.Sleep(2000);
                GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                RG_Instance.ReportStepLog(_valueToType+" : has been entered in skill textbox","DONE",false);
            }
            RG_Instance.ReportStepLog("Selecting Skill-1 in MODE 2 Skill popup", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(5000);
            string Mode2ErrPopup = GetPopupText();
            CompareValue(Mode2ErrPopup, "Please select atleast 3 more skills from the mapped list", true);
            RG_Instance.ReportStepLog("Selecting Skill-2 in MODE 2 Skill popup", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//*[@id='SkillModal']/div/div/div[2]/div[4]")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(2000);
            string Mode2ErrPopup1 = GetPopupText();
            CompareValue(Mode2ErrPopup1, "Please select atleast 3 more skills from the mapped list", true);
            RG_Instance.ReportStepLog("Selecting Skill-3 in MODE 2 Skill popup", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//*[@id='SkillModal']/div/div/div[2]/div[4]")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(3000);
            string Mode2ErrPopup2 = GetPopupText();
            CompareValue(Mode2ErrPopup2, "Please select atleast 3 more skills from the mapped list", true);
            RG_Instance.ReportStepLog("Selecting Skill-4 in MODE 2 Skill popup", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//*[@id='SkillModal']/div/div/div[2]/div[4]")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(2000);
            if (IsObjectExist("xpath://div[@id='txtaddskill']", "Skill Popup"))
            {
                RG_Instance.ReportStepLog("Mode 2 Skill Functionality working as expected", "PASS", false);
            }
            else
            {                
                RG_Instance.ReportStepLog("Mode 2 Skill Popup getting error message", "FAIL", true);
            }

            RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
        }

        public void SetMultiValueInSkillPopUpSales(string _value1ToType, string _value2ToType, string _value3ToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//div[@id='txtaddskill']"));
            WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }
                obj_Input.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.Clear();
                obj_Input1.SendKeys(_value1ToType);
                Thread.Sleep(2000);
                GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                RG_Instance.ReportStepLog(_value1ToType + " : has been entered in skill textbox", "DONE", false);
            }
            RG_Instance.ReportStepLog("Selecting Skill-1 in MODE 2 Skill popup", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
            //IWebElement obj_Done = GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done'] or //div[@id='addi']//input[1]"));
            //var str = String.Format("window.scrollTo({0}, {1})", 0, obj_Done.Location.Y - 150);
            //js.ExecuteScript(str);

            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
           
            Thread.Sleep(5000);
            string Mode2ErrPopup = GetPopupText();
            CompareValue(Mode2ErrPopup, "Please select at least 3 Skill", true);
            RG_Instance.ReportStepLog("Selecting Skill-2 in MODE 2 Skill popup", "DONE", false);       
            Thread.Sleep(5000);
            DeleteText("xpath://input[@id='txtERSskill']");
            Thread.Sleep(5000);
            //obj_Input1.SendKeys();
            obj_Input1.SendKeys(_value2ToType);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            RG_Instance.ReportStepLog(_value2ToType + " : has been entered in skill textbox", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            //Thread.Sleep(2000);
            //GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(2000);
            string Mode2ErrPopup1 = GetPopupText();
            CompareValue(Mode2ErrPopup1, "Please select at least 3 Skill", true);
            RG_Instance.ReportStepLog("Selecting Skill-3 in MODE 2 Skill popup", "DONE", false);            
            Thread.Sleep(5000);
            DeleteText("xpath://input[@id='txtERSskill']");
            Thread.Sleep(5000);
            obj_Input1.SendKeys(_value3ToType);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            RG_Instance.ReportStepLog(_value3ToType + " : has been entered in skill textbox", "DONE", false);            
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(3000);                                 
            RG_Instance.ReportStepLog("Three Skills entered successfully in : " + _objDesc, "Done", false);
        }
        public void SetMultiValueInSkillPopUpAppsSI(string _valueToType, string _value1ToType, string _value2ToType, string _value3ToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//div[@id='txtaddskill']"));
            WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }
                obj_Input.Click();
            }
            else
            {
                IWebElement obj_Input12 = GlobalVariable.driver.FindElement(By.XPath("//*[(@id='Skills' and @class='skill inputMaterial')]"));
                WaitForObjectToAppear(obj_Input12);
                if (!string.IsNullOrEmpty(obj_Input12.Text))
                {
                    return;
                }
                obj_Input12.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.Clear();
                obj_Input1.SendKeys(_valueToType);
                Thread.Sleep(2000);
                GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                RG_Instance.ReportStepLog(_valueToType + " : has been entered in skill textbox", "DONE", false);
            }
            RG_Instance.ReportStepLog("Selecting Skill in Apps&SI bundle Skill", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//tr[1]//td[8]//i[1]")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            //IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
            //IWebElement obj_Done = GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done'] or //div[@id='addi']//input[1]"));
            //var str = String.Format("window.scrollTo({0}, {1})", 0, obj_Done.Location.Y - 150);
            //js.ExecuteScript(str);

            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }

            Thread.Sleep(5000);
            string Mode2ErrPopup = GetPopupText();
            CompareValue(Mode2ErrPopup, "Please select atleast 3", true);
            RG_Instance.ReportStepLog("Selecting Skill-1 in Apps & SI Skill bundle", "DONE", false);
            Thread.Sleep(5000);
            DeleteText("xpath://input[@id='txtERSskill']");
            Thread.Sleep(5000);
            //obj_Input1.SendKeys();
            obj_Input1.SendKeys(_value1ToType);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            RG_Instance.ReportStepLog(_value1ToType + " : has been entered in skill textbox", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//tr[2]//td[8]//i[1]")).Click();
            //Thread.Sleep(2000);
            //GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(2000);
            string Mode2ErrPopup1 = GetPopupText();
            CompareValue(Mode2ErrPopup1, "Please select atleast 3", true);
            RG_Instance.ReportStepLog("Selecting Skill-2 in Apps & SI Skill bundle", "DONE", false);
            Thread.Sleep(5000);
            DeleteText("xpath://input[@id='txtERSskill']");
            Thread.Sleep(5000);
            obj_Input1.SendKeys(_value2ToType);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            RG_Instance.ReportStepLog(_value2ToType + " : has been entered in skill textbox", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//tr[3]//td[8]//i[1]")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(3000);
            string Mode2ErrPopup2 = GetPopupText();
            CompareValue(Mode2ErrPopup2, "Please select atleast 3", true);
            RG_Instance.ReportStepLog("Selecting Skill-3 in Apps & SI Skill bundle", "DONE", false);
            Thread.Sleep(5000);
            DeleteText("xpath://input[@id='txtERSskill']");
            Thread.Sleep(5000);
            obj_Input1.SendKeys(_value3ToType);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
            RG_Instance.ReportStepLog(_value3ToType + " : has been entered in skill textbox", "DONE", false);
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//tr[4]//td[8]//i[1]")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            Thread.Sleep(3000);
            RG_Instance.ReportStepLog("Three Skills entered successfully in : " + _objDesc, "Done", false);
        }
        public void SetValueInERSSkillPopUp(string _valueToType, string _objDesc)
        {
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//div[@id='txtaddskill']"));
            WaitForObjectToAppear(obj_Input);
            if (obj_Input.Displayed)
            {
                if (!string.IsNullOrEmpty(obj_Input.Text))
                {
                    return;
                }

                obj_Input.Click();
            }
            Thread.Sleep(5000);

            IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']"));
            WaitForObjectToAppear(obj_Input1);
            if (obj_Input1.Displayed)
            {
                obj_Input1.Clear();
                obj_Input1.SendKeys(_valueToType);
                Thread.Sleep(2000);
                GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtERSskill']/following::div[1]/div[1]/div[1]")).Click();
                RG_Instance.ReportStepLog(_valueToType + " : has been entered in skill textbox", "DONE", false);
            }
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//i[@class='icon-add addIcon openNextCol']")).Click();
            Thread.Sleep(2000);
            GlobalVariable.driver.FindElement(By.XPath("//input[contains(@id,'primary')]/ancestor::td[1]")).Click();
            Thread.Sleep(2000);
            try
            {
                GlobalVariable.driver.FindElement(By.XPath("//button[text()='Done']")).Click();
            }
            catch
            {
                GlobalVariable.driver.FindElement(By.XPath("//div[@id='addi']//input[1]")).Click();
            }
            RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
        }

        public void SetValueInJobFilter(string _valueToType, string _objDesc)
        {
            //IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//i[contains(@class, 'icon-filter prefix jobFilter')]"));
            IWebElement obj_Input = GlobalVariable.driver.FindElement(By.XPath("//i[@class = 'icon-filter prefix jobFilter' or contains(@class, 'icon-filter prefix jobFilter')]"));
            WaitForObjectToAppear(obj_Input);
            bool bflag = true;
            if (IsObjectExist("xpath://i[@class = 'icon-filter prefix jobFilter' or contains(@class, 'icon-filter prefix jobFilter')]", "Job Filter icon"))
            {
                obj_Input.Click();
            }
            else
            {
                SetTextInElement("xpath://input[@id='JobID']", "analyst", false, "Job Field");
                ClickElement("xpath://div[@class='tt-dataset tt-dataset-jobs']//div[1]", "Job");
                bflag = false;
            }
            Thread.Sleep(2000);
            if(bflag)
            {
                IWebElement obj_Input1 = GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtJob']"));
                WaitForObjectToAppear(obj_Input1);
                if (obj_Input1.Displayed)
                {
                    obj_Input1.SendKeys(OpenQA.Selenium.Keys.Control + "A");
                    obj_Input1.SendKeys(OpenQA.Selenium.Keys.Delete);
                    obj_Input1.SendKeys(_valueToType);
                    Thread.Sleep(5000);
                    GlobalVariable.driver.FindElement(By.XPath("//input[@id='txtJob']/following::div[1]/div[1]/div[1]")).Click();
                    Thread.Sleep(8000);
                    //IWebElement job2 = GlobalVariable.driver.FindElement(By.XPath("//div[@role='dialog']//li[2]//span[1]"));
                    string demandType = EF_Instance.ReadValueFromExcel(GlobalVariable.dataFilePath, "Requisition", GlobalVariable.currentIteration, "DemandType");
                    if (demandType.ToLower().Equals("cyberjob"))
                    {
                        try
                        {
                            GlobalVariable.driver.FindElement(By.XPath("//ul[@class='jobListUL']/li[2]")).Click();
                        }
                        catch
                        {
                            GlobalVariable.driver.FindElement(By.XPath("//ul[@class='jobListUL']/li[1]")).Click();
                        }
                        
                    }
                    else
                    {
                        GlobalVariable.driver.FindElement(By.XPath("//ul[@class='jobListUL']/li[1]")).Click();
                    }                   
                }
                Thread.Sleep(5000);
                IWebElement btnDone = GlobalVariable.driver.FindElement(By.XPath("//button[@class='btn primary-button updatejob']"));
                PerformMouseClick(btnDone);
            }           
            RG_Instance.ReportStepLog("<b>" + _valueToType + "</b> : entered successfully in : " + _objDesc, "Done", false);
        }

        public void PerformMouseClick(IWebElement _obj)
        {
            Actions act = new Actions(GlobalVariable.driver);
            act.MoveToElement(_obj).Click().Build().Perform();
            act = null;
        }

        public void SetCalendarDate(string _objProp, string _dateToSet, string _objDesc)
        {            
            IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
            //js.ExecuteScript("document.getElementById('" + _objProp.Split(':')[1] + "').readOnly = 'false';");
            js.ExecuteScript("document.getElementById('" + _objProp.Split(':')[1] + "').value = ' ';");
            js.ExecuteScript("document.getElementById('" + _objProp.Split(':')[1] + "').value = '" + _dateToSet + "';");
                
            RG_Instance.ReportStepLog("<b>" + _dateToSet + "</b> : set successfully in the calendar : " + _objDesc, "Done", false);
        }

        public int GetElementCount(string objectXPath)
        {
            try
            {
                IList<IWebElement> objList = GlobalVariable.driver.FindElements(By.XPath(objectXPath));
                RG_Instance.ReportStepLog("Object Occurrence on the page : " + objList.Count.ToString(), "Done", false);
                return objList.Count;
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog("No such object found on the page " + e.Message.ToString(), "Fail", true);
                return 0;
            }
        }

        public void FileUpload(string filePath)
        {
            try
            {
                PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//i[@class=' icon-upload prefix']")));
                AutoItX.Init();
                AutoItX.WinWait("Open");
                AutoItX.WinActivate("Open");
                IntPtr winHandle = AutoItX.WinGetHandle("Open");
                AutoItX.Send(filePath);
                AutoItX.Send("{ENTER}");
                ////AutoItX.WinWaitActive("Open");
                ////IntPtr winHandle = AutoItX.WinGetHandle("Open");
                ////AutoItX.Send(filePath);
                ////AutoItX.WinKill(winHandle);
                //PerformMouseClick(GlobalVariable.driver.FindElement(By.XPath("//i[@class=' icon-upload prefix']")));
                //GlobalVariable.driver.SwitchTo().ActiveElement();
                //Thread.Sleep(5000);
                //SendKeys.Send(filePath);
                //Thread.Sleep(5000);
                //SendKeys.Send("{ENTER}");
                RG_Instance.ReportStepLog("File uploaded successfully", "Done", false);
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog("Failed to upload file", "Fail", true);
            }
        }

        public void SetFileToUpload(string _objProp, string _filePath, string _objDesc)
        {
            try
            {
                IWebElement obj = TestObject(_objProp);
                WaitForObjectToAppear(obj);
                if (obj.Displayed)
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                    //js.ExecuteScript("arguments[0].readOnly = 'false';", obj);
                    js.ExecuteScript("arguments[0].value = '" + _filePath + "';", obj);

                    RG_Instance.ReportStepLog("<b>" + _filePath + "</b> : set successfully in : " + _objDesc, "Done", false);
                }
                else
                {
                    RG_Instance.ReportStepLog(_objDesc + " : field not exists on the screen", "Fail", true);
                }
            }
            catch (Exception e)
            {
                RG_Instance.ReportStepLog(e.Message.ToString(), "Fail", true);
            }
        }


        public string GenerateRandomNumber()
        {
            Random rd = new Random();
            string rdNum = rd.Next(9999).ToString();
            RG_Instance.UpdateLogFile("Generated Random Number : " + rdNum);
            return rdNum;
        }

        public void SwitchToParentWindow()
        {
            IList<string> multiWin = GlobalVariable.driver.WindowHandles.ToList();
            foreach (string winHandle in multiWin)
            {
                string winTitle = GlobalVariable.driver.SwitchTo().Window(winHandle).Title.ToString();
                if (winTitle.ToLower().Replace(" ", "").Contains("smartrecruit"))
                {
                    GlobalVariable.driver.SwitchTo().Window(winHandle);
                    break;
                }
            }
        }

        public void SwitchToNewWindow(string _windowTitle)
        {
            IList<string> multiWin = GlobalVariable.driver.WindowHandles.ToList();
            foreach (string winHandle in multiWin)
            {
                string winTitle = GlobalVariable.driver.SwitchTo().Window(winHandle).Title.ToString();
                if (winTitle.ToLower().Replace(" ", "").Contains(_windowTitle))
                {
                    GlobalVariable.driver.SwitchTo().Window(winHandle);
                    break;
                }
            }
        }

        public void CloseExtraWindow()
        {
            IList<string> multiWin = GlobalVariable.driver.WindowHandles.ToList();
            foreach (string winHandle in multiWin)
            {
                string winUrl = GlobalVariable.driver.SwitchTo().Window(winHandle).Url.ToString();
                if (winUrl.ToLower().Contains("popup.htm"))
                {
                    GlobalVariable.driver.SwitchTo().Window(winHandle).Close();
                }
            }

            IList<string> multiWind = GlobalVariable.driver.WindowHandles.ToList();
            GlobalVariable.driver.SwitchTo().Window(multiWind.First());
        }

        public void HandlePopupAlert(string operation)
        {

            WebDriverWait wait = new WebDriverWait(GlobalVariable.driver, TimeSpan.FromMinutes(3));
            wait.Until(ExpectedConditions.AlertIsPresent());

            if (operation != string.Empty && operation.ToLower() == "accept")
            {
                GlobalVariable.driver.SwitchTo().Alert().Accept();
                RG_Instance.ReportStepLog("Popup alert appeared on the screen and clicked OK button", "Done", false);
            }
            else if (operation != string.Empty && operation.ToLower() == "dismiss")
            {
                GlobalVariable.driver.SwitchTo().Alert().Dismiss();
                RG_Instance.ReportStepLog("Popup alert appeared on the screen and clicked CANCEL button", "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(operation + " : Invalid operation", "Fail", true);
            }

        }

        public string GetPopupText()
        {
            WebDriverWait wait = new WebDriverWait(GlobalVariable.driver, TimeSpan.FromMinutes(3));
            wait.Until(ExpectedConditions.AlertIsPresent());

            string popupText = GlobalVariable.driver.SwitchTo().Alert().Text.ToString();
            GlobalVariable.driver.SwitchTo().Alert().Accept();
            RG_Instance.ReportStepLog("Popup displayed with text - "+"<b>" + popupText + "</b>", "Done", false);
            return popupText;
        }

        public string GetDropdownValue(string _objProp, string _objDesc)
        {
            string selectedVal = null;
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Displayed)
            {
                SelectElement se = new SelectElement(obj);
                selectedVal = se.SelectedOption.Text;
                RG_Instance.ReportStepLog("<b>" + selectedVal + "</b> : value fetched from dropdown : " + _objDesc, "Done", false);
            }
            else
            {
                RG_Instance.ReportStepLog(_objDesc + " : control not exists on the screen", "Fail", true);
            }

            return selectedVal;
        }
        private bool WaitForObjectToAppear(IWebElement _objct)
        {
            int counter = 0;
            bool bFlag = true;
            try
            {
                do
                {
                    Thread.Sleep(2000);
                    counter++;
                    if (counter == 70)
                    {
                        bFlag = false;
                        break;
                    }
                    bFlag = true;
                } while (_objct.Displayed == false);

                IJavaScriptExecutor js = (IJavaScriptExecutor)GlobalVariable.driver;
                //js.ExecuteScript("arguments[0].scrollIntoView(true);", _objct);
                var str = String.Format("window.scrollTo({0}, {1})", 0, _objct.Location.Y - 150);
                js.ExecuteScript(str);

                return bFlag;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void WaitForObjectPresent(string _objProp)
        {

            IWebElement obj = TestObject(_objProp);
            WebDriverWait wait = new WebDriverWait(GlobalVariable.driver, TimeSpan.FromMinutes(6));
            IWebElement oEle = wait.Until(ExpectedConditions.ElementToBeClickable(obj));

        }

        public bool IsObjectExist(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            //WaitForObjectToAppear(obj);
            if (obj != null)
            {
                if (obj.Displayed)
                {
                    RG_Instance.UpdateLogFile(_objDesc + " : Object exists on the page");
                    return true;
                }
                else
                {
                    RG_Instance.UpdateLogFile(_objDesc + " : No such object exists on the page");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsObjectEnabled(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            WaitForObjectToAppear(obj);
            if (obj.Enabled)
            {
                RG_Instance.UpdateLogFile(_objDesc + " : Object appeared as enabled");
                return true;
            }
            else
            {
                RG_Instance.UpdateLogFile(_objDesc + " : Object appeared as disabled");
                return false;
            }
        }

        public bool IsObjectReadOnly(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            try
            {
                if( (obj.GetAttribute("readonly").ToLower() == "true") || (obj.GetAttribute("readonly") == ""))
                {
                    RG_Instance.UpdateLogFile(_objDesc + " : Object in read-only mode");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            { 
                RG_Instance.UpdateLogFile(_objDesc + " : Object not in read-only mode");
                return false;
            }
        }

        public void PressEnterKey(string _objProp, string _objDesc)
        {
            try
            {
                IWebElement obj = TestObject(_objProp);
                WaitForObjectToAppear(obj);
                if (obj.Enabled)
                {
                    obj.SendKeys(OpenQA.Selenium.Keys.Return);
                    RG_Instance.UpdateLogFile(_objDesc + " pressed successfully");
                }
                else
                {
                    RG_Instance.UpdateLogFile(_objDesc + " failed to press");
                    GlobalVariable.testStepStatus = false;
                }
            }
            catch (Exception e)
            {
                RG_Instance.UpdateLogFile(_objDesc + " : " + e.Message.ToString());
                GlobalVariable.testStepStatus = false;
            }
        }

        public void SetFocusOnIFrame(string _objProp, string _objDesc)
        {
            IWebElement obj = TestObject(_objProp);
            GlobalVariable.driver.SwitchTo().Frame(obj);
            RG_Instance.UpdateLogFile(_objDesc + " : focus set to frame successfully");
        }

        public void RemoveFocusFromIFrame()
        {
            GlobalVariable.driver.SwitchTo().DefaultContent();

        }

        public bool VerifyElementValue(string _objProp, string _valToCompare, bool _compareFlag, string _objDesc)
        {
            if (_valToCompare != null)
            {
                _valToCompare = _valToCompare.Trim();
                _valToCompare = _valToCompare.ToLower();
            }

            string _actualVal = GetElementText(_objProp, _objDesc);

            if (_actualVal != null)
            {
                _actualVal = _actualVal.Trim();
                _actualVal = _actualVal.ToLower();
            }

            if (_compareFlag)
            {
                if (_actualVal.Equals(_valToCompare))
                {
                    RG_Instance.ReportStepLog("Expected values matched successfully: <b>" + _valToCompare + "</b>", "Pass", false);
                    return true;
                }
                else
                {
                    RG_Instance.ReportStepLog("Actual value : <b>" + _actualVal + "</b> not matches expected value : <b>" + _valToCompare + "</b>", "Fail", true);
                    return false;
                }
            }
            else
            {
                if (!_actualVal.Equals(_valToCompare))
                {
                    RG_Instance.ReportStepLog("Actual value : <b>" + _actualVal + "</b> not matches expected value : <b>" + _valToCompare + "</b>", "Pass", false);
                    return true;
                }
                else
                {
                    RG_Instance.ReportStepLog("Expected values matched : <b>" + _valToCompare + "</b>", "Fail", true);
                    return false;
                }
            }
        }

        public bool CompareValue(string _actualVal, string _expectVal, bool _compareFlag)
        {
            if (_expectVal != null)
            {
                _expectVal = _expectVal.Trim();
                _expectVal = _expectVal.ToLower();
            }

            if (_actualVal != null)
            {
                _actualVal = _actualVal.Trim();
                _actualVal = _actualVal.ToLower();
            }

            if (_compareFlag)
            {
                if (_actualVal.Contains(_expectVal))
                {
                    RG_Instance.ReportStepLog("Expected values matched successfully: <b>" + _expectVal + "</b>", "Pass", false);
                    return true;
                }
                else
                {
                    RG_Instance.ReportStepLog("Actual value : <b>" + _actualVal + "</b> not matches expected value : <b>" + _expectVal + "</b>", "Fail", true);
                    return false;
                }
            }
            else
            {
                if (!_actualVal.Contains(_expectVal))
                {
                    RG_Instance.ReportStepLog("Actual value : <b>" + _actualVal + "</b> not matches expected value : <b>" + _expectVal + "</b>", "Pass", false);
                    return true;
                }
                else
                {
                    RG_Instance.ReportStepLog("Expected values matched : <b>" + _expectVal + "</b>", "Fail", true);
                    return false;
                }
            }
        }

        public void CompareDate(string _firstDate, string _operator, string _secondDate, string _objDesc)
        {

            DateTime firstDate = DateTime.ParseExact(_firstDate, "dd-MMMM-yyyy", null);
            DateTime secondDate = DateTime.ParseExact(_secondDate, "dd-MMMM-yyyy", null);
            int returnVal = DateTime.Compare(firstDate, secondDate);

            if (_operator.Equals("="))
            {
                if (returnVal == 0)
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                }
                else
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                    GlobalVariable.testStepStatus = false;
                }
            }
            else if (_operator.Equals("<"))
            {
                if (returnVal < 0)
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                }
                else
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                    GlobalVariable.testStepStatus = false;
                }
            }
            else if (_operator.Equals(">"))
            {
                if (returnVal > 0)
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                }
                else
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                    GlobalVariable.testStepStatus = false;
                }
            }
            else if (_operator.Equals("!"))
            {
                if (returnVal != 0)
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                    GlobalVariable.testStepStatus = false;
                }
                else
                {
                    RG_Instance.UpdateLogFile("<b><font color='#8A2BE2'>Validation: </font></b>" + _firstDate + " : " + _objDesc + " : " + _secondDate);
                }
            }
        }


    }
}