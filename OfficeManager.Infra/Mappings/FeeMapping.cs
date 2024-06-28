using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class FeeMapping : IEntityTypeConfiguration<Fee>
    {
        public void Configure(EntityTypeBuilder<Fee> builder)
        {
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.PaymentDate).IsRequired();
            builder.Property(p => p.PaymentStatus).IsRequired();

            builder.HasOne(d => d.Case).WithMany().HasForeignKey(d => d.CaseId);
            builder.HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId);
        }
    }
}