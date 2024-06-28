using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.BirthDate).IsRequired();

            builder.HasOne(d => d.Office).WithMany(x => x.Users).HasForeignKey(d => d.OfficeId);
        }
    }
}
