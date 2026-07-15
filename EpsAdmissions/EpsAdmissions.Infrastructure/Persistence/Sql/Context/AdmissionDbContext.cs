using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EpsAdmissions.Infrastructure.Persistence.Sql.Context;

public sealed class AdmissionDbContext(DbContextOptions<AdmissionDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Admission> Admissions => Set<Admission>();

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdmissionDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}