using System.Collections.Generic;

namespace Bugs_and_Berries_game.Visual.Arrangements
{
    public class TileCoordinate
    {
        private int row;
        public int Row { get { return row; } }
        private int column;
        public int Column { get { return column; } }
        public TileCoordinate (int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    public class TileArrangements : ITileCoordinateHolder
    {
        private List<TileCoordinate> arrangement;
        public TileArrangements()
        {
            InitializeDefault();
        }

        private void AddMapping(int locationId, int row, int column)
        {
            arrangement.Add(new TileCoordinate(row, column));
        }

        public TileCoordinate TileCoordinateFor(int locationId)
        {
            if (locationId >= 0 && locationId < arrangement.Count)
            {
                return arrangement[locationId];
            }
            return new TileCoordinate(-1, -1);
        }

        public void InitializeDefault()
        {
            // for now, this is the only way to set up the level.  Some day, the editor will allow different levels
            // to be created, saved, and loaded.
            if (arrangement != null)
            {
                arrangement = null;
            }
            arrangement = new List<TileCoordinate>();
            AddMapping(0, 6, 0);
            AddMapping(1, 6, 1);
            AddMapping(2, 6, 2);
            AddMapping(3, 5, 2);
            AddMapping(4, 4, 2);
            AddMapping(5, 3, 2);
            AddMapping(6, 2, 2);
            AddMapping(7, 1, 2);
            AddMapping(8, 0, 2);
            AddMapping(9, 6, 3);
            AddMapping(10, 5, 3);
            AddMapping(11, 4, 3);
            AddMapping(12, 3, 3);
            AddMapping(13, 2, 3);
            AddMapping(14, 1, 3);
            AddMapping(15, 0, 3);
            AddMapping(16, 6, 4);
            AddMapping(17, 5, 4);
            AddMapping(18, 4, 4);
            AddMapping(19, 3, 4);
            AddMapping(20, 2, 4);
            AddMapping(21, 1, 4);
            AddMapping(22, 0, 4);
            AddMapping(23, 6, 5);
            AddMapping(24, 5, 5);
            AddMapping(25, 4, 5);
            AddMapping(26, 3, 5);
            AddMapping(27, 2, 5);
            AddMapping(28, 1, 5);
            AddMapping(29, 0, 5);
        }
    }
}
