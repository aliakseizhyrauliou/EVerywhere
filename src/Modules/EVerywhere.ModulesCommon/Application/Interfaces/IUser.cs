namespace EVerywhere.ModulesCommon.Application.Interfaces;

public interface IUser
{
    string? Id { get; }
    string? FirstName { get;}
    string? LastName { get; }
    long? OperatorId { get; }
    long? AggregatorId { get; }
}