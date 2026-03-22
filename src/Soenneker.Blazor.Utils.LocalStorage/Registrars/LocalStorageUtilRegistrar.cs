using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.LocalStorage.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Blazor.Utils.LocalStorage.Registrars;

/// <summary>
/// Registration for the interop and utility services.
/// </summary>
public static class LocalStorageUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ILocalStorageInterop"/> and <see cref="ILocalStorageUtil"/> as scoped services.
    /// </summary>
    public static IServiceCollection AddLocalStorageUtilAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
                .TryAddScoped<ILocalStorageInterop, LocalStorageInterop>();

        services.TryAddScoped<ILocalStorageUtil, LocalStorageUtil>();

        return services;
    }
}
