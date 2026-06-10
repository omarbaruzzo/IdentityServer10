/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

namespace IdentityServer10.EntityFramework.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for API resources.
    /// </summary>
    /// <seealso cref="Mapster.IRegister" />
    public class ApiResourceMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the API resource entity/model mappings.
        /// </summary>
        /// <param name="config">The Mapster configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<KeyValuePair<string, string>, Entities.ApiResourceProperty>()
                .Map(dest => dest.Key, src => src.Key)
                .Map(dest => dest.Value, src => src.Value);

            config.NewConfig<Entities.ApiResourceClaim, string>().MapWith(src => src.Type);
            config.NewConfig<string, Entities.ApiResourceClaim>().MapWith(src => new Entities.ApiResourceClaim { Type = src });

            config.NewConfig<Entities.ApiResourceScope, string>().MapWith(src => src.Scope);
            config.NewConfig<string, Entities.ApiResourceScope>().MapWith(src => new Entities.ApiResourceScope { Scope = src });

            config.NewConfig<Entities.ApiResourceSecret, Models.Secret>();
            config.NewConfig<Models.Secret, Entities.ApiResourceSecret>();

            config.NewConfig<Entities.ApiResource, Models.ApiResource>()
                .IgnoreNullValues(true)
                .Map(dest => dest.ApiSecrets, src => src.Secrets)
                .Map(dest => dest.AllowedAccessTokenSigningAlgorithms,
                    src => AllowedSigningAlgorithmsConverter.Convert(src.AllowedAccessTokenSigningAlgorithms))
                .Map(dest => dest.Properties,
                    src => src.Properties == null
                        ? new Dictionary<string, string>()
                        : src.Properties.ToDictionary(p => p.Key, p => p.Value));

            config.NewConfig<Models.ApiResource, Entities.ApiResource>()
                .Map(dest => dest.Secrets, src => src.ApiSecrets)
                .Map(dest => dest.AllowedAccessTokenSigningAlgorithms,
                    src => AllowedSigningAlgorithmsConverter.Convert(src.AllowedAccessTokenSigningAlgorithms));
        }
    }
}
