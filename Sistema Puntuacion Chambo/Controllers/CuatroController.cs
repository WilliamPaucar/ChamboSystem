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
    public class CuatroController : Controller
    {
        private DatosEntities db = new DatosEntities();

        // GET: Cuatro
        public ActionResult Index()
        {
            var tbl_presentacion_tres = db.tbl_presentacion_tres.Include(t => t.tbl_candidatas).Include(t => t.tbl_users_jueces);
            return View(tbl_presentacion_tres.ToList());
        }

        // GET: Cuatro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_tres tbl_presentacion_tres = db.tbl_presentacion_tres.Find(id);
            if (tbl_presentacion_tres == null)
            {
                return HttpNotFound();
            }
            return View(tbl_presentacion_tres);
        }

        // GET: Cuatro/Create
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Uno");

            }

            else if (Session["id"] != null)
            {
                ViewBag.CandidatasList = db.tbl_candidatas;
                return View();
            }

            else { return RedirectToAction("Index", "Error"); ; }
        }

        // POST: Cuatro/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int[] imagen_id, int[] notaUno, int[] notaDos, int[] notaTres)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("Index", "Error");

            }
            int i = 0;
            int juezID = ((int)Session["id"]);
            if (ModelState.IsValid)
            {

                foreach (int cand in imagen_id)
                {


                    tbl_presentacion_tres c = new tbl_presentacion_tres();
                    c.fk_candidatas_id = cand;
                    c.fk_users_id = juezID;
                    c.nota1 = Convert.ToByte(notaUno[i]);
                    c.nota2 = Convert.ToByte(notaDos[i]);
                    c.nota3 = Convert.ToByte(notaTres[i]);
                    db.tbl_presentacion_tres.Add(c);
                    db.SaveChanges();
                    i++;


                }
                return RedirectToAction("Final", "Final");
            }
            else
            {
                return RedirectToAction("Login", "Uno");
            }
        }

        // GET: Cuatro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_tres tbl_presentacion_tres = db.tbl_presentacion_tres.Find(id);
            if (tbl_presentacion_tres == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_candidatas_id = new SelectList(db.tbl_candidatas, "id", "nombres", tbl_presentacion_tres.fk_candidatas_id);
            ViewBag.fk_users_id = new SelectList(db.tbl_users_jueces, "id", "nombres_completos", tbl_presentacion_tres.fk_users_id);
            return View(tbl_presentacion_tres);
        }

        // POST: Cuatro/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fk_candidatas_id,fk_users_id,nota1,nota2,nota3")] tbl_presentacion_tres tbl_presentacion_tres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_presentacion_tres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_candidatas_id = new SelectList(db.tbl_candidatas, "id", "nombres", tbl_presentacion_tres.fk_candidatas_id);
            ViewBag.fk_users_id = new SelectList(db.tbl_users_jueces, "id", "nombres_completos", tbl_presentacion_tres.fk_users_id);
            return View(tbl_presentacion_tres);
        }

        // GET: Cuatro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_tres tbl_presentacion_tres = db.tbl_presentacion_tres.Find(id);
            if (tbl_presentacion_tres == null)
            {
                return HttpNotFound();
            }
            return View(tbl_presentacion_tres);
        }

        // POST: Cuatro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_presentacion_tres tbl_presentacion_tres = db.tbl_presentacion_tres.Find(id);
            db.tbl_presentacion_tres.Remove(tbl_presentacion_tres);
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
