using System.Text.Json.Serialization;

namespace Soenneker.Blazor.Utils.LocalStorage;

[JsonSerializable(typeof(StorageDocument))]
internal partial class StorageJsonContext : JsonSerializerContext;
