using System;
using System.Collections.Generic;

namespace Bugs_and_Berries_game.Scripting
{
    // uses Chain of Responsibility design pattern
    public class PlayerInterpreter: Interpreter
    {
        private Scripting.IMover mover;
        private Scripting.ISoundPlayer soundPlayer;
        private Scripting.IItemPicker itemPicker;
        public PlayerInterpreter(Scripting.IMover mover, 
            Scripting.ISoundPlayer soundPlayer, Scripting.IItemPicker itemPicker)
        {
            this.mover = mover;
            this.soundPlayer = soundPlayer;
            this.itemPicker = itemPicker;
            dictionary = new Dictionary<Instructions.OpCodes, Action<int>>();
            dictionary.Add(Instructions.OpCodes.PlaySound, PlaySound);
            dictionary.Add(Instructions.OpCodes.MoveTo, MoveTo);
            dictionary.Add(Instructions.OpCodes.IgnoreInput, IgnoreInput);
            dictionary.Add(Instructions.OpCodes.PickupBerry, PickupBerryAt);
            dictionary.Add(Instructions.OpCodes.PickupSunblock, PickupSunblockAt);
        }

        public void Dispatch(Instructions.OpCodes opCode, int param)
        {
            if (dictionary.ContainsKey(opCode))
            {
                Action<int> action = dictionary[opCode];
                action(param);
            }
        }

        public void PlaySound(int soundType)
        {
            soundPlayer.PlaySound(soundType);
        }

        public void MoveTo(int destinationId)
        {
            mover.MoveTo(ObjectLogic.Constants.ObjectIds.PlayerId, destinationId);
        }

        public void IgnoreInput(int milliseconds)
        {
        }

        public void PickupBerryAt(int destinationId)
        {
            itemPicker.PickupBerryAt(destinationId);
        }

        public void PickupSunblockAt(int destinationId)
        {
            itemPicker.PickupSunblockAt(destinationId);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return @"Player object";
        }
    }
}
