using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Models
{
    public class PlanBatchInfo
    {
        [Key]
        public long Phid { get; set; }

        [Display(Name = "计划批次")]
        public string PlanBatch { get; set; }

        [Display(Name = "计划批次名称")]
        public string PlanName { get; set; }

        [Display(Name = "市级属地")]
        public string CityPlace { get; set; }

        [Display(Name = "招标模式")]
        public string BidMode { get; set; }

        [Display(Name = "采购方式")]
        public string BuyWay { get; set; }

        [Display(Name = "招标年度")]
        public string BidYear { get; set; }

        [Display(Name = "招标紧急标识")]
        public string BidFlag { get; set; }

        [Display(Name = "计划审查日期")]
        public DateTime? PlanCheckDt { get; set; }

        [Display(Name = "计划截至日期")]
        public DateTime? PlanEndDt { get; set; }

        [Display(Name = "状态标识")]
        public string Status { get; set; }

        [Display(Name = "计划开始时间")]
        public DateTime? PlanStartDt { get; set; }

        [Display(Name = "市计划开始时间")]
        public DateTime? CityStartDt { get; set; }

        [Display(Name = "招标模式细化期")]
        public string Period { get; set; }

    }
}