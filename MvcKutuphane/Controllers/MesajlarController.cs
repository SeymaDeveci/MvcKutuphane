using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index() //Gelen mesajlar
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }

        public ActionResult Giden() //Giden mesajar
        {
            var uyemail = (string)Session["Mail"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail.ToString()).ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR m)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMesaj");
            }
            var uyemail = (string)Session["Mail"].ToString();
            m.GONDEREN = uyemail.ToString();
            m.TARİH = DateTime.Parse(DateTime.Now.ToString());
            db.TBLMESAJLAR.Add(m);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"].ToString();
            var gelensayisi = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();
            ViewBag.d1 = gelensayisi;

            var gidensayisi = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d2 = gidensayisi;
            return PartialView();
        }

        public ActionResult GelenMesajOku(TBLMESAJLAR m)
        {
            var mesaj_id = db.TBLMESAJLAR.Find(m.ID);
            return View("GelenMesajOku", mesaj_id);
        }

        public ActionResult GidenMesajOku(TBLMESAJLAR m)
        {
            var mesaj_id = db.TBLMESAJLAR.Find(m.ID);
            return View("GidenMesajOku", mesaj_id);
        }

    }
}