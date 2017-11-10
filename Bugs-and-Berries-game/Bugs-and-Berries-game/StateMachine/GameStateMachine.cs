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
        private delegate void TransitionFunc();
        private UpdateFunc currentUpdateFunc;
        private Dictionary<GameStateCodes, UpdateFunc> gameStateUpdateMappings;
        private Dictionary<GameStateCodes, TransitionFunc> transitionFunctionMappings;
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
            transitionFunctionMappings = new Dictionary<GameStateCodes, TransitionFunc>
            {
                { GameStateCodes.StartingUp, StartingUpTransition },
                { GameStateCodes.PlayerReady, PlayerReadyTransition },
                { GameStateCodes.Playing, PlayingTransition },
                { GameStateCodes.PlayerDying, PlayerDyingTransition },
                { GameStateCodes.Paused, PausedTransition },
                { GameStateCodes.GameOver, GameOverTransition }
            };
            Dispatch(GameStateCodes.StartingUp);
       }

        public GameStateCodes StateCode { get { return gameStateCode; } }

        public void Update(int msElapsed)
        {
            currentUpdateFunc(msElapsed);
        }

        public void Die(int chancesLeft)
        {
            if(chancesLeft > 0)
            {
                Dispatch(GameStateCodes.PlayerDying);
            }
            else
            {
                Dispatch(GameStateCodes.GameOver);
            }
        }

        private void Dispatch(GameStateCodes newStateCode)
        {
            if (transitionFunctionMappings.ContainsKey(newStateCode))
            {
                TransitionFunc transitionFunc = transitionFunctionMappings[newStateCode];
                transitionFunc();
            }
            if (gameStateUpdateMappings.ContainsKey(newStateCode))
            {
                gameStateCode = newStateCode;
                currentUpdateFunc = gameStateUpdateMappings[newStateCode];
                stateTimeMsElapsed = 0;
            }
        }

        private void StartingUpTransition()
        {

        }
        private void StartingUpUpdate(int msElapsed)
        {
            const int MaxStartingUpTime = 1000;
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= MaxStartingUpTime)
            {
                Dispatch(GameStateCodes.PlayerReady);
            }
        }

        private void PlayerReadyTransition()
        {

        }

        private void PlayerReadyUpdate(int msElapsed)
        {
            // In this state, the player is at the starting position, is blinking, and the input is ignored until
            // the state is over.
            const int MaxReadingTime = 500;
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= MaxReadingTime)
            {
                Dispatch(GameStateCodes.Playing);
            }
        }

        private void PlayingTransition()
        {

        }

        private void PlayingUpdate(int msElapsed)
        {
            //stateTimeMsElapsed += msElapsed;
        }

        private void PlayerDyingTransition()
        {

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

        private void PausedTransition()
        {

        }

        private void PausedUpdate(int msElapsed)
        {
            // wait indefinately for user input; don't risk overrunning stateTimeMsElapsed
        }

        private void GameOverTransition()
        {

        }

        private void GameOverUpdate(int msElapsed)
        {
            // wait indefinately for user input; don't risk overrunning stateTimeMsElapsed
        }

    }
}
