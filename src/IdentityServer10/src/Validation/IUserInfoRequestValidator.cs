/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer10.Validation;

/// <summary>
/// Validator for userinfo requests
/// </summary>
public interface IUserInfoRequestValidator
{
    /// <summary>
    /// Validates a userinfo request.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns></returns>
    Task<UserInfoRequestValidationResult> ValidateRequestAsync(string accessToken);
}
