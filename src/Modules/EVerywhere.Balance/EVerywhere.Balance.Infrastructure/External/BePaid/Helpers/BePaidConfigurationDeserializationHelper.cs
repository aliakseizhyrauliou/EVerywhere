using Barion.Balance.Infrastructure.External.BePaid.Configuration;
using EVerywhere.Balance.Domain.Entities;
using Newtonsoft.Json;

namespace EVerywhere.Balance.Infrastructure.External.BePaid.Helpers;

public static class BePaidConfigurationDeserializationHelper
{
    public static BePaidConfiguration? DeserializeToBePaidConfiguration(PaymentSystemConfiguration configuration)
    {
        var deserializedModel = JsonConvert.DeserializeObject<BePaidConfiguration>(configuration.Data);

        if (deserializedModel is null)
        {
            throw new Exception("error_while_deserialize_bePaid_configuration");
        }

        return deserializedModel;
    }
}