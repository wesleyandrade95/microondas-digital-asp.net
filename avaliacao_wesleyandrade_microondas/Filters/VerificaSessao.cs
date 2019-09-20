using avaliacao_wesleyandrade_microondas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace avaliacao_wesleyandrade_microondas.Filters
{
    public class VerificaSessao : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["microondas"]==null)
                filterContext.Result = new RedirectResult("/Home/Index");
        }
    }
}