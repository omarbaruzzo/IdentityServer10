/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer8.Validation;

namespace IdentityServer.IntegrationTests.Clients.Setup;

public class TestCustomTokenRequestValidator : ICustomTokenRequestValidator
{
    public Task ValidateAsync(CustomTokenRequestValidationContext context)
    {
        context.Result.CustomResponse = new Dictionary<string, object>
        {
            {"custom", "custom" }
        };

        return Task.CompletedTask;
    }
}
