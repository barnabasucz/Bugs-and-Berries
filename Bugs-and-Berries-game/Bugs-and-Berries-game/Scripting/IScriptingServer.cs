namespace Bugs_and_Berries_game.Scripting
{
    public interface IScriptingServer
    {
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
