using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainTable.DAL.Models;

namespace TrainTable.DAL
{
    internal class TrainTableDBContext : IdentityDbContext<IdentityUser>
    {
        public TrainTableDBContext()
        {
        }

        public TrainTableDBContext(DbContextOptions<TrainTableDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Train> Trains { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<FavoriteTrain> FavoriteTrains { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Train>().ToTable("Train");
            builder.Entity<City>().ToTable("City");
            builder.Entity<FavoriteTrain>().ToTable("FavoriteTrains");
        }
    }
}
