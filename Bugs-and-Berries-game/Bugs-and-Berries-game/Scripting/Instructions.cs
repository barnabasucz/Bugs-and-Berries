namespace Bugs_and_Berries_game.Scripting.Instructions
{
    public class Instruction
    {
        private OpCodes opCode;
        private int operand;
        public Instruction (OpCodes opCode, int operand)
        {
            this.opCode = opCode;
            this.operand = operand;
        }
        public OpCodes OpCode { get { return opCode; } }
        public int Operand { get { return operand; } }
    }

    // need to think about how to expose these to the script editor.
    public enum OpCodes
    {
        Undefined,
        PlaySound,
        MoveTo,
        IgnoreInput,
        Bite,
        PickupBerry,
        PickupSunblock,
        TryNorth,
        TrySouth,
        TryWest,
        TryEast,
        Idle
    }
}
