using Bugs_and_Berries_game.Scripting.Instructions;
using System;
using System.Collections.Generic;

namespace Bugs_and_Berries_game.Scripting
{
    public enum ScriptedObjectType
    {
        Player,
        Bug
    }
    public abstract class Interpreter
    {
        protected Dictionary<Instructions.OpCodes, Action<int>> dictionary;

        public void AcceptInstructions(List<Instructions.Instruction> instructions)
        {
            foreach (Instructions.Instruction instruction in instructions)
            {
                if (dictionary.ContainsKey(instruction.OpCode))
                {
                    Action<int> action = dictionary[instruction.OpCode];
                    action.Invoke(instruction.Operand);
                }
            }
        }

    }
}
