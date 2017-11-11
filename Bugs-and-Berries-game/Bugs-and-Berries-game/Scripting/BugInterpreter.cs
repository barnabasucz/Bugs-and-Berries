using System;
using System.Collections.Generic;

namespace Bugs_and_Berries_game.Scripting
{
    public class BugInterpreter: Interpreter
    {
        private int bugId;
        private Scripting.IMover mover;
        private Scripting.ILocator locator;
        private World.NavMeshes.NavMesh navMesh;
        private int totalIdlingTime;
        private int maxIdlingTime;
        private bool idling;

        public BugInterpreter(Scripting.IMover mover, Scripting.ILocator locator, World.NavMeshes.NavMesh navMesh, int bugId)
        {
            this.bugId = bugId;
            this.mover = mover;
            this.locator = locator;
            this.navMesh = navMesh;
            totalIdlingTime = 0;
            maxIdlingTime = 0;
            idling = false;
            dictionary = new Dictionary<Instructions.OpCodes, Action<int>>();
            dictionary.Add(Instructions.OpCodes.MoveTo, MoveTo);
            dictionary.Add(Instructions.OpCodes.Bite, Bite);
            dictionary.Add(Instructions.OpCodes.TryNorth, TryNorth);
            dictionary.Add(Instructions.OpCodes.TrySouth, TrySouth);
            dictionary.Add(Instructions.OpCodes.TryWest, TryWest);
            dictionary.Add(Instructions.OpCodes.TryEast, TryEast);
            dictionary.Add(Instructions.OpCodes.Idle, Idle);
        }

        public void MoveTo(int destinationId)
        {
            mover.MoveTo(bugId, destinationId);
        }

        public void Bite(int ignored)
        {
        }

        public void TryNorth(int ignore)
        {
            int locationId = locator.LocationFor(bugId);
            foreach(var consequence in navMesh.NorthConsequences(locationId))
            {
                if (consequence.OpCode == Instructions.OpCodes.MoveTo)
                {
                    mover.MoveTo(bugId, consequence.Operand);
                }
            }
        }

        public void TrySouth(int ignore)
        {
            int locationId = locator.LocationFor(bugId);
            foreach (var consequence in navMesh.SouthConsequences(locationId))
            {
                if (consequence.OpCode == Instructions.OpCodes.MoveTo)
                {
                    mover.MoveTo(bugId, consequence.Operand);
                }
            }
        }

        public void TryWest(int locationId)
        {
        }

        public void TryEast(int locationId)
        {
        }

        public void Idle(int milliseconds)
        {
            totalIdlingTime = 0;
            maxIdlingTime = milliseconds;
            idling = true;
        }

        public bool IsIdling()
        {
            return idling;
        }

        public void UpdateIdlingTime(int elapsedMs)
        {
            if (idling)
            {
                totalIdlingTime += elapsedMs;
                if (totalIdlingTime >= maxIdlingTime)
                {
                    idling = false;
                    totalIdlingTime = 0;
                    maxIdlingTime = 0;
                }
            }
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
