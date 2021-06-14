using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OvertimeManager.MVC5.Web.DbContexts;

namespace OvertimeManager.MVC5.Web.DbModels
{
    [Table("StateOvertimeRule")]
    public partial class StateOvertimeRule
    {
        [Key]
        public Guid StateOvertimeRuleKeyId { get; set; }

        [Required]
        [StringLength(2)]
        public string StateCode { get; set; }

        [Required]
        [StringLength(500)]
        public string RuleName { get; set; }

        [Required]
        [StringLength(255)]
        public string RuleTypeName { get; set; }

        public int RuleEqualToGreaterThreshold { get; set; }

        public decimal OvertimeRate { get; set; } // = 1.5m;

        public decimal HourlyWageToUse { get; set; }

        public virtual StateCodes StateCode1 { get; set; }

        public virtual StateOvertimeRuleType StateOvertimeRuleType { get; set; }
    }
}
