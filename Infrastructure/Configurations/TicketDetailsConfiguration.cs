using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Entities.Configurations
{
    public class TicketDetailsConfiguration : IEntityTypeConfiguration<TicketDetails>
    {
        public void Configure(EntityTypeBuilder<TicketDetails> builder)
        {
            builder
                .HasKey(x => x.Id);
        }
    }
}