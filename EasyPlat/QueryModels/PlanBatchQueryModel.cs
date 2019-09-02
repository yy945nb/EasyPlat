using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyPlat.QueryModels
{
    public class PlanBatchQueryModel: BaseQueryModel
    {
        public string PlanBatch { get; set; }
        public string PlanName { get; set; }
        public string CityPlace { get; set; }
        public string BidMode { get; set; }
        public string BuildWay { get; set; }
        public string BidYear { get; set; }
        public DateTime? PlanStartDt { get; set; }
        public DateTime? PlanEndDt { get; set; }
    }
}