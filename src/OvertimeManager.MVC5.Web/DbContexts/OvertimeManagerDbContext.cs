using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using OvertimeManager.MVC5.Web.DbModels;

namespace OvertimeManager.MVC5.Web.DbContexts
{
    public partial class OvertimeManagerDbContext : DbContext
    {
        public OvertimeManagerDbContext()
            : base("name=OvertimeManagerDbContext")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeHour> EmployeeHours { get; set; }
        public virtual DbSet<StateCodes> StateCodes { get; set; }
        public virtual DbSet<StateOvertimeRule> StateOvertimeRules { get; set; }
        public virtual DbSet<StateOvertimeRuleType> StateOvertimeRuleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.EmployeeHour)
                .WithRequired(e => e.Company)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeHour)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeHour>()
                .Property(e => e.HourlyWage)
                .HasPrecision(10, 2);

            modelBuilder.Entity<StateCodes>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<StateCodes>()
                .Property(e => e.StateName)
                .IsUnicode(false);

            modelBuilder.Entity<StateCodes>()
                .HasMany(e => e.Company)
                .WithRequired(e => e.StateCode1)
                .HasForeignKey(e => e.StateCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StateCodes>()
                .HasMany(e => e.StateOvertimeRule)
                .WithRequired(e => e.StateCode1)
                .HasForeignKey(e => e.StateCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StateOvertimeRule>()
                .Property(e => e.StateCode)
                .IsUnicode(false);

            modelBuilder.Entity<StateOvertimeRule>()
                .Property(e => e.RuleName)
                .IsUnicode(false);

            modelBuilder.Entity<StateOvertimeRule>()
                .Property(e => e.RuleTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<StateOvertimeRule>()
                .Property(e => e.OvertimeRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<StateOvertimeRule>()
                .Property(e => e.HourlyWageToUse)
                .HasPrecision(10, 2);

            modelBuilder.Entity<StateOvertimeRuleType>()
                .Property(e => e.RuleTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<StateOvertimeRuleType>()
                .HasMany(e => e.StateOvertimeRule)
                .WithRequired(e => e.StateOvertimeRuleType)
                .WillCascadeOnDelete(false);
        }
    }
}
