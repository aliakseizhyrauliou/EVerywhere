using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Events.Holds;

public class CaptureHoldEvent(Hold hold) : BaseEvent
{
    public Hold Hold { get; set; } = hold;
}