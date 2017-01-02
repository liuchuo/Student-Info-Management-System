using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace StudentInfoManagmentSystem.Util {
    public static class ExcelIO {

        public static DataTable Read(string filepath) {
            int flag = (filepath.EndsWith(".xls")) ? 0 : (filepath.EndsWith(".xlsx") ? 1 : 2);

            if (flag == 2 || !File.Exists(filepath)) return null;

            var dt = new DataTable();
            try {
                IWorkbook workbook;
                using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read)) {
                    switch (flag) {
                        case 0: workbook = new HSSFWorkbook(stream); break;
                        case 1: workbook = new XSSFWorkbook(stream); break;
                        default: return null;
                    }
                }

                ISheet sheet = workbook.GetSheetAt(0);
                dt.TableName = sheet.SheetName;

                // write header row
                IRow headerRow = sheet.GetRow(0);
                foreach (ICell headerCell in headerRow) {
                    dt.Columns.Add(headerCell.ToString());
                }

                // write the rest
                int rowIndex = 0;
                foreach (IRow row in sheet) {
                    // skip header row
                    if (rowIndex++ == 0) continue;
                    DataRow dataRow = dt.NewRow();
                    dataRow.ItemArray = row.Cells.Select(c => c.ToString()).ToArray();
                    dt.Rows.Add(dataRow);
                }
            } catch (Exception e) {
                MessageBox.Show(e.Message);
                return null;
            }
            return dt;
        }

        
        public static void Write(string filepath, DataTable dt) {
            var ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            Write(filepath, ds);
        }

        public static void Write(string filepath, DataSet ds) {
            int flag = (filepath.EndsWith(".xls")) ? 0 : (filepath.EndsWith(".xlsx") ? 1 : 2);

            if (flag == 2 || ds.Tables.Count <= 0) return;

            try {
                IWorkbook workbook;
                switch (flag) {
                    case 0: workbook = new HSSFWorkbook(); break;
                    case 1: workbook = new XSSFWorkbook(); break;
                    default: return;
                }

                for (int i = 0; i < ds.Tables.Count; i++) {
                    ISheet sheet = workbook.CreateSheet(ds.Tables[i].TableName);

                    IRow row = sheet.CreateRow(0);
                    for (int colindex = 0; colindex < ds.Tables[i].Columns.Count; colindex++) {
                        row.CreateCell(colindex, CellType.String).SetCellValue(ds.Tables[i].Columns[colindex].ColumnName);
                    }
                    for (int rowindex = 1; rowindex <= ds.Tables[i].Rows.Count; rowindex++) {
                        row = sheet.CreateRow(rowindex);
                        for (int colindex = 0; colindex < ds.Tables[i].Columns.Count; colindex++) {
                            row.CreateCell(colindex, CellType.String).SetCellValue(ds.Tables[i].Rows[rowindex-1][colindex].ToString());
                        }
                    }
                }

                using (var stream = File.Create(filepath)) {
                    workbook.Write(stream);
                }

            } catch (Exception e) {
                MessageBox.Show(e.Message);
                return;
            }
        }
        
    }
}
