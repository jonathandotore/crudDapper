﻿using AutoMapper;
using CrudDapper.Dto;
using CrudDapper.Models;

namespace CrudDapper.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Users, ListUserDto>();
            CreateMap<IEnumerable<Users>, ListUserDto>();
        }
    }
}
