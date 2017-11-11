using System.Collections.Generic;
using System.Linq;

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

    // temporary class until I have the wave editor done and working
    public class WaveBuilder
    {
        private WaveBuilder() { }
        public static List<int[]> DefaultWaves()
        {
            List<int[]> configurations = new List<int[]>();
            for(int i = 0; i < 10; i++)
            {
                int[] nextWave =
                {
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Berry,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Bug,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Bug,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Berry,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Berry,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Berry,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.Bug,
                    (int)LocationContainer.GameObjectTypes.None,
                    (int)LocationContainer.GameObjectTypes.None
                };
                configurations.Add(nextWave);
            }
            return configurations;
        }
    }

    public class LocationContainer: IGameItemHolder, Scripting.ILocator
    {
        private Scripting.IMover mover;
        private World.NavMeshes.NavMesh navMesh;
        private List<int[]> waveConfigurations;
        int currentWave;
        private List<Location> locations;
        private int playerLocationId;
        private Dictionary<int, int> bugLocations;
        private Input.BugAI bugAI;
        const int maxMsTimePerWave = 10000; // TODO: associate ms time with each wave, set via the editor, to enhance difficulty
        private int msSinceLastWave;

        public LocationContainer(Scripting.IMover mover, World.NavMeshes.NavMesh navMesh)
        {
            this.mover = mover;
            this.navMesh = navMesh;
            bugAI = new Input.BugAI(mover, navMesh, this);
            locations = new List<Location>(Globals.LocationCount);
            for (int i = 0; i < Globals.LocationCount; i++)
            {
                locations.Add(new Location(false, false, false, false));
            }
            playerLocationId = 0;
            bugLocations = new Dictionary<int, int>();
            waveConfigurations = WaveBuilder.DefaultWaves();
            ResetGame();
        }

        public void Update(int msElapsed)
        {
            msSinceLastWave += msElapsed;
            if (msSinceLastWave >= maxMsTimePerWave)
            {
                msSinceLastWave = 0;
                currentWave++;
                if (currentWave >= waveConfigurations.Count)
                {
                    currentWave = 0;
                }
                Feed(waveConfigurations[currentWave]);
            }
            else
            {
                bugAI.Update(msElapsed);
            }
        }

        public int PlayerLocationId { get { return playerLocationId; } }

        public enum GameObjectTypes
        {
            None = 0,
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
            bugAI = null;
            bugAI = new Input.BugAI(mover, navMesh, this);
            int bugsAdded = 0;
            bugLocations = null;
            bugLocations = new Dictionary<int, int>();
            // do NOT reset the player location!
            if(configuration.Length == Globals.LocationCount)
            {
                for(int i = 0; i < Globals.LocationCount; i++)
                {
                    int locationConfig = configuration[i];
                    bool isPlayer;
                    bool isBug;
                    if (i != playerLocationId)
                    {
                        // avoid teleporting the player or obliterating him completely:
                        isPlayer = (0 != (locationConfig & (int)GameObjectTypes.Player));
                        // avoid spawn-killing the player with a newly spawned bug:
                        isBug = (0 != (locationConfig & (int)GameObjectTypes.Bug));
                    }
                    else
                    {
                        isPlayer = true;
                        isBug = false;
                    }
                    bool isBerry = (0 != (locationConfig & (int)GameObjectTypes.Berry));
                    bool isSunblock = (0 != (locationConfig & (int)GameObjectTypes.Sunblock));
                    Location loc = new Location(isPlayer, isBerry, isSunblock, isBug);
                    if (isBug)
                    {
                        bugsAdded++;
                        bugLocations.Add(bugsAdded, i);
                        bugAI.AddBug(bugsAdded);
                    }
                    locations[i] = loc;
                }
            }
        }

        public void ResetGame()
        {
            msSinceLastWave = 0;
            currentWave = 0;
            if (waveConfigurations.Count > 0)
            {
                Feed(waveConfigurations[0]);
            }
            ResetPlayer();
        }

        public void ResetPlayer()
        {
            locations[playerLocationId].IsPlayer = false;
            playerLocationId = 0;
            locations[playerLocationId].IsPlayer = true;
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
        
        public void RemoveBug(int locationId)
        {
            if (locationId >= 0 && locationId < locations.Count)
            {
                Location loc = locations[locationId];
                loc.IsBug = false;
                if (bugLocations.ContainsValue(locationId))
                {
                    int bugId = bugLocations.FirstOrDefault(x=>x.Value==locationId).Key;
                    bugAI.RemoveBug(bugId);
                }
            }
        }

        public void RemoveSunblock()
        {
            Location loc = locations[0];
            loc.IsSunblock = false;
        }

        public void PlaySound(int soundType)
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(int objectId, int destinationId)
        {
            throw new System.NotImplementedException();
        }

        public void IgnoreInput(int milliseconds)
        {
            throw new System.NotImplementedException();
        }

        public void BitePlayer()
        {
            throw new System.NotImplementedException();
        }

        public void PickupBerryAt(int destinationId)
        {
            throw new System.NotImplementedException();
        }

        public void PickupSunblockAt(int destinationId)
        {
            throw new System.NotImplementedException();
        }

        public void TrySouth(int locationId)
        {
            throw new System.NotImplementedException();
        }

        public void TryWest(int locationId)
        {
            throw new System.NotImplementedException();
        }

        public void TryEast(int locationId)
        {
            throw new System.NotImplementedException();
        }

        public void Idle(int milliseconds)
        {
            throw new System.NotImplementedException();
        }

        public int LocationFor(int objectId)
        {
            if (objectId == ObjectLogic.Constants.ObjectIds.PlayerId)
            {
                return playerLocationId;
            }
            else
            {
                return bugLocations[objectId];
            }
        }
    }
}  
