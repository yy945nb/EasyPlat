using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyPlat.QueryModels
{
    public class WorkProjectScaleQueryModel : BaseQueryModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string VolLevel { get; set; }
        public int CircuitNum { get; set; }
    }
}