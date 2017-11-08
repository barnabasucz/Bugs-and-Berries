using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.Input
{
    public class UserInput
    {
        private int elapsedIgnoreMilliseconds;
        private bool ignoringInput;
        private int maxIgnoreLength;

        public UserInput()
        {
            elapsedIgnoreMilliseconds = 0;
            ignoringInput = false;
        }

        public void ReceiveNorth()
        {

        }

        public void ReceiveSouth()
        {

        }

        public void ReceiveWest()
        {

        }

        public void ReceiveEast()
        {

        }

        public void ReceiveAction()
        {

        }

        public void ReceivePause()
        {

        }

        public void Update(int elapsedMilliseconds)
        {
            if(ignoringInput)
            {
                elapsedIgnoreMilliseconds += elapsedMilliseconds;
                if (elapsedMilliseconds > maxIgnoreLength)
                {
                    ignoringInput = false;
                    elapsedIgnoreMilliseconds = -1;
                    maxIgnoreLength = -1;
                }
            }
        }

        public void IgnoreInput(int milliseconds)
        {
            elapsedIgnoreMilliseconds = 0;
            maxIgnoreLength = milliseconds;
            ignoringInput = true;
        }
    }
}
