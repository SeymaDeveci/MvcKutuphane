using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        // GET: Kitap
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(String p) //Lİsteleme işlemi yapar.P ise dışarıdan gönderilen parametre.
        {
            var kitaplar = from k in db.TBLKITAP select k; //TBLKITAP tablosundaki kitap değişkenini al
            if (!string.IsNullOrEmpty(p)) //p den gelen değer string değer boş değilse
            {
                kitaplar = kitaplar.Where(m => m.AD.Contains(p)); //dışarıdan gönderilen p parametresi kitaplardan gönderilen herhangi bir adı içeriyorsa bunu kitaplar değişkeninin içerisine ata
            }
            //var kitaplar = db.TBLKITAP.ToList();
            return View(kitaplar.ToList());
        }
        [HttpGet] //sayfa yüklendiğinde içinde veriler bulıunsun
         public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();  //Liste oluşturup linq sorgusu yazdık
                                                         //i değeri kategori tablosunun listesini tutuyor
                                                         //listeden yeni bir item seç
                                                         //seçilen değerin texti i den gelen ad değeri olsun
                                                         //seçilen değerin value değeri iden gelen id değeri olsun
                                                         //sonunda bu ifadeyi liste olarak döndür
            ViewBag.dgr1 = deger1; //dgr1 frontend tarafında çağrılacak Viewbag ile taşıma işlemi yapıtık deger1 taşınacak.
            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' +i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault(); //view tarafonda gönderilen p parametresinin degerine tabloda eşit olan değeri bul FirstOrDefault= diziden seçileni ya da seçim yapılmamışsa varsayılan degeri döndür
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id); // id ye göre gönderdiğim kitabı bul ve değerlerini aktar
            db.TBLKITAP.Remove(kitap); //kitap ile gelen değerin verilerini tblkitap tablosundan kaldır
            db.SaveChanges(); //değişiklikleri kaydet
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id); // id değeriden yazar bilgilerini bul ve ktp ye ata
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View("KitapGetir", ktp); //ktp bilgilerini KitapGetir sayfası ile döndür
        }
        public ActionResult KitapGuncelle(TBLKITAP p) //Birden fazla ilişkili tablo alanı nasıl güncellenir?
        {
            var kitap = db.TBLKITAP.Find(p.ID); //TBLKİTAP içinde p'den gelen ıd değerini bul
            kitap.AD = p.AD; //kitap nesnesi veritabanında olan sütun p parametresi ile kullanıcı tarafından gönderilen değer
            kitap.BASIMYILI = p.BASIMYILI;
            kitap.SAYFA = p.SAYFA;
            kitap.DURUM = p.DURUM;
            kitap.YAYINEVI = p.YAYINEVI;
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault(); //p'den gelen kategori tablosundaki ıd'ye eşit olacak değeri dödür seçilmiş ise seçilmiş olanı seçilmediyse default değeri döndür
            var yzr = db.TBLKATEGORI.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();
            kitap.KATEGORI = ktg.ID;
            kitap.YAZAR = yzr.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}