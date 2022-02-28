using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: Ayarlar
        public ActionResult Index2()
        {
            var kullaniciler = db.TBLADMIN.ToList();
            return View(kullaniciler);
        }

        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniAdmin(TBLADMIN a)
        {
            db.TBLADMIN.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            db.TBLADMIN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            return View("AdminGuncelle", admin);
        }
        [HttpPost]
        public ActionResult AdminGuncelle(TBLADMIN a)
        {
            var admin = db.TBLADMIN.Find(a.ID);
            admin.Kullanici = a.Kullanici;
            admin.Sifre = a.Sifre;
            admin.Yetki = a.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
    }
}