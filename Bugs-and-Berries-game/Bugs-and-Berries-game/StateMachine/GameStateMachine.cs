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
        private int maxDyingTime;
        private int maxReadyingTime;
        private List<IStateTransitionObserver> observers;

        public GameStateMachine()
        {
            stateTimeMsElapsed = 0;
            maxDyingTime = 0;
            maxReadyingTime = 0;
            gameStateUpdateMappings = new Dictionary<GameStateCodes, UpdateFunc>
            {
                { GameStateCodes.StartingUp, StartingUpUpdate },
                { GameStateCodes.PlayerReady, PlayerReadyUpdate },
                { GameStateCodes.Playing, PlayingUpdate },
                { GameStateCodes.PlayerDying, PlayerDyingUpdate },
                { GameStateCodes.Paused, PausedUpdate },
                { GameStateCodes.GameOver, GameOverUpdate }
            };
            observers = new List<IStateTransitionObserver>();
            Dispatch(GameStateCodes.StartingUp);
       }

       public void Subscribe(IStateTransitionObserver observer)
        {
            this.observers.Add(observer);
        }

        public void Unsubscribe(IStateTransitionObserver observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }

        public GameStateCodes StateCode { get { return gameStateCode; } }

        public void Update(int msElapsed)
        {
            currentUpdateFunc(msElapsed);
        }

        public void ResetGame()
        {
            Dispatch(GameStateCodes.PlayerReady);
        }

        public void Die(int chancesLeft, int maxDyingTime)
        {
            if(chancesLeft > 0)
            {
                Dispatch(GameStateCodes.PlayerDying);
                this.maxDyingTime = maxDyingTime;
            }
            else
            {
                Dispatch(GameStateCodes.GameOver);
            }
        }

        public void ReadyPlayer(int maxReadyingTime)
        {
            this.maxReadyingTime = maxReadyingTime;
            Dispatch(GameStateCodes.PlayerReady);
        }


        private void Dispatch(GameStateCodes newStateCode)
        {
            if (gameStateUpdateMappings.ContainsKey(newStateCode))
            {
                gameStateCode = newStateCode;
                currentUpdateFunc = gameStateUpdateMappings[newStateCode];
                stateTimeMsElapsed = 0;
                foreach(var observer in observers)
                {
                    observer.Transition(newStateCode);
                }
            }
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

        private void PlayerReadyUpdate(int msElapsed)
        {
            // In this state, the player is at the starting position, is blinking, and the input is ignored until
            // the state is over.
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= maxReadyingTime)
            {
                Dispatch(GameStateCodes.Playing);
            }
        }

        private void PlayingUpdate(int msElapsed)
        {
        }

        private void PlayerDyingUpdate(int msElapsed)
        {
            stateTimeMsElapsed += msElapsed;
            if (stateTimeMsElapsed >= maxDyingTime)
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
