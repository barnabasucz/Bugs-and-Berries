using System;
using System.Collections.Generic;

namespace Bugs_and_Berries_game.Scripting
{
    // uses Chain of Responsibility design pattern
    public class PlayerInterpreter: Interpreter
    {
        public PlayerInterpreter(IScriptingServer server)
        {
            this.server = server;
            dictionary = new Dictionary<Instructions.OpCodes, Action<int>>();
            dictionary.Add(Instructions.OpCodes.PlaySound, PlaySound);
            dictionary.Add(Instructions.OpCodes.MoveTo, MoveTo);
            dictionary.Add(Instructions.OpCodes.IgnoreInput, IgnoreInput);
            dictionary.Add(Instructions.OpCodes.PickupBerry, PickupBerryAt);
            dictionary.Add(Instructions.OpCodes.PickupSunblock, PickupSunblockAt);
        }

        public void PlaySound(int soundType)
        {
            server.PlaySound(soundType);
        }

        public void MoveTo(int destinationId)
        {
            server.MoveTo(ObjectLogic.Constants.ObjectIds.PlayerId, destinationId);
        }

        public void IgnoreInput(int milliseconds)
        {
            server.IgnoreInput(milliseconds);
        }

        public void PickupBerryAt(int destinationId)
        {
            server.PickupBerryAt(destinationId);
        }

        public void PickupSunblockAt(int destinationId)
        {
            server.PickupSunblockAt(destinationId);
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
