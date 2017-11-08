using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.StateMachine
{
    public enum GameStateCodes
    {
        StartingUp,
        PlayerReady,
        Playing,
        PlayerDying,
        Paused,
        GameOver
    }

    public class GameStateMachine
    {
        private int stateTimeMsElapsed;
        private delegate void UpdateFunc(int msElapsed);
        private UpdateFunc currentUpdateFunc;
        private Dictionary<GameStateCodes, UpdateFunc> gameStateUpdateMappings;
        private GameStateCodes gameStateCode;

        public GameStateMachine()
        {
            stateTimeMsElapsed = 0;
            gameStateUpdateMappings = new Dictionary<GameStateCodes, UpdateFunc>
            {
                { GameStateCodes.StartingUp, StartingUpUpdate },
                { GameStateCodes.PlayerReady, PlayerReadyUpdate },
                { GameStateCodes.Playing, PlayingUpdate },
                { GameStateCodes.PlayerDying, PlayerDyingUpdate },
                { GameStateCodes.Paused, PausedUpdate },
                { GameStateCodes.GameOver, GameOverUpdate }
            };
            Dispatch(GameStateCodes.StartingUp);
       }

        public GameStateCodes StateCode { get { return gameStateCode; } }

        public void Update(int msElapsed)
        {
            currentUpdateFunc(msElapsed);
        }

        private void Dispatch(GameStateCodes newStateCode)
        {
            if (gameStateUpdateMappings.ContainsKey(newStateCode))
            {
                gameStateCode = newStateCode;
                currentUpdateFunc = gameStateUpdateMappings[newStateCode];
                stateTimeMsElapsed = 0;
            }
        }

        private void StartingUpUpdate(int msElapsed)
        {
            const int MaxStartingUpTime = 1500;
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= MaxStartingUpTime)
            {
                Dispatch(GameStateCodes.PlayerReady);
            }
        }

        private void PlayerReadyUpdate(int msElapsed)
        {
            //stateTimeMsElapsed += msElapsed;
        }

        private void PlayingUpdate(int msElapsed)
        {
            //stateTimeMsElapsed += msElapsed;
        }

        private void PlayerDyingUpdate(int msElapsed)
        {
            const int MaxDyingTime = 3000;
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= MaxDyingTime)
            {
                Dispatch(GameStateCodes.PlayerReady);
            }
        }

        private void PausedUpdate(int msElapsed)
        {
            // wait indefinately for user input; don't risk overrunning stateTimeMsElapsed
        }

        private void GameOverUpdate(int msElapsed)
        {
            // wait indefinately for user input; don't risk overrunning stateTimeMsElapsed
        }

    }
}
