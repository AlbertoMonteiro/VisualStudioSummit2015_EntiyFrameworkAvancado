using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisualStudioSummitDemo.Models;

namespace VisualStudioSummitDemo.Controllers
{
    public class ContactDealersController : Controller
    {
        private DemoContext db = new DemoContext();

        // GET: ContactDealers
        public ActionResult Index()
        {
            var contactDealers = db.ContactDealers.Include(c => c.Contact);
            return View(contactDealers.ToList());
        }

        // GET: ContactDealers/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactDealer contactDealer = db.ContactDealers.Find(id);
            if (contactDealer == null)
            {
                return HttpNotFound();
            }
            return View(contactDealer);
        }

        // GET: ContactDealers/Create
        public ActionResult Create()
        {
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name");
            return View();
        }

        // POST: ContactDealers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ContactId")] ContactDealer contactDealer)
        {
            if (ModelState.IsValid)
            {
                db.ContactDealers.Add(contactDealer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", contactDealer.ContactId);
            return View(contactDealer);
        }

        // GET: ContactDealers/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactDealer contactDealer = db.ContactDealers.Find(id);
            if (contactDealer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", contactDealer.ContactId);
            return View(contactDealer);
        }

        // POST: ContactDealers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ContactId")] ContactDealer contactDealer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactDealer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", contactDealer.ContactId);
            return View(contactDealer);
        }

        // GET: ContactDealers/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactDealer contactDealer = db.ContactDealers.Find(id);
            if (contactDealer == null)
            {
                return HttpNotFound();
            }
            return View(contactDealer);
        }

        // POST: ContactDealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContactDealer contactDealer = db.ContactDealers.Find(id);
            db.ContactDealers.Remove(contactDealer);
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
