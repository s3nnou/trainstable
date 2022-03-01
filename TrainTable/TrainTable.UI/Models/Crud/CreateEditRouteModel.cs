using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class CreateEditRouteModel
    {
        public List<SelectListItem> CitiesFrom { get; set; }
        public List<SelectListItem> CitiesTo { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? FromCityId { get; set; }
        public int? ToCityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
