using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(p => p.Cpf).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Phone).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Address).IsRequired();

            builder.HasMany(x => x.Meetings).WithOne(y => y.Client).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(d => d.Office).WithMany().HasForeignKey(d => d.OfficeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
