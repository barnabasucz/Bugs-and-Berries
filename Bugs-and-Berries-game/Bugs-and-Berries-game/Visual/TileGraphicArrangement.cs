using System;
using System.Collections.Generic;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Bugs_and_Berries_game.Visual
{
    class TileGraphicArrangement : ICanvasBitmapHolder
    {
        List<Microsoft.Graphics.Canvas.CanvasBitmap> bitmaps;
        List<int> bitmapArrangement;

        public enum BitmapIds
        {
            None = -1,
            E = 0,
            WE = 1,
            NWE = 2,
            NS = 3,
            NSE = 4, 
            S = 5,
            NSWE = 6,
            NSW = 7,
            NW = 8,
            Berry = 9,
            Bug = 10,
            PlayerIdle = 11,
            PlayerPicking = 12
        }

        public TileGraphicArrangement()
        {
            InitializeDefault();
        }

        async System.Threading.Tasks.Task CreateResourcesAsync(
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender)
        {
            await LoadBitmaps(sender);
        }

        void ICanvasBitmapHolder.CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, 
            Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        async System.Threading.Tasks.Task LoadBitmaps(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender)
        {
            try
            {
                Package package = Package.Current;
                StorageFolder installedLocation = package.InstalledLocation;
                string folderName = installedLocation.Path + "\\" + "Assets" + "\\";
                bitmaps = null;
                bitmaps = new List<Microsoft.Graphics.Canvas.CanvasBitmap>((int)BitmapIds.PlayerPicking + 1);
                // E, WE need to be redrawn to look like they are in the shade of the tent
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"E.png")); //bitmaps[(int)BitmapIds.E]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName +@"WE.png")); //bitmaps[(int)BitmapIds.WE] 
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NWE.png")); //bitmaps[(int)BitmapIds.NWE]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NS.png")); //bitmaps[(int)BitmapIds.NS]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NSE.png")); //bitmaps[(int)BitmapIds.NSE]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"S.png")); //bitmaps[(int)BitmapIds.S]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NSWE.png")); //bitmaps[(int)BitmapIds.NSWE]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NSW.png")); //bitmaps[(int)BitmapIds.NSW]
                bitmaps.Add( await CanvasBitmap.LoadAsync(sender, folderName + @"NW.png")); //bitmaps[(int)BitmapIds.NW]
                // Add Tent Left and Tent Right, here (also insert relevant enums before BitmapIds.Berry)
                bitmaps.Add(await CanvasBitmap.LoadAsync(sender, folderName + @"Berry.png")); // bitmaps[(int)BitmapIds.Berry]
                bitmaps.Add(await CanvasBitmap.LoadAsync(sender, folderName + @"Bug.png")); // bitmaps[(int)BitmapIds.Bug]
                bitmaps.Add(await CanvasBitmap.LoadAsync(sender, folderName + @"PlayerIdle.png")); // bitmaps[(int)BitmapIds.PlayerIdle]
                bitmaps.Add(await CanvasBitmap.LoadAsync(sender, folderName + @"PlayerPicking.png")); // bitmaps[(int)BitmapIds.PlayerPicking]

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InitializeDefault()
        {
            bitmapArrangement = null;
            bitmapArrangement = new List<int>(World.Globals.LocationCount);
            bitmapArrangement.Add((int)BitmapIds.E);
            bitmapArrangement.Add((int)BitmapIds.WE);
            bitmapArrangement.Add((int)BitmapIds.NWE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NSE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.S);
            bitmapArrangement.Add((int)BitmapIds.NWE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NSWE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.S);
            bitmapArrangement.Add((int)BitmapIds.NWE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NSWE);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.S);
            bitmapArrangement.Add((int)BitmapIds.NW);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NSW);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.NS);
            bitmapArrangement.Add((int)BitmapIds.S);
        }

        public CanvasBitmap BerryBitmap()
        {
            return bitmaps[(int)BitmapIds.Berry];
        }

        public CanvasBitmap BugBitmap()
        {
            return bitmaps[(int)BitmapIds.Bug];
        }

        public CanvasBitmap PlayerIdleBitmap()
        {
            return bitmaps[(int)BitmapIds.PlayerIdle];
        }

        public CanvasBitmap PlayerPickingBitmap()
        {
            return bitmaps[(int)BitmapIds.PlayerPicking];
        }

        public CanvasBitmap BitmapForLocation(int locationId)
        {
            if (locationId >= 0 && locationId < bitmapArrangement.Count)
            {
                return bitmaps[bitmapArrangement[locationId]];
            }
            // otherwise, return a bitmap for the "not found" graphic, a big red X where the graphic would be
            // for now, to keep compiler happy:
            throw new ArgumentOutOfRangeException("locationId", "locationId was out of range");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}
