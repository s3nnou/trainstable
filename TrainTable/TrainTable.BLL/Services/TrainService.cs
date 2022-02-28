using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrainTable.BLL.Services.Interfaces;
using TrainTable.DAL;
using TrainTable.DAL.Models;

namespace TrainTable.BLL.Services
{
    internal class TrainService : IService<Train>
    {
        private readonly IRepository<Train> _repository;

        public TrainService(IRepository<Train> repository)
        {
            _repository = repository;
        }

        public async Task<Train> Create(Train entity)
        {
            ValidateTrain(entity);

            Train createdTrain = await _repository.Create(entity);
            return createdTrain;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public IReadOnlyCollection<Train> ReadAll()
        {
            var trains = _repository.ReadAll().ToList();
            return trains;
        }

        public IReadOnlyCollection<Train> ReadAll(Expression<Func<Train, bool>> predicate)
        {
            var trains = _repository.ReadAll().Where(predicate).ToList();
            return trains;
        }

        public async Task<Train> ReadById(int id)
        {
            var train = await _repository.ReadById(id);
            return train;
        }

        public async Task<Train> Update(Train entity)
        {
            ValidateTrain(entity);

            var train = await _repository.Update(entity);
            return train;
        }

        private void ValidateTrain(Train train)
        {
            if (train == null)
            {
                throw new ArgumentNullException("train", "Value cannot be null.")
            }

            if (string.IsNullOrEmpty(train.Name))
            {
                throw new Exception("Train name cannot be empty.");
            }

            if (train.Departure == default)
            {
                throw new Exception("Train name cannot be empty.");
            }

            if (train.Destination == default)
            {
                throw new Exception("Train name cannot be empty.");
            }

            if (IsStartDateGreaterEndDate(train))
            {
                throw new Exception("The start date of the event must not be greater than the end date of the event.");
            }

            if (IsStartOrEndDateLessDateNow(train))
            {
                throw new Exception("The start or end date of the event must not be less than today's date.");
            }
        }

        private bool IsStartDateGreaterEndDate(Train train)
        {
            var isStartDateGreaterEndDate = train.DepertureTime > train.DestinationTime;
            return isStartDateGreaterEndDate;
        }

        private bool IsStartOrEndDateLessDateNow(Train train)
        {
            var isStartOrEndDateLessDateNow = (train.DepertureTime < DateTimeOffset.Now) || (train.DestinationTime < DateTimeOffset.Now);
            return isStartOrEndDateLessDateNow;
        }
    }
}
