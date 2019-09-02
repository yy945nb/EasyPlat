using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EasyPlat.Extends
{
    public class ExcelStyle
    {
        public static Style FirstTitleStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 20;
                style.Font.IsBold = true;
                style.IsTextWrapped = true;
                style.Font.Name = "宋体";
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
                style.Font.IsBold = false;
                style.IsTextWrapped = true;
                style.Font.Name = "宋体";
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }

        public static Style ColumnStyle
        {
            get
            {
                Style style = new Style();
                style.HorizontalAlignment = TextAlignmentType.Center;
                style.Font.Size = 10;
                style.Font.IsBold = true;
                style.Font.Name = "宋体";
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
                style.IsTextWrapped = true;
                style.Font.Name = "宋体";
                style.SetBorder(BorderType.TopBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.RightBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.LeftBorder, CellBorderType.Thin, Color.Black);
                style.SetBorder(BorderType.BottomBorder, CellBorderType.Thin, Color.Black);
                return style;
            }
        }
        public static Style FootContentStyle
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
    }
}
