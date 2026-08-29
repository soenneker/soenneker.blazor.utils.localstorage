[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.localstorage.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.localstorage/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.localstorage/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.localstorage/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.localstorage.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.localstorage/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.utils.localstorage)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.localstorage/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.localstorage/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.LocalStorage

A scoped Blazor utility for storing strings and JSON-serialized .NET values in the browser’s `localStorage`.

Values persist across page loads and browser restarts for the same origin, subject to browser policy and user clearing. For tab-lifetime data, use session storage instead.

## Installation

```bash
dotnet add package Soenneker.Blazor.Utils.LocalStorage
```

```csharp
using Soenneker.Blazor.Utils.LocalStorage.Registrars;

builder.Services.AddLocalStorageUtilAsScoped();
```

```razor
@using Soenneker.Blazor.Utils.LocalStorage.Abstract
@inject ILocalStorageUtil LocalStorage
```

Storage calls require browser interop. Use the service after interactive rendering, not during server prerendering.

## Store and retrieve strings

```csharp
await LocalStorage.Set("myapp:theme", "dark");

string? theme = await LocalStorage.Get("myapp:theme");
bool exists = await LocalStorage.ContainsKey("myapp:theme");
```

`Get` returns `null` for a missing key and preserves an existing empty string. Keys must be non-empty, and stored values cannot be null.

`Initialize()` is optional. It eagerly loads the JavaScript module but does not read or create a storage entry.

## Store typed values

```csharp
public sealed record LayoutPreference(bool Compact, int PageSize);

await LocalStorage.Set(
    "myapp:layout",
    new LayoutPreference(Compact: true, PageSize: 25));

LayoutPreference? preference =
    await LocalStorage.Get<LayoutPreference>("myapp:layout");
```

Non-string values are JSON-serialized with web defaults. Malformed or incompatible stored JSON causes deserialization to throw. Stored model shapes should be versioned or migrated when they change.

`Get<T>` returns the default value when the key is missing, when a non-string stored value is blank, or when JSON deserializes as `null`. Use `ContainsKey` when the missing-key distinction matters.

## Remove and inspect entries

```csharp
await LocalStorage.Remove("myapp:layout");

IReadOnlyList<string> keys = await LocalStorage.GetKeys();
int count = await LocalStorage.GetLength();
```

Key order is controlled by the browser and should not be used as a stable sort order. Prefix application keys, such as `myapp:`, to avoid collisions with other code on the same origin.

`Clear` removes every local-storage entry for the origin, including entries created by unrelated libraries in the same application:

```csharp
await LocalStorage.Clear();
```

Prefer removing known, namespaced keys individually unless deleting all origin storage is explicitly intended.

## Concurrency and failures

Each browser storage operation is synchronous and atomic, but a .NET read-modify-write sequence is not. Another tab can change a value between calls. This package does not expose the browser `storage` event or provide cross-tab locking.

Writes can throw because storage is disabled, unavailable in the browsing mode, or over quota. Large values also block the browser’s main thread during the underlying operation. Handle interop exceptions and use IndexedDB for larger datasets.

Cancellation stops waiting for Blazor interop; it cannot undo a synchronous browser write that already completed.

## Security

Local storage is readable and writable by JavaScript running on the origin. Do not store passwords, bearer tokens, private keys, server session identifiers, or other high-value secrets. An XSS vulnerability can expose or modify every stored value.

Treat retrieved content as untrusted input. Validate it before using it in authorization logic, URLs, queries, file operations, or rendered output. Local storage is a client-side convenience, not an authoritative data store.
