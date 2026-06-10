/*
 Copyright (c) 2024 OmarBaruzzo, Omar Baruzzo - https://github.com/omarbaruzzo/ 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code and license this software can be found 

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.
*/

using IdentityServer10.Models;

namespace IdentityServer10.EntityFramework.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for clients.
    /// </summary>
    /// <seealso cref="Mapster.IRegister" />
    public class ClientMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the client entity/model mappings.
        /// </summary>
        /// <param name="config">The Mapster configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<KeyValuePair<string, string>, Entities.ClientProperty>()
                .Map(dest => dest.Key, src => src.Key)
                .Map(dest => dest.Value, src => src.Value);

            config.NewConfig<Entities.ClientCorsOrigin, string>().MapWith(src => src.Origin);
            config.NewConfig<string, Entities.ClientCorsOrigin>().MapWith(src => new Entities.ClientCorsOrigin { Origin = src });

            config.NewConfig<Entities.ClientIdPRestriction, string>().MapWith(src => src.Provider);
            config.NewConfig<string, Entities.ClientIdPRestriction>().MapWith(src => new Entities.ClientIdPRestriction { Provider = src });

            config.NewConfig<Entities.ClientScope, string>().MapWith(src => src.Scope);
            config.NewConfig<string, Entities.ClientScope>().MapWith(src => new Entities.ClientScope { Scope = src });

            config.NewConfig<Entities.ClientPostLogoutRedirectUri, string>().MapWith(src => src.PostLogoutRedirectUri);
            config.NewConfig<string, Entities.ClientPostLogoutRedirectUri>().MapWith(src => new Entities.ClientPostLogoutRedirectUri { PostLogoutRedirectUri = src });

            config.NewConfig<Entities.ClientRedirectUri, string>().MapWith(src => src.RedirectUri);
            config.NewConfig<string, Entities.ClientRedirectUri>().MapWith(src => new Entities.ClientRedirectUri { RedirectUri = src });

            config.NewConfig<Entities.ClientGrantType, string>().MapWith(src => src.GrantType);
            config.NewConfig<string, Entities.ClientGrantType>().MapWith(src => new Entities.ClientGrantType { GrantType = src });

            config.NewConfig<Entities.ClientClaim, ClientClaim>()
                .MapWith(src => new ClientClaim(src.Type, src.Value, ClaimValueTypes.String));
            config.NewConfig<ClientClaim, Entities.ClientClaim>()
                .Map(dest => dest.Type, src => src.Type)
                .Map(dest => dest.Value, src => src.Value);

            config.NewConfig<Entities.ClientSecret, Models.Secret>();
            config.NewConfig<Models.Secret, Entities.ClientSecret>();

            config.NewConfig<Entities.Client, Models.Client>()
                .IgnoreNullValues(true)
                .Map(dest => dest.AllowedIdentityTokenSigningAlgorithms,
                    src => AllowedSigningAlgorithmsConverter.Convert(src.AllowedIdentityTokenSigningAlgorithms))
                .Map(dest => dest.Properties,
                    src => src.Properties == null
                        ? new Dictionary<string, string>()
                        : src.Properties.ToDictionary(p => p.Key, p => p.Value));

            config.NewConfig<Models.Client, Entities.Client>()
                .Map(dest => dest.AllowedIdentityTokenSigningAlgorithms,
                    src => AllowedSigningAlgorithmsConverter.Convert(src.AllowedIdentityTokenSigningAlgorithms));
        }
    }
}
