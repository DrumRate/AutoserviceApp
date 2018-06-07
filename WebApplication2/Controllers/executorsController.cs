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

    public class executorsController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string search)
        {
            var result = db.executor
            .Where(a => a.FIO.ToLower().Contains(search.ToLower())
                     || a.job_position.ToLower().Contains(search.ToLower()))
                .ToList();

            return View(result);
        }

        private autoservice1Entities db = new autoservice1Entities();

        // GET: executors
        public ActionResult Index()
        {
            return View(db.executor.ToList());
        }

        // GET: executors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            executor executor = db.executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // GET: executors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: executors/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FIO,job_position")] executor executor)
        {
            if (ModelState.IsValid)
            {
                db.executor.Add(executor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(executor);
        }

        // GET: executors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            executor executor = db.executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // POST: executors/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FIO,job_position")] executor executor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(executor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(executor);
        }

        // GET: executors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            executor executor = db.executor.Find(id);
            if (executor == null)
            {
                return HttpNotFound();
            }
            return View(executor);
        }

        // POST: executors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            executor executor = db.executor.Find(id);
            db.executor.Remove(executor);
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
