using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyPlat.QueryModels
{
    public class ProjectPlanQueryModel: BaseQueryModel
    {
        public string ProjectName { get; set; }
        public string CLevel { get; set; }
        public string Build_bdt { get; set; }
        public string Build_tdt { get; set; }
        public string Catagory { get; set; }
        public string PType { get; set; }
        public string Nature { get; set; }
        public string IsBeginOrWork { get; set; }
    }
}