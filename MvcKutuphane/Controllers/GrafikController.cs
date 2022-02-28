using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models;

namespace MvcKutuphane.Controllers
{
    public class GrafikController : Controller
    {
        
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizeKitapResult()
        {
            return Json(liste()); //json değerinde liste metodunun değerini döndürecek
        }
        public List<Class1> liste() //Class1 i liste formatında liste isimli metod ile çağırma
        {
            List<Class1> cs = new List<Class1>(); //cs isminde nesne türet
            cs.Add(new Class1() //Ekleme yapıldı manuel olarak
            {
                yayinevi = "Güneş",
                sayi = 7
            });
            cs.Add(new Class1()
            {
                yayinevi = "Mars",
                sayi = 3
            });
            cs.Add(new Class1()
            {
                yayinevi = "Venüs",
                sayi = 10
            });
            return cs; // cs nesnesini döndür
        }
    }
}