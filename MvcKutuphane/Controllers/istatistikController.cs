using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            ViewBag.dgr1 = deger1;
            var deger2 = db.TBLKITAP.Count();
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0) //dosya boyutu null dan farklı ise
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName)); //sunucu içerisinde web2 klasörüne kaydet / Yoldan gelen dosya ismini alma
                dosya.SaveAs(dosyayolu); //dosyayolundan gelen dosyayı kaydet

            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart() //Kartlara değer çekme işlemi
        {
            var deger1 = db.TBLKITAP.Count();
            ViewBag.dgr1 = deger1;

            var deger2 = db.TBLUYELER.Count();
            ViewBag.dgr2 = deger2;

            var deger3 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr3 = deger3;

            var deger4 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            ViewBag.dgr4 = deger4;

            var deger5 = db.TBLKATEGORI.Count();
            ViewBag.dgr5 = deger5;

            var deger6 = db.EnAktifUye().FirstOrDefault();
            ViewBag.dgr6 = deger6;

            var deger7 = db.EnCokOkunanKitap().FirstOrDefault();
            ViewBag.dgr7 = deger7;

            var deger11 = db.TBLILETISIM.Count();
            ViewBag.dgr11 = deger11;

            var deger8 = db.EnFazlaKitabıOlanYazar().FirstOrDefault(); //en fazla kitabı olan yazar birden fazla olabilir ama sen bana listedeki en üst sırada olanı getir
            ViewBag.dgr8 = deger8;

            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(y => y.Count()).Select(z =>
            z.Key).FirstOrDefault();
            ViewBag.dgr9 = deger9;

            var deger10 = db.EnBasariliPersonel().FirstOrDefault();
            ViewBag.dgr10 = deger10;

            var deger12 = db.BugünküEmanetler().FirstOrDefault();
            ViewBag.dgr12 = deger12;

            return View();
        }
    }
}