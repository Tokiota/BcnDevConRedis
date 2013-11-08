using System.Web;
using System.Web.Mvc;

using AutoComplete.Filter;

namespace AutoComplete
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilter());
        }
    }
}