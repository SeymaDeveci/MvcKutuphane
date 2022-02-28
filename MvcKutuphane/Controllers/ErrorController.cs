using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        // GET: Error
        //Hata sayfalarını biz tanımlıyoruz. Uygulamada daha çok bu tercih edilir.
        public ActionResult Page400()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
            return View();
        }

        public ActionResult Page401()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
            return View();
        }

        public ActionResult Page403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
            return View();
        }

        //public ActionResult Page500()
        //{
        //    Response.StatusCode = 500;
        //    Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
        //    return View();
        //}

        //public ActionResult Page502()
        //{
        //    Response.StatusCode = 502;
        //    Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
        //    return View();
        //}

        //public ActionResult Page503()
        //{
        //    Response.StatusCode = 503;
        //    Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
        //    return View();
        //}

        //public ActionResult Page504()
        //{
        //    Response.StatusCode = 504;
        //    Response.TrySkipIisCustomErrors = true; //hata sayfalarının gelmesi için
        //    return View();
        //}
    }
}