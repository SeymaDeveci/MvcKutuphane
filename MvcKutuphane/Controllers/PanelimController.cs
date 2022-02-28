using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    [Authorize] //Controllera ait bütün metodlar için authorize işlemi yapar yani üye giriş yapmadan sayfaya erişemez
    public class PanelimController : Controller
    {
        // GET: Panelim
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index() //Form yüklendiğinde verileri listeleme işlemi yapar
        {
            var uye_mail = (string)Session["Mail"]; //Sayfa yüklendiğinde ilgili üyenin mail adresini tutar
            //var degerler = db.TBLUYELER.FirstOrDefault(z => z.MAIL == uye_mail);
            var degerler = db.TBLDUYURULAR.ToList();
            var d1 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.AD).FirstOrDefault();
            ViewBag.d1 = d1;
            var d2 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.d2 = d2;
            var d3 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.d3 = d3;
            var d4 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.d4 = d4;
            var d5 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.OKUL).FirstOrDefault();
            ViewBag.d5 = d5;
            var d6 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;
            var d7 = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;
            //Üyenin kütüphaneden aldığı kitap sayısı
            var uye_id = db.TBLUYELER.Where(x => x.MAIL == uye_mail).Select(y => y.ID).FirstOrDefault();
            var d8 = db.TBLHAREKET.Where(x => x.UYE == uye_id).Count();
            ViewBag.d8 = d8;
            //Aldığım Mesaj Sayısı
            var d9 = db.TBLMESAJLAR.Where(x => x.ALICI == uye_mail).Count();
            ViewBag.d9 = d9;
            //Gönderdiğim Mesaj Sayısı
            var d10 = db.TBLMESAJLAR.Where(x => x.GONDEREN == uye_mail).Count();
            ViewBag.d10 = d10;
            //Toplam Duyurular Sayısı
            var d11 = db.TBLDUYURULAR.Count();
            ViewBag.d11 = d11;

            return View(degerler);
        }

        //Üyenin Ayarlar kısmındaki bilgilerini güncelleme işlemi
        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"]; //Mail adlı kullanıcı bilgisini değişkene atama
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici); //gelen kullanıcı bilgisine eşit tablodan kullanıcı bilgisine erişme
            uye.SIFRE = p.SIFRE; // Sifre güncelleme için gelen şifreyi eski şifrenin yerine ataa
            uye.AD = p.AD;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges(); // veritabanındaki değişiklikleri kaydet
            return RedirectToAction("Index");
        }

        public ActionResult Kitaplarim(String search)
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(y => y.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            //search işlemi için
            var kitaplar = from k in degerler select k;
            if (!string.IsNullOrEmpty(search))
            {
                kitaplar = kitaplar.Where(k => k.TBLKITAP.AD.ToUpper().Contains(search.ToUpper()));
                degerler = kitaplar.ToList();
            }
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
            return View(duyurulistesi);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        //Ayarlardaki partial view kısmı. üyenin bilgilerini getirir güncelleme işlemi yapar.Index işleminde yapılanı burada yaptık
        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici).Select(y => y.ID).FirstOrDefault(); //üyenin id bilgisini alma
            var uyebul = db.TBLUYELER.Find(id); //id ye göre tablodaki üyeyi bul ve bilgilerini getir
            return PartialView("Partial2", uyebul);
        }
    }
}