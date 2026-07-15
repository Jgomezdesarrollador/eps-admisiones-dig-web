using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace EpsAdmissions.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAdmitPatientUseCase, AdmitPatientUseCase>();

        return services;
    }
}