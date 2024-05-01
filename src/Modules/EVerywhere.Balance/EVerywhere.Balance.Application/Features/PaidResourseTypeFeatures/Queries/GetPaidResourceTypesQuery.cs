using AutoMapper;
using EVerywhere.Balance.Application.Repositories;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaidResourseTypeFeatures.Queries;

public record GetPaidResourceTypesQuery : IRequest<List<PaidResourceTypeDto>>;

public class GetPaidResourceTypesQueryHandler(IMapper mapper, 
    IPaidResourceTypeRepository repository) 
    : IRequestHandler<GetPaidResourceTypesQuery, List<PaidResourceTypeDto>>
{
    public async Task<List<PaidResourceTypeDto>> Handle(GetPaidResourceTypesQuery request, CancellationToken cancellationToken)
    {
        var paidResources = await repository.GetListAsync(cancellationToken);

        return mapper.Map<List<PaidResourceTypeDto>>(paidResources);
    }
}