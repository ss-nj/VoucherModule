using Application.DTOs.MasterDtos;
using Application.DTOs.PersonDtos;
using Application.DTOs.SubsidiaryDtos;
using AutoMapper;
using Domain.Entities;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api.Configurations
{
    public class MapperConfig : Profile
    {
        MapperConfig()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Person, GetPersonDto>().ReverseMap();
            CreateMap<Person, UpdatePersonDto>().ReverseMap();
            CreateMap<Person, CreatePersonDto>().ReverseMap();

            CreateMap<Master, MasterDto>().ReverseMap();
            CreateMap<Master, GetMasterDto>().ReverseMap();
            CreateMap<Master, UpdateMasterDto>().ReverseMap();
            CreateMap<Master, CreateMasterDto>().ReverseMap();

            CreateMap<Subsidiary, SubsidiaryDto>().ReverseMap();
            CreateMap<Subsidiary, GetSubsidiaryDto>().ReverseMap();
            CreateMap<Subsidiary, UpdateSubsidiaryDto>().ReverseMap();
            CreateMap<Subsidiary, CreateSubsidiaryDto>().ReverseMap();
        }


    }
}
