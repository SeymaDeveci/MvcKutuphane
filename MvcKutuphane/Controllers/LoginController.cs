using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous] // bununla bu sayfadaki metodların authenticate olmadan erişilmesini sağlamış olduk yani authorize özelliğini burada muaf tuttuk
    public class LoginController : Controller
    {
        // GET: Login
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult GirisYap() //sayfa yüklendiğinde çalış
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p) //sayfada bir değer döndüğünde post olduğunda çalış
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE); //veritabanındaki veriler ile girilen bilgileri eşleştirme
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                //TempData["ID"] = bilgiler.ID.ToString();
                //TempData["Ad"] = bilgiler.AD.ToString();
                //TempData["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["KullaniciAdi"] = bilgiler.KULLANICIADI.ToString();
                //TempData["Telefon"] = bilgiler.TELEFON.ToString();
                //TempData["Okul"] = bilgiler.OKUL.ToString();
                //TempData["Sifre"] = bilgiler.SIFRE.ToString();
                return RedirectToAction("Index", "Panelim"); //Giriş başarılı olursa Panelim Controller daki Index aksiyonuna gönder
            }
            else
            {
                return View(); //Giriş başarılı değilse aynı sayfayı döndür
            }         
        }
    }
}