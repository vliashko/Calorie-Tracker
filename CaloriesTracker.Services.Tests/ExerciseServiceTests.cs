using AutoMapper;
using CaloriesTracker.Contracts;
using CaloriesTracker.Entities.DataTransferObjects;
using CaloriesTracker.Entities.Models;
using CaloriesTracker.LoggerService;
using CaloriesTracker.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CaloriesTracker.Services.Tests
{
    public class ExerciseServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public ExerciseServiceTests()
        {
            mockRepo = new Mock<IRepositoryManager>();
            profile = new MappingProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            profile = null;
            configuration = null;
            mapper = null;
        }

        [Fact]
        public async void GetAllExercises_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.Exercise.GetAllExercisesPaginationAsync(1, 5, "", false))
                .ReturnsAsync(GetExercises(0));
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExercisesPaginationAsync(1, 5, "");
            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllExercises_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Exercise.GetAllExercisesPaginationAsync(1, 5, "", false))
                .ReturnsAsync(GetExercises(1));
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExercisesPaginationAsync(1, 5, "");
            Assert.Single(result);
        }
        [Fact]
        public async void GetExercise_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(() => null);
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));
            Assert.Null(result);
        }
        [Fact]
        public async void GetExercise_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync
                (
                    new Exercise
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        Name = "Pull-ups",
                        Description = "Performed on the crossbar. Duration 40 seconds",
                        CaloriesSpent = 5,
                    }
                );
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));
            Assert.Equal("Pull-ups", result.Name);
            Assert.IsType<ExerciseForReadDto>(result);
        }
        [Fact]
        public async void CreateExercise_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync
                (
                    new Exercise
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        Name = "Pull-ups",
                        Description = "Performed on the crossbar. Duration 40 seconds",
                        CaloriesSpent = 5,
                    }
                );
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.CreateExerciseAsync(new ExerciseForCreateDto
            {
                Name = "Test",
                Description = "Desc Test",
                CaloriesSpent = 1
            });
            Assert.IsType<ExerciseForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateExercise_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                .ReturnsAsync(() => null);
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.UpdateExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), new ExerciseForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void UpdateExercise_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                 .ReturnsAsync
                 (
                     new Exercise
                     {
                         Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                         Name = "Pull-ups",
                         Description = "Performed on the crossbar. Duration 40 seconds",
                         CaloriesSpent = 5,
                     }
                 );
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.UpdateExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), new ExerciseForUpdateDto
            {
                Name = "Test",
                Description = "Desc Test",
                CaloriesSpent = 1
            });
            Assert.Equal(204, result.StatusCode);
        }
        [Fact]
        public async void PartiallyUpdateExercise_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                .ReturnsAsync(() => null);
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.PartiallyUpdateExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Marvin.JsonPatch.JsonPatchDocument<ExerciseForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void PartiallyUpdateExercise_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                 .ReturnsAsync
                 (
                     new Exercise
                     {
                         Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                         Name = "Pull-ups",
                         Description = "Performed on the crossbar. Duration 40 seconds",
                         CaloriesSpent = 5,
                     }
                 );
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.PartiallyUpdateExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Marvin.JsonPatch.JsonPatchDocument<ExerciseForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }
        [Fact]
        public async void DeleteIngredient_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(() => null);
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.DeleteExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void DeleteIngredient_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                 .ReturnsAsync
                 (
                     new Exercise
                     {
                         Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                         Name = "Pull-ups",
                         Description = "Performed on the crossbar. Duration 40 seconds",
                         CaloriesSpent = 5,
                     }
                 );
            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.DeleteExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));
            Assert.Equal(204, result.StatusCode);
        }
        private IEnumerable<Exercise> GetExercises(int num)
        {
            var exercises = new List<Exercise>();
            if (num > 0)
            {
                exercises.Add(
                    new Exercise
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        Name = "Pull-ups",
                        Description = "Performed on the crossbar. Duration 40 seconds",
                        CaloriesSpent = 5,
                    });
            }
            return exercises;
        }
    }
}
