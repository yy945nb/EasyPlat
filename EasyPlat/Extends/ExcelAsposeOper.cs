using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlat.Extends
{
    public class ExcelAsposeOper
    {
        private static object lockObj = new object();

        /// <summary>
        /// 生产excel文件并保存
        /// </summary>
        /// <param name="excelWork"></param>
        /// <param name="isSaveToHtml">是否以html格式保存，若为true 则以html文件保存，默认为false</param>
        public static void ExportExcel(ExcelWork excelWork, bool isSaveToHtml = false)
        {
            lock (lockObj)
            {
                if (excelWork == null || !FileCheck(excelWork.FilePath))
                    return;
                if (excelWork.ExcelSheet == null || !excelWork.ExcelSheet.Any())
                    return;
                Workbook workbook = new Workbook();
                //清除页
                workbook.Worksheets.Clear();
                for (var i = 0; i < excelWork.ExcelSheet.Count; i++)
                {
                    if (excelWork.ExcelSheet[i].SheetDataGrid == null)
                        continue;
                    GenerateWorkSheet(workbook, i, excelWork.ExcelSheet[i]);
                }
                DeleteFile(excelWork.FilePath);
                if (!isSaveToHtml)
                    workbook.Save(excelWork.FilePath);
                else
                    workbook.Save(excelWork.FilePath, SaveFormat.Html);
            }
        }

        /// <summary>
        /// 生成excel 单个Sheet内容
        /// </summary>
        /// <param name="workbook">excel整个工作薄，包含一个或多个Sheet</param>
        /// <param name="sheetIndex">Sheet索引</param>
        /// <param name="excelSheet">要生成Sheet的内容数据</param>
        static void GenerateWorkSheet(Workbook workbook, int sheetIndex, ExcelSheet excelSheet)
        {
            if (excelSheet == null || excelSheet.SheetDataGrid == null)
                return;
            workbook.Worksheets.Add(excelSheet.SheetName);

            Worksheet sheet = workbook.Worksheets[sheetIndex];
            Cells cells = sheet.Cells;
            int rowIndex = 0;
            GenerateExcelTitle(cells, excelSheet.TitleContent, ref rowIndex);
            GenerateExcelColumnTitle(cells, excelSheet.SheetDataGrid.DataGridColumn, ref rowIndex);
            GenerateExcelGridContent(cells, excelSheet.SheetDataGrid, excelSheet.SheetDataGrid.MergeCellByCols, ref rowIndex);
            GenerateExcelFootContent(cells, excelSheet.FootContent, ref rowIndex);
            SetColumnsWidth(cells, excelSheet.SheetDataGrid.DataGridColumn.ShowColumnWidth);
        }

        /// <summary>
        /// 生成excel的标题行
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="titles">标题集合，根据定义的CellContent值可以指定标题行所在位置，样式等属性</param>
        /// <param name="rowIndex">记录当前sheet的末尾行索引，改方法返回的rowIndex的值最后一行所在行索引</param>
        static void GenerateExcelTitle(Cells cells, List<CellContent> titles, ref int rowIndex)
        {
            if (titles == null || !titles.Any())
                return;
            foreach (var obj in titles)
            {
                if (obj == null)
                    continue;
                cells.CreateRange(obj.RowIndex, obj.ColumnIndex, obj.TotalRows, obj.TotalColumns).Merge();
                ExcelCellContent.SetCellContent(obj.CellValue, obj.CellStyle, cells[obj.RowIndex, obj.ColumnIndex],
                    (obj.RowHeight <= 0 ? 20 : obj.RowHeight));
                rowIndex = obj.RowIndex;
            }
        }

        /// <summary>
        /// 生成数据列表在Sheet 显示的列头
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="dgColumn">定义的每一列属性</param>
        /// <param name="rowIndex">记录当前sheet的末尾行索引，改方法返回的rowIndex的值最后一行所在行索引</param>
        static void GenerateExcelColumnTitle(Cells cells, DataGridColumn dgColumn, ref int rowIndex)
        {
            if (dgColumn == null)
                return;
            if (dgColumn.ShowColumns == null || !dgColumn.ShowColumns.Any())
                return;
            foreach (var obj in dgColumn.ShowColumns)
            {
                if (obj == null)
                    continue;
                cells.CreateRange(obj.RowIndex, obj.ColumnIndex, (obj.TotalRows <= 0 ? 1 : obj.TotalRows), (obj.TotalColumns <= 0 ? 1 : obj.TotalColumns)).Merge();
                ExcelCellContent.SetCellContent(obj.CellValue, obj.CellStyle, cells[obj.RowIndex, obj.ColumnIndex],
                    (obj.RowHeight <= 0 ? 20 : obj.RowHeight));
                rowIndex = obj.RowIndex;
            }
        }

        /// <summary>
        /// 生成数据列表主要内容
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="sheetDataGrid">列表内容数据</param>
        /// <param name="mergeCellByCols">要合并的列</param>
        /// <param name="rowIndex">记录当前sheet的末尾行索引，改方法返回的rowIndex的值最后一行所在行索引</param>
        static void GenerateExcelGridContent(Cells cells, SheetDataGrid sheetDataGrid, Dictionary<string, string> mergeCellByCols, ref int rowIndex)
        {
            if (sheetDataGrid == null || sheetDataGrid.ContentItems == null)
                return;
            if (sheetDataGrid.DataGridColumn == null || sheetDataGrid.DataGridColumn.ShowColumns == null)
                return;
            List<string> columns = sheetDataGrid.DataGridColumn.ShowColumns.Select(o => o.MappingValue).ToList();
            if (columns == null || !columns.Any())
                return;

            DataTable dtContent = sheetDataGrid.ContentItems;
            rowIndex = sheetDataGrid.StartRowIndex;
            DataRow preDataRow = null;
            foreach (DataRow row in dtContent.Rows)
            {
                for (var col = 0; col < columns.Count; col++)
                {
                    if (string.IsNullOrEmpty(columns[col]))
                        continue;
                    var cellValue = row[columns[col]] == null ? "" : row[columns[col]].ToString();

                    ExcelCellContent.SetCellContent(cellValue, sheetDataGrid.ContentStyle ?? ExcelCellContent.ContentStyle,
                    cells[rowIndex, col], (sheetDataGrid.ContentRowHeight <= 0 ? 20 : sheetDataGrid.ContentRowHeight));

                    if (dtContent.Columns.Contains("IsMerge") && row["IsMerge"] != null && Convert.ToBoolean(row["IsMerge"]) == false)
                        continue;
                    if (mergeCellByCols != null && mergeCellByCols.Keys.Contains(columns[col]))
                    {
                        List<string> mergeCellByColsList = new List<string>();
                        if (!string.IsNullOrEmpty(mergeCellByCols[columns[col]]))
                        {
                            mergeCellByColsList.AddRange(mergeCellByCols[columns[col]].Split(','));
                        }
                        if (!mergeCellByColsList.Contains(columns[col]))
                            mergeCellByColsList.Add(columns[col]);
                        MergeCell(cells, dtContent, preDataRow, row, mergeCellByColsList, rowIndex, col);
                    }
                }
                rowIndex++;
                preDataRow = row;
            }
            rowIndex = rowIndex - 1;//减去循环完后新增的多余一行索引
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="dtContent">sheet主要显示的全部内容</param>
        /// <param name="preDataRow">前一行数据</param>
        /// <param name="curDataRow">当前行数据</param>
        /// <param name="mergeCellByCols">要合并的列</param>
        /// <param name="rowIndex">合并内容的行索引param>
        /// <param name="columnIndex">合并内容的列索引</param>
        static void MergeCell(Cells cells, DataTable dtContent, DataRow preDataRow, DataRow curDataRow,
            List<string> mergeCellByCols, int rowIndex, int columnIndex)
        {
            if (curDataRow == null || dtContent == null)
                return;

            string preStr = "";
            string curStr = "";
            string strExpr = "";
            foreach (var col in mergeCellByCols)
            {
                if (string.IsNullOrEmpty(col))
                    continue;
                curStr += curDataRow[col] == null || curDataRow[col] == DBNull.Value ? "" : curDataRow[col].ToString();
                if (strExpr != "")
                    strExpr += "+";
                strExpr += "IsNull([" + col + "],'')";

                if (preDataRow != null)
                    preStr += preDataRow[col] == null || preDataRow[col] == DBNull.Value ? "" : preDataRow[col].ToString();
            }

            if (curStr != preStr)
            {
                int mergeRows = string.IsNullOrEmpty(strExpr) ? 1 : dtContent.Select(strExpr + "=" + "'" + curStr + "'").Count();
                cells.Merge(rowIndex, columnIndex, mergeRows, 1);
            }
        }

        /// <summary>
        /// 生成sheet底部显示内容
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="footContent">定义的底部显示内容、样式等信息</param>
        /// <param name="rowIndex">记录当前sheet的末尾行索引，改方法返回的rowIndex的值最后一行所在行索引</param>
        static void GenerateExcelFootContent(Cells cells, List<CellContent> footContent, ref int rowIndex)
        {
            if (footContent == null || !footContent.Any())
                return;
            foreach (var obj in footContent)
            {
                if (obj == null)
                    continue;

                cells.CreateRange(obj.RowIndex + rowIndex, obj.ColumnIndex, obj.TotalRows, obj.TotalColumns).Merge();
                ExcelCellContent.SetCellContent(obj.CellValue, cells[obj.RowIndex + rowIndex, obj.ColumnIndex],
                    (obj.RowHeight <= 0 ? 20 : obj.RowHeight));
                for (var i = 0; i < obj.TotalColumns; i++)
                {
                    cells[obj.RowIndex + rowIndex, obj.ColumnIndex + i].SetStyle(obj.CellStyle);
                }
            }
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="columnWidth"></param>
        static void SetColumnsWidth(Cells cells, int[] columnWidth)
        {
            if (columnWidth == null)
                return;
            if (!columnWidth.Any())
                return;
            for (int i = 0; i < columnWidth.Length; i++)
            {
                cells.SetColumnWidth(i, columnWidth[i]);
            }
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="columnIndex"></param>
        /// <param name="columnWidth"></param>
        static void SetColumnsWidth(Cells cells, int columnIndex, int columnWidth)
        {
            if (columnWidth <= 0)
                return;
            cells.SetColumnWidth(columnIndex, columnWidth);
        }

       /// <summary>
       /// 删除指定目录下的文件， 先判断该文件是否存在，若存在则删除，
       /// 然后在获取该目录下的所有文件，删除文件创建时间大于指定时间的文件
       /// </summary>
       /// <param name="path"></param>
        static void DeleteFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
                File.Delete(path);

            string[] files = Directory.GetFiles(fileInfo.DirectoryName);
            foreach (var obj in files)
            {
                FileInfo fi = new FileInfo(obj);
                TimeSpan ts = DateTime.Now - fi.CreationTime;
                if(ts.Minutes>10)
                    File.Delete(obj);
            }
        }

        /// <summary>
        /// 检查目录路径是否存在，若不存在则创建路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileCheck(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            FileInfo fileInfo = new FileInfo(path);

            if (!Directory.Exists(fileInfo.Directory.ToString()))
                Directory.CreateDirectory(fileInfo.Directory.ToString());
            return true;
        }
    }
}
