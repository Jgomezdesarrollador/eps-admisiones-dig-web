using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Context;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Settings;
using EpsAdmissions.Infrastructure.Persistence.Sql.Context;
using EpsAdmissions.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EpsAdmissions.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region SQL Server

        services.AddDbContext<AdmissionDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SqlServer"),
                sql =>
                {
                    sql.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });
        });

        #endregion

        #region MongoDB

        services.Configure<MongoSettings>(
            configuration.GetSection(MongoSettings.SectionName));

        services.AddSingleton<IMongoClient>(_ =>
        {
            var settings = configuration
                .GetSection(MongoSettings.SectionName)
                .Get<MongoSettings>()!;

            return new MongoClient(settings.ConnectionString);
        });

        services.AddSingleton<MongoContext>();

        #endregion

        #region Repositories

        services.AddScoped<IAdmissionRepository, AdmissionRepository>();

        services.AddScoped<IOutboxRepository, OutboxRepository>();

        services.AddScoped<IClinicalHistoryStorage, MongoClinicalHistoryStorage>();

        #endregion

        #region Unit Of Work

        services.AddScoped<IUnitOfWork>(provider =>
            (IUnitOfWork)provider.GetRequiredService<AdmissionDbContext>());

        #endregion

        return services;
    }
}