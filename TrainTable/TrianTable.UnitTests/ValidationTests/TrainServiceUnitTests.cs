using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainTable.DAL;
using TrainTable.DAL.Enums;
using TrainTable.DAL.Models;
using TrainTable.BLL.Services;
using FluentAssertions;

namespace TrianTable.UnitTests.ValidationTests
{
    [TestFixture]
    public class TrainServiceUnitTests
    {
        [Test]
        public async Task Create_WhenTrainIsValid_ShouldReturnAddedTrainAsync()
        {
            // Arrange
            var trainToCreate = new Train
            {
                Name = "Train1",
                Departure = 1,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year + 1, 6, 20),
                DestinationTime = new DateTime(DateTime.Now.Year + 1, 6, 22),
                Type = TrainType.Express,
            };

            Train addedTrain = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());
            mockTrainRepository.Setup(mock => mock.Create(It.IsAny<Train>())).Callback<Train>(area => addedTrain = area);

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            await trainService.Create(trainToCreate);

            // Assert
            addedTrain.Should().NotBeNull();
            addedTrain.Should().BeEquivalentTo(new Train
            {
                Name = "Train1",
                Departure = 1,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year + 1, 6, 20),
                DestinationTime = new DateTime(DateTime.Now.Year + 1, 6, 22),
                Type = TrainType.Express,
            }, options => options.Excluding(area => area.Id));
        }

        [Test]
        public async Task Create_WhenTrainIsNull_ShouldThrowArgumentNullExceptionAsync()
        {
            // Arrange
            Train trainToCreate = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            Func<Task> testFunc = async () => await trainService.Create(trainToCreate);

            // Assert
            await testFunc.Should().ThrowAsync<ArgumentNullException>();
        }

        [Test]
        public async Task Create_WhenTrainNameIsEmpty_ShouldThrowExceptionAsync()
        {
            // Arrange
            var trainToCreate = new Train
            {
                Name = string.Empty,
                Departure = 1,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year + 1, 6, 20),
                DestinationTime = new DateTime(DateTime.Now.Year + 1, 6, 22),
                Type = TrainType.Express,
            };

            Train addedTrain = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            Func<Task> testFunc = async () => await trainService.Create(trainToCreate);

            // Assert
            await testFunc.Should().ThrowAsync<Exception>().WithMessage("Train name cannot be empty.");
        }

        [Test]
        public async Task Create_WhenTrainDepartureIsEmpty_ShouldThrowExceptionAsync()
        {
            // Arrange
            var trainToCreate = new Train
            {
                Name = "Train1",
                Departure = 0,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year + 1, 6, 20),
                DestinationTime = new DateTime(DateTime.Now.Year + 1, 6, 22),
                Type = TrainType.Express,
            };

            Train addedTrain = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            Func<Task> testFunc = async () => await trainService.Create(trainToCreate);

            // Assert
            await testFunc.Should().ThrowAsync<Exception>().WithMessage("Departure cannot be empty.");
        }

        [Test]
        public async Task Create_WhenTrainDepartureTimeGreaterDestinationTime_ShouldThrowExceptionAsync()
        {
            // Arrange
            var trainToCreate = new Train
            {
                Name = "Train1",
                Departure = 1,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year + 1, 6, 24),
                DestinationTime = new DateTime(DateTime.Now.Year + 1, 6, 22),
                Type = TrainType.Express,
            };

            Train addedTrain = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            Func<Task> testFunc = async () => await trainService.Create(trainToCreate);

            // Assert
            await testFunc.Should().ThrowAsync<Exception>().WithMessage("The depature date must not be greater than the destination date.");
        }

        [Test]
        public async Task Create_WhenTrainDepartureOrDestinationTimeLessDateNow_ShouldThrowExceptionAsync()
        {
            // Arrange
            var trainToCreate = new Train
            {
                Name = "Train1",
                Departure = 1,
                Destination = 2,
                DepertureTime = new DateTime(DateTime.Now.Year - 1, 6, 20),
                DestinationTime = new DateTime(DateTime.Now.Year - 1, 6, 22),
                Type = TrainType.Express,
            };

            Train addedTrain = null;

            var mockTrainRepository = new Mock<IRepository<Train>>();
            mockTrainRepository.Setup(mock => mock.ReadAll()).Returns(new List<Train>().AsQueryable());

            var trainService = new TrainService(mockTrainRepository.Object);

            // Act
            Func<Task> testFunc = async () => await trainService.Create(trainToCreate);

            // Assert
            await testFunc.Should().ThrowAsync<Exception>().WithMessage("The depature or destination date must not be less than today's date.");
        }
    }
}
