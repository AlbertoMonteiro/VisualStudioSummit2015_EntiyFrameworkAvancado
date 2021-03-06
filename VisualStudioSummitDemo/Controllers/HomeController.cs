﻿using System.Web.Mvc;
using VisualStudioSummitDemo.Interceptors;
using VisualStudioSummitDemo.Interceptors.MultiTenant;

namespace VisualStudioSummitDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChooseTenant(long id)
        {
            MultiTenantInterceptor.TentantId = id;

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}