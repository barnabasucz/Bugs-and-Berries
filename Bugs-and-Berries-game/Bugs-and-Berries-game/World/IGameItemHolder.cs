using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.World
{
    public interface IGameItemHolder
    {
        bool IsPlayerAt(int locationId);
        bool IsBerryAt(int locationId);
        bool IsSunBlockAt(int locationId);
        bool IsBugAt(int locationId);
        void MoveBug(int fromLocationId, int toLocationId);
        void MovePlayer(int fromLocationId, int toLocationId);
        void RemoveBerry(int locationId);
        void RemoveSunblock();
    }
}
