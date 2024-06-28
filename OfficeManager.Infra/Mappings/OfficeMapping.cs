using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class OfficeMapping : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Cnpj).IsRequired();

            builder.HasMany(x => x.Users).WithOne(y => y.Office);
        }
    }
}
