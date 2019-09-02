using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EasyPlat.Dto
{
    public class PlanBatchDto
    {
        public long Phid { get; set; }
        public string PlanBatch { get; set; }
        public string PlanName { get; set; }
        public string CityPlace { get; set; }
        public string BidMode { get; set; }
        public string BuyWay { get; set; }
        public string BidYear { get; set; }
        public string BidFlag { get; set; }
        public DateTime? PlanCheckDt { get; set; }
        public DateTime? PlanEndDt { get; set; }
        public string Status { get; set; }
        public DateTime? PlanStartDt { get; set; }
        public DateTime? CityStartDt { get; set; }
        public string Period { get; set; }
    }
}