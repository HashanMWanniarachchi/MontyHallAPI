using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MontyHallAPI;
using MontyHallAPI.Controllers;
using MontyHallAPI.Services;
using System;
using Xunit;

namespace MontyHallAPITests
{

    //Fixture class for dependency injection for the SimulateGameController
    public class MontyHallGameSolverServiceFixture {

        public ServiceProvider ServiceProvider { get; set; }
        public MontyHallGameSolverServiceFixture() {
            var serviceCollection = new ServiceCollection();
            
            //Set the dependency injection for helper service
            serviceCollection.AddScoped<IMontyHallGameSolverService, MontyHallGameSolverService>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }


    }
    public class SimulateGameControllerTest: IClassFixture<MontyHallGameSolverServiceFixture>
    {
        //Arrange
        private ServiceProvider _serviceProvider;
        private readonly SimulateGameController _controller;
        public SimulateGameControllerTest(MontyHallGameSolverServiceFixture fixture) {
            _serviceProvider = fixture.ServiceProvider;
            _controller = new SimulateGameController(_serviceProvider.GetService<IMontyHallGameSolverService>());
        }

        [Fact]
        public void Get_WhenCalledWithZeroGames_ReturnsError()
        {
            //Act
            Action act = () => _controller.GetResult(0, true);

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Number of games should be greater than 0", exception.Message);
        }

        [Fact]
        public void Get_WhenCalledWithMinusNumberOfGames_ReturnsError()
        {
            //Act
            Action act = () => _controller.GetResult(-1, true);

            //Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Number of games should be greater than 0", exception.Message);
        }

        [Fact]
        public void Get_WhenCalledWithCorrectValues_ReturnsNotNullResult()
        {
            //Act
            IActionResult result = _controller.GetResult(100, true);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Get_WhenCalledWithCorrectValues_ReturnsGameSessionObject()
        {
            //Act
            IActionResult result = _controller.GetResult(100, true);

            OkObjectResult okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);

            SimulatedSession solvedSession = okResult.Value as SimulatedSession;

            Assert.NotNull(solvedSession);
            Assert.Equal(100, solvedSession.NumberOfGames);
            Assert.True(solvedSession.ShouldSwitch);
            Assert.NotEqual(0, solvedSession.Wins);

        }
    }
}
