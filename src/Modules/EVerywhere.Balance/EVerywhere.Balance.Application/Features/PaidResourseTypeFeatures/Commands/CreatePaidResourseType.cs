using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using FluentValidation;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaidResourseTypeFeatures.Commands;

public record CreatePaidResourceTypeCommand : IRequest<long>
{
    public required string TypeName { get; set; }
}

public class CreatePaidResourceTypeCommandValidator : AbstractValidator<CreatePaidResourceTypeCommand>
{
    public CreatePaidResourceTypeCommandValidator()
    {
        RuleFor(x => x.TypeName)
            .NotNull();
    }
}

public class CreatePaidResourceTypeCommandHandler(IPaidResourceTypeRepository repository) 
    : IRequestHandler<CreatePaidResourceTypeCommand, long>
{
    public async Task<long> Handle(CreatePaidResourceTypeCommand request, CancellationToken cancellationToken)
    {
        var newPaidResourceType = new PaidResourceType
        {
            TypeName = request.TypeName
        };

        await repository.InsertAsync(newPaidResourceType, cancellationToken);

        return newPaidResourceType.Id;
    }
}