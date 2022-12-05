using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MontyHallAPI.Services
{
    public interface IMontyHallGameSolverService {
        SimulatedSession SimulateGame(int numberOfGames, bool shouldSwitch);
    }

    public class MontyHallGameSolverService : IMontyHallGameSolverService
    {
        public MontyHallGameSolverService() { 
        
        }

        public SimulatedSession SimulateGame(int numberOfGames, bool shouldSwitch)
        {
            Random randomGenerator = new Random();//random number generator for door selection
            int wins = 0; //winning count

            for (int game = 0; game < numberOfGames; game++)
            {

                int[] doors = { 0, 0, 0 }; //1 is a car, 0 is a goat

                int winningDoor = randomGenerator.Next(3); //select a random door number to put the car 

                doors[winningDoor] = 1;

                int contestantChoice = randomGenerator.Next(3);//select first door choice

                int hostChoice;

                do
                {
                    hostChoice = randomGenerator.Next(3); //host chooses the door

                } while (doors[hostChoice] == 1 || hostChoice == contestantChoice); // host should not open the door with the car, and should not choose the door selected by the contestant

                if (shouldSwitch) //switch the door selection
                {
                    int remainingDoor = 3 - contestantChoice - hostChoice; //arrays start with 0. 0+1+2 = 3; Hence, if we substract the indexes of the selected two door, we should be able to get index of the remaining door
                    wins += doors[remainingDoor];//switch the selection to the other door and open
                }
                else
                { //no switching
                    wins += doors[contestantChoice]; //open the door contestant had selected in the first place
                }
            }

            //create SimulatedSession object from the data
            SimulatedSession session = new SimulatedSession(numberOfGames, shouldSwitch, wins);

            return session;

        }
    }
}
