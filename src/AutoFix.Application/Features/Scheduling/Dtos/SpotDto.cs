using AutoFix.Domain.Workorders.Enums;

namespace AutoFix.Application.Features.Scheduling.Dtos;

public class SpotDto
{
    public Spot Spot { get; set; }
    public List<AvailabilitySlotDto> Slots { get; set; } = [];
}