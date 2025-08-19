using System.Diagnostics.Contracts;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MIOTO.APPLICATION.Behaviors;

namespace MIOTO.APPLICATION.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConffigMediatR(this IServiceCollection services)
        => services.AddMediatR(opt => opt.RegisterServicesFromAssembly(AssemblyReference.Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

    // public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
    //     => services.AddAutoMapper(typeof(ServiceProfile));
}