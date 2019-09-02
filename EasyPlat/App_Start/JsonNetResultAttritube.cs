using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyPlat.Extends;

namespace EasyPlat.App_Start
{
    public class JsonNetResultAttritube : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ActionResult result = filterContext.Result;

            if (result is JsonResult && !(result is JsonNetResult))
            {
                JsonResult jsonResult = (JsonResult)result;
                JsonNetResult jsonNetResult = new JsonNetResult();
                jsonNetResult.ContentEncoding = jsonResult.ContentEncoding;
                jsonNetResult.ContentType = jsonResult.ContentType;
                jsonNetResult.JsonRequestBehavior = jsonResult.JsonRequestBehavior;
                jsonNetResult.Data = jsonResult.Data;
                jsonNetResult.MaxJsonLength = jsonResult.MaxJsonLength;
                jsonNetResult.RecursionLimit = jsonResult.RecursionLimit;
                filterContext.Result = jsonNetResult;
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}