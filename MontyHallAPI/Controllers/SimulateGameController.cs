using Microsoft.AspNetCore.Mvc;
using MontyHallAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Controllers
{
    [Route("api")]
    public class SimulateGameController : Controller
    {
        //Helper service for simulating Monty Hall games
        private IMontyHallGameSolverService _simulationService;

        public SimulateGameController(IMontyHallGameSolverService simulationService) {
            _simulationService = simulationService;  
        }

        [HttpGet]
        [Route("simulation")]
        public IActionResult GetResult([FromQuery] int numberOfGames, [FromQuery]bool shouldSwitch)
        {
            //Check for parameters and throw errors if parameters are invalid or missing
            if ( String.IsNullOrEmpty(numberOfGames.ToString()) || String.IsNullOrEmpty(shouldSwitch.ToString())) {
                throw new ArgumentException("Number of games and switching option cannot be null");
            }

            if (numberOfGames <= 0) {
                throw new ArgumentException("Number of games should be greater than 0");
            }

            //Get simulated result from the service
            SimulatedSession session = _simulationService.SimulateGame(numberOfGames, shouldSwitch);

            return Ok(session);
        }
    }
}
