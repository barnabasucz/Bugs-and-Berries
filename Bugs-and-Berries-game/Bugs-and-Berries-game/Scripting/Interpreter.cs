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
        protected IScriptingServer server;

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

        public override bool Equals(object obj)
        {
            var interpreter = obj as Interpreter;
            return interpreter != null &&
                   EqualityComparer<Dictionary<OpCodes, Action<int>>>.Default.Equals(dictionary, interpreter.dictionary) &&
                   EqualityComparer<IScriptingServer>.Default.Equals(server, interpreter.server);
        }

        public override int GetHashCode()
        {
            var hashCode = -10539953;
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<OpCodes, Action<int>>>.Default.GetHashCode(dictionary);
            hashCode = hashCode * -1521134295 + EqualityComparer<IScriptingServer>.Default.GetHashCode(server);
            return hashCode;
        }
    }
}
