using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.LocalStorage.Abstract;

namespace Soenneker.Blazor.Utils.LocalStorage.Registrars;

/// <summary>
/// Registration for the Librarian-backed storage utility.
/// </summary>
public static class LocalStorageUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ILocalStorageUtil"/> as a scoped service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddLocalStorageUtilAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped();

        services.TryAddScoped<ILocalStorageUtil, LocalStorageUtil>();

        return services;
    }
}
