using AutoMapper;
using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Queries;

public class CheckoutDto
{
    public required string Url { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PaymentSystemWidget, CheckoutDto>();
        }
    }
}