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
    public class FavoritesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Train> _trainService;
        private readonly IService<City> _cityService;
        private readonly IService<FavoriteTrain> _favoriteTrainService;
        private readonly UserManager<User> _userManager;

        public FavoritesController(ILogger<HomeController> logger,
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


        public async Task<IActionResult> Favorites()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var favsId = _favoriteTrainService.ReadAll().Where(i => i.UserId == user.Id).Select(x => x.Id);
            if (!favsId.Any())
            {
                var list1 = new List<TrainInfo>();

                return View(list1);
            }

            var routes = _trainService.ReadAll().Where(x => favsId.Contains(x.Id));
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
    }
}
