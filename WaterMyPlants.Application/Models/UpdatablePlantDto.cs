using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterMyPlants.Application.Models
{
    public class UpdatablePlantDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Localization { get; set; }
        public string? Description { get; set; }
        public int WaterIntervalDays { get; set; }
    }
}
