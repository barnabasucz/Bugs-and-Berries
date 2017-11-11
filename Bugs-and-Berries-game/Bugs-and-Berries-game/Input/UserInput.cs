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
        private Scripting.PlayerInterpreter interpreter;
        private World.NavMeshes.NavMesh navMesh;

        public UserInput(Scripting.IMover mover, Scripting.ISoundPlayer soundPlayer,
            Scripting.IItemPicker itemPicker, World.NavMeshes.NavMesh navMesh)
        {
            elapsedIgnoreMilliseconds = 0;
            ignoringInput = false;
            interpreter = new Scripting.PlayerInterpreter(mover, soundPlayer, itemPicker);
            this.navMesh = navMesh;
        }

         public void ReceiveNorth(int locationId)
        {
            if (!ignoringInput)
            {
                foreach(var instruction in navMesh.NorthConsequences(locationId))
                {
                    interpreter.Dispatch(instruction.OpCode, instruction.Operand);
                }
            }
        }

        public void ReceiveSouth(int locationId)
        {
            if (!ignoringInput)
            {
                foreach(var instruction in navMesh.SouthConsequences(locationId))
                {
                    interpreter.Dispatch(instruction.OpCode, instruction.Operand);
                }
            }
        }

        public void ReceiveWest(int locationId)
        {
            if (!ignoringInput)
            {
                foreach(var instruction in navMesh.WestConsequences(locationId))
                {
                    interpreter.Dispatch(instruction.OpCode, instruction.Operand);
                }
            }
        }

        public void ReceiveEast(int locationId)
        {
            if (!ignoringInput)
            {
                foreach(var instruction in navMesh.EastConsequences(locationId))
                {
                    interpreter.Dispatch(instruction.OpCode, instruction.Operand);
                }
            }
        }

        public void ReceiveAction(int locationId)
        {
            if (!ignoringInput)
            {
                interpreter.Dispatch(Scripting.Instructions.OpCodes.PickupBerry, 0);
            }
        }

        public void ReceivePause()
        {
            // Don't ignore pause.
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
