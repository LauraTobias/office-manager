using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class CaseMapping : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.ConclusionDate).IsRequired();

            builder.HasOne(d => d.Client).WithMany().HasForeignKey(d => d.ClientId);
        }
    }
}