using EVerywhere.ModulesCommon.Application.Interfaces;
using MediatR;

namespace EVerywhere.ModulesCommon.UseCase;

public abstract class BaseUseCases(IMediator mediator, IUser currentUser) : IBaseUseCases;