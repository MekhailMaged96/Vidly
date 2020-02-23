using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            /* make all controllers in appliaction to be authorized */
           // filters.Add(new AuthorizeAttribute());
        }
    }
}
