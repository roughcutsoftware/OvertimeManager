using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OvertimeManager.MVC5.Web.DbContexts;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("EmployeeHour")]
    public partial class EmployeeHour
    {
        [Key]
        public Guid EmployeeHourKeyId { get; set; }

        public Guid EmployeeKeyId { get; set; }

        public Guid CompanyKeyId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public decimal HourlyWage { get; set; }

        public virtual Company Company { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
