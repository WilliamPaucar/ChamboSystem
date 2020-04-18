using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sistema_Puntuacion_Chambo.Models.DatosBD;
using NReco.PdfGenerator;
namespace Sistema_Puntuacion_Chambo.Controllers
{
    public class FinalController : Controller
    {

        private DatosEntities db = new DatosEntities();
        // GET: Finparticipantesal
        public ActionResult Index()
        {

            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Uno");
            }
            else {
                List<byte> copied = new List<byte>();
                List<byte> result = new List<byte>();

                ViewBag.CandidatasList = db.tbl_candidatas;
                ViewBag.JuecesList = db.tbl_users_jueces.Where(x => x.tipo == "U").ToList();
                // Agregar rango a List && Conversion de ObjetoResult a List 
                ViewBag.Notario = db.tbl_users_jueces.Where(x => x.tipo == "N").ToList();
                foreach (var z in db.tbl_candidatas)
                {
                    var m = db.PromediarNotas(z.id);
                    // Agregar rango a List && Conversion de ObjetoResult a List 
                    copied.AddRange(m.Where(x => x.HasValue).Select(x => x.Value));
                }
                ViewBag.participantes = copied;

                return View(db.tbl_users_jueces.Where(x => x.tipo == "U").Count()); }

        }
        public ActionResult Informe()
        {

            //var ViewAsString = RenderViewAsString("Index", tbl_candidatas);
            //htmlToPdf.PageWidth = 1600;
            //htmlToPdf.PageHeight = 900;
            //string ViewContent = 
            var htmlContent = String.Format("<body>Hello world: {0}" +
                "<h1>Rayios</h1></body>", DateTime.Now);


            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            //var pdfBytes = htmlToPdf.GeneratePdfFromFile("~/Views/Final/Index", null);
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
            FileResult FileResult = new FileContentResult(pdfBytes, "application/pdf");

            return FileResult;

            //DateTime.Now);
        }

    
    public ActionResult Final()
    {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Uno");

            }
            else
                return View();
        


    }
}
    
}

              