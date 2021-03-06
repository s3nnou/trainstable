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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IService<Train> _trainService;
        private readonly IService<City> _cityService;
        private readonly IService<FavoriteTrain> _favoriteTrainService;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
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

        public IActionResult Index()
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
            };

            return View(new TimeTableModel
            {
                FromCities = list,
                ToCities = list,
            });
        }

        public async Task<IActionResult> Table(int city1, int city2)
        {
            var routes = _trainService.ReadAll();
            var list = new List<TrainInfo>();
            foreach (var route in routes)
            {
                var cityFrom = await _cityService.ReadById(city1);
                var cityTo = await _cityService.ReadById(city2);

                list.Add(new TrainInfo
                {
                    Departure = cityFrom.Name,
                    DepertureTime = route.DepartureTime,
                    Destination = cityTo.Name,
                    DestinationTime = route.DestinationTime,
                    Id = route.Id,
                    Name = route.Name,
                    Type = route.TypeId,
                    Time = route.DepartureTime.Subtract(route.DestinationTime) ,
                });
            }

            return PartialView(list);
        }

        public async Task<IActionResult> Add(int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var ids = _favoriteTrainService.ReadAll().Count() + 1;

            await _favoriteTrainService.Create(new FavoriteTrain { Id = ids, TrainId = id, UserId = user.Id });

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
