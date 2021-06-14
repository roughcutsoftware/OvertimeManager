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
    public class StateOvertimeRulesController : Controller
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();

        // GET: StateOvertimeRules
        public async Task<ActionResult> Index()
        {
            var stateOvertimeRules = db.StateOvertimeRules.Include(s => s.StateCode1).Include(s => s.StateOvertimeRuleType);
            return View(await stateOvertimeRules.ToListAsync());
        }

        // GET: StateOvertimeRules/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRule stateOvertimeRule = await db.StateOvertimeRules.FindAsync(id);
            if (stateOvertimeRule == null)
            {
                return HttpNotFound();
            }
            return View(stateOvertimeRule);
        }

        // GET: StateOvertimeRules/Create
        public ActionResult Create()
        {
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName");
            ViewBag.RuleTypeName = new SelectList(db.StateOvertimeRuleTypes, "RuleTypeName", "RuleTypeName");
            return View();
        }

        // POST: StateOvertimeRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StateOvertimeRuleKeyId,StateCode,RuleName,RuleTypeName,RuleEqualToGreaterThreshold,OvertimeRate,HourlyWageToUse")] StateOvertimeRule stateOvertimeRule)
        {
            if (ModelState.IsValid)
            {
                stateOvertimeRule.StateOvertimeRuleKeyId = Guid.NewGuid();

                // temp-debugging
                stateOvertimeRule.OvertimeRate = decimal.Parse(stateOvertimeRule.OvertimeRate.ToString());
                stateOvertimeRule.HourlyWageToUse = decimal.Parse(stateOvertimeRule.HourlyWageToUse.ToString());

                db.StateOvertimeRules.Add(stateOvertimeRule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", stateOvertimeRule.StateCode);
            ViewBag.RuleTypeName = new SelectList(db.StateOvertimeRuleTypes, "RuleTypeName", "RuleTypeName", stateOvertimeRule.RuleTypeName);
            return View(stateOvertimeRule);
        }

        // GET: StateOvertimeRules/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRule stateOvertimeRule = await db.StateOvertimeRules.FindAsync(id);
            if (stateOvertimeRule == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", stateOvertimeRule.StateCode);
            ViewBag.RuleTypeName = new SelectList(db.StateOvertimeRuleTypes, "RuleTypeName", "RuleTypeName", stateOvertimeRule.RuleTypeName);
            return View(stateOvertimeRule);
        }

        // POST: StateOvertimeRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StateOvertimeRuleKeyId,StateCode,RuleName,RuleTypeName,RuleEqualToGreaterThreshold,OvertimeRate,HourlyWageToUse")] StateOvertimeRule stateOvertimeRule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stateOvertimeRule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", stateOvertimeRule.StateCode);
            ViewBag.RuleTypeName = new SelectList(db.StateOvertimeRuleTypes, "RuleTypeName", "RuleTypeName", stateOvertimeRule.RuleTypeName);
            return View(stateOvertimeRule);
        }

        // GET: StateOvertimeRules/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StateOvertimeRule stateOvertimeRule = await db.StateOvertimeRules.FindAsync(id);
            if (stateOvertimeRule == null)
            {
                return HttpNotFound();
            }
            return View(stateOvertimeRule);
        }

        // POST: StateOvertimeRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            StateOvertimeRule stateOvertimeRule = await db.StateOvertimeRules.FindAsync(id);
            db.StateOvertimeRules.Remove(stateOvertimeRule);
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
