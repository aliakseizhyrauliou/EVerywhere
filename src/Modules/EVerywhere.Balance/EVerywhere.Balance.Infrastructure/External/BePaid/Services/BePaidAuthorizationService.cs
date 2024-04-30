using System.Net.Http.Headers;
using EVerywhere.Balance.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace EVerywhere.Balance.Infrastructure.External.BePaid.Services;

public class BePaidAuthorizationService(IConfiguration configuration) 
    : IPaymentSystemAuthorizationService
{
    public HttpRequestMessage Authorize(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", configuration["BEPAID_TOKEN"]);

        return requestMessage;
    }

    public bool ValidateReceivedWebHookRequest(string headerValue)
    {
        return headerValue == configuration["BEPAID_TOKEN"];
    }
}