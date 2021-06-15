using System;
using System.Collections.Generic;
using System.Linq;
using OvertimeManager.MVC5.Web.DbContexts;
using OvertimeManager.MVC5.Web.DbModels;

namespace OvertimeManager.MVC5.Web.Tests.Functional
{
    public class EmployeeHoursService
    {
        
        private OvertimeManagerDbContext db; // = new OvertimeManagerDbContext();

        public EmployeeHoursService(OvertimeManagerDbContext _db)
        {
            this.db = _db;
        }

        public List<EmployeeHour> GetEmployeePayPeriodHoursByKeyId(Guid employeeKeyId, DateTime payPeriodStartDateTime)
        {
            // determine pay-period end-datetime
            DateTime payPeriodEndDateTime = payPeriodStartDateTime.AddDays(6);

            //
            List<EmployeeHour> empHoursList = db.EmployeeHours
                .Where(wh => wh.EmployeeKeyId == employeeKeyId)
                .Where(wh => wh.StartDateTime >= payPeriodStartDateTime && wh.EndDateTime <= payPeriodEndDateTime)
                .OrderBy(ob => ob.StartDateTime)
                .ToList();

            //
            return empHoursList;
        }

        public List<EmployeeHour> GetPayPeriodEmployeeHours(DateTime payPeriodStartDateTime)
        {

            // determine pay-period end-datetime
            DateTime payPeriodEndDateTime = payPeriodStartDateTime.AddDays(6);

            //
            List<EmployeeHour> empHoursList = db.EmployeeHours
                .Where(wh => wh.StartDateTime >= payPeriodStartDateTime && wh.EndDateTime <= payPeriodEndDateTime)
                .OrderBy(ob => ob.StartDateTime)
                .ToList();

            //
            return empHoursList;

        }

        public List<IGrouping<Guid, EmployeeHour>> GetEmployeeWorkHourGroupsForPayPeriodByCompany(Guid companyKeyId, DateTime payPeriodStartDateTime)
        {

            // determine pay-period end-datetime
            DateTime payPeriodEndDateTime = payPeriodStartDateTime.AddDays(6);

            //
            List<IGrouping<Guid, EmployeeHour>> employeePayPeriodHourGroups = db.EmployeeHours
                .Where(wh => wh.CompanyKeyId == companyKeyId)
                .Where(wh => wh.StartDateTime >= payPeriodStartDateTime && wh.EndDateTime <= payPeriodEndDateTime)
                .GroupBy(gb => gb.EmployeeKeyId)
                .ToList();

            //
            return employeePayPeriodHourGroups;



        }
    }
}