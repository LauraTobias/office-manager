using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class DocumentMapping : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.Property(p => p.Url).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.UploadDate).IsRequired();

            builder.HasOne(d => d.Case).WithMany().HasForeignKey(d => d.CaseId);
        }
    }
}