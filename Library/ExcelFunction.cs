/*******************************************************************************
 * About - This library contains all the functions related to excel read/write
 * Author - Amit Gupta (51679563)
 * Created - 15-Nov-18
 * Proprietary - BTIS Team/HCL Technologies Ltd.
 *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace SR_Automation.Library
{
    class ExcelFunction
    {
        //public string ReadValueFromExcel(string filePath, string sheetName, int recordNum, string columnName)
        //{
        //    Excel.Application xlApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    Excel.Range range;

        //    string dataValue = string.Empty;
        //    int cCnt = 0;

        //    try
        //    {
        //        xlApp = new Excel.Application();
        //        xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(sheetName);
        //        range = xlWorkSheet.UsedRange;

        //        for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
        //        {
        //            string var = (string)(range.Cells[1, cCnt] as Excel.Range).Value2;
        //            if (var.ToLower().Trim().Equals(columnName.ToLower().Trim()))
        //            {
        //                dataValue = Convert.ToString((range.Cells[recordNum + 1, cCnt] as Excel.Range).Value);
        //                dataValue = dataValue.Trim();
        //                break;
        //            }
        //        }

        //        xlApp.DisplayAlerts = false;
        //        xlWorkBook.Close(false, null, null);
        //        xlApp.Quit();

        //        ReleaseObject(xlWorkSheet);
        //        ReleaseObject(xlWorkBook);
        //        ReleaseObject(xlApp);

        //        return dataValue;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.Out.WriteLine(e.ToString());
        //        return null; 
        //    }

        //}
        public string ReadValueFromExcel(string filePath, string sheetName, int recordNum, string columnName)
        {
            string dataValue = string.Empty;
            int rowCounter = 1;

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (!reader.Name.Trim().Equals(sheetName.Trim()))
                        {
                            reader.NextResult();
                        }

                        reader.Read();
                        for (int c = 0; c < reader.FieldCount; c++)
                        {
                            if (reader.GetString(c).ToLower().Trim().Equals(columnName.ToLower().Trim()))
                            {
                                while (reader.Read())
                                {
                                    ++rowCounter;
                                    if ((rowCounter - 1) == recordNum)
                                    {
                                        break;
                                    }
                                }
                                dataValue = Convert.ToString(reader.GetValue(c)).Trim();
                                break;
                            }
                        }
                    }
                }

                return dataValue;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
                return null;
            }
        }

        public void StoreValueInExcel(string filePath, string sheetName, int recordNum, string columnName, string valToStore)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            string dataValue = string.Empty;
            int cCnt = 0;

            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(sheetName);
                range = xlWorkSheet.UsedRange;

                for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                {
                    string var = (string)(range.Cells[1, cCnt] as Excel.Range).Value2;
                    if (var.ToLower().Trim().Equals(columnName.ToLower().Trim()))
                    {
                        (range.Cells[recordNum + 1, cCnt] as Excel.Range).Value = valToStore;
                        break;
                    }
                }

                xlApp.DisplayAlerts = false;
                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
        }

        //public string ReadSuiteFile(string filePath)
        //{
        //    Excel.Application xlApp;
        //    Excel.Workbook xlWorkBook;
        //    Excel.Worksheet xlWorkSheet;
        //    Excel.Range range;

        //    int lineCounter = 0;
        //    string testCaseNames = string.Empty;
        //    string moduleName = null, entityName = null;
        //    string tcName = null, methodName = null, iterationStart = null, iterationEnd = null, testDesc = null;

        //    GlobalVariable.skipDic = new Dictionary<string, int>();

        //    try
        //    {
        //        xlApp = new Excel.Application();
        //        xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //        range = xlWorkSheet.UsedRange;

        //        for (int rCnt = 3; rCnt <= range.Rows.Count; rCnt++)
        //        {
        //            string serialNo = Convert.ToString((range.Cells[rCnt, 1] as Excel.Range).Value2);
        //            if (serialNo == null)
        //            {
        //                lineCounter++;
        //            }
        //            else
        //            {
        //                lineCounter = 0;
        //            }

        //            if (lineCounter == 3)
        //            {
        //                break;
        //            }

        //            string status = (range.Cells[rCnt, 2] as Excel.Range).Value;
        //            if (status != null)
        //            {
        //                if (status.ToLower().Equals("yes"))
        //                {
        //                    entityName = (string)(range.Cells[rCnt, 4] as Excel.Range).Value;
        //                    tcName = (string)(range.Cells[rCnt, 5] as Excel.Range).Value;
        //                    methodName = (string)(range.Cells[rCnt, 6] as Excel.Range).Value;
        //                    iterationStart = Convert.ToString((range.Cells[rCnt, 7] as Excel.Range).Value2);
        //                    iterationEnd = Convert.ToString((range.Cells[rCnt, 8] as Excel.Range).Value2);
        //                    testDesc = Convert.ToString((range.Cells[rCnt, 9] as Excel.Range).Value2);

        //                    if (testCaseNames == string.Empty)
        //                    {
        //                        testCaseNames = entityName + ":" + tcName + ":" + methodName + ":" + iterationStart + ":" + iterationEnd + ":" + testDesc;
        //                    }
        //                    else
        //                    {
        //                        testCaseNames += "#" + entityName + ":" + tcName + ":" + methodName + ":" + iterationStart + ":" + iterationEnd + ":" + testDesc;
        //                    }

        //                    if (!methodName.ToLower().Contains("login") && !methodName.ToLower().Contains("logout"))
        //                    {
        //                        moduleName = entityName;

        //                        if (!GlobalVariable.skipDic.ContainsKey(moduleName))
        //                        {
        //                            GlobalVariable.skipDic[moduleName.Replace(" ", "")] = 1;
        //                        }
        //                        else
        //                        {
        //                            GlobalVariable.skipDic[moduleName.Replace(" ", "")] += 1;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        GlobalVariable.testSkipCount -= 1;
        //                    }
        //                }
        //            }
        //        }

        //        xlApp.DisplayAlerts = false;
        //        xlWorkBook.Close(false, null, null);
        //        xlApp.Quit();

        //        ReleaseObject(xlWorkSheet);
        //        ReleaseObject(xlWorkBook);
        //        ReleaseObject(xlApp);

        //        return testCaseNames;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.Out.WriteLine(e.ToString());
        //        return null;
        //    }
        //}
        public string ReadSuiteFile(string filePath)
        {
            int lineCounter = 0;
            string testCaseNames = string.Empty;
            string moduleName = null, entityName = null;
            string tcName = null, methodName = null, iterationStart = null, iterationEnd = null, testDesc = null;

            GlobalVariable.skipDic = new Dictionary<string, int>();

            try
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        while (!reader.Name.Trim().Equals("SuiteExcel"))
                        {
                            reader.NextResult();
                        }

                        while (reader.Read())
                        {
                            ++lineCounter;
                            if (lineCounter < 3)
                            {
                                continue;
                            }
                            string status = reader.GetString(1);
                            if (status != null)
                            {
                                if (status.ToLower().Equals("yes"))
                                {
                                    entityName = reader.GetString(3).Trim();
                                    tcName = reader.GetString(4).Trim();
                                    methodName = reader.GetString(5).Trim();
                                    iterationStart = reader.GetString(6).Trim();
                                    iterationEnd = reader.GetString(7).Trim();
                                    testDesc = reader.GetString(8).Trim();

                                    if (testCaseNames == string.Empty)
                                    {
                                        testCaseNames = entityName + ":" + tcName + ":" + methodName + ":" + iterationStart + ":" + iterationEnd + ":" + testDesc;
                                    }
                                    else
                                    {
                                        testCaseNames += "#" + entityName + ":" + tcName + ":" + methodName + ":" + iterationStart + ":" + iterationEnd + ":" + testDesc;
                                    }

                                    if (!methodName.ToLower().Contains("login") && !methodName.ToLower().Contains("logout"))
                                    {
                                        moduleName = entityName;

                                        if (!GlobalVariable.skipDic.ContainsKey(moduleName))
                                        {
                                            GlobalVariable.skipDic[moduleName.Replace(" ", "")] = 1;
                                        }
                                        else
                                        {
                                            GlobalVariable.skipDic[moduleName.Replace(" ", "")] += 1;
                                        }
                                    }
                                    else
                                    {
                                        GlobalVariable.testSkipCount -= 1;
                                    }
                                }
                            }

                        }
                    }
                }

                return testCaseNames;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
                return null;
            }
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }


    }
}
