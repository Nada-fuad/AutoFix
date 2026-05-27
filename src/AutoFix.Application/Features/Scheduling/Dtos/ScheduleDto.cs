using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Application.Features.Scheduling.Dtos
{
    public class ScheduleDto
    {
        public DateOnly OnDate { get; set; }
        public bool EndOfDay { get; set; }
        public List<SpotDto> Spots { get; set; } = [];
    }
}
