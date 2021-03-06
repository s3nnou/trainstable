using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrainTable.DAL.Models;

namespace TrainTable.DAL
{
    internal class TrainTableDBContext : IdentityDbContext<User>
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

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Train>().ToTable("Train");
            builder.Entity<City>().ToTable("City");
            builder.Entity<FavoriteTrain>().ToTable("FavoriteTrain");
            builder.Entity<User>().ToTable("AspNetUsers");
        }
    }
}
