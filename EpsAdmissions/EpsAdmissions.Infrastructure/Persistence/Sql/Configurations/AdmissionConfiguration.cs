using EpsAdmissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EpsAdmissions.Infrastructure.Persistence.Sql.Configurations;

public sealed class AdmissionConfiguration : IEntityTypeConfiguration<Admission>
{
    public void Configure(EntityTypeBuilder<Admission> builder)
    {
        builder.ToTable("Admissions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PatientDocument)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(x => x.CopaymentAmount)
               .HasPrecision(18, 2);

        builder.Property(x => x.MongoDocumentId)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Status)
               .HasConversion<int>();

        builder.Property(x => x.AdmissionDate)
               .IsRequired();
    }
}