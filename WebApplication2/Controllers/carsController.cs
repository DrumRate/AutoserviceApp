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
    public class carsController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string search)
        {



            var result = db.car
                .Where(a => a.color.ToLower().Contains(search.ToLower())
                          || a.mark.ToLower().Contains(search.ToLower())
                          || a.state_num.ToLower().Contains(search.ToLower())
                          || a.model.ToLower().Contains(search.ToLower()))
                .ToList();

            return View(result);
        }

        private autoservice1Entities db = new autoservice1Entities();

        // GET: cars
        public ActionResult Index()
        {
            var car = db.car.Include(c => c.client);
            return View(car.ToList());
        }

        // GET: cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: cars/Create
        public ActionResult Create()
        {
            ViewBag.id_client = new SelectList(db.client, "id", "FIO");
            return View();
        }

        // POST: cars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,state_num,mark,model,color,id_client")] car car)
        {
            if (ModelState.IsValid)
            {
                db.car.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_client = new SelectList(db.client, "id", "FIO", car.id_client);
            return View(car);
        }

        // GET: cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_client = new SelectList(db.client, "id", "FIO", car.id_client);
            return View(car);
        }

        // POST: cars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,state_num,mark,model,color,id_client")] car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_client = new SelectList(db.client, "id", "FIO", car.id_client);
            return View(car);
        }

        // GET: cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            car car = db.car.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            car car = db.car.Find(id);
            db.car.Remove(car);
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
