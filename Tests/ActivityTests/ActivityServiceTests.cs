using ActivityMicroService.Contracts;
using ActivityMicroService.DataTransferObjects;
using ActivityMicroService.Models;
using ActivityMicroService.Services;
using AutoMapper;
using IngredientMicroService.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ActivityTests
{
    public class ActivityServiceTests : IDisposable
    {
        private readonly Guid userId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e");

        Mock<IActivityRepository> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public ActivityServiceTests()
        {
            mockRepo = new Mock<IActivityRepository>();
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
        public async void GetAllActivitiesForUser_ReturnsZeroItems_WhenDBEmpty()
        {
            mockRepo.Setup(x => x.GetAllActivitiesForUserForDateAsync(userId, DateTime.Now.Date, false))
                .ReturnsAsync(GetActivitiesForUser(0));
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.GetActivitiesForUserProfileForDateAsync(userId);
            Assert.Empty(result);
        }
        [Fact]
        public async void GetActivityForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Null(result);
        }
        [Fact]
        public async void GetActivityForUser_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.IsType<ActivityForReadDto>(result);
        }
        [Fact]
        public async void CreateActivityForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.CreateActivityForUserProfileAsync(userId,
                new ActivityForCreateDto
                {
                    Name = "Test",
                    Moment = DateTime.Now,
                    ExercisesWithReps = new List<ActivityExerciseForCreateDto>()
                });
            Assert.IsType<ActivityForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateActivityForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.UpdateActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new ActivityForUpdateDto { });
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void UpdateActivityForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.UpdateActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new ActivityForUpdateDto { });
            Assert.Equal(204, result.StatusCode);
        }
        [Fact]
        public async void PartiallyUpdateActivityForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<ActivityForUpdateDto> { });
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void PartiallyUpdateActivityForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync
               (
                   new Activity
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Activity",
                       Moment = DateTime.Now,
                       UserProfileId = userId,
                       ExercisesWithReps = new List<ActivityExercise>()
                   }
               );
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.PartiallyUpdateActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<ActivityForUpdateDto> { });
            Assert.Equal(204, result.StatusCode);
        }
        [Fact]
        public async void DeleteActivityForUser_Returns404_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.DeleteActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public async void DeleteAcitivityForUser_Returns204_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.GetActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Activity
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Activity",
                       Moment = DateTime.Now,
                       UserProfileId = userId,
                       ExercisesWithReps = new List<ActivityExercise>()
                   }
               );
            var service = new ActivityService(mockRepo.Object, mapper);
            var result = await service.DeleteActivityAsync(new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.Equal(204, result.StatusCode);
        }
        private IEnumerable<Activity> GetActivitiesForUser(int num)
        {
            var activities = new List<Activity>();
            if (num > 0)
            {
                activities.Add(
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Moment = DateTime.Now,
                        UserProfileId = userId,
                        ExercisesWithReps = new List<ActivityExercise>()
                    });
            }
            return activities;
        }
    }
}
