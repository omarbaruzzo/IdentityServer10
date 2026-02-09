/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

global using IdentityModel;
global using IdentityServer10;
global using IdentityServer10.Configuration;
global using IdentityServer10.Events;
global using IdentityServer10.Extensions;
global using IdentityServer10.Models;
global using IdentityServer10.Services;
global using IdentityServer10.Stores;
global using IdentityServer10.Test;
global using IdentityServer10.Validation;
global using IdentityServerHost.Configuration;
global using IdentityServerHost.Data;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Newtonsoft.Json;
global using Serilog;
global using Serilog.Events;
global using Serilog.Sinks.SystemConsole.Themes;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Security.Claims;
global using System.Text;
global using static IdentityServer10.IdentityServerConstants;
global using ILogger = Microsoft.Extensions.Logging.ILogger;
global using ClaimValueTypes = System.Security.Claims.ClaimValueTypes;
