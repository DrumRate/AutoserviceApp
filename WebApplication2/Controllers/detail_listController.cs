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
    public class detail_listController : Controller
    {
        private autoservice1Entities db = new autoservice1Entities();

        // GET: detail_list
        public ActionResult Index()
        {
            var detail_list = db.detail_list.Include(d => d.detail).Include(d => d.work);
            return View(detail_list.ToList());
        }

        // GET: detail_list/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detail_list detail_list = db.detail_list.Find(id);
            if (detail_list == null)
            {
                return HttpNotFound();
            }
            return View(detail_list);
        }

        // GET: detail_list/Create
        public ActionResult Create()
        {
            ViewBag.id_detail = new SelectList(db.detail, "id", "name");
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work");
            return View();
        }

        // POST: detail_list/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_work,id_detail")] detail_list detail_list)
        {
            if (ModelState.IsValid)
            {
                db.detail_list.Add(detail_list);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_detail = new SelectList(db.detail, "id", "name", detail_list.id_detail);
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", detail_list.id_work);
            return View(detail_list);
        }

        // GET: detail_list/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detail_list detail_list = db.detail_list.Find(id);
            if (detail_list == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_detail = new SelectList(db.detail, "id", "name", detail_list.id_detail);
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", detail_list.id_work);
            return View(detail_list);
        }

        // POST: detail_list/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_work,id_detail")] detail_list detail_list)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detail_list).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_detail = new SelectList(db.detail, "id", "name", detail_list.id_detail);
            ViewBag.id_work = new SelectList(db.work, "id", "desc_work", detail_list.id_work);
            return View(detail_list);
        }

        // GET: detail_list/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detail_list detail_list = db.detail_list.Find(id);
            if (detail_list == null)
            {
                return HttpNotFound();
            }
            return View(detail_list);
        }

        // POST: detail_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detail_list detail_list = db.detail_list.Find(id);
            db.detail_list.Remove(detail_list);
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
