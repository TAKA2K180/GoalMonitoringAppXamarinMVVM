using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoalMonitoringApp.Core.Models;
using GoalMonitoringApp.Data.Data;
using GoalMonitoringApp.Data.Services;
using Xunit;

namespace GoalMonitoringApp.Tests
{
    public class GoalRepositoryTests
    {
        [Fact]
        public async Task GetAllGoalsAsync_ReturnsAllGoals()
        {
            // Arrange
            string databasePath = ":memory:"; // Use an in-memory database for testing
            var goalDatabase = new GoalDatabase(databasePath);
            var goalRepository = new GoalRepository(goalDatabase);

            // Add test data
            var goal1 = new Goals { Id = new Guid(), Title = "Goal 1" };
            var goal2 = new Goals { Id = new Guid(), Title = "Goal 2" };
            await goalRepository.SaveGoalAsync(goal1);
            await goalRepository.SaveGoalAsync(goal2);

            // Act
            var goals = await goalRepository.GetAllGoalsAsync();

            // Assert
            Assert.Equal(2, goals.Count);
            Assert.Contains(goals, g => g.Title == "Goal 1");
            Assert.Contains(goals, g => g.Title == "Goal 2");
        }
    }
}
