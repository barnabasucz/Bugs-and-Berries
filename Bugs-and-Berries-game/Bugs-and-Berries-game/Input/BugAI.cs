using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bugs_and_Berries_game.Input
{
    public class BugMind
    {
        private int bugId;
        public int BugId { get { return bugId; } }
        Scripting.BugInterpreter interpreter;
        public Scripting.BugInterpreter Interpreter { get { return interpreter; } }
        private BugAI.AIType aiType;
        public BugAI.AIType AIType { get { return aiType; } set { aiType = value; } }
        private List<Scripting.Instructions.Instruction> behavior;
        private int instructionCounter;
        public int InstructionCounter { get { return instructionCounter; } set { instructionCounter = value; } }

        public BugMind(Scripting.IMover mover, Scripting.ILocator locator, 
            World.NavMeshes.NavMesh navMesh, int bugId, List<Scripting.Instructions.Instruction> behavior)
        {
            this.bugId = bugId;
            this.behavior = behavior;
            aiType = BugAI.AIType.Bug1Behavior;
            instructionCounter = 0;
            interpreter = new Scripting.BugInterpreter(mover, locator, navMesh, bugId);
        }



        public void Update(int elapsedMs)
        {
            interpreter.UpdateIdlingTime(elapsedMs);
            if (!interpreter.IsIdling())
            {
                if (InstructionCounter >= behavior.Count)
                {
                    InstructionCounter = 0;
                }
                Scripting.Instructions.Instruction instruction = behavior[InstructionCounter];
                InstructionCounter++;
                interpreter.AcceptInstructions(new List<Scripting.Instructions.Instruction> { instruction });
            }
        }
    }

    public class BugAI
    {
        private Scripting.IMover mover;
        private World.NavMeshes.NavMesh navMesh;
        private Scripting.ILocator locator;
        private List<BugMind> bugs;
        private List<Scripting.Instructions.Instruction> bugType1Behavior;
        private List<Scripting.Instructions.Instruction> bugType2Behavior;
        private List<Scripting.Instructions.Instruction> bugType3Behavior;

        public enum AIType
        {
            Bug1Behavior
        }

        public BugAI(Scripting.IMover mover, World.NavMeshes.NavMesh navMesh, Scripting.ILocator locator)
        {
            this.mover = mover;
            this.navMesh = navMesh;
            this.locator = locator;
            bugs = new List<BugMind>();
            bugType1Behavior = new List<Scripting.Instructions.Instruction>
            {
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
           };
            bugType2Behavior = new List<Scripting.Instructions.Instruction>
            {
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryEast, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryWest, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
         };
            bugType3Behavior = new List<Scripting.Instructions.Instruction>
            {
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 800),
          };
        }
        private List<Scripting.Instructions.Instruction> RandomBehavior(int seed)
        {
            if (seed % 3 == 0)
            {
                return bugType1Behavior;
            }
            else if (seed % 3 == 1)
            {
                return bugType2Behavior;
            }
            else
            {
                return bugType3Behavior;
            }

        }

        public void AddBug(int bugId)
        {
            bugs.Add(new BugMind(mover, locator, navMesh, bugId, RandomBehavior(bugId)));
        }

        public void RemoveBug(int bugId)
        {
            for(int i = 0; i < bugs.Count; i++)
            {
                BugMind bug = bugs[i];
                if (bug.BugId == bugId)
                {
                    bugs.Remove(bug);
                    break;
                }
            }
        }

        public void Update(int msElapsed)
        {
            foreach(var bug in bugs)
            {
                if (bugType1Behavior.Count > 0)
                {
                    bug.Update(msElapsed);
                }
            }
        }
    }
}
