﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using AutoMapper;
using IdentityServer4.Models;

namespace LC.Oauth2.Mappers
{
    /// <summary>
    /// Defines entity/model mapping for identity resources.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class IdentityResourceMapperProfile : Profile
    {
        /// <summary>
        /// <see cref="IdentityResourceMapperProfile"/>
        /// </summary>
        public IdentityResourceMapperProfile()
        {
            CreateMap<Entities.IdentityResourceProperty, KeyValuePair<string, string>>()
                .ReverseMap();

            CreateMap<Entities.IdentityResource, IdentityResource>(MemberList.Destination)
                .ConstructUsing(src => new IdentityResource())
                .ReverseMap();

            CreateMap<Entities.IdentityClaim, string>()
               .ConstructUsing(x => x.Type)
               .ReverseMap()
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src));
        }
    }
}
