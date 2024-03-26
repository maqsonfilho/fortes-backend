using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Domain.Models;

namespace Fortes.Web.Challenge.Application.MappingProfiles;

public class SupplierMappingProfile : Profile
{
    public SupplierMappingProfile()
    {
        CreateMap<Supplier, SupplierDto>()
            .ReverseMap();
    }
}


