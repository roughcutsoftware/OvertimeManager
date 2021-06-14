using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("StateOvertimeRuleType")]
    public partial class StateOvertimeRuleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateOvertimeRuleType()
        {
            StateOvertimeRule = new HashSet<StateOvertimeRule>();
        }

        public Guid StateOvertimeRuleTypeKeyId { get; set; }

        [Key]
        [StringLength(255)]
        public string RuleTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StateOvertimeRule> StateOvertimeRule { get; set; }
    }
}
