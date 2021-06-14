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
    public class CompaniesController : Controller
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            var companies = db.Companies.Include(c => c.StateCode1);
            return View(await companies.ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompanyKeyId,CompanyName,StateCode")] Company company)
        {
            if (ModelState.IsValid)
            {
                company.CompanyKeyId = Guid.NewGuid();
                db.Companies.Add(company);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", company.StateCode);
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", company.StateCode);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompanyKeyId,CompanyName,StateCodes")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StateCode = new SelectList(db.StateCodes, "StateCode", "StateName", company.StateCode);
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
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
