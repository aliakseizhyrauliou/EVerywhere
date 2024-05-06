using EVerywhere.ModulesCommon.Application.Interfaces;

namespace EVerywhere.Web.Services;

public class CurrentUserMock : IUser
{
    public string? Id => "mock_id";
    public string? FirstName => "Alexey";
    public string? LastName => "Zhurauliou";
    public long? OperatorId => 1;
    public long? AggregatorId => 1;
}