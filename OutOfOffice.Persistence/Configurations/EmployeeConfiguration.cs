using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutOfOffice.Core.Models;

namespace OutOfOffice.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder) 
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FullName).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(e => e.Subdivision).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(e => e.Position).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(e => e.Status).IsRequired().HasColumnType("NVARCHAR(100)");
            builder.Property(e => e.OutOfOfficeBalance).IsRequired().HasColumnType("INT");
            builder.Property(e => e.PhotoPath).HasColumnType("NVARCHAR(500)");

            builder.Ignore(e => e.Photo);

            builder.HasOne(e => e.PeoplePartner).WithMany(e => e.PeoplePartnerEmployees).HasForeignKey(e => e.PeoplePartnerId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

            builder.Property(e => e.PeoplePartnerId).IsRequired(false);

            builder.HasMany(e => e.AssignedProjects).WithMany(p => p.Employees);
        }
    }
}
