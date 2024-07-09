using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutOfOffice.Core.Models;
using OutOfOffice.Persistence.Configurations;

namespace OutOfOffice.Persistence
{
    public class OutOfOfficeDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EmployeeEntity> Employees => Set<EmployeeEntity>();
        public DbSet<LeaveRequestEntity> LeaveRequests => Set<LeaveRequestEntity>();
        public DbSet<ApprovalRequestEntity> ApprovalRequests => Set<ApprovalRequestEntity>();
        public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

        public OutOfOfficeDbContext(DbContextOptions<OutOfOfficeDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new LeaveRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ApprovalRequestConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        }
    }
}
