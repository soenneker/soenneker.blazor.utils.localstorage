using System;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Soenneker.Blazor.Utils.ModuleImport;
using System.Threading.Tasks;
using Soenneker.Librarian.LocalStorage;
using System.Threading;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

public sealed class LocalStorageMetadataTests
{
    [Test]
    public async ValueTask SourceGeneratedMetadataRoundTripsWithoutReflection(CancellationToken cancellationToken)
    {
        await using var modules = new ModuleImportUtil(new SnapshotRuntime());
        await using var storage = new LocalStorageUtil(modules, NullLogger<LocalStorageLibrarianDatabase>.Instance);
        var context = new StorageJsonContext(new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await storage.Set("item", new StorageItem { Name = "saved" }, context.StorageItem, cancellationToken: cancellationToken);
        if (await storage.Get("item", cancellationToken: cancellationToken) != "{\"name\":\"saved\"}")
            throw new InvalidOperationException("The supplied metadata must control JSON serialization.");

        StorageItem? item = await storage.Get("item", context.StorageItem, cancellationToken: cancellationToken);
        if (item?.Name != "saved")
            throw new InvalidOperationException("The stored object did not round-trip.");

        await storage.Remove("item", cancellationToken: cancellationToken);
        if (await storage.Get("item", context.StorageItem, cancellationToken: cancellationToken) is not null)
            throw new InvalidOperationException("A missing entry must return default.");

        await storage.Set("item", "   ", cancellationToken: cancellationToken);
        if (await storage.Get("item", context.StorageItem, cancellationToken: cancellationToken) is not null)
            throw new InvalidOperationException("A blank JSON entry must return default.");

        await storage.Set("item", "unquoted text", context.String, cancellationToken: cancellationToken);
        if (await storage.Get("item", cancellationToken: cancellationToken) != "unquoted text" || await storage.Get("item", context.String, cancellationToken: cancellationToken) != "unquoted text")
            throw new InvalidOperationException("String values must retain the existing raw-string behavior.");
    }

}
