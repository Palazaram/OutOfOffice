using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.Core.Models;

namespace OutOfOffice.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectEntity> builder) 
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProjectType).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(p => p.StartDate).IsRequired().HasColumnType("DATE");
            builder.Property(p => p.EndDate).IsRequired(false).HasColumnType("DATE");
            builder.Property(p => p.Comment).IsRequired(false).HasColumnType("NVARCHAR(400)");
            builder.Property(p => p.Status).IsRequired().HasColumnType("NVARCHAR(100)");

            builder.HasOne(p => p.ProjectManager).WithMany(e => e.Projects).HasForeignKey(er => er.ProjectManagerId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Employees).WithMany(e => e.AssignedProjects);
        }
    }
}
