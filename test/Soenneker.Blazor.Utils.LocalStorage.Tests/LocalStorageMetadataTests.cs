using System;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Soenneker.Blazor.Utils.ModuleImport;
using System.Threading.Tasks;
using Soenneker.Librarian.LocalStorage;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

public sealed class LocalStorageMetadataTests
{
    [Test]
    public async ValueTask SourceGeneratedMetadataRoundTripsWithoutReflection()
    {
        await using var modules = new ModuleImportUtil(new SnapshotRuntime());
        await using var storage = new LocalStorageUtil(modules, NullLogger<LocalStorageLibrarianDatabase>.Instance);
        var context = new StorageJsonContext(new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await storage.Set("item", new StorageItem { Name = "saved" }, context.StorageItem);
        if (await storage.Get("item") != "{\"name\":\"saved\"}")
            throw new InvalidOperationException("The supplied metadata must control JSON serialization.");

        StorageItem? item = await storage.Get("item", context.StorageItem);
        if (item?.Name != "saved")
            throw new InvalidOperationException("The stored object did not round-trip.");

        await storage.Remove("item");
        if (await storage.Get("item", context.StorageItem) is not null)
            throw new InvalidOperationException("A missing entry must return default.");

        await storage.Set("item", "   ");
        if (await storage.Get("item", context.StorageItem) is not null)
            throw new InvalidOperationException("A blank JSON entry must return default.");

        await storage.Set("item", "unquoted text", context.String);
        if (await storage.Get("item") != "unquoted text" || await storage.Get("item", context.String) != "unquoted text")
            throw new InvalidOperationException("String values must retain the existing raw-string behavior.");
    }

}
