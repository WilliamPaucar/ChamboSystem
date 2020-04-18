using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sistema_Puntuacion_Chambo.Models.DatosBD;

namespace Sistema_Puntuacion_Chambo.Controllers
{
    public class JuecesController : Controller
    {
        private DatosEntities db = new DatosEntities();

        // GET: Jueces
        public ActionResult Index()
        {
            return View(db.tbl_users_jueces.ToList());
        }

        // GET: Jueces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users_jueces tbl_users_jueces = db.tbl_users_jueces.Find(id);
            if (tbl_users_jueces == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users_jueces);
        }

        // GET: Jueces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jueces/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nombres_completos,username,password,tipo")] tbl_users_jueces tbl_users_jueces)
        {
            if (ModelState.IsValid)
            {
                db.tbl_users_jueces.Add(tbl_users_jueces);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_users_jueces);
        }

        // GET: Jueces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users_jueces tbl_users_jueces = db.tbl_users_jueces.Find(id);
            if (tbl_users_jueces == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users_jueces);
        }

        // POST: Jueces/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nombres_completos,username,password,tipo")] tbl_users_jueces tbl_users_jueces)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_users_jueces).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_users_jueces);
        }

        // GET: Jueces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_users_jueces tbl_users_jueces = db.tbl_users_jueces.Find(id);
            if (tbl_users_jueces == null)
            {
                return HttpNotFound();
            }
            return View(tbl_users_jueces);
        }

        // POST: Jueces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_users_jueces tbl_users_jueces = db.tbl_users_jueces.Find(id);
            db.tbl_users_jueces.Remove(tbl_users_jueces);
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
