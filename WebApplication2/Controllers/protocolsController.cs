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
    public class protocolsController : Controller
    {
        private autoservice1Entities db = new autoservice1Entities();

        // GET: protocols
        public ActionResult Index()
        {
            return View(db.protocol.ToList());
        }

        // GET: protocols/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            protocol protocol = db.protocol.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        // GET: protocols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: protocols/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,act_delivering,remarks")] protocol protocol)
        {
            if (ModelState.IsValid)
            {
                db.protocol.Add(protocol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(protocol);
        }

        // GET: protocols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            protocol protocol = db.protocol.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        // POST: protocols/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,act_delivering,remarks")] protocol protocol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(protocol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(protocol);
        }

        // GET: protocols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            protocol protocol = db.protocol.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        // POST: protocols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            protocol protocol = db.protocol.Find(id);
            db.protocol.Remove(protocol);
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
