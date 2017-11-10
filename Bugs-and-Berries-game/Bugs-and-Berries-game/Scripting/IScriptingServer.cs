namespace Bugs_and_Berries_game.Scripting
{
    public interface IScriptingServer
    {
        // consider changing this to a single-function interface.
        // each implementer in the chain will receive (and possibly forward) an instruction.
        // Each object in the chain will also keep a dictionary of which opcodes it handles, of course
        // mapped to the relevant local function.  If the object doesn't have a mapping in the dictionary, it of course
        // forwards the instruction to its successor.  Recommend a name such as scriptServerSuccessor to go along with the dictionary.
        void PlaySound(int soundType);
        void MoveTo(int objectId, int destinationId);
        void IgnoreInput(int milliseconds);
        void BitePlayer();
        void PickupBerryAt(int destinationId);
        void PickupSunblockAt(int destinationId);
        void TryNorth(int locationId);
        void TrySouth(int locationId);
        void TryWest(int locationId);
        void TryEast(int locationId);
        void Idle(int milliseconds);
    }
}
