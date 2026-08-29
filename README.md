[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.localstorage.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.localstorage/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.localstorage/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.localstorage/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.localstorage.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.localstorage/)
[![](https://img.shields.io/badge/Demo-Live-blueviolet?style=for-the-badge&logo=github)](https://soenneker.github.io/soenneker.blazor.utils.localstorage)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.localstorage/codeql.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.localstorage/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.LocalStorage

Blazor interop for browser `localStorage` operations.

## Install

```bash
dotnet add package Soenneker.Blazor.Utils.LocalStorage
```

## Quick start

```csharp
using Soenneker.Blazor.Utils.LocalStorage.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddLocalStorageUtilAsScoped();
```

Adds `ILocalStorageInterop` and `ILocalStorageUtil` as scoped services.

## What you get

- `ILocalStorageInterop` — Blazor interop for browser `localStorage` operations.
- `ILocalStorageUtil` — A higher-level Blazor utility for browser `localStorage` built on top of `ILocalStorageInterop`.
- `LocalStorageUtilRegistrar` — Registration for the interop and utility services.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ILocalStorageInterop.Initialize(cancellationToken)` | Ensures the JavaScript module for this package has been loaded and initialized. | A task that completes when the Local Storage is ready for use. |
| `ILocalStorageInterop.Get(key, cancellationToken)` | Gets a stored string value by key, or null if the key does not exist. | A task whose result is the text returned by get. |
| `ILocalStorageInterop.Remove(key, cancellationToken)` | Removes a stored value by key. | A task that completes when the remove operation is complete. |
| `ILocalStorageInterop.Clear(cancellationToken)` | Clears all browser local storage entries. | A task that completes when the Local Storage has been cleared. |
| `ILocalStorageInterop.ContainsKey(key, cancellationToken)` | Returns whether the specified key exists in browser local storage. | true if the specified key exists in the target store; otherwise, false. |
| `ILocalStorageInterop.GetKeys(cancellationToken)` | Returns all local storage keys in index order. | The matching keys as a materialized collection. |
| `ILocalStorageInterop.GetLength(cancellationToken)` | Returns the total number of local storage entries. | A task whose result is the requested value. |
| `ILocalStorageUtil.Initialize(cancellationToken)` | Ensures the underlying JavaScript module has been loaded and is ready for use. | A task that completes when the Local Storage is ready for use. |
| `ILocalStorageUtil.Get(key, cancellationToken)` | Gets a stored string value by key, or null if the key does not exist. | A task whose result is the text returned by get. |
| `ILocalStorageUtil.Remove(key, cancellationToken)` | Removes a stored value by key. | A task that completes when the remove operation is complete. |
| `ILocalStorageUtil.Clear(cancellationToken)` | Clears all browser local storage entries. | A task that completes when the Local Storage has been cleared. |
| `ILocalStorageUtil.ContainsKey(key, cancellationToken)` | Returns whether the specified key exists in browser local storage. | true if the specified key exists in the target store; otherwise, false. |
| `ILocalStorageUtil.GetKeys(cancellationToken)` | Returns all local storage keys in index order. | The matching keys as a materialized collection. |
| `ILocalStorageUtil.GetLength(cancellationToken)` | Returns the total number of local storage entries. | A task whose result is the requested value. |
| `LocalStorageUtilRegistrar.AddLocalStorageUtilAsScoped(services)` | Adds `ILocalStorageInterop` and `ILocalStorageUtil` as scoped services. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Dispose instances you own when their scope ends so held resources can be released.
