using Stoker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Stoker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Error( )
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            
        }
        protected void Application_Start()
        {
            Database.SetInitializer<ApplicationDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
