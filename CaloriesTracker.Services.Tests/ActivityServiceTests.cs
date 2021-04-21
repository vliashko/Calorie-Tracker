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
    public class ActivityServiceTests : IDisposable
    {
        Mock<IRepositoryManager> mockRepo;
        MappingProfile profile;
        IMapper mapper;
        MapperConfiguration configuration;

        public ActivityServiceTests()
        {
            mockRepo = new Mock<IRepositoryManager>();
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        Weight = 84f,
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 174,
                    }
                );
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
            mockRepo.Setup(x => x.Activity.GetAllActivitiesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetActivitiesForUser(0));

            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetActivities(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllActivitiesForUser_ReturnsOneItem_WhenDBHasOneResource()
        {
            mockRepo.Setup(x => x.Activity.GetAllActivitiesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), false))
                .ReturnsAsync(GetActivitiesForUser(1));

            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetActivities(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"));

            Assert.Single(result);
        }
        [Fact]
        public async void GetAllActivitiesForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetAllActivitiesForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), false))
                .ReturnsAsync(() => null);

            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetActivities(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetActivityForUser_ReturnsNull_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);

            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.Null(result);
        }
        [Fact]
        public async void GetActivityForUser_ReturnsCorrectType_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Start = DateTime.Now,
                        Finish = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.GetActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));

            Assert.IsType<ActivityForReadDto>(result);
        }
        [Fact]
        public async void CreateActivityForUser_ReturnsCorrectTypeAndObject_WhenValidObjectSubmitted()
        {
            mockRepo.Setup(x => x.User.GetUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"), true))
                .ReturnsAsync
                (
                    new UserProfile
                    {
                        Id = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                        Weight = 84f,
                        DateOfBirth = DateTime.Now,
                        Gender = 0,
                        Height = 174,
                    }
                );

            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Start = DateTime.Now,
                        Finish = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );

            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.CreateActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new ActivityForCreateDto
                {
                    Name = "Test",
                    Start = DateTime.Now,
                    Finish = DateTime.Now,
                    ExercisesWithReps = new List<ActivityExerciseForCreateDto>()
                });
            Assert.IsType<ActivityForReadDto>(result);
            Assert.Equal("Test", result.Name);
        }
        [Fact]
        public async void UpdateActivityForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new ActivityForUpdateDto { });
            Assert.False(result);
        }
        [Fact]
        public async void UpdateActivityForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync
                (
                    new Activity
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "Activity",
                        Start = DateTime.Now,
                        Finish = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        ExercisesWithReps = new List<ActivityExercise>()
                    }
                );
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.UpdateActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new ActivityForUpdateDto { });
            Assert.True(result);
        }
        [Fact]
        public async void PartiallyUpdateActivityForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<ActivityForUpdateDto> { });
            Assert.False(result);
        }
        [Fact]
        public async void PartiallyUpdateActivityForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
               new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), true))
               .ReturnsAsync
               (
                   new Activity
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Activity",
                       Start = DateTime.Now,
                       Finish = DateTime.Now,
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       ExercisesWithReps = new List<ActivityExercise>()
                   }
               );
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.PartiallyUpdateActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Marvin.JsonPatch.JsonPatchDocument<ActivityForUpdateDto> { });
            Assert.True(result);
        }
        [Fact]
        public async void DeleteActivityForUser_ReturnsFalse_WhenNonExistentIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
                .ReturnsAsync(() => null);
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37b"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.False(result);
        }
        [Fact]
        public async void DeleteAcitivityForUser_ReturnsTrue_WhenValidIDProvided()
        {
            mockRepo.Setup(x => x.Activity.GetActivityForUserAsync(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
               new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), false))
               .ReturnsAsync
               (
                   new Activity
                   {
                       Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                       Name = "Activity",
                       Start = DateTime.Now,
                       Finish = DateTime.Now,
                       UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                       ExercisesWithReps = new List<ActivityExercise>()
                   }
               );
            var service = new ActivityService(mockRepo.Object, new LoggerManager(), mapper);
            var result = await service.DeleteActivity(new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
            Assert.True(result);
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
                        Name = "Recipe",
                        Start = DateTime.Now,
                        Finish = DateTime.Now,
                        UserProfileId = new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"),
                        ExercisesWithReps = new List<ActivityExercise>()
                    });
            }
            return activities;
        }
    }
}
