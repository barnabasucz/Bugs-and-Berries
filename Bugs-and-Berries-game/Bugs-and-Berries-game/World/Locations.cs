using System.Collections.Generic;

namespace Bugs_and_Berries_game.World.Locations
{
    public class Location
    {
        public Location(bool isPlayer, bool isBerry, bool isSunblock, bool isBug)
        {
            this.isPlayer = isPlayer;
            this.isBerry = isBerry;
            this.isSunblock = isSunblock;
            this.isBug = isBug;

        }
        private bool isPlayer;
        public bool IsPlayer { get { return isPlayer; } set { isPlayer = value; } }
        private bool isBerry;
        public bool IsBerry { get { return isBerry; } set { isBerry = value; } }
        private bool isSunblock;
        public bool IsSunblock { get { return isSunblock; } set { isSunblock = value; } }
        private bool isBug;
        public bool IsBug { get { return isBug; } set { isBug = value; } }
    }

    public class LocationContainer: IGameItemHolder
    {
        private List<Location> locations;
        private int playerLocationId;
        private Dictionary<int, int> bugLocations;
        public LocationContainer()
        {
            locations = new List<Location>(Globals.LocationCount);
            for (int i = 0; i < Globals.LocationCount; i++)
            {
                locations.Add(new Location(false, false, false, false));
            }
            playerLocationId = 0;
            bugLocations = new Dictionary<int, int>();
            ResetGame();
        }

        public int PlayerLocationId { get { return playerLocationId; } }

        public enum GameObjectTypes
        {
            Undefined = 0,
            Player = 1,
            Berry = 2,
            Sunblock = 4,
            Bug = 8
        }

        public void Feed(int[] configuration)
        {
            // load in 30 locations from a file, memory array, stream, etc.
            // Can store each location in a byte, or in a word, integer, etc.
            // Using int for now, even though it might waste space.  Will make adding new object 
            // types easy in the future.
            int bugsAdded = 0;
            if(configuration.Length == Globals.LocationCount)
            {
                for(int i = 0; i < Globals.LocationCount; i++)
                {
                    int locationConfig = configuration[i];
                    bool isPlayer = (0 != (locationConfig & (int)GameObjectTypes.Player));
                    bool isBerry = (0 != (locationConfig & (int)GameObjectTypes.Berry));
                    bool isSunblock = (0 != (locationConfig & (int)GameObjectTypes.Sunblock));
                    bool isBug = (0 != (locationConfig & (int)GameObjectTypes.Bug));
                    Location loc = new Location(isPlayer, isBerry, isSunblock, isBug);
                    if (isBug)
                    {
                        bugsAdded++;
                        bugLocations.Add(bugsAdded, i);
                    }
                    locations[i] = loc;
                }
            }
        }

        public void ResetGame()
        {
            // set the wave back to #0, then feed in the first (#0) wave.
            ResetPlayer();
        }

        public void ResetPlayer()
        {
            locations[playerLocationId].IsPlayer = false;
            locations[0].IsPlayer = true;
        }

        private Location location(int locationId)
        {
            if (locationId >=0 && locationId < locations.Count)
            {
                return locations[locationId];
            }
            return null;
        }

        public bool IsPlayerAt(int locationId)
        {
            Location loc = location(locationId);
            return (loc != null ? loc.IsPlayer : false);
        }

        public bool IsBerryAt(int locationId)
        {
            Location loc = location(locationId);
            return (loc != null ? loc.IsBerry : false);
        }

        public bool IsSunBlockAt(int locationId)
        {
            Location loc = location(locationId);
            return (loc != null ? loc.IsSunblock : false);
        }

        public bool IsBugAt(int locationId)
        {
            Location loc = location(locationId);
            return (loc != null ? loc.IsBug : false);
        }

        public void MoveBug(int bugId, int toLocationId)
        {
            if (bugLocations.ContainsKey(bugId))
            {
                if (toLocationId >= 0 && toLocationId < locations.Count)
                {
                    Location from = locations[bugLocations[bugId]];
                    Location to = locations[toLocationId];
                    from.IsBug = false;
                    to.IsBug = true;
                    bugLocations[bugId] = toLocationId;
                }
            }
       }

        public void MovePlayer(int toLocationId)
        {
            if (toLocationId >= 0 && toLocationId < locations.Count)
            {
                Location from = locations[playerLocationId];
                Location to = locations[toLocationId];
                from.IsPlayer = false;
                to.IsPlayer = true;
                playerLocationId = toLocationId;
            }
        }

        public void RemoveBerry(int locationId)
        {
            if (locationId >= 0 && locationId < locations.Count)
            {
                Location loc = locations[locationId];
                loc.IsBerry = false;
            }
        }

        public void RemoveSunblock()
        {
            Location loc = locations[0];
            loc.IsSunblock = false;
        }
    }
}
