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
    internal class CityService : IService<City>
    {
        private readonly IRepository<City> _repository;

        public CityService(IRepository<City> repository)
        {
            _repository = repository;
        }

        public async Task<City> Create(City entity)
        {
            ValidateCity(entity);

            var createdCity = await _repository.Create(entity);
            return createdCity;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public IReadOnlyCollection<City> ReadAll()
        {
            var cities = _repository.ReadAll().ToList();
            return cities;
        }

        public IReadOnlyCollection<City> ReadAll(Expression<Func<City, bool>> predicate)
        {
            var cities = _repository.ReadAll().Where(predicate).ToList();
            return cities;
        }

        public async Task<City> ReadById(int id)
        {
            var city = await _repository.ReadById(id);
            return city;
        }

        public async Task<City> Update(City entity)
        {
            ValidateCity(entity);

            var city = await _repository.Update(entity);
            return city;
        }

        private void ValidateCity(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            if (string.IsNullOrEmpty(city.Name))
            {
                throw new Exception("City name cannot be empty.");
            }
        }
    }
}
