using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlat.Extends
{
    public class ExcelCellContent
    {
        public static Style FirstTitleStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 20;
                style.Font.IsBold = true;
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static Style SecordTitleStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 10;
                style.Font.IsBold = true;
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static Style ColumnsTitleStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 10;
                style.Font.IsBold = true;
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static Style ContentStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 10;
                style.Font.IsBold = false;
                style.IsTextWrapped = true;//单元格内容自动换行 
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static Style FootStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 10;
                style.Font.IsBold = true;
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static void SetCellContent(string cellValue, Style style, Cell cell)
        {
            cell.PutValue(cellValue);
            cell.SetStyle(style);

        }

        public static void SetCellContent(string cellValue, Style style, Cell cell, int rowHeight)
        {
            cell.PutValue(cellValue);
            cell.SetStyle(style);
            cell.Worksheet.Cells.SetRowHeight(cell.Row, rowHeight);
        }

        public static void SetCellContent(string cellValue,  Cell cell, int rowHeight)
        {
            cell.PutValue(cellValue);
            cell.Worksheet.Cells.SetRowHeight(cell.Row, rowHeight);
        }
 
    }
}
