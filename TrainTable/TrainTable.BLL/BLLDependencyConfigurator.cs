using Microsoft.Extensions.DependencyInjection;
using TrainTable.BLL.Services;
using TrainTable.BLL.Services.Interfaces;
using TrainTable.DAL;
using TrainTable.DAL.Models;

namespace TrainTable.BLL
{
    public static class BLLDependencyConfigurator
    {
        public static void BLLDependencyConfig(this IServiceCollection services, string connectionString)
        {
            services.DALDependencyConfig(connectionString);

            services.AddScoped<IService<Train>, TrainService>();
            services.AddScoped<IService<City>, CityService>();
            services.AddScoped<IService<FavoriteTrain>, FavoriteTrainService>();
        }
    }
}
