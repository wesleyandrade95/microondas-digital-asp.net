using System.Web;
using System.Web.Mvc;

namespace avaliacao_wesleyandrade_microondas
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
