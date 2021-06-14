using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OvertimeManager.MVC5.Web.DbContexts;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("Company")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            EmployeeHour = new HashSet<EmployeeHour>();
        }

        [Key]
        public Guid CompanyKeyId { get; set; }

        [Required]
        [StringLength(255)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }

        public virtual StateCodes StateCode1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeHour> EmployeeHour { get; set; }
    }
}
