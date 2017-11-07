using System.Collections.Generic;
using Instruction = Bugs_and_Berries_game.Scripting.Instructions.Instruction;
using OpCodes = Bugs_and_Berries_game.Scripting.Instructions.OpCodes;

namespace Bugs_and_Berries_game.World.NavMeshes
{

    public class NavMeshBuilder
    {
        private NavMeshBuilder() { }

        // note: if certain walkable spots should trigger different sounds (walking through grass, etc),
        // each location could dictate a different sound for each direction walked!
        public static NavMeshLocation BuildE(int locationId, int eastId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            east.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            east.Add(new Instruction(OpCodes.MoveTo, eastId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildWE(int locationId, int westId, int eastId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            west.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            west.Add(new Instruction(OpCodes.MoveTo, westId));
            east.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            east.Add(new Instruction(OpCodes.MoveTo, eastId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNWE(int locationId, int northId, int westId, int eastId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            west.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            west.Add(new Instruction(OpCodes.MoveTo, westId));
            east.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            east.Add(new Instruction(OpCodes.MoveTo, eastId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNS(int locationId, int northId, int southId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            south.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            south.Add(new Instruction(OpCodes.MoveTo, southId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNSE(int locationId, int northId, int southId, int eastId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            south.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            south.Add(new Instruction(OpCodes.MoveTo, southId));
            east.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            east.Add(new Instruction(OpCodes.MoveTo, eastId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildS(int locationId, int southId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            south.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            south.Add(new Instruction(OpCodes.MoveTo, southId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNSWE(int locationId, int northId, int southId, int westId, int eastId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            south.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            south.Add(new Instruction(OpCodes.MoveTo, southId));
            west.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            west.Add(new Instruction(OpCodes.MoveTo, westId));
            east.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            east.Add(new Instruction(OpCodes.MoveTo, eastId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNSW(int locationId, int northId, int southId, int westId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            south.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            south.Add(new Instruction(OpCodes.MoveTo, southId));
            west.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            west.Add(new Instruction(OpCodes.MoveTo, westId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }

        public static NavMeshLocation BuildNW(int locationId, int northId, int westId)
        {
            var north = new List<Instruction>();
            var south = new List<Instruction>();
            var west = new List<Instruction>();
            var east = new List<Instruction>();
            north.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            north.Add(new Instruction(OpCodes.MoveTo, northId));
            west.Add(new Instruction(OpCodes.PlaySound, (int)Sounds.SoundIds.Walk));
            west.Add(new Instruction(OpCodes.MoveTo, westId));
            return new NavMeshLocation(locationId, north, south, west, east);
        }
    }

    // Note: NavMesh only shows how the standing places are connected, not what items or actors
    // are currently standing on each place...
    public class NavMesh
    {
        private List<NavMeshLocation> locations;
        public NavMesh()
        {
            // for now, just build the NavMesh in code (there is only one "level" now).
            // todo: load this from a file, that the game editor builds and saves.
            // the game editor will need to know how to package up the levels.
            // At first, the levels will probably be distributed with the application itself;
            // eventually, they will be served from the cloud and streamed down to the player at program
            // startup...for future enhancement...
            // hypothetically, this could be a singleton to prevent more than one copy of "the"
            // level from being instantiated, but currently it's only a coincidence that there is
            // only one level, and eventually there could be more than one.
            locations = new List<NavMeshLocation>(Globals.LocationCount);
            locations.Add(NavMeshBuilder.BuildE(0, 1));
            locations.Add(NavMeshBuilder.BuildWE(1, 0, 2));
            locations.Add(NavMeshBuilder.BuildNWE(2, 3, 1, 9));
            locations.Add(NavMeshBuilder.BuildNS(3, 4, 2));
            locations.Add(NavMeshBuilder.BuildNS(4, 5, 3));
            locations.Add(NavMeshBuilder.BuildNSE(5, 6, 4, 12));
            locations.Add(NavMeshBuilder.BuildNS(6, 7, 5));
            locations.Add(NavMeshBuilder.BuildNS(7, 8, 6));
            locations.Add(NavMeshBuilder.BuildS(8, 7));
            locations.Add(NavMeshBuilder.BuildNWE(9, 10, 2, 16));
            locations.Add(NavMeshBuilder.BuildNS(10, 11, 9));
            locations.Add(NavMeshBuilder.BuildNS(11, 12, 10));
            locations.Add(NavMeshBuilder.BuildNSWE(12, 13, 11, 5, 19));
            locations.Add(NavMeshBuilder.BuildNS(13, 14, 12));
            locations.Add(NavMeshBuilder.BuildNS(14, 15, 13));
            locations.Add(NavMeshBuilder.BuildS(15, 14));
            locations.Add(NavMeshBuilder.BuildNWE(16, 17, 9, 23));
            locations.Add(NavMeshBuilder.BuildNS(17, 18, 16));
            locations.Add(NavMeshBuilder.BuildNS(18, 19, 17));
            locations.Add(NavMeshBuilder.BuildNSWE(19, 20, 18, 12, 26));
            locations.Add(NavMeshBuilder.BuildNS(20, 21, 19));
            locations.Add(NavMeshBuilder.BuildNS(21, 22, 20));
            locations.Add(NavMeshBuilder.BuildS(22, 21));
            locations.Add(NavMeshBuilder.BuildNW(23, 24, 16));
            locations.Add(NavMeshBuilder.BuildNS(24, 25, 23));
            locations.Add(NavMeshBuilder.BuildNS(25, 26, 24));
            locations.Add(NavMeshBuilder.BuildNSW(26, 27, 25, 19));
            locations.Add(NavMeshBuilder.BuildNS(27, 28, 26));
            locations.Add(NavMeshBuilder.BuildNS(28, 29, 27));
            locations.Add(NavMeshBuilder.BuildS(29, 28));
        }

        public List<Instruction> NorthConsequences(int fromId)
        {
            NavMeshLocation location = Location(fromId);
            return (location != null ? location.NorthConsequences : new List<Instruction>());
        }

        public List<Instruction> SouthConsequences(int fromId)
        {
            NavMeshLocation location = Location(fromId);
            return (location != null ? location.SouthConsequences : new List<Instruction>());
        }

        public List<Instruction> WestConsequences(int fromId)
        {
            NavMeshLocation location = Location(fromId);
            return (location != null ? location.WestConsequences : new List<Instruction>());
        }

        public List<Instruction> EastConsequences(int fromId)
        {
            NavMeshLocation location = Location(fromId);
            return (location != null ? location.EastConsequences : new List<Instruction>());
        }

        private NavMeshLocation Location(int locationId)
        {
            if (locationId >= 0 && locationId < locations.Count)
            {
                return locations[locationId];
            }
            return null;
        }
    }

    public class NavMeshLocation
    {
        private int locationId;
        private List<Instruction> northConsequences;
        private List<Instruction> southConsequences;
        private List<Instruction> westConsequences;
        private List<Instruction> eastConsequences;

        public NavMeshLocation(int locationId, List<Instruction> north, List<Instruction> south, List<Instruction> west,
            List<Instruction> east)
        {
            this.locationId = locationId;
            northConsequences = north;
            southConsequences = south;
            westConsequences = west;
            eastConsequences = east;
        }

        public List<Instruction> NorthConsequences { get { return northConsequences; } }
        public List<Instruction> SouthConsequences { get { return southConsequences; } }
        public List<Instruction> WestConsequences { get { return westConsequences; } }
        public List<Instruction> EastConsequences { get { return eastConsequences; } }
    }
}
