using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class employmentsController : Controller
    {
        private autoservice1Entities db = new autoservice1Entities();

        // GET: employments
        public ActionResult Index()
        {
            var employment = db.employment.Include(e => e.work).Include(e => e.executor);
            return View(employment.ToList());
        }

        // GET: employments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employment employment = db.employment.Find(id);
            if (employment == null)
            {
                return HttpNotFound();
            }
            return View(employment);
        }

        // GET: employments/Create
        public ActionResult Create()
        {
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work");
            ViewBag.id_executor = new SelectList(db.executor, "id", "FIO");
            return View();
        }

        // POST: employments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_work,id_executor")] employment employment)
        {
            if (ModelState.IsValid)
            {
                db.employment.Add(employment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", employment.id_work);
            ViewBag.id_executor = new SelectList(db.executor, "id", "FIO", employment.id_executor);
            return View(employment);
        }

        // GET: employments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employment employment = db.employment.Find(id);
            if (employment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", employment.id_work);
            ViewBag.id_executor = new SelectList(db.executor, "id", "FIO", employment.id_executor);
            return View(employment);
        }

        // POST: employments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_work,id_executor")] employment employment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", employment.id_work);
            ViewBag.id_executor = new SelectList(db.executor, "id", "FIO", employment.id_executor);
            return View(employment);
        }

        // GET: employments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employment employment = db.employment.Find(id);
            if (employment == null)
            {
                return HttpNotFound();
            }
            return View(employment);
        }

        // POST: employments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employment employment = db.employment.Find(id);
            db.employment.Remove(employment);
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
