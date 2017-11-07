using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugs_and_Berries_game.Visual
{
    public interface ITileCoordinateHolder
    {
        Arrangements.TileCoordinate TileCoordinateFor(int locationId);
    }
}
