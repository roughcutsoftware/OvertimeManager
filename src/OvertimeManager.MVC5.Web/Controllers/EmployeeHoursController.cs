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
    public class EmployeeHoursController : Controller
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();

        // GET: EmployeeHours
        public async Task<ActionResult> Index()
        {
            var employeeHours = db.EmployeeHours.Include(e => e.Company).Include(e => e.Employee);
            return View(await employeeHours.ToListAsync());
        }

        // GET: EmployeeHours/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeHour employeeHour = await db.EmployeeHours.FindAsync(id);
            if (employeeHour == null)
            {
                return HttpNotFound();
            }
            return View(employeeHour);
        }

        // GET: EmployeeHours/Create
        public ActionResult Create()
        {
            ViewBag.CompanyKeyId = new SelectList(db.Companies, "CompanyKeyId", "CompanyName");
            ViewBag.EmployeeKeyId = new SelectList(db.Employees, "EmployeeKeyId", "LastName");
            return View();
        }

        // POST: EmployeeHours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmployeeHourKeyId,EmployeeKeyId,CompanyKeyId,StartDateTime,EndDateTime,HourlyWage")] EmployeeHour employeeHour)
        {
            if (ModelState.IsValid)
            {
                employeeHour.EmployeeHourKeyId = Guid.NewGuid();
                db.EmployeeHours.Add(employeeHour);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyKeyId = new SelectList(db.Companies, "CompanyKeyId", "CompanyName", employeeHour.CompanyKeyId);
            ViewBag.EmployeeKeyId = new SelectList(db.Employees, "EmployeeKeyId", "LastName", employeeHour.EmployeeKeyId);
            return View(employeeHour);
        }

        // GET: EmployeeHours/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeHour employeeHour = await db.EmployeeHours.FindAsync(id);
            if (employeeHour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyKeyId = new SelectList(db.Companies, "CompanyKeyId", "CompanyName", employeeHour.CompanyKeyId);
            ViewBag.EmployeeKeyId = new SelectList(db.Employees, "EmployeeKeyId", "LastName", employeeHour.EmployeeKeyId);
            return View(employeeHour);
        }

        // POST: EmployeeHours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmployeeHourKeyId,EmployeeKeyId,CompanyKeyId,StartDateTime,EndDateTime,HourlyWage")] EmployeeHour employeeHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeHour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyKeyId = new SelectList(db.Companies, "CompanyKeyId", "CompanyName", employeeHour.CompanyKeyId);
            ViewBag.EmployeeKeyId = new SelectList(db.Employees, "EmployeeKeyId", "LastName", employeeHour.EmployeeKeyId);
            return View(employeeHour);
        }

        // GET: EmployeeHours/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeHour employeeHour = await db.EmployeeHours.FindAsync(id);
            if (employeeHour == null)
            {
                return HttpNotFound();
            }
            return View(employeeHour);
        }

        // POST: EmployeeHours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EmployeeHour employeeHour = await db.EmployeeHours.FindAsync(id);
            db.EmployeeHours.Remove(employeeHour);
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
