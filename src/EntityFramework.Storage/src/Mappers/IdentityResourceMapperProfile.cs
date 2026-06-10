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
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="Mapster.IRegister" />
    public class IdentityResourceMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the identity resource entity/model mappings.
        /// </summary>
        /// <param name="config">The Mapster configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<KeyValuePair<string, string>, Entities.IdentityResourceProperty>()
                .Map(dest => dest.Key, src => src.Key)
                .Map(dest => dest.Value, src => src.Value);

            config.NewConfig<Entities.IdentityResourceClaim, string>().MapWith(src => src.Type);
            config.NewConfig<string, Entities.IdentityResourceClaim>().MapWith(src => new Entities.IdentityResourceClaim { Type = src });

            config.NewConfig<Entities.IdentityResource, Models.IdentityResource>()
                .IgnoreNullValues(true)
                .Map(dest => dest.Properties,
                    src => src.Properties == null
                        ? new Dictionary<string, string>()
                        : src.Properties.ToDictionary(p => p.Key, p => p.Value));

            config.NewConfig<Models.IdentityResource, Entities.IdentityResource>();
        }
    }
}
