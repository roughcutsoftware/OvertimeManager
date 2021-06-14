using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OvertimeManager.MVC5.Web.DbContexts;
using OvertimeManager.MVC5.Web.DbModels;

namespace OvertimeManager.MVC5.Web.Controllers
{
    public class StateCodesController : Controller
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();

        // GET: StateCodes
        public async Task<ActionResult> Index()
        {
            return View(await db.StateCodes.ToListAsync());
        }

        // GET: StateCodes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateCodes stateCodes = await db.StateCodes.FindAsync(id);
            if (stateCodes == null)
            {
                return HttpNotFound();
            }
            return View(stateCodes);
        }

        // GET: StateCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StateCode,StateKeyId,StateName")] StateCodes stateCodes)
        {
            if (ModelState.IsValid)
            {
                db.StateCodes.Add(stateCodes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stateCodes);
        }

        // GET: StateCodes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateCodes stateCodes = await db.StateCodes.FindAsync(id);
            if (stateCodes == null)
            {
                return HttpNotFound();
            }
            return View(stateCodes);
        }

        // POST: StateCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StateCode,StateKeyId,StateName")] StateCodes stateCodes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateCodes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stateCodes);
        }

        // GET: StateCodes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateCodes stateCodes = await db.StateCodes.FindAsync(id);
            if (stateCodes == null)
            {
                return HttpNotFound();
            }
            return View(stateCodes);
        }

        // POST: StateCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StateCodes stateCodes = await db.StateCodes.FindAsync(id);
            db.StateCodes.Remove(stateCodes);
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
