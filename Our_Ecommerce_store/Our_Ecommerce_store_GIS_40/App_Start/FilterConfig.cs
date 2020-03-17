using System.Web;
using System.Web.Mvc;

namespace Our_Ecommerce_store_GIS_40
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
