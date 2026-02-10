# Change Log
All notable changes to this project will be documented in this file.
 
The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning 2](http://semver.org/).
 
## [10.0.0] - 2026-02-10

### Summary

First stable release of **IdentityServer10**, an independently maintained fork of the IdentityServer8 codebase, updated to .NET 10 and rebranded as a separate project.

### Changed

- Migrated all core projects to **.NET 10** (`net10.0`) and set product/version metadata to `10.0.0`.
- Updated central package versions in `Directory.Packages.props` to .NET 10–compatible versions for ASP.NET Core, Entity Framework Core, Swashbuckle, test SDKs and related infrastructure.
- Updated documentation and migration guidance (`MIGRATION_NET10.md`, root `README.md`, `src/README.md`) to clearly distinguish:
  - the historical IdentityServer8 line (8.x),
  - from the new IdentityServer10 line (10.0.0).
- Cleaned up repository links and issue trackers so that:
  - all **current** issues/packages refer to `https://github.com/omarbaruzzo/IdentityServer10`,
  - all references to IdentityServer8 correctly point to the original upstream project `alexhiggins732/IdentityServer8`.

### Security

- Updated vulnerable dependencies to patched versions:
  - `Google.Protobuf` from `3.23.4` → `3.25.5` (addresses CVE-2024-7254, DoS via nested groups).
  - `HtmlAgilityPack` from `1.11.40` → `1.12.4` (removes vulnerable transitive dependencies such as `System.Text.RegularExpressions 4.3.0`).
  - `jQuery.validation` from `1.19.5` → `1.21.0` (fixes XSS issues present in versions < 1.20.0).
- Verified that other critical security-related libraries (JWT, IdentityModel, SqlClient, Azure.Identity, Swashbuckle) are on versions without known high/critical vulnerabilities for the 10.0.0 baseline.

### Tests and behavior

- Fixed and hardened test suites to align with actual runtime behavior:
  - Updated `UrlSanitizer` tests to match the current implementation, which uses full-string URL encoding.
  - Ensured persisted grant integration tests clear the database state before seeding, preventing test cross‑contamination.
  - Made the CORS policy service origin check case‑insensitive on both sides, aligning with RFC expectations and existing in‑memory implementations.

---

## [Unreleased] - 2024-02-17

- Current templates and quickstarts being added to seperate template and quickstart repositories to continue previous version functionality.
- DotNet tool to install template currently under development.
- 
## [8.0.4] - 2024-02-17

Identity Server 8.0.4 is a security release that addresses hundreds of security vulnerabilities in the IdentityServer8 code base. We recommend that you update to this version.

- Fix over 100+ security vulnerabilities in the IdentityServer8 code base:
 - #17 Unsafe expansion of self-closing HTML tag
 - #18 URL redirection from remote source
 - #19 DOM text reinterpreted as HTML
 - #20 Incomplete string escaping or encoding
 - #21 Inefficient regular expression bug dependencies
 - #22 Bad HTML filtering regexp bug dependencies
 - #23 User-controlled bypass of sensitive method bug
 - #24 Unsafe jQuery plugins bug dependencies

Additional the codebase has been refactored to use the latest DotNet 8 features and best practices. 

This includes refactroing in #25 and consolidation of reused code that remove some nearly 1 million lines of code from the base.:
- Convert Top Level usings
- Convert Implicit usings.
- Samples use shared API and MVC projects to reduce code duplication and need to maintain dozens of copies of the same code.

## [8.0.3] - 2024-02-12

- Security Updates: Addtional priority critical security patches addressing issues outline in #9 and #10.
 - [Security: User-controlled bypass of sensitive method] - Login Controller and view have have explicit methods to handle login and cancel to address User-controlled bypass of sensitive method
 - [Security: Logging of user-controlled data] - Unsanitized user input could be used to forge logs and inject arbitrary commands, including server side includes, xss and sql injection into log files.
- [Maitenance]: Removed over half a million lines of code from the orginal Identity Server 4 code base using packages and libaries.
 - This will allow for easier maintenance and updates to the code base.
 - Developrs can now focus on the core functionality of Identity Server 8 and use LibMan to manage client side packages and keep packages up to date.
- Documentation Website: identityserver8.readthedocs.io has been created and is now the official documentation website for IdentityServer8
- Gitter: A Gitter chat room has been created for IdentityServer8. You can join the chat at https://app.gitter.im/#/room/#identityserver8:gitter.im
- Framework Upgrade: Upgrade Samples, including Clients, Quickstarts, and Key Management, to use DotNet 8 sdk style.
- [Quickstarts] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/Quickstarts) - Updated Quickstart samples to use Dotnet 8 startup with implicit usings and minimal Api.
- [Clients] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/Clients) - Updated client samples to use Dotnet 8 startup with implicit usings and minimal Api.
- [Key Management] (https://github.com/alexhiggins732/IdentityServer8/tree/master/samples/KeyManagement) - Updated Key management samples to use Dotnet 8 startup with implicit usings and minimal Api. Changed default Entity Framework storage to file system storage as original Key Management is a paid solution. Roadmap: Add DbContext implementation fof key management.
- Client Side Packages: Client Side packages have now been ignored in source and are now installed using LibMan during the build process. This will allow for easier updates and management of client side packages.

## [8.0.2] - 2024-02-12

- Security Updates: Addtional priority critical security patches addressing issues outline in #9 and #10.

## [8.0.1] - 2024-02-10
 
- Security Update: High priority critical security patches addressing issues outline in #9 and #10.

 
### Added
- `IdentityServer8.Security` nuget packages with services to sanitize user input including html, json, xml, javascript, scripts, urls, logs, css, and style sheets.

### Changed
- [Account Login Controller] (https://github.com/alexhiggins732/IdentityServer8/issues/9) 
- [Account Login View] (https://github.com/alexhiggins732/IdentityServer8/issues/9)  
 
### Fixed
- [Security: User-controlled bypass of sensitive method]
  Login Controller and view have have explicit methods to handle login and cancel to address User-controlled bypass of sensitive method
- [Security: Logging of user-controlled data]
  Unsanitized user input could be used to forge logs and inject arbitrary commands, including server side includes, xss and sql injection into log files.
  
## [8.0.1] - 2024-02-10
  
Updated build scripts to use Git Flow branching for SemVer2 compatible nuget packages.
 
### Added

- CodeQl Security scanning
- Dependabot Package scanning. 
### Changed
  
- [IdentityServer8 8.0.1 changes](https://github.com/alexhiggins732/IdentityServer8/pull/7) 

### Fixed
 
- Nuget Package version conflicts.
 
## [8.0.0] - 2024-02-09
 
### Added
Build scripts and readme documentation for initial port from Identity Server 4 and Identity Server 4 Admin   
### Changed
Upgraded Main Identity Server projects and Nuget packages to DotNet 8 
### Fixed
 
- Changed mixed dependencies on `System.Text.Json` and `Newtonsoft.Json` to use `System.Text.Json` which resolved several bugs.
- Change package dependencies and version requirements to run on the latest DotNet 8 packages, resolving many security vulnerablities.