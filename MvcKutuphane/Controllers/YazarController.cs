using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity; //Entityleri ekliyoruz

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities(); //db nesnesi ile entityleri çağıracağız
        public ActionResult Index() //yazarlar tablosunu listeleme
        {
            var degerler = db.TBLYAZAR.ToList();
            return View(degerler);
        }
        [HttpGet] //yazar ekle sayfasına gelindiğinde ve işlem yapmadan çıkıldığında çalışır
        public ActionResult YazarEkle()
        {
            return View();
        }
        [HttpPost] //yazarekle sayfasına gelindiğindeve yazar ekle buttonuna basıldığında çalışır
        public ActionResult YazarEkle(TBLYAZAR p)
        {
            if (!ModelState.IsValid) //model üzerinde yaptığım işlem geçerli değilse
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Yazar");
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id); // id ye göre gönderdiğim yazarı bul ve değerlerini yazara aktar
            db.TBLYAZAR.Remove(yazar); //yazar ile gelen değerin verilerini tblyazar tablosundan kaldır
            db.SaveChanges(); //değişiklikleri kaydet
            return RedirectToAction("Index");
        }

        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id); // id değeriden yazar bilgilerini bul ve yzr ye ata
            return View("YazarGetir", yzr); //yzr bilgilerini YazarGetir sayfası ile döndür
        }
        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yzr = db.TBLYAZAR.Find(p.ID); //gönderilen p parameterindeki id değerine göre tablodaki güncellenecek veriyi bul
            yzr.AD = p.AD; //yzr deki AD değeri p den gelen AD değeri olacak
            yzr.SOYAD = p.SOYAD;
            yzr.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarinKitaplari(int id)
        {
            var yazar = db.TBLKITAP.Where(x => x.YAZAR == id).ToList();
            var yazar_adi = db.TBLYAZAR.Where(x => x.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault(); //tek değer döndürmesi için firstordefault kullanma
            ViewBag.yzrad = yazar_adi; //yazarın adını viewbag ile html tarafına taşıma
            return View(yazar);
        }
    }
}