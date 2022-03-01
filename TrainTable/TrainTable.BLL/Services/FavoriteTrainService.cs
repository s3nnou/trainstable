using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrainTable.BLL.Services.Interfaces;
using TrainTable.DAL;
using TrainTable.DAL.Models;

namespace TrainTable.BLL.Services
{
    internal class FavoriteTrainService : IService<FavoriteTrain>
    {
        private readonly IRepository<FavoriteTrain> _repository;

        public FavoriteTrainService(IRepository<FavoriteTrain> repository)
        {
            _repository = repository;
        }

        public async Task<FavoriteTrain> Create(FavoriteTrain entity)
        {
            ValidateFavoriteTrain(entity);

            var createdFavoriteTrain = await _repository.Create(entity);
            return createdFavoriteTrain;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public IReadOnlyCollection<FavoriteTrain> ReadAll()
        {
            var favoriteTrains = _repository.ReadAll().ToList();
            return favoriteTrains;
        }

        public IReadOnlyCollection<FavoriteTrain> ReadAll(Expression<Func<FavoriteTrain, bool>> predicate)
        {
            var favoriteTrains = _repository.ReadAll().Where(predicate).ToList();
            return favoriteTrains;
        }

        public async Task<FavoriteTrain> ReadById(int id)
        {
            var favoriteTrain = await _repository.ReadById(id);
            return favoriteTrain;
        }

        public async Task<FavoriteTrain> Update(FavoriteTrain entity)
        {
            ValidateFavoriteTrain(entity);

            var favoriteTrain = await _repository.Update(entity);
            return favoriteTrain;
        }

        private void ValidateFavoriteTrain(FavoriteTrain favoriteTrain)
        {
            if (favoriteTrain == null)
            {
                throw new ArgumentNullException(nameof(favoriteTrain));
            }
        }
    }
}
