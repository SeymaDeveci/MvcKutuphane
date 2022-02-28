using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        // GET: Uye
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(int? sayfa, String p)
        {
            var uyeler = from k in db.TBLUYELER select k;
            if (!string.IsNullOrEmpty(p))
            {
                uyeler = uyeler.Where(u => u.AD.Contains(p) || u.SOYAD.Contains(p));
                sayfa = 1;
            }
            //var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa, 4); //sayfa değeri=1 sayfa 1 den başlayacak ve listedeki ilk 4 değeri getir sonra diğer sayfaya geç
            int pageSize = 4;
            int sayfaNumarasi = (sayfa ?? 1);//(page ?? 1) ifadesi bir değere sahipse page değerini döndürür veya page null ise 1 döndürür.
            return View(uyeler.ToList().ToPagedList(sayfaNumarasi,pageSize)); 
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid) //TBLUYE.cs deki data annotation şartını sağlamazsa
            {
                return View("UyeEkle");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id); 
            db.TBLUYELER.Remove(uye); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id); // id den gönderileni tabloda bul ve uye ye ata
            return View("UyeGetir", uye); //UyGetir sayfasını döndür ama uye den gelen değer ile döndür
        }

        public ActionResult UyeGuncelle(TBLUYELER p)
        {
            var uye = db.TBLUYELER.Find(p.ID); // ID değerine göre bul ve uye ye ata
            uye.AD = p.AD; // değiştirilen adı uye ye ata
            uye.SOYAD = p.SOYAD;
            uye.MAIL = p.MAIL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.OKUL = p.OKUL;
            uye.SIFRE = p.SIFRE;
            uye.TELEFON = p.TELEFON;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmisi(int id)
        {
            var ktpgecmis = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            var uye_adi = db.TBLUYELER.Where(x => x.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.uyeAdi = uye_adi;
            return View(ktpgecmis);
        }
    }
}