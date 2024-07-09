using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.Core.Models;

namespace OutOfOffice.Persistence.Configurations
{
    public class ApprovalRequestConfiguration : IEntityTypeConfiguration<ApprovalRequestEntity>
    {
        public void Configure(EntityTypeBuilder<ApprovalRequestEntity> builder) 
        {
            builder.HasKey(er => er.Id);

            builder.Property(er => er.Status).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(er => er.Comment).IsRequired(false).HasColumnType("NVARCHAR(400)");

            builder.HasOne(er => er.Approver).WithMany(e => e.ApprovalRequests).HasForeignKey(er => er.ApproverId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);
            builder.HasOne(er => er.LeaveRequest).WithMany(lr => lr.ApprovalRequests).HasForeignKey(er => er.LeaveRequestId).OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.ApproverId).IsRequired(false);
        }
    }
}
