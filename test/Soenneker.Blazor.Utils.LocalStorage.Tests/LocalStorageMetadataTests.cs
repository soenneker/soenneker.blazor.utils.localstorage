using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.Utils.LocalStorage.Abstract;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

public sealed class LocalStorageMetadataTests
{
    [Test]
    public async Task SourceGeneratedMetadataRoundTripsWithoutReflection()
    {
        var interop = new StorageStub();
        ILocalStorageUtil storage = new LocalStorageUtil(interop);
        var context = new StorageJsonContext(new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await storage.Set("item", new StorageItem { Name = "saved" }, context.StorageItem);
        if (interop.Value != "{\"name\":\"saved\"}")
            throw new InvalidOperationException("The supplied metadata must control JSON serialization.");

        StorageItem? item = await storage.Get("item", context.StorageItem);
        if (item?.Name != "saved")
            throw new InvalidOperationException("The stored object did not round-trip.");

        interop.Value = null;
        if (await storage.Get("item", context.StorageItem) is not null)
            throw new InvalidOperationException("A missing entry must return default.");

        interop.Value = "   ";
        if (await storage.Get("item", context.StorageItem) is not null)
            throw new InvalidOperationException("A blank JSON entry must return default.");

        await storage.Set("item", "unquoted text", context.String);
        if (interop.Value != "unquoted text" || await storage.Get("item", context.String) != "unquoted text")
            throw new InvalidOperationException("String values must retain the existing raw-string behavior.");
    }

    private sealed class StorageStub : ILocalStorageInterop
    {
        public string? Value { get; set; }
        public ValueTask<string?> Get(string key, CancellationToken cancellationToken = default) => ValueTask.FromResult(Value);
        public ValueTask Set(string key, string value, CancellationToken cancellationToken = default)
        {
            Value = value;
            return ValueTask.CompletedTask;
        }
        public ValueTask Initialize(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask Remove(string key, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask Clear(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask<bool> ContainsKey(string key, CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask<IReadOnlyList<string>> GetKeys(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask<int> GetLength(CancellationToken cancellationToken = default) => throw new NotSupportedException();
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }
}

internal sealed class StorageItem
{
    public string? Name { get; set; }
}

[JsonSerializable(typeof(StorageItem))]
[JsonSerializable(typeof(string))]
internal partial class StorageJsonContext : JsonSerializerContext;
