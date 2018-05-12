using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;

namespace Web_AMO_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            Application.Lock();
            StreamReader sr = new StreamReader(Server.MapPath("/TruyCap.txt"));
            int counter = int.Parse(sr.ReadLine());
            sr.Close();
            sr.Dispose();
            Application["counter"] = counter;
            Application["online"] = 0;
            Application.UnLock();

            log4net.Config.XmlConfigurator.Configure();
        }
        protected void Session_Start()
        {
            Session["IdTK"] = null;
            Session["HoTen"] = null;
            Session["Email"] = null;
            Session["Quyen"] = null;
            Session["AnhDaiDien"] = null;


            Application.Lock();
            Application["counter"] = Convert.ToInt64(Application["counter"]) + 1;
            Application["online"] = Convert.ToInt64(Application["online"]) + 1;           
            StreamWriter sw = new StreamWriter(Server.MapPath("/TruyCap.txt"));
            sw.Write(Application["counter"]);
            sw.Close();
            sw.Dispose();
            Application.UnLock();
        }

        protected void Session_End()
        {
            Application.Lock();
            Application["online"] = Convert.ToInt64(Application["online"]) - 1;
            Application.UnLock();
        }
    }
}
