using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Blazor.Utils.LocalStorage.Abstract;

/// <summary>
/// A higher-level Blazor utility for browser <c>localStorage</c> built on top of <see cref="ILocalStorageInterop"/>.
/// </summary>
public interface ILocalStorageUtil
{
    /// <summary>
    /// Ensures the underlying JavaScript module has been loaded and is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Local Storage is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a stored string value by key, or null if the key does not exist.
    /// </summary>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by get.</returns>
    ValueTask<string?> Get(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a JSON-serialized value by key, or default if the key does not exist.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the local storage.</typeparam>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the value returned by get.</returns>
    [RequiresUnreferencedCode("JSON deserialization uses reflection. Use the overload accepting JsonTypeInfo<T> for trimming.")]
    [RequiresDynamicCode("JSON deserialization may require runtime code generation. Use the overload accepting JsonTypeInfo<T> for AOT.")]
    ValueTask<T?> Get<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a JSON value using supplied serialization metadata, or default for a missing or blank entry.
    /// String values are returned directly without JSON decoding.
    /// </summary>
    /// <typeparam name="T">The stored value type.</typeparam>
    /// <param name="key">The storage key.</param>
    /// <param name="typeInfo">Source-generated metadata for AOT-compatible deserialization.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The stored value, or default when no JSON value is present.</returns>
    ValueTask<T?> Get<T>(string key, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a string value for the specified key.
    /// </summary>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="value">Value to serialize and store under the specified key.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the set operation is complete.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    ValueTask Set(string key, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets a JSON-serialized value for the specified key.
    /// </summary>
    /// <typeparam name="T">Type of value handled by the local storage.</typeparam>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="value">Value to serialize and store under the specified key.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the set operation is complete.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    [RequiresUnreferencedCode("JSON serialization uses reflection. Use the overload accepting JsonTypeInfo<T> for trimming.")]
    [RequiresDynamicCode("JSON serialization may require runtime code generation. Use the overload accepting JsonTypeInfo<T> for AOT.")]
    ValueTask Set<T>(string key, T value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores a JSON value using supplied serialization metadata. String values are stored directly without JSON encoding.
    /// </summary>
    /// <typeparam name="T">The stored value type.</typeparam>
    /// <param name="key">The storage key.</param>
    /// <param name="value">The non-null value to store.</param>
    /// <param name="typeInfo">Source-generated metadata for AOT-compatible serialization.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the value has been stored.</returns>
    ValueTask Set<T>(string key, T value, JsonTypeInfo<T> typeInfo, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a stored value by key.
    /// </summary>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the remove operation is complete.</returns>
    ValueTask Remove(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears all browser local storage entries.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Local Storage has been cleared.</returns>
    ValueTask Clear(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns whether the specified key exists in browser local storage.
    /// </summary>
    /// <param name="key">Key used to locate the target entry.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if the specified key exists in the target store; otherwise, false.</returns>
    ValueTask<bool> ContainsKey(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all local storage keys in index order.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by get Keys.</returns>
    ValueTask<IReadOnlyList<string>> GetKeys(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the total number of local storage entries.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested value.</returns>
    ValueTask<int> GetLength(CancellationToken cancellationToken = default);
}
