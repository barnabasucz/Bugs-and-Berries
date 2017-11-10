﻿using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation.Peers;
using Windows.UI.Xaml.Automation.Provider;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bugs_and_Berries_game.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainGamePage : Page, Scripting.IScriptingServer
    {
        private StateMachine.GameStateMachine gameStateMachine;
        private Visual.Visualizer visualizer;
        private World.Locations.LocationContainer locationContainer;
        private Visual.Arrangements.TileArrangements tileArrangements;
        private World.NavMeshes.NavMesh navMesh;
        private Input.UserInput userInput;
        private int score;
        private int chancesLeft;

        private bool isLoaded;
        private bool upKeyPressed;
        private bool downKeyPressed;
        private bool leftKeyPressed;
        private bool rightKeyPressed;
        private bool spacebarPressed;
        private bool letterAKeyPressed;
        //private bool escapeKeyPressed;
        ButtonAutomationPeer northButtonPeer;
        ButtonAutomationPeer southButtonPeer;
        ButtonAutomationPeer westButtonPeer;
        ButtonAutomationPeer eastButtonPeer;
        ButtonAutomationPeer actionButtonPeer;
        //ButtonAutomationPeer pauseButtonPeer;
        IInvokeProvider northProv;
        IInvokeProvider southProv;
        IInvokeProvider westProv;
        IInvokeProvider eastProv;
        IInvokeProvider actionProv;
        //IInvokeProvider pauseProv;

        public MainGamePage()
        {
            isLoaded = false;
            gameStateMachine = new StateMachine.GameStateMachine();
            locationContainer = new World.Locations.LocationContainer(this);
            tileArrangements = new Visual.Arrangements.TileArrangements();
            visualizer = new Visual.Visualizer(
                locationContainer, tileArrangements,
                new Visual.TileGraphicArrangement());
            navMesh = new World.NavMeshes.NavMesh();
            userInput = new Input.UserInput(this, navMesh);
            this.InitializeComponent();
            upKeyPressed = false;
            downKeyPressed = false;
            leftKeyPressed = false;
            rightKeyPressed = false;
            spacebarPressed = false;
            letterAKeyPressed = false;
            //escapeKeyPressed = false;
            northButtonPeer = new ButtonAutomationPeer(NorthButton);
            northProv = northButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            southButtonPeer = new ButtonAutomationPeer(SouthButton);
            southProv = southButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            westButtonPeer = new ButtonAutomationPeer(WestButton);
            westProv = westButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            eastButtonPeer = new ButtonAutomationPeer(EastButton);
            eastProv = eastButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            actionButtonPeer = new ButtonAutomationPeer(ActionButton);
            actionProv = actionButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            //pauseButtonPeer = new ButtonAutomationPeer(PauseButton);
            //pauseProv = pauseButtonPeer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            score = 0;
            chancesLeft = 3;
        }

        private void NorthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceiveNorth(locationContainer.PlayerLocationId);
                spacebarPressed = false;
            }
        }

        private void WestButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceiveWest(locationContainer.PlayerLocationId);
                spacebarPressed = false;
            }
        }

        private void SouthButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceiveSouth(locationContainer.PlayerLocationId);
                spacebarPressed = false;
            }
        }

        private void EastButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceiveEast(locationContainer.PlayerLocationId);
                spacebarPressed = false;
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceiveAction(locationContainer.PlayerLocationId);
                spacebarPressed = false;
                int playerLocationId = locationContainer.PlayerLocationId;
                if(locationContainer.IsBerryAt(playerLocationId))
                {
                    locationContainer.RemoveBerry(playerLocationId);
                    score++;
                }
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceivePause();
                spacebarPressed = false;
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.LcdScreen.RemoveFromVisualTree();
            this.LcdScreen = null;
        }

        public void LoadComplete()
        {
            isLoaded = true;
            LcdScreen.Invalidate();
        }
        private void LcdScreen_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            isLoaded = false;
            visualizer.CreateResources(sender, args, LoadComplete);
        }

        private void Grid_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            try
            {
                Windows.System.VirtualKey key = e.Key;
                switch (key)
                {
                    case Windows.System.VirtualKey.Up:
                        if (!upKeyPressed)
                        {
                            northProv.Invoke();
                        }
                        upKeyPressed = true;
                        break;
                    case Windows.System.VirtualKey.Down:
                        if (!downKeyPressed)
                        {
                            southProv.Invoke();
                        }
                        downKeyPressed = true;
                        break;
                    case Windows.System.VirtualKey.Left:
                        if (!leftKeyPressed)
                        {
                            westProv.Invoke();
                        }
                        leftKeyPressed = true;
                        break;
                    case Windows.System.VirtualKey.Right:
                        if (!rightKeyPressed)
                        {
                            eastProv.Invoke();
                        }
                        rightKeyPressed = true;
                        break;
                    case Windows.System.VirtualKey.Space:
                        spacebarPressed = true;
                        break;
                    case Windows.System.VirtualKey.A:
                        if (!letterAKeyPressed)
                        {
                            actionProv.Invoke();
                        }
                        letterAKeyPressed = true;
                        break;
                    //case Windows.System.VirtualKey.Escape:
                    //    if (!escapeKeyPressed)
                    //    {
                    //        pauseProv.Invoke();
                    //    }
                    //    break;
                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void Grid_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            Windows.System.VirtualKey key = e.Key;
            switch (key)
            {
                case Windows.System.VirtualKey.Up:
                    upKeyPressed = false;
                    break;
                case Windows.System.VirtualKey.Down:
                    downKeyPressed = false;
                    break;
                case Windows.System.VirtualKey.Left:
                    leftKeyPressed = false;
                    break;
                case Windows.System.VirtualKey.Right:
                    rightKeyPressed = false;
                    break;
                case Windows.System.VirtualKey.A:
                    letterAKeyPressed = false;
                    break;
                //case Windows.System.VirtualKey.Escape:
                //    escapeKeyPressed = false;
                //    break;
                default:
                    break;
            }
        }

        private void LcdScreen_Update(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, 
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedUpdateEventArgs args)
        {
            int msElapsed = args.Timing.ElapsedTime.Milliseconds;
            int playerLocationId = locationContainer.PlayerLocationId;
            if (locationContainer.IsBugAt(playerLocationId))
            {
                chancesLeft--;
                gameStateMachine.Die(chancesLeft);
                visualizer.Blink(2000);
                locationContainer.ResetPlayer();
            }

            gameStateMachine.Update(msElapsed);
            visualizer.Update(gameStateMachine.StateCode, msElapsed);
            locationContainer.Update(msElapsed);
            userInput.Update(msElapsed);
        }

        private void LcdScreen_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, 
            Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
        {
            if (isLoaded)
            {
                args.DrawingSession.Clear(Color.FromArgb(255, 220, 220, 220));
                visualizer.Draw(sender, args, gameStateMachine.StateCode);
                args.DrawingSession.DrawText("Score: " + score.ToString().PadLeft(9, '0'), 
                    new System.Numerics.Vector2(0f, 0f), 
                    Color.FromArgb(255, 0, 0, 0));
                args.DrawingSession.DrawText("Chances Left: " + chancesLeft.ToString(),
                   new System.Numerics.Vector2(0f, 50f),
                   Color.FromArgb(255, 0, 0, 0));
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Pages.HelpPage));
            Window.Current.Activate();
        }

        public void PlaySound(int soundType)
        {
            // NOP
            // (later, ask the sound player object to start the indicated sound)
            //throw new System.NotImplementedException();
        }

        public void MoveTo(int objectId, int destinationId)
        {
            if (objectId == ObjectLogic.Constants.ObjectIds.PlayerId)
            {
                locationContainer.MovePlayer(destinationId);
            }
            else
            {
                locationContainer.MoveBug(objectId, destinationId);
            }
        }

        public void IgnoreInput(int milliseconds)
        {
            // assume this consequence came from the player, because bug interpreter doesn't interpret IgnoreInput.
            userInput.IgnoreInput(milliseconds);
        }

        public void BitePlayer()
        {
            // to do: move game state to player dying
            throw new System.NotImplementedException();
        }

        public void PickupBerryAt(int destinationId)
        {
            visualizer.Pick();
        }

        public void PickupSunblockAt(int destinationId)
        {
            throw new System.NotImplementedException();
        }

        public void TryNorth(int locationId)
        {
            // possible that this doesn't belong in the IScriptingServer interface, because an earlier object should always handle it
            throw new System.NotImplementedException();
        }

        public void TrySouth(int locationId)
        {
            // possible that this doesn't belong in the IScriptingServer interface, because an earlier object should always handle it
            throw new System.NotImplementedException();
        }

        public void TryWest(int locationId)
        {
            // possible that this doesn't belong in the IScriptingServer interface, because an earlier object should always handle it
            throw new System.NotImplementedException();
        }

        public void TryEast(int locationId)
        {
            // possible that this doesn't belong in the IScriptingServer interface, because an earlier object should always handle it
            throw new System.NotImplementedException();
        }

        public void Idle(int milliseconds)
        {
            // possible that this doesn't belong in the IScriptingServer interface, because an earlier object should always handle it
            throw new System.NotImplementedException();
        }
    }
}
