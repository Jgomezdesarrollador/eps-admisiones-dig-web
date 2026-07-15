using EpsAdmissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpsAdmissions.Infrastructure.Persistence.Sql.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EventType)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(x => x.Payload)
               .HasColumnType("nvarchar(max)")
               .IsRequired();

        builder.Property(x => x.CreatedAt)
               .IsRequired();

        builder.Property(x => x.Processed);

        builder.Property(x => x.RetryCount);

        builder.Property(x => x.AggregateId)
       .HasMaxLength(50)
       .IsRequired();
    }
}