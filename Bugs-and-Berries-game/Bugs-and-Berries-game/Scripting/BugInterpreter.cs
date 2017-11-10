using System;
using System.Collections.Generic;

namespace Bugs_and_Berries_game.Scripting
{
    public class BugInterpreter: Interpreter
    {
        private int bugId;
        public BugInterpreter(IScriptingServer server, int bugId)
        {
            this.bugId = bugId;
            this.server = server;
            dictionary = new Dictionary<Instructions.OpCodes, Action<int>>();
            dictionary.Add(Instructions.OpCodes.MoveTo, MoveTo);
            dictionary.Add(Instructions.OpCodes.Bite, Bite);
            dictionary.Add(Instructions.OpCodes.TryNorth, TryNorth);
            dictionary.Add(Instructions.OpCodes.TrySouth, TrySouth);
            dictionary.Add(Instructions.OpCodes.TryWest, TryWest);
            dictionary.Add(Instructions.OpCodes.TryEast, TryEast);
            dictionary.Add(Instructions.OpCodes.Idle, Idle);
        }

        public void Dispatch(Instructions.OpCodes opCode, int param)
        {
            if (dictionary.ContainsKey(opCode))
            {
                Action<int> action = dictionary[opCode];
                action(param);
            }
        }

        public void MoveTo(int destinationId)
        {
            server.MoveTo(bugId, destinationId);
        }

        public void Bite(int ignored)
        {
            server.BitePlayer();
        }

        public void TryNorth(int locationId)
        {
            //server.TryNorth(locationId);
        }

        public void TrySouth(int locationId)
        {
            //server.TrySouth(locationId);
        }

        public void TryWest(int locationId)
        {
            //server.TryWest(locationId);
        }

        public void TryEast(int locationId)
        {
            //server.TryEast(locationId);
        }

        public void Idle(int milliseconds)
        {
            // might want to make either the BugAI or the BugMind be the server for this object!
            //server.Idle(milliseconds);
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
            return @"Bug object #" + bugId.ToString();
        }
    }
}
