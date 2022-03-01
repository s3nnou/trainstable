using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainTable.DAL.Models;
using TrainTable.DAL.Repositories;

namespace TrainTable.DAL
{
    public static class DalDependencyConfigurator
    {
        public static void DALDependencyConfig(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<DbContext, TrainTableDBContext>();
            services.AddDbContext<TrainTableDBContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TrainTableDBContext>();
        }
    }
}
