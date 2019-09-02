using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyPlat.QueryModels
{
    public class BaseQueryModel
    {
        public int page { get; set; }
        public int limit { get; set; }
        public int? Phid { get; set; }

    }
}