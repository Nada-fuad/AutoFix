using AutoFix.Domain.Common;

namespace AutoFix.Domain.Workorders.Events;

public sealed class WorkOrderCompleted : DomainEvent
{
    public Guid WorkOrderId { get; set; }
}