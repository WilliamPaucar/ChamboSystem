using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Puntuacion_Chambo.Models.DatosBD;
using System.Web.Helpers;

namespace Sistema_Puntuacion_Chambo.Controllers
{
    public class tbl_candidatasController : Controller
    {
        private DatosEntities db = new DatosEntities();

        // GET: tbl_candidatas
        public ActionResult Index()
        {

            if (Session["username"] != null && Session["id"] == null)
            {
                return View(db.tbl_candidatas.ToList());
                
            }
            else if (Session["username"] != null && Session["id"] != null)
            {
                return RedirectToAction("Index","Error");

            }
            else
            {
                return RedirectToAction("Login", "Uno");
            }
           
        }
      

        // GET: tbl_candidatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_candidatas tbl_candidatas = db.tbl_candidatas.Find(id);
            if (tbl_candidatas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_candidatas);
        }

        // GET: tbl_candidatas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_candidatas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombres,apellidos,imagen,respresentacion")] tbl_candidatas tbl_candidatas)
        {
            HttpPostedFileBase imageFile = Request.Files[0];
            WebImage imagen = new WebImage(imageFile.InputStream);
    

            if (ModelState.IsValid)
            {
                tbl_candidatas.imagen = imagen.GetBytes();
                db.tbl_candidatas.Add(tbl_candidatas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_candidatas);
        }
      



            // GET: tbl_candidatas/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_candidatas tbl_candidatas = db.tbl_candidatas.Find(id);

            if (tbl_candidatas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_candidatas);
        }

        // POST: tbl_candidatas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombres,apellidos,imagen,respresentacion")] tbl_candidatas tbl_candidatas)
        {
            
            HttpPostedFileBase imageFile = Request.Files[0];
            WebImage imagen = new WebImage(imageFile.InputStream);
            tbl_candidatas.imagen = imagen.GetBytes();
           
            if (ModelState.IsValid)
            {
                db.Entry(tbl_candidatas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_candidatas);
        }

        // GET: tbl_candidatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_candidatas tbl_candidatas = db.tbl_candidatas.Find(id);
            if (tbl_candidatas == null)
            {
                return HttpNotFound();
            }
            return View(tbl_candidatas);
        }

        // POST: tbl_candidatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_candidatas tbl_candidatas = db.tbl_candidatas.Find(id);
            db.tbl_candidatas.Remove(tbl_candidatas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
