using EasyPlat.App_Start;
using System.Web;
using System.Web.Mvc;

namespace EasyPlat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JsonNetResultAttritube());
        }
    }
}
