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
    public class worksController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string search)
        {
            var result = db.work
            .Where(a => a.desc_work.ToLower().Contains(search.ToLower()))
                .ToList();

            return View(result);
        }

        private autoservice1Entities db = new autoservice1Entities();

        // GET: works
        public ActionResult Index()
        {
            var work = db.work.Include(w => w.car).Include(w => w.protocol);
            return View(work.ToList());
        }

        // GET: works/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            work work = db.work.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // GET: works/Create
        public ActionResult Create()
        {
            ViewBag.id_car = new SelectList(db.car, "id", "state_num");
            ViewBag.id_protocol = new SelectList(db.protocol, "id", "act_delivering");
            return View();
        }

        // POST: works/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_car,desc_work,value,id_protocol")] work work)
        {
            if (ModelState.IsValid)
            {
                db.work.Add(work);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_car = new SelectList(db.car, "id", "state_num", work.id_car);
            ViewBag.id_protocol = new SelectList(db.protocol, "id", "act_delivering", work.id_protocol);
            return View(work);
        }

        // GET: works/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            work work = db.work.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_car = new SelectList(db.car, "id", "state_num", work.id_car);
            ViewBag.id_protocol = new SelectList(db.protocol, "id", "act_delivering", work.id_protocol);
            return View(work);
        }

        // POST: works/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_car,desc_work,value,id_protocol")] work work)
        {
            if (ModelState.IsValid)
            {
                db.Entry(work).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_car = new SelectList(db.car, "id", "state_num", work.id_car);
            ViewBag.id_protocol = new SelectList(db.protocol, "id", "act_delivering", work.id_protocol);
            return View(work);
        }

        // GET: works/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            work work = db.work.Find(id);
            if (work == null)
            {
                return HttpNotFound();
            }
            return View(work);
        }

        // POST: works/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            work work = db.work.Find(id);
            db.work.Remove(work);
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
