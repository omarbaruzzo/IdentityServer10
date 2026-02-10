# Migration IdentityServer10: .NET 8 → .NET 10

Summary of the files and changes applied to move the project to .NET 10 and version **10.0.0**.

## Build workaround (applied)

- **Workload resolver**: in `Directory.Build.props` the property `MSBuildEnableWorkloadResolver=false` is set to avoid MSB4276 (missing `WorkloadAutoImportPropsLocator` directories on some SDK 10 installations).
- **Packages**: versions in `Directory.Packages.props` have been updated to .NET 10–compatible values (ASP.NET 10.0.2, EF 10.0.2, Swashbuckle 10.0.0, Microsoft.NET.Test.Sdk 18.0.1, coverlet 6.0.2, Pomelo EF MySql 8.0.2, Aspire/ServiceDiscovery 10.2.0, etc.).

---

## 1. Build and target framework (required)

### `Directory.Build.props` (root)
- Change `<TargetFramework>net8.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`.
- Optionally align product/version metadata (here already set for IdentityServer10):
  - `<Version>10.0.0</Version>`
  - `<IdentityServerVersion>10.0.0</IdentityServerVersion>`

### `Directory.Build.targets` (root)
- Update conditions like `Condition="'%(TargetFramework)' == 'net8.0'"` → `Condition="'%(TargetFramework)' == 'net10.0'"`.
- If there are comments mentioning “.NET 8”, update them to “.NET 10” where appropriate (the ILLink workaround may still be needed).

---

## 2. SDK and `global.json` (required)

### `global.json`
- Ensure SDK 10 is referenced, for example: `"version": "10.0.102"` (or the latest 10.0.x installed on your environment).

---

## 3. Central package versions (required)

### `Directory.Packages.props`
Update the version variables for .NET 10:

| Variable                  | From   | To        |
|---------------------------|--------|-----------|
| `AspnetVersion`          | 8.0.0  | 10.0.0+   |
| `AspnetMinorVersion`     | 8.0.1  | 10.0.x    |
| `MicrosoftExtensionsVersion` | 8.0.0 | 10.0.0+ |
| `EfVersion`              | 8.0.0  | 10.0.0+   |
| `RuntimeVersion`         | 8.0.0  | 10.0.0    |
| `AspireVersion`          | 8.0.0-preview.* | 10.0.0-preview.* (when available) |

Also review any hard‑coded 8.0.x versions and bump them to 10.0.x (or to the appropriate central variable), for example:

- `Microsoft.AspNetCore.Authentication.OpenIdConnect`
- `Microsoft.AspNetCore.TestHost`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.Extensions.DependencyInjection` / `Abstractions` / `Logging.Abstractions`

---

## 4. GitHub Actions (required if you use CI)

For all workflows in `.github/workflows/` that use `setup-dotnet`:

- Files: `develop.yml`, `master.yml`, `release.yml`, `pre-release.yml`
- Change `dotnet-version: 8.0.x` → `dotnet-version: 10.0.x`

---

## 5. Samples (recommended)

### `samples/Directory.Build.props`
- Update the sample identity server version, for example:  
  `<IdentityServerVersion>10.0.0</IdentityServerVersion>`

---

## 6. Documentation and READMEs (optional but recommended)

- **`docs/conf.py`**: update `version` and `release` from 8.x values to 10.x (e.g. `version = '10.0.0'`, `release = '10.0.0'`).
- **`docs/index.rst`**: update any text that mentions “.NET 8” to “.NET 10”.
- **`README.md`** and **`src/README.md`**: replace references to “.NET 8”, “8.0.4”, and “latest .NET 8 SDK” with `.NET 10`, `10.0.0`, and “latest .NET 10 SDK” where they refer to the current IdentityServer10 line (keep IdentityServer8 references clearly marked as historical).

---

## 7. Connection strings and databases (optional)

Database names in connection strings may still contain `8.0.0`. You can leave them as‑is or align them to the new version, for example:

- `src/EntityFramework.Storage/host/ConsoleHost/Program.cs`: `IdentityServer8.EntityFramework-8.0.0` → `IdentityServer10.EntityFramework-10.0.0`
- `src/EntityFramework/migrations/SqlServer/appsettings.json`
- `src/EntityFramework.Storage/migrations/SqlServer/appsettings.json`
- `src/AspNetIdentity/migrations/SqlServer/appsettings.json`
- `src/AspNetIdentity/host/appsettings.json`

If you rename databases, remember to create or migrate the corresponding development databases.

---

## 8. No per-project `TargetFramework` overrides

`.csproj` files do not explicitly define `TargetFramework`; they inherit from `Directory.Build.props`. You generally do **not** need to touch individual projects just to change the framework version.

---

## Recommended migration order

1. `Directory.Build.props` (update `TargetFramework`, `Version`, `IdentityServerVersion`).
2. `Directory.Build.targets` (adjust conditions to `net10.0`).
3. `global.json` (point to an SDK 10.0.x).
4. `Directory.Packages.props` (bump all remaining 8.0.x packages to 10.0.x where required).
5. Workflows in `.github/workflows/`.
6. `samples/Directory.Build.props`.
7. Run `dotnet restore` and `dotnet build` from the root (e.g. `src/IdentityServer10.sln`) and fix any .NET 10 compatibility or breaking changes as they appear.
8. Update documentation and connection strings if you want them to reflect the new versioning.

---

## Notes

- **.NET 10** is LTS (support until November 2028). Make sure you have an SDK 10.x installed (`dotnet --list-sdks`).
- After upgrading, review the official ASP.NET Core 10 and Entity Framework Core 10 breaking changes documentation.
- The ILLink/netstandard2.1 workaround in `Directory.Build.targets` might still be required; if you rely on trimming, keep the `netstandard2.1` entries in `KnownILLinkPack` and the `net10.0` condition in place.
