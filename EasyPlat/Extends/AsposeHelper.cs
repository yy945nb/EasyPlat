using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EasyPlat.Extends
{
    public class AsposeHelper
    {
        /// <summary>
        /// 导出EXCEL并且动态生成多级表头
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="group">分组</param>
        /// <param name="dt">dataTable</param>
        /// <param name="path">保存路径</param>
        public static void SaveColumnsHierarchy(List<JqxTableColumns> columns, List<JqxTableColumnsGroup> group, DataTable dt, string path)
        {

            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格
            for (int i = 0; i <= dt.Rows.Count + 1; i++)
            {
                sheet.Cells.SetRowHeight(i, 30);
            }
            List<AsposeCellInfo> acList = new List<AsposeCellInfo>();
            List<string> acColumngroupHistoryList = new List<string>();
            int currentX = 0;
            foreach (var it in columns)
            {
                AsposeCellInfo ac = new AsposeCellInfo();
                ac.y = 0;
                if (it.columngroup == null)
                {
                    ac.text = it.text;
                    ac.x = currentX;
                    ac.xCount = 1;
                    acList.Add(ac);
                    currentX++;
                    ac.yCount = 2;
                }
                else if (!acColumngroupHistoryList.Contains(it.columngroup))//防止重复
                {
                    var sameCount = columns.Where(itit => itit.columngroup == it.columngroup).Count();
                    ac.text = group.First(itit => itit.name == it.columngroup).text;
                    ac.x = currentX;
                    ac.xCount = sameCount;
                    acList.Add(ac);
                    currentX = currentX + sameCount;
                    acColumngroupHistoryList.Add(it.columngroup);
                    ac.yCount = 1;
                    ac.groupName = it.columngroup;
                }
                else
                {
                    //暂无逻辑
                }
            }
            //表头
            foreach (var it in acList)
            {
                cells.Merge(it.y, it.x, it.yCount, it.xCount);//合并单元格 
                cells[it.y, it.x].PutValue(it.text);//填写内容 
                cells[it.y, it.x].SetStyle(_thStyle);
                if (!string.IsNullOrEmpty(it.groupName))
                {
                    var cols = columns.Where(itit => itit.columngroup == it.groupName).ToList();
                    foreach (var itit in cols)
                    {
                        var colsIndex = cols.IndexOf(itit);
                        cells[it.y + 1, it.x + colsIndex].PutValue(itit.text);//填写内容 
                        cells[it.y + 1, it.x + colsIndex].SetStyle(_thStyle);
                    }
                }
            }
            //表格
            if (dt != null && dt.Rows.Count > 0)
            {
                var rowList = dt.AsEnumerable().ToList();
                foreach (var it in rowList)
                {
                    int dtIndex = rowList.IndexOf(it);
                    var dtColumns = dt.Columns.Cast<DataColumn>().ToList();
                    foreach (var itit in dtColumns)
                    {
                        var dtColumnsIndex = dtColumns.IndexOf(itit);
                        cells[2 + dtIndex, dtColumnsIndex].PutValue(it[dtColumnsIndex]);
                        cells[2 + dtIndex, dtColumnsIndex].SetStyle(_tdStyle);

                    }
                }
            }
            workbook.Save(path);
        }

        /// <summary>
        /// 导出EXCEL并且动态生成多级表头
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="group">分组</param>
        /// <param name="replaceDic">需替换的列名称</param>
        /// <param name="mergeCols">需合并单元格的规则</param>
        /// <param name="dt">dataTable</param>
        public static void SaveColumnsHierarchy(string fileName, List<JqxTableColumns> columns, List<JqxTableColumnsGroup> group, Dictionary<string, string> replaceDic, Dictionary<string, string> mergeCols, DataTable dt)
        {
            Workbook workbook = new Workbook(); //工作簿 
            Worksheet sheet = workbook.Worksheets[0]; //工作表 
            Cells cells = sheet.Cells;//单元格
            for (int i = 0; i <= dt.Rows.Count + 1; i++)
            {
                sheet.Cells.SetRowHeight(i, 20);
            }
            List<AsposeCellInfo> acList = new List<AsposeCellInfo>();
            List<string> acColumngroupHistoryList = new List<string>();
            int currentX = 0;
            foreach (var it in columns)
            {
                AsposeCellInfo ac = new AsposeCellInfo();
                ac.y = 0;
                if (it.columngroup == null)
                {
                    ac.text = it.text;
                    ac.x = currentX;
                    ac.xCount = 1;
                    acList.Add(ac);
                    currentX++;
                    ac.yCount = 2;
                }
                else if (!acColumngroupHistoryList.Contains(it.columngroup))//防止重复
                {
                    var sameCount = columns.Where(itit => itit.columngroup == it.columngroup).Count();
                    ac.text = group.First(itit => itit.name == it.columngroup).text;
                    ac.x = currentX;
                    ac.xCount = sameCount;
                    acList.Add(ac);
                    currentX = currentX + sameCount;
                    acColumngroupHistoryList.Add(it.columngroup);
                    ac.yCount = 1;
                    ac.groupName = it.columngroup;
                }
                else
                {
                    //暂无逻辑
                }
            }
            //表头
            foreach (var it in acList)
            {
                cells.Merge(it.y, it.x, it.yCount, it.xCount);//合并单元格 
                cells[it.y, it.x].PutValue(it.text);//填写内容 
                cells[it.y, it.x].SetStyle(_thStyle);
                if (!string.IsNullOrEmpty(it.groupName))
                {
                    var cols = columns.Where(itit => itit.columngroup == it.groupName).ToList();
                    foreach (var itit in cols)
                    {
                        var colsIndex = cols.IndexOf(itit);
                        cells[it.y + 1, it.x + colsIndex].PutValue(itit.text);//填写内容 
                        cells[it.y + 1, it.x + colsIndex].SetStyle(_thStyle);
                    }
                }
            }
            //表格
            if (dt != null && dt.Rows.Count > 0)
            {
                var rowList = dt.AsEnumerable().ToList();
                foreach (var it in rowList)
                {
                    int dtIndex = rowList.IndexOf(it);
                    var dtColumns = dt.Columns.Cast<DataColumn>().ToList();
                    foreach (var itit in dtColumns)
                    {
                        var dtColumnsIndex = dtColumns.IndexOf(itit);
                        cells[2 + dtIndex, dtColumnsIndex].PutValue(it[dtColumnsIndex]);
                        cells[2 + dtIndex, dtColumnsIndex].SetStyle(_tdStyle);

                    }
                }
            }

            // 合并单元格
            if (mergeCols.Any())
            {
                GenerateExcelGridContent(cells, dt, mergeCols);
            }
            // 替换列名称
            if (replaceDic.Any())
            {
                foreach (var item in replaceDic)
                {
                    workbook.Replace(item.Key, item.Value);
                }
            }

            // 自动伸缩列
            sheet.AutoFitColumns();

            // 自动合并行
            //sheet.AutoFitRows();

            var response = HttpContext.Current.Response;
            response.Clear();
            response.Buffer = true;
            response.Charset = "utf-8";
            response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/ms-excel";
            response.BinaryWrite(workbook.SaveToStream().ToArray());
            response.End();
        }

        /// <summary>
        /// 处理合并单元格二维维度
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="dt"></param>
        /// <param name="mergeCellByCols"></param>
        /// <param name="rowIndex"></param>
        static void GenerateExcelGridContent(Cells cells, DataTable dt, Dictionary<string, string> mergeCellByCols)
        {
            // 表头有2行，索引从2开始，即合并行从第三行开始
            var rowIndex = 2;
            DataRow preDataRow = null;

            foreach (DataRow row in dt.Rows)
            {
                for (var col = 0; col < dt.Columns.Count; col++)
                {
                    if (string.IsNullOrEmpty(dt.Columns[col].ToString()))
                        continue;

                    if (mergeCellByCols != null && dt.Columns[col] != null && mergeCellByCols.Keys.Contains(dt.Columns[col].ToString()))
                    {
                        List<string> mergeCellByColsList = new List<string>();

                        if (!string.IsNullOrEmpty(mergeCellByCols[dt.Columns[col].ToString()]))
                        {
                            mergeCellByColsList.AddRange(mergeCellByCols[dt.Columns[col].ToString()].Split(','));
                        }
                        if (!mergeCellByColsList.Contains(dt.Columns[col].ToString()))
                        {
                            mergeCellByColsList.Add(dt.Columns[col].ToString());
                        }

                        MergeCell(cells, dt, preDataRow, row, mergeCellByColsList, rowIndex, col);
                    }
                }

                rowIndex++;
                preDataRow = row;
            }
        }

        /// <summary>
        /// 具体合并单元格方法
        /// </summary>
        /// <param name="cells"></param>
        /// <param name="dtContent"></param>
        /// <param name="preDataRow"></param>
        /// <param name="curDataRow"></param>
        /// <param name="mergeCellByCols"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        static void MergeCell(Cells cells, DataTable dtContent, DataRow preDataRow, DataRow curDataRow, List<string> mergeCellByCols, int rowIndex, int columnIndex)
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
        /// 表头样式
        /// </summary>
        private static Style _thStyle
        {
            get
            {
                Style s = new Style();
                s.Font.IsBold = true;
                s.Font.Name = "宋体";
                s.HorizontalAlignment = TextAlignmentType.Center;  //标题居中对齐
                return s;
            }
        }

        /// <summary>
        /// 单元格样式
        /// </summary>
        private static Style _tdStyle
        {
            get
            {
                Style s = new Style();
                s.Font.IsBold = false;
                s.Font.Name = "宋体";
                s.HorizontalAlignment = TextAlignmentType.Center;  //单元格居中对齐
                return s;
            }
        }

        public class JqxTableColumns
        {
            public string field { get; set; }
            public string cellsAlign { get; set; }
            public string align { get; set; }
            public string text { get; set; }
            public string columngroup { get; set; }
        }

        public class JqxTableColumnsGroup
        {
            public string text { get; set; }
            public string align { get; set; }
            public string name { get; set; }
        }

        public class AsposeCellInfo
        {
            public string text { get; set; }
            public int x { get; set; }
            public int xCount { get; set; }
            public int y { get; set; }
            public int yCount { get; set; }
            public string groupName { get; set; }
        }
    }
}
