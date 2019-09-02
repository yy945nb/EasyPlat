using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlat.Extends
{
    /// <summary>
    /// 定义excel工作薄
    /// </summary>
    public class ExcelWork
    {
        /// <summary>
        /// 定义改Excel包含的一个或多个Sheet
        /// </summary>
        public List<ExcelSheet> ExcelSheet { get; set; }

        /// <summary>
        /// excel文件路径
        /// </summary>
        public string FilePath;
    }

    /// <summary>
    /// 定义Excel一个sheet
    /// </summary>
    public class ExcelSheet
    {
        /// <summary>
        /// sheet名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 该sheet总行数
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 改sheet总列数
        /// </summary>
        public int TotalColums { get; set; }

        /// <summary>
        /// 定义Sheet列表数据，该属性主要用于针对列表数据类型的报表
        /// </summary>
        public SheetDataGrid SheetDataGrid { get; set; }

        /// <summary>
        /// 定义Sheet标题内容，也可以用该属性制作非数据列表类型的报表
        /// </summary>
        public List<CellContent> TitleContent { get; set; }

        /// <summary>
        /// 定义末尾行内容，其单元格所在行号默认=CellContent.RowIndex+该Excel末尾行行索引
        /// </summary>
        public List<CellContent> FootContent { get; set; }
    }

    /// <summary>
    /// 定义Excel中要显示的数据列表类型
    /// </summary>
    public class SheetDataGrid
    {
        /// <summary>
        /// 要显示的数据列表，
        /// 注：合并时请对数据进行排序，同时对不需要合并的行请注明IsMerge标识为false,默认都需要合并
        /// </summary>
        public DataTable ContentItems { get; set; }

        /// <summary>
        /// 统一控制数据列表每行高度
        /// </summary>
        public int ContentRowHeight { get; set; }

        /// <summary>
        /// 统一控制数据列表的样式
        /// </summary>
        public Style ContentStyle { get; set; }

        /// <summary>
        /// 从Excel第几行开始生成Excel
        /// </summary>
        public int StartRowIndex { get; set; }

        /// <summary>
        /// 数据列表的标题
        /// </summary>
        public DataGridColumn DataGridColumn { get; set; }

        /// <summary>
        /// 根据value中指定的列值来合并colname 合并原则：该列及value中指定各列内容相同则合并,若该属性未定义则不进行合并
        /// key值 要合并的列，Value值：那些列相同才合并的key值的列
        /// 使用示例  key值： "CompanyName",Value值： 'Date,ProjectName'
        /// </summary>
        public Dictionary<string, string> MergeCellByCols { get; set; }
    }

    /// <summary>
    /// 创建指定位置样式的单元格
    /// </summary>
    public class CellContent
    {
        /// <summary>
        /// 单元格显示内容
        /// </summary>
        public string CellValue { get; set; }

        /// <summary>
        /// 单元格内容映射model或其他的映射值：如企业名称映射该值为CompanyName
        /// </summary>
        public string MappingValue { get; set; }

        /// <summary>
        /// 单元格所在行索引
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// 单元格所在列索引
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 合并总行数
        /// </summary>
        public int TotalRows { get; set; }

        /// <summary>
        /// 合并总列数
        /// </summary>
        public int TotalColumns { get; set; }

        /// <summary>
        /// 单元格样式
        /// </summary>
        public Style CellStyle { get; set; }

        /// <summary>
        /// 单元格行高
        /// </summary>
        public int RowHeight { get; set; }

        /// <summary>
        /// 单元格列宽
        /// </summary>
        public int ColumnWidth { get; set; }

        //public CellContent()
        //{
        //    CellValue = "";
        //    MappingValue = "";
        //    RowIndex = 1;
        //    ColumnIndex = 1;
        //    TotalRows = 1;
        //    TotalColumns = 1;
        //    CellStyle = ExcelStyle.ColumnStyle;
        //    RowHeight = 1;
        //    RowHeight = 1;
        //    ColumnWidth = 25;
        //}
    }

    public class DataGridColumn
    {
        /// <summary>
        /// 定义要显示的列,添加的先后顺序作为列的显示顺序
        /// </summary>
        public List<CellContent> ShowColumns { get; set; }
        /// <summary>
        /// 定义每一列的宽度
        /// </summary>
        public int[] ShowColumnWidth { get; set; }
    }

}
