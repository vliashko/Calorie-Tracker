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
            mockRepo.Setup(x => x.Exercise.GetAllExercisesAsync(false))
                .ReturnsAsync(GetExercises(0));

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExercises();

            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllExercises_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Exercise.GetAllExercisesAsync(false))
                .ReturnsAsync(GetExercises(1));

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExercises();

            Assert.Single(result);
        }
        [Fact]
        public async void GetExercise_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(() => null);

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.GetExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

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
            var result = await service.GetExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

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
            var result = await service.CreateExercise(new ExerciseForCreateDto
            {
                Name = "Test",
                Description = "Desc Test",
                CaloriesSpent = 1
            });

            Assert.IsType<ExerciseForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateExercise_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                .ReturnsAsync(() => null);

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.UpdateExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), new ExerciseForUpdateDto { });

            Assert.False(result);
        }
        [Fact]
        public async void UpdateExercise_ReturnsTrue_WhenValidIDProvided()
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
            var result = await service.UpdateExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), new ExerciseForUpdateDto
            {
                Name = "Test",
                Description = "Desc Test",
                CaloriesSpent = 1
            });

            Assert.True(result);
        }
        [Fact]
        public async void PartiallyUpdateExercise_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), true))
                .ReturnsAsync(() => null);

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.PartiallyUpdateExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Marvin.JsonPatch.JsonPatchDocument<ExerciseForUpdateDto> { });

            Assert.False(result);
        }
        [Fact]
        public async void PartiallyUpdateExercise_ReturnsTrue_WhenValidIDProvided()
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
            var result = await service.PartiallyUpdateExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Marvin.JsonPatch.JsonPatchDocument<ExerciseForUpdateDto> { });

            Assert.True(result);
        }
        [Fact]
        public async void DeleteIngredient_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Exercise.GetExerciseAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(() => null);

            var service = new ExerciseService(mapper, mockRepo.Object, new LoggerManager());
            var result = await service.DeleteExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.False(result);
        }
        [Fact]
        public async void DeleteIngredient_ReturnsTrue_WhenValidIDProvided()
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
            var result = await service.DeleteExercise(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.True(result);
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
