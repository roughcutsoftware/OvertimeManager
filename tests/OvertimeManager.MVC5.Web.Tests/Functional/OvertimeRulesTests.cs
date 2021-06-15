using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OvertimeManager.MVC5.Web.DbContexts;
using OvertimeManager.MVC5.Web.DbModels;
using Shouldly;

namespace OvertimeManager.MVC5.Web.Tests.Functional
{
    // arrange
    // act
    // assert
    public class OvertimeRulesTests
    {
        private OvertimeManagerDbContext db = new OvertimeManagerDbContext();
        private readonly EmployeeHoursService _employeeHoursService;
        private DateTime payPeriodStartDateTime = DateTime.Parse("06/06/2021 12:00 AM");

        public OvertimeRulesTests()
        {
            _employeeHoursService = new EmployeeHoursService(db);
        }


        [TestCase()]
        public void Should_Process_CA_GreaterThanEightHoursRule()
        {

            // arrange
            // get emp-hours 
            Guid employeeKeyId = Guid.Parse("DC5979B3-E4D9-41AD-A5B7-0003329469EE");
            
            List<EmployeeHour> empHoursList = _employeeHoursService.GetEmployeePayPeriodHoursByKeyId(employeeKeyId, payPeriodStartDateTime);

            // get company-info
            Company company = empHoursList.FirstOrDefault().Company;

            // get workday rules-for-given-state
            List<StateOvertimeRule> stateWorkdayRules = db.StateOvertimeRules
                .Where(wh => wh.StateCode == company.StateCode && wh.RuleTypeName == "Workday")
                //.OrderByDescending(od => od.RuleEqualToGreaterThreshold)
                .OrderBy(ob => ob.RuleEqualToGreaterThreshold)
                .ToList();

            

            // process emp-hours
            decimal workweekRegularWages = 0;
            decimal workweekOvertimeWages = 0;

            //
            bool hasWorkdayOvertimeBeenApplied = false;

            // act
            foreach (StateOvertimeRule overtimeRule in stateWorkdayRules.Take(1))
            {
                //
                foreach (EmployeeHour empHour in empHoursList)
                {
                    //
                    int hoursWorked = empHour.EndDateTime.Hour - empHour.StartDateTime.Hour;
                    int overtimeHoursWorked = 0;
                    
                    //bool hasWorkdayOvertimeBeenApplied = false;

                    //
                    if (hoursWorked > 8)
                    {
                        overtimeHoursWorked = hoursWorked - 8;
                        workweekRegularWages += (hoursWorked - overtimeHoursWorked) * empHour.HourlyWage;
                    }
                    else
                    {
                        workweekRegularWages += hoursWorked * empHour.HourlyWage;
                    }

                    // determine if overtime-rule-applies
                    //bool hasWorkdayOvertimeBeenApplied = false;
                    //
                    if (hoursWorked > overtimeRule.RuleEqualToGreaterThreshold)
                    {
                        workweekOvertimeWages += overtimeHoursWorked * (empHour.HourlyWage * overtimeRule.OvertimeRate);

                    }

                }
            }


            // assert
            workweekRegularWages.ShouldBe(410.00m);
            workweekOvertimeWages.ShouldBe(30.75m);


        }


        [TestCase]
        public void Should_Process_CA_GreaterThanTwelveHoursRule()
        {

			// arrange
			// get emp-hours 
            Guid employeeKeyId = Guid.Parse("443656EB-8C95-4DDE-9ED2-00183DEF3AD6");

            //List<EmployeeHour> empHoursList = db.EmployeeHours
            //    .Where(wh => wh.EmployeeKeyId == employeeKeyId)
            //    .OrderBy(ob => ob.StartDateTime)
            //    .ToList();

            List<EmployeeHour> empHoursList = _employeeHoursService.GetEmployeePayPeriodHoursByKeyId(employeeKeyId, payPeriodStartDateTime);


            // get company-info
            Company company = empHoursList.FirstOrDefault().Company;

            // get workday rules-for-given-state
            List<StateOvertimeRule> stateWorkdayRules = db.StateOvertimeRules
                .Where(wh => wh.StateCode == company.StateCode && wh.RuleTypeName == "Workday")
                .OrderByDescending(od => od.RuleEqualToGreaterThreshold)
                //.OrderBy(ob => ob.RuleEqualToGreaterThreshold)
                .ToList();

            

            // process emp-hours
            decimal workweekRegularWages = 0;
            decimal workweekOvertimeWages = 0;

            //
            bool hasWorkdayOvertimeBeenApplied = false;

            // act
            foreach (StateOvertimeRule overtimeRule in stateWorkdayRules.Take(1))
            {
                //
                foreach (EmployeeHour empHour in empHoursList)
                {
                    //
                    int hoursWorked = empHour.EndDateTime.Hour - empHour.StartDateTime.Hour;
                    int overtimeHoursWorked = 0;
                    //hoursWorked.Dump();
                    //bool hasWorkdayOvertimeBeenApplied = false;

                    //
                    if (hoursWorked > 8)
                    {
                        overtimeHoursWorked = hoursWorked - 8;
                        workweekRegularWages += (hoursWorked - overtimeHoursWorked) * empHour.HourlyWage;
                    }
                    else
                    {
                        workweekRegularWages += hoursWorked * empHour.HourlyWage;
                    }

                    // determine if overtime-rule-applies
                    //bool hasWorkdayOvertimeBeenApplied = false;
                    //
                    if (hoursWorked > overtimeRule.RuleEqualToGreaterThreshold)
                    {
                        workweekOvertimeWages += overtimeHoursWorked * (empHour.HourlyWage * overtimeRule.OvertimeRate);

                    }

                }
            }


            // assert
            workweekRegularWages.ShouldBe(410.00m);
            workweekOvertimeWages.ShouldBe(205.0000m);


        }

        [TestCase]
        public void Should_Process_CA_All_Workday_Rules()
        {


            // arrange
            // set company
            Guid companyKeyId = Guid.Parse("054232C0-AB31-46FD-BD26-13B8F4925B7F");

            // get company-info
            Company company = db.Companies.FirstOrDefault(fd => fd.CompanyKeyId == companyKeyId);

            // get workday rules-for-given-state
            List<StateOvertimeRule> stateWorkdayRules = db.StateOvertimeRules
                .Where(wh => wh.StateCode == company.StateCode && wh.RuleTypeName == "Workday")
                .OrderByDescending(od => od.OvertimeRate)
                .ThenByDescending(od => od.RuleEqualToGreaterThreshold)
                .ToList();


            // get employee work-hour groups
            List<IGrouping<Guid, EmployeeHour>> employeeWorkHourGroups =
                _employeeHoursService.GetEmployeeWorkHourGroupsForPayPeriodByCompany(company.CompanyKeyId, payPeriodStartDateTime);

            // begin working through emp-hour groups
            foreach (IGrouping<Guid, EmployeeHour> employeeWorkHourGroup in employeeWorkHourGroups.OrderBy(ob => ob.Key))
            {
                string groupKey = employeeWorkHourGroup.Key.ToString();

                // get employee info for later use
                Employee employee = db.Employees.FirstOrDefault(fd => fd.EmployeeKeyId == employeeWorkHourGroup.Key);


                // ****************************************************************************************
                // ************************* start processing by - applying rules *************************
                // ****************************************************************************************

                // ****************************************************************************************
                // iterate through all rules for each emp-hour group
                // ****************************************************************************************
                // process emp-hours
                decimal workweekTotalHours = 0;
                decimal workweekRegularWages = 0;
                decimal workweekOvertimeWages = 0;

                //
                bool hasWorkdayOvertimeBeenApplied = false;

                // act
                foreach (StateOvertimeRule overtimeRule in stateWorkdayRules)
                {
                    // if overtime already applied by previous rule(s), DO NOT APPLY ADD'L
                    if (hasWorkdayOvertimeBeenApplied)
                    {
                        continue;

                    }
                    else
                    {
                        // reset for this iteration
                        workweekRegularWages = 0;
                        workweekOvertimeWages = 0;
                    }

                    // ****************************************************************************************
                    // process individual employee-hours, and apply rule, if applicable
                    // ****************************************************************************************
                    foreach (EmployeeHour empHour in employeeWorkHourGroup.OrderBy(ob => ob.StartDateTime).ToList())
                    {
                        //
                        int hoursWorked = empHour.EndDateTime.Hour - empHour.StartDateTime.Hour;
                        int overtimeHoursWorked = 0;

                        // add up hours worked for workweek, for later use/processing 
                        workweekTotalHours += hoursWorked;

                        //
                        if (hoursWorked > 8)
                        {
                            overtimeHoursWorked = hoursWorked - 8;
                            workweekRegularWages += (hoursWorked - overtimeHoursWorked) * empHour.HourlyWage;
                        }
                        else
                        {
                            workweekRegularWages += hoursWorked * empHour.HourlyWage;
                        }

                        // determine if overtime-rule-applies
                        //bool hasWorkdayOvertimeBeenApplied = false;
                        //
                        if (hoursWorked > overtimeRule.RuleEqualToGreaterThreshold)
                        {
                            workweekOvertimeWages += overtimeHoursWorked * (empHour.HourlyWage * overtimeRule.OvertimeRate);

                            // signal a overtime-rule has been applied
                            hasWorkdayOvertimeBeenApplied = true;

                        }

                    }


                }

                // assert
                workweekRegularWages.ShouldBe(410.00m);
                //workweekOvertimeWages.ShouldBe(205.0000m);

            }


        }
    }
}
