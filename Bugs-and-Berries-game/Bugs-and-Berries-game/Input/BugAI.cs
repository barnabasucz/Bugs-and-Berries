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
        private int instructionCounter;
        public int InstructionCounter { get { return instructionCounter; } set { instructionCounter = value; } }

        public BugMind(Scripting.IScriptingServer server, int bugId)
        {
            this.bugId = bugId;
            aiType = BugAI.AIType.Bug1Behavior;
            instructionCounter = 0;
            interpreter = new Scripting.BugInterpreter(server, bugId);
        }

        public void Update(int elapsedMs, List<Scripting.Instructions.Instruction> instructions)
        {
            if (InstructionCounter >= instructions.Count)
            {
                InstructionCounter = 0;
            }
            Scripting.Instructions.Instruction instruction = instructions[InstructionCounter];
            InstructionCounter++;
            interpreter.AcceptInstructions(new List<Scripting.Instructions.Instruction> { instruction });
        }
    }

    public class BugAI
    {
        private Scripting.IScriptingServer server;
        private List<BugMind> bugs;
        private List<Scripting.Instructions.Instruction> bugType1Behavior;

        public enum AIType
        {
            Bug1Behavior
        }

        public BugAI(Scripting.IScriptingServer server)
        {
            this.server = server;
            bugs = new List<BugMind>();
            bugType1Behavior = new List<Scripting.Instructions.Instruction>
            {
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TryNorth, 0),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.Idle, 1000),
                new Scripting.Instructions.Instruction(Scripting.Instructions.OpCodes.TrySouth, 0),
           };
        }

        public void AddBug(int bugId)
        {
            bugs.Add(new BugMind(server, bugId));
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
                    bug.Update(msElapsed, bugType1Behavior);
                }
            }
        }
    }
}
