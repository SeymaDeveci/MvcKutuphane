using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index() //listeleme işlemi
        {
            var degerler = db.TBLKATEGORI.Where(x => x.DURUM == true).ToList();
            return View(degerler);
        }
        [HttpGet] //sayfa yüklendiğinde çalış
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost] //sayfa yüklendiğinde veri girilip butona tıklandığında çalış
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            db.TBLKATEGORI.Add(p);
            p.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index", "Kategori");
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id); //id den gönderileni bul kategori nesnesine ata
            //db.TBLKATEGORI.Remove(kategori); // kategori nesnesinden gönderilen değeri kaldır
            kategori.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id); // id den gönderileni tabloda bul ve ktg ye ata
            return View("KategoriGetir", ktg); //KategoriGetir sayfasını döndür ama ktg den gelen değer ile döndür
        }

        public ActionResult KategoriGuncelle(TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID); // ID değerine göre bul ve ktg ye ata
            ktg.AD = p.AD; // değiştirilen adı ktg ye ata
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}