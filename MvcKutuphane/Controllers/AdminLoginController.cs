using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: AdminLogin
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(TBLADMIN p)
        {
            //girilen değerler tablodaki değere eşit olmalı ve bilgiler parametresine bu kişinin bilgileri aktarılmalı
            var bilgiler = db.TBLADMIN.FirstOrDefault(x=>x.Kullanici == p.Kullanici && x.Sifre == p.Sifre);
            if (bilgiler != null) //parametre girişi olduysa
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Kullanici, false); //bilgileri kullanici bilgisi ile taşı
                Session["Kullanici"] = bilgiler.Kullanici.ToString(); //kullaniciya göre session yapacağız bunu sessiona atadık
                return RedirectToAction("Index", "Kategori"); //giriş başarılıysa yönlendir
            }
            else
            {
                return View();
            }
        }
    }
}