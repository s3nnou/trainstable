using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrainTable.BLL.Services.Interfaces;
using TrainTable.DAL.Models;
using TrainTable.UI.Models;
using TrainTable.UI.Models.Home;
using WebUI.Models;

namespace TrainTable.UI.Controllers
{
    public class CrudController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Train> _trainService;
        private readonly IService<City> _cityService;
        private readonly IService<FavoriteTrain> _favoriteTrainService;
        private readonly UserManager<User> _userManager;

        public CrudController(ILogger<HomeController> logger,
            IService<Train> trainService,
            IService<City> cityService,
            UserManager<User> userManager,
            IService<FavoriteTrain> favoriteTrainService)
        {
            _logger = logger;
            _trainService = trainService;
            _cityService = cityService;
            _userManager = userManager;
            _favoriteTrainService = favoriteTrainService;
        }

        public async Task<IActionResult> Index()
        {
            var routes = _trainService.ReadAll();
            var list = new List<TrainInfo>();
            foreach (var route in routes)
            {
                var cityFrom = await _cityService.ReadById(route.DepartureId);
                var cityTo = await _cityService.ReadById(route.DestinationId);

                list.Add(new TrainInfo
                {
                    Departure = cityFrom.Name,
                    DepertureTime = route.DepartureTime,
                    Destination = cityTo.Name,
                    DestinationTime = route.DestinationTime,
                    Id = route.Id,
                    Name = route.Name,
                    Type = route.TypeId,
                    Time = route.DepartureTime.Subtract(route.DestinationTime),
                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEditRoute(int id)
        {
            var cities = _cityService.ReadAll();

            var list = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();

            foreach (var city in cities)
            {
                list.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = city.Name,
                    Value = city.Id.ToString(),
                });
            }

            if (id == 0)
            {
                var model = new CreateEditRouteModel
                {
                    CitiesFrom = list,
                    CitiesTo = list,

                };

                return View(model);
            }
            else
            {
                var route = await _trainService.ReadById(id);

                var model = new CreateEditRouteModel
                {
                    CitiesFrom = list,
                    CitiesTo = list,
                    Id = route.Id,
                    FromCityId = route.DepartureId,
                    EndTime = route.DestinationTime,
                    Name = route.Name,
                    StartTime = route.DepartureTime,
                    ToCityId = route.DestinationId,
                };

                return View(model);
            }
        }
    }
}
