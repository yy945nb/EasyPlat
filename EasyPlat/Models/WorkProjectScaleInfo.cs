using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Models
{
    /// <summary>
    /// 投产项目规模
    /// </summary>
    public class WorkProjectScaleInfo
    {
        [Key]
        public long Phid { get; set; }

        /// <summary>
        /// 主项目id
        /// </summary>
        public long PPhid { get; set; }

        [Display(Name = "年份")]
        public string Year { get; set; }

        [Display(Name = "月份")]
        public string Month { get; set; }

        [Display(Name = "电压等级")]
        public string VolLevel { get; set; }

        [Display(Name = "项目数量")]
        public int PNumber { get; set; }

        [Display(Name = "线路回数")]
        public int CircuitNum { get; set; }

        [Display(Name = "主变台数")]
        public int TableNum { get; set; }

        [Display(Name = "线路长度")]
        public double Length { get; set; }

        [Display(Name = "变电容量")]
        public decimal Volume { get; set; }
    }
}