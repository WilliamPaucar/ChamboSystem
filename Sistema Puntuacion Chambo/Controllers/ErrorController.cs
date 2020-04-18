using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Puntuacion_Chambo.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            if (Session["id"]!=null) { 
            ViewBag.Home= Session["id"].ToString();
            }
            return View();
        }
    }
}