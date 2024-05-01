using AutoMapper;
using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Application.Features.PaidResourseTypeFeatures.Queries;

public class PaidResourceTypeDto
{
    public required int Id { get; set; }
    public required string TypeName { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaidResourceType, PaidResourceTypeDto>();
        }
    }
}