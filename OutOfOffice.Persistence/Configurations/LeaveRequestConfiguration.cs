using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.Core.Models;

namespace OutOfOffice.Persistence.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequestEntity>
    {
        public void Configure(EntityTypeBuilder<LeaveRequestEntity> builder) 
        {
            builder.HasKey(lr => lr.Id);

            builder.Property(lr => lr.AbsenceReason).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(lr => lr.StartDate).IsRequired().HasColumnType("DATE");
            builder.Property(lr => lr.EndDate).IsRequired().HasColumnType("DATE");
            builder.Property(lr => lr.Comment).IsRequired(false).HasColumnType("NVARCHAR(400)");
            builder.Property(lr => lr.Status).IsRequired().HasColumnType("NVARCHAR(100)");

            builder.HasOne(lr => lr.Employee).WithMany(e => e.LeaveRequests).HasForeignKey(lr => lr.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
