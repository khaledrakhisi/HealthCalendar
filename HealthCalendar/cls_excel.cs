using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace HealthCalendar
{
    class cls_excel
    {
        private Excel.Application xlApp = null;
        private Excel.Workbooks xlWorkBooks = null;
        private Excel.Workbook xlWorkBook = null;
        private Excel.Sheets xlWorkSheets = null;
        private Excel.Worksheet xlWorkSheet = null;
        private Excel.Range xlRange = null;
        private Excel.Range filteredRange = null;
        private Excel.Range areaRange = null;

        private string excelFileFullPath;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePlace">Full excel file path</param>
        /// <param name="sheetNumber">1-Based sheet index</param>
        public cls_excel(string filePlace, int sheetNumber, bool readOnly = true)
        {
            excelFileFullPath = filePlace;
            try
            {
                xlApp = new Excel.Application();
                xlWorkBooks = xlApp.Workbooks;
                xlWorkBook = xlWorkBooks.Open(excelFileFullPath, 0, readOnly, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, 0, true, 1, 0);
                xlWorkSheets = xlWorkBook.Sheets;
                xlWorkSheet = xlWorkSheets.get_Item(sheetNumber);
                xlRange = xlWorkSheet.UsedRange;

                //xlApp.Visible = true; 
            }
            catch (Exception ex)
            {
                cls_utility.Log("cannot open excel file. " + excelFileFullPath + "--" + ex.Message);
            }
        }

        public List<cls_calendar> GetCalendarOccasions(int fetchCount, bool ascendingSort, int nColumn1 = -1, string sCriteria1 = "", int nColumn2 = -1, string sCriteria2 = "")
        {
            List<cls_calendar> result = new List<cls_calendar>();
            try
            {
                filteredRange = null;
                areaRange = null;

                ClearFilter();

                if (nColumn1 != -1)
                {
                    xlRange.AutoFilter(nColumn1, sCriteria1, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                }
                if (nColumn2 != -1)
                {
                    xlRange.AutoFilter(nColumn2, sCriteria2, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);                    
                }
                
                filteredRange = xlRange.SpecialCells(Excel.XlCellType.xlCellTypeVisible);
                if (ascendingSort)
                {
                    filteredRange.Sort(filteredRange.Columns[3], Excel.XlSortOrder.xlDescending);
                }
                
                for (int areaId = 1; areaId <= filteredRange.Areas.Count; areaId++)
                {
                    areaRange = filteredRange.Areas[areaId];
                    object[,] areaValues = areaRange.Value;
                    for (int row = 0; row < areaValues.GetLength(0); row++)
                    {
                        if (areaValues[row + 1, 1].ToString().ToLower() == "id") continue;
                        result.Add(new cls_calendar
                        {
                            id = Convert.ToInt32(areaValues[row+1, 1]),
                            occasion = Convert.ToString(areaValues[row+1, 2]),
                            pDateBegin = Convert.ToString(areaValues[row+1, 3]),
                            pDateEnd = Convert.ToString(areaValues[row+1, 4]),
                            gregorianDate = Convert.ToString(areaValues[row+1, 5]),                           
                        });

                        if (fetchCount != -1 && row >= fetchCount)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in GetCalendarOccasions " + excelFileFullPath + "--" + ex.Message);
            }
            finally
            {
                
            }

            return result;
        }

        public List<cls_advertisment> GetAdvs(int nColumn1 = -1, string sCriteria1 = "", int nColumn2 = -1, string sCriteria2 = "")
        {
            List<cls_advertisment> result = new List<cls_advertisment>();
            try
            {
                filteredRange = null;
                areaRange = null;

                ClearFilter();

                if (nColumn1 != -1)
                {
                    xlRange.AutoFilter(nColumn1, sCriteria1, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                }
                if (nColumn2 != -1)
                {
                    xlRange.AutoFilter(nColumn2, sCriteria2, Excel.XlAutoFilterOperator.xlAnd, Type.Missing, true);
                }
                //xlApp.Visible = true;
                filteredRange = xlRange.SpecialCells(Excel.XlCellType.xlCellTypeVisible);

                for (int areaId = 1; areaId <= filteredRange.Areas.Count; areaId++)
                {
                    areaRange = filteredRange.Areas[areaId];
                    object[,] areaValues = areaRange.Value;
                    for (int row = 0; row < areaValues.GetLength(0); row++)
                    {
                        if (areaValues[row + 1, 1].ToString().ToLower() == "id") continue;
                        result.Add(new cls_advertisment
                        {
                            id = Convert.ToString(areaValues[row + 1, 1]),
                            adv = Convert.ToString(areaValues[row + 1, 2]),
                            pDateBegin = Convert.ToString(areaValues[row + 1, 3]),
                            pDateEnd = Convert.ToString(areaValues[row + 1, 4]),
                            imagePath = Convert.ToString(areaValues[row + 1, 5]),
                            imageOriginalPath = Convert.ToString(areaValues[row + 1, 6]),
                            backColor = Convert.ToString(areaValues[row + 1, 7])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                cls_utility.Log("Error in GetAdvs " + excelFileFullPath + "--" + ex.Message);
            }
            finally
            {

            }

            return result;
        }

        public void ClearFilter()
        {
            xlWorkSheet.AutoFilter.ShowAllData();
        }

        public void InsertRowAtEnd(string col1Value = null, string col2Value = null, string col3Value = null, string col4Value = null, string col5Value = null, string col6Value = null, string col7Value = null)
        {
            int nRowCount = xlRange.Rows.Count + 1;
            Excel.Range line = (Excel.Range)xlWorkSheet.Rows[nRowCount];
            line.Insert();

            xlApp.Cells[nRowCount, 1] = col1Value;
            xlApp.Cells[nRowCount, 2] = col2Value;
            xlApp.Cells[nRowCount, 3] = col3Value;
            xlApp.Cells[nRowCount, 4] = col4Value;
            xlApp.Cells[nRowCount, 5] = col5Value;
            xlApp.Cells[nRowCount, 6] = col6Value; 
            xlApp.Cells[nRowCount, 7] = col7Value; 
        }

        public void SaveChanges()
        {
            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(excelFileFullPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }

        public void DeleteRows(int nRowIndex)
        {
            Excel.Range ran = xlWorkSheet.Rows[nRowIndex];
            ran.Select();
            ran.EntireRow.Delete(Excel.XlDirection.xlUp);
            //areaRange.EntireRow.Delete(Excel.XlDirection.xlUp);
        }

        public void DeleteEntireRange()
        {
            if (filteredRange.Areas.Count > 1)
            {
                Excel.Range dataWithoutFirstRow = xlWorkSheet.Range[xlWorkSheet.UsedRange.Cells[2, 1], xlWorkSheet.UsedRange.SpecialCells(Excel.XlCellType.xlCellTypeLastCell)];
                dataWithoutFirstRow.EntireRow.Delete();
            }
        }

        public void CloseAndRelease()
        {
            try
            {
                xlWorkBook.Close(false);
                xlWorkBooks.Close();
                Marshal.FinalReleaseComObject(areaRange);
                Marshal.FinalReleaseComObject(filteredRange);
                Marshal.FinalReleaseComObject(xlRange);
                Marshal.FinalReleaseComObject(xlWorkSheet);
                Marshal.FinalReleaseComObject(xlWorkSheets);
                Marshal.FinalReleaseComObject(xlWorkBook);
                Marshal.FinalReleaseComObject(xlWorkBooks);
                xlApp.Quit();
                Marshal.FinalReleaseComObject(xlApp);

                GC.Collect();
            }
            catch(Exception ex)
            {
                cls_utility.Log("Error in releasing Excel COM objects. " + ex.Message);
            }
        }
    }
}
