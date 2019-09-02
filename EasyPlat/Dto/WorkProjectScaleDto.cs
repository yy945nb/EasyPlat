using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Dto
{
    /// <summary>
    /// 投产项目规模
    /// </summary>
    public class WorkProjectScaleDto
    {
        public string TJDate { get { return this.Year + this.Month; } }

        public int Sort
        {
            get
            {
                var sort = 0;

                switch (this.VolLevel)
                {
                    case "35":
                        sort = 1;
                        break;
                    case "110":
                        sort = 2;
                        break;
                    case "220":
                        sort = 3;
                        break;
                    case "500":
                        sort = 4;
                        break;
                    case "当月汇总":
                        sort = 5;
                        break;
                    case "当月规模占年度比例":
                        sort = 6;
                        break;
                    case "1-当月规模累计":
                        sort = 7;
                        break;
                    case "1-当月规模占年度比例":
                        sort = 8;
                        break;
                    default:
                        break;
                }
                return sort;
            }
        }

        public string Year { get; set; }
        public string Month { get; set; }
        public string VolLevel { get; set; }
        public int PNumber { get; set; }
        public int CircuitNum { get; set; }
        public int TableNum { get; set; }
        public double Length { get; set; }
        public decimal Volume { get; set; }
        public double AvgRate { get; set; }
    }
}