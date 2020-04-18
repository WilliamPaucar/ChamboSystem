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
        public class UnoController : Controller
    {
        private DatosEntities db = new DatosEntities();
      
      
        /****SECCION LOGIN***/

            public ActionResult Login()
        {
            if(Session["username"]!=null){

                return RedirectToAction("Create", "Uno", new { username = Session["username"].ToString() });
            }
            else
            {
                return View();
            }
            
        }
           
        /****CERRAR SESION******/
        public ActionResult log()
        {
            Session.Remove("username");
            Session.Remove("id");
            return RedirectToAction("Login","Uno");
        }


        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            var bdUserU = db.tbl_users_jueces.SingleOrDefault(x => x.username == user && x.password == pass && x.tipo == "U");
            var bdUser = db.tbl_users_jueces.SingleOrDefault(x => x.username== user && x.password==pass && x.tipo=="A");

                if (bdUser != null && bdUser.tipo=="A")
                {
                Session["username"] = user;
                return RedirectToAction("Index", "tbl_candidatas", new { username = user });
                
            }
            else if
                (bdUserU != null && bdUserU.tipo == "U")
            {
                 Session["username"] = user;
                  Session["id"] = bdUserU.id;
                
                 return RedirectToAction("Create", "Uno", new { username = user });

            }
            else
            {
                ViewBag.tried = "yes";
                return View();
            }


          

           
        }
        /****FIN LOGIN***/

        // GET: Uno
        public ActionResult Index()
        {
            var tbl_presentacion_uno = db.tbl_presentacion_uno.Include(t => t.tbl_candidatas).Include(t => t.tbl_users_jueces);
            return View(tbl_presentacion_uno.ToList());
        }

        // GET: Uno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_uno tbl_presentacion_uno = db.tbl_presentacion_uno.Find(id);
            if (tbl_presentacion_uno == null)
            {
                return HttpNotFound();
            }
            return View(tbl_presentacion_uno);
        }


        //***Programacion principal de la puntuacion**//    

       
        // GET: Uno/Create
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

        // POST: Uno/Create 
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create(int[] imagen_id, int[] notaUno, int[] notaDos, int[] notaTres)
        {

            int i = 0;
            int juezID = ((int)Session["id"]);
            if (ModelState.IsValid)
            {
              
                foreach (int cand in imagen_id)
                {
                    

                    tbl_presentacion_uno c = new tbl_presentacion_uno();
                    c.fk_candidatas_id = cand;
                    c.fk_users_id = juezID;
                    c.nota1 = Convert.ToByte(notaUno[i]);
                    c.nota2 = Convert.ToByte(notaDos[i]);
                    c.nota3 = Convert.ToByte(notaTres[i]);
                    db.tbl_presentacion_uno.Add(c);
                    db.SaveChanges();
                    i++;
                    

                }
                return RedirectToAction("Create", "Dos");
            }
            else { return RedirectToAction("Login","Uno"); 
            }
            
        }

        // GET: Uno/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_uno tbl_presentacion_uno = db.tbl_presentacion_uno.Find(id);
            if (tbl_presentacion_uno == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_candidatas_id = new SelectList(db.tbl_candidatas, "id", "nombres", tbl_presentacion_uno.fk_candidatas_id);
            ViewBag.fk_users_id = new SelectList(db.tbl_users_jueces, "id", "nombres_completos", tbl_presentacion_uno.fk_users_id);
            return View(tbl_presentacion_uno);
        }

        // POST: Uno/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fk_candidatas_id,fk_users_id,nota1,nota2,nota3")] tbl_presentacion_uno tbl_presentacion_uno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_presentacion_uno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_candidatas_id = new SelectList(db.tbl_candidatas, "id", "nombres", tbl_presentacion_uno.fk_candidatas_id);
            ViewBag.fk_users_id = new SelectList(db.tbl_users_jueces, "id", "nombres_completos", tbl_presentacion_uno.fk_users_id);
            return View(tbl_presentacion_uno);
        }

        // GET: Uno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_presentacion_uno tbl_presentacion_uno = db.tbl_presentacion_uno.Find(id);
            if (tbl_presentacion_uno == null)
            {
                return HttpNotFound();
            }
            return View(tbl_presentacion_uno);
        }

        // POST: Uno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_presentacion_uno tbl_presentacion_uno = db.tbl_presentacion_uno.Find(id);
            db.tbl_presentacion_uno.Remove(tbl_presentacion_uno);
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
