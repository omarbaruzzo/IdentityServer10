# Migrazione IdentityServer8: .NET 8 → .NET 10

Riepilogo dei file e delle modifiche applicate per portare il progetto su .NET 10.

## Workaround build (applicato)

- **Workload resolver**: in `Directory.Build.props` è impostato `MSBuildEnableWorkloadResolver=false` per evitare l’errore MSB4276 (directory WorkloadAutoImportPropsLocator mancanti in alcuni installazioni dell’SDK 10).
- **Pacchetti**: aggiornate le versioni in `Directory.Packages.props` a quelle compatibili con .NET 10 (ASP.NET 10.0.2, EF 10.0.2, Swashbuckle 10.0.0, Microsoft.NET.Test.Sdk 18.0.1, coverlet 6.0.2, Pomelo EF MySql 8.0.2, Aspire/ServiceDiscovery 10.2.0).

---

## 1. Build e target framework (obbligatori)

### `Directory.Build.props` (root)
- **Riga 4:** `<TargetFramework>net8.0</TargetFramework>` → `<TargetFramework>net10.0</TargetFramework>`
- **Righe 6-7:** Aggiornare se vuoi allineare la versione del prodotto (es. `10.0.0`):
  - `<Version>8.0.4</Version>` → `<Version>10.0.0</Version>`
  - `<IdentityServerVersion>8.0.4</IdentityServerVersion>` → `<IdentityServerVersion>10.0.0</IdentityServerVersion>`

### `Directory.Build.targets` (root)
- **Riga 11:** `Condition="'%(TargetFramework)' == 'net8.0'"` → `Condition="'%(TargetFramework)' == 'net10.0'"`
- **Commento righe 2-5:** Puoi aggiornare il TODO da ".NET 8" a ".NET 10" (il workaround ILLink potrebbe essere ancora necessario).

---

## 2. SDK e global.json (obbligatorio)

### `global.json`
- **Riga 3:** `"version":"8.0.408"` → `"version":"10.0.102"` (o l’ultima 10.0.x disponibile sul tuo ambiente).

---

## 3. Versioni pacchetti centralizzate (obbligatorio)

### `Directory.Packages.props`
Aggiornare le variabili di versione per .NET 10:

| Variabile | Da | A |
|-----------|----|---|
| `AspnetVersion` | 8.0.0 | 10.0.0 |
| `AspnetMinorVersion` | 8.0.1 | 10.0.x (es. 10.0.1) |
| `MicrosoftExtensionsVersion` | 8.0.0 | 10.0.0 |
| `EfVersion` | 8.0.0 | 10.0.0 |
| `RuntimeVersion` | 8.0.0 | 10.0.0 |
| `AspireVersion` | 8.0.0-preview.2... | 10.0.0-preview.x (quando disponibile) |

Controllare anche i pacchetti con versione hardcoded 8.0.x, ad esempio:
- `Microsoft.AspNetCore.Authentication.OpenIdConnect` Version="8.0.0"
- `Microsoft.AspNetCore.TestHost` Version="8.0.1"
- `Microsoft.EntityFrameworkCore.Design` Version="8.0.1"
- `Microsoft.Extensions.DependencyInjection` / `Abstractions` / `Logging.Abstractions` Version="8.0.0"

Portarli a 10.0.x (o alla variabile centralizzata se già presente).

---

## 4. GitHub Actions (obbligatorio se usi CI)

In tutti i workflow in `.github/workflows/` che usano `setup-dotnet`:

- **File:** `develop.yml`, `master.yml`, `release.yml`, `pre-release.yml`
- **Modifica:** `dotnet-version: 8.0.x` → `dotnet-version: 10.0.x`

---

## 5. Samples (consigliato)

### `samples/Directory.Build.props`
- **Riga 3:** `<IdentityServerVersion>8.0.4-alpha.2</IdentityServerVersion>` → `<IdentityServerVersion>10.0.0</IdentityServerVersion>` (o la versione che usi dopo la migrazione).

---

## 6. Documentazione e README (opzionale)

- **`docs/conf.py`** (righe 72-74): `version = '8.0.0'`, `release = '8.0.4'` → aggiornare a 10.0.x.
- **`docs/index.rst`**: Testi che citano "DotNet 8" → "DotNet 10".
- **`README.md`** e **`src/README.md`**: Riferimenti a ".NET 8", "8.0.4", "latest .NET 8 SDK" → ".NET 10", "10.0.x", "latest .NET 10 SDK".

---

## 7. Connection string e database (opzionale)

I nomi database nelle connection string contengono "8.0.0"; puoi lasciarli o allinearli alla nuova versione:

- `src/EntityFramework.Storage/host/ConsoleHost/Program.cs`: `IdentityServer8.EntityFramework-8.0.0` → eventualmente `...-10.0.0`
- `src/EntityFramework/migrations/SqlServer/appsettings.json`
- `src/EntityFramework.Storage/migrations/SqlServer/appsettings.json`
- `src/AspNetIdentity/migrations/SqlServer/appsettings.json`
- `src/AspNetIdentity/host/appsettings.json`

Se li cambi, ricordati di creare/aggiornare il database di sviluppo (es. migrazioni o nuovo DB).

---

## 8. Nessun override per target nei .csproj

I `.csproj` non definiscono `TargetFramework` in modo esplicito: ereditano da `Directory.Build.props`. Non serve toccare i singoli progetti per il solo cambio di framework.

---

## Ordine consigliato

1. `Directory.Build.props` (TargetFramework + eventuale Version/IdentityServerVersion).
2. `Directory.Build.targets` (condizione `net10.0`).
3. `global.json` (SDK 10.0.x).
4. `Directory.Packages.props` (tutte le variabili e i pacchetti 8.0.x → 10.0.x).
5. Workflow in `.github/workflows/`.
6. `samples/Directory.Build.props`.
7. Eseguire `dotnet restore` e `dotnet build` dalla root (es. `src/IdentityServer8.sln`) e correggere eventuali errori di compatibilità o breaking change .NET 10.
8. Documentazione e connection string se vuoi allineare anche quelli.

---

## Note

- **.NET 10** è LTS (supporto fino a novembre 2028). Assicurati di avere l’SDK 10.x installato (`dotnet --list-sdks`).
- Dopo il cambio, verifica eventuali **breaking change** in ASP.NET Core 10 e Entity Framework Core 10 (documentazione Microsoft).
- Il workaround in `Directory.Build.targets` per ILLink/netstandard2.1 potrebbe restare necessario; in caso di problemi di trimming, tieni il target `netstandard2.1` nel `KnownILLinkPack` e la condizione su `net10.0`.
