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
    /// Defines entity/model mapping for persisted grants.
    /// </summary>
    /// <seealso cref="Mapster.IRegister" />
    public class PersistedGrantMapperProfile : IRegister
    {
        /// <summary>
        /// Registers the persisted grant entity/model mappings.
        /// </summary>
        /// <param name="config">The Mapster configuration.</param>
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.PersistedGrant, Models.PersistedGrant>();
            config.NewConfig<Models.PersistedGrant, Entities.PersistedGrant>();
        }
    }
}
