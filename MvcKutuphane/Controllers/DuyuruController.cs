using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKutuphane.Models.Entity;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
        // GET: Duyuru
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var duyurular = db.TBLDUYURULAR.ToList();
            return View(duyurular);
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURULAR d)
        {
            if (ModelState.IsValid)
            {
                db.TBLDUYURULAR.Add(d); //tblduyurular tablosuna gelen parametreleri ekle
                db.SaveChanges(); //değişiklikleri kaydet
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            db.TBLDUYURULAR.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruIcerik(int id)
        {
            var duyuru = db.TBLDUYURULAR.Where(x => x.ID == id).ToList();
            var duyuru_adi = db.TBLDUYURULAR.Where(x => x.ID == id).Select(z => z.KATEGORI).FirstOrDefault();
            ViewBag.dyrad = duyuru_adi;
            return View(duyuru);
        }

        [HttpGet]
        public ActionResult DuyuruGuncelle(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            return View("DuyuruGuncelle",duyuru);
        }

        [HttpPost]
        public ActionResult DuyuruGuncelle(TBLDUYURULAR d)
        {
            var duyuru = db.TBLDUYURULAR.Find(d.ID);
            duyuru.KATEGORI = d.KATEGORI;
            duyuru.ICERİK = d.ICERİK;
            duyuru.TARIH = d.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}