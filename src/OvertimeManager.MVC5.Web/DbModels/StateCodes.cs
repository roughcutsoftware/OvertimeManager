using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OvertimeManager.MVC5.Web.DbContexts;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("StateCode")]
    public partial class StateCodes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateCodes()
        {
            Company = new HashSet<Company>();
            StateOvertimeRule = new HashSet<StateOvertimeRule>();
        }

        public Guid StateKeyId { get; set; }

        [Key]
        [Column("StateCode")]
        [StringLength(2)]
        public string StateCode { get; set; }

        [Required]
        [StringLength(75)]
        public string StateName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Company { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StateOvertimeRule> StateOvertimeRule { get; set; }
    }
}
