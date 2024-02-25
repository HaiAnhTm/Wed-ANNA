using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNet_E_Commerce_Glasses_Web.Models;

namespace DotNet_E_Commerce_Glasses_Web.Controllers.ForManager
{
    public class ManagerConsumerController : Controller
    {
        private GlassesEntities db = new GlassesEntities();

        // GET: ManagerConsumer
        public async Task<ActionResult> Index()
        {
            var consumers = db.Consumers.Include(c => c.Account);
            return View(await consumers.ToListAsync());
        }

        // GET: ManagerConsumer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = await db.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            return View(consumer);
        }

        // GET: ManagerConsumer/Create
        public ActionResult Create()
        {
            ViewBag.IdAccount = new SelectList(db.Accounts, "IdAccount", "Username");
            return View();
        }

        // POST: ManagerConsumer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdConsumer,IdAccount,Username,Address,DateOfBirth,NumberPhone,Image,ListCart")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                db.Consumers.Add(consumer);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdAccount = new SelectList(db.Accounts, "IdAccount", "Username", consumer.IdAccount);
            return View(consumer);
        }

        // GET: ManagerConsumer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = await db.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAccount = new SelectList(db.Accounts, "IdAccount", "Username", consumer.IdAccount);
            return View(consumer);
        }

        // POST: ManagerConsumer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdConsumer,IdAccount,Username,Address,DateOfBirth,NumberPhone,Image,ListCart")] Consumer consumer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdAccount = new SelectList(db.Accounts, "IdAccount", "Username", consumer.IdAccount);
            return View(consumer);
        }

        // GET: ManagerConsumer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consumer consumer = await db.Consumers.FindAsync(id);
            if (consumer == null)
            {
                return HttpNotFound();
            }
            return View(consumer);
        }

        // POST: ManagerConsumer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Consumer consumer = await db.Consumers.FindAsync(id);
            db.Consumers.Remove(consumer);
            await db.SaveChangesAsync();
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
