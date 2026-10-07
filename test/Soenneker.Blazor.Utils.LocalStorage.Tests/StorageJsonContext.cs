using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Utils.LocalStorage.Tests;

[JsonSerializable(typeof(StorageItem))]
[JsonSerializable(typeof(string))]
internal partial class StorageJsonContext : JsonSerializerContext;
