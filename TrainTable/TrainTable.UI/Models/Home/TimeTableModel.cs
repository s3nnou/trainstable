using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class TimeTableModel
    {
        public List<SelectListItem> FromCities { get; set; }

        public List<SelectListItem> ToCities { get; set; }

    }
}
