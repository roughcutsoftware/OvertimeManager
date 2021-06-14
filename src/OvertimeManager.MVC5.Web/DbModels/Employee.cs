using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("Employee")]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmployeeHour = new HashSet<EmployeeHour>();
        }

        [Key]
        public Guid EmployeeKeyId { get; set; }

        [Required]
        [StringLength(75)]
        public string LastName { get; set; }

        [Required]
        [StringLength(75)]
        public string FirstName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeHour> EmployeeHour { get; set; }
    }
}
