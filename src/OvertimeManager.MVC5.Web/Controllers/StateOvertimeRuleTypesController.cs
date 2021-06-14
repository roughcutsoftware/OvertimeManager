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
    public class StateOvertimeRuleTypesController : Controller
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();

        // GET: StateOvertimeRuleTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.StateOvertimeRuleTypes.ToListAsync());
        }

        // GET: StateOvertimeRuleTypes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRuleType stateOvertimeRuleType = await db.StateOvertimeRuleTypes.FindAsync(id);
            if (stateOvertimeRuleType == null)
            {
                return HttpNotFound();
            }
            return View(stateOvertimeRuleType);
        }

        // GET: StateOvertimeRuleTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StateOvertimeRuleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RuleTypeName,StateOvertimeRuleTypeKeyId")] StateOvertimeRuleType stateOvertimeRuleType)
        {
            if (ModelState.IsValid)
            {
                db.StateOvertimeRuleTypes.Add(stateOvertimeRuleType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stateOvertimeRuleType);
        }

        // GET: StateOvertimeRuleTypes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRuleType stateOvertimeRuleType = await db.StateOvertimeRuleTypes.FindAsync(id);
            if (stateOvertimeRuleType == null)
            {
                return HttpNotFound();
            }
            return View(stateOvertimeRuleType);
        }

        // POST: StateOvertimeRuleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RuleTypeName,StateOvertimeRuleTypeKeyId")] StateOvertimeRuleType stateOvertimeRuleType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateOvertimeRuleType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stateOvertimeRuleType);
        }

        // GET: StateOvertimeRuleTypes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRuleType stateOvertimeRuleType = await db.StateOvertimeRuleTypes.FindAsync(id);
            if (stateOvertimeRuleType == null)
            {
                return HttpNotFound();
            }
            return View(stateOvertimeRuleType);
        }

        // POST: StateOvertimeRuleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            StateOvertimeRuleType stateOvertimeRuleType = await db.StateOvertimeRuleTypes.FindAsync(id);
            db.StateOvertimeRuleTypes.Remove(stateOvertimeRuleType);
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
