using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EasyPlat.Extends
{
    public static class ControllerExpand
    {
        public static JsonNetResult JsonNet(this Controller JsonNet, object data)
        {
            return new JsonNetResult()
            {
                Data = data
            };
        }
        public static JsonNetResult JsonNet(this Controller JonsNet, object data, JsonRequestBehavior behavior)
        {
            return new JsonNetResult()
            {
                Data = data, JsonRequestBehavior = behavior
            };
        }
        public static JsonNetResult JsonNet(this Controller JonsNet, object data, string contentType, Encoding contentEncoding)
        {
            return new JsonNetResult()
            {
                Data = data, ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
        public static JsonNetResult JsonNet(
            this System.Web.Mvc.Controller JonsNet, 
            object data, string contentType, 
            Encoding contentEncoding, 
            JsonRequestBehavior behavior)
        {
            return new JsonNetResult() {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
    }
}