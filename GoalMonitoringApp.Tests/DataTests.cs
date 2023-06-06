using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using NUnit.Framework;

namespace GoalMonitoringApp.Tests
{
    public class DataTests
    {
        [Test]
        public async Task InsertGoalAsync_InsertsDataSuccessfully()
        {
            // Arrange
            var database = new GoalDatabase();
            var repository = new GoalRepository(database);
            var goal = new Goals
            {
                Title = "Test Goal",
                Description = "This is a test goal"
            };

            // Act
            await repository.SaveGoalAsync(goal);

            // Assert
            var goals = await repository.GetAllGoalsAsync();
            Assert.AreEqual(1, goals.Count); // Assuming only one goal is inserted
            var insertedGoal = goals[0];
            Assert.AreEqual("Test Goal", insertedGoal.Title);
            Assert.AreEqual("This is a test goal", insertedGoal.Description);
        }

    }
}
