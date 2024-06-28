using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OfficeManager.Domain.Entities;

namespace OfficeManager.Infra.Mappings
{
    public class ClientMeetingMapping : IEntityTypeConfiguration<ClientMeeting>
    {
        public void Configure(EntityTypeBuilder<ClientMeeting> builder)
        {
            builder.Property(p => p.MeetingDate).IsRequired();
            builder.Property(p => p.Description).IsRequired();

            builder.HasOne(d => d.Client).WithMany(x => x.Meetings).HasForeignKey(d => d.ClientId);
        }
    }
}
