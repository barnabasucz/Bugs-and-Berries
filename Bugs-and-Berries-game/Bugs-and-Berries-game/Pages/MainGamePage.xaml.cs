using Bugs_and_Berries_game.Scripting.Instructions;
using Bugs_and_Berries_game.StateMachine;
using System.Collections.Generic;
using Windows.UI;
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
    public sealed partial class MainGamePage : Page, 
        Scripting.IMover, Scripting.ISoundPlayer, Scripting.IItemPicker, IStateTransitionObserver
    {
        // The Main Game Page also serves as the hub of all game logic.
        private StateMachine.GameStateMachine gameStateMachine;
        private World.Locations.LocationContainer locationContainer;
        private Visual.Arrangements.TileArrangements tileArrangements;
        private Visual.Visualizer visualizer;
        private World.NavMeshes.NavMesh navMesh;
        private Input.UserInput userInput;
        private delegate void TransitionFunc();
        private Dictionary<StateMachine.GameStateCodes, TransitionFunc> transitionFunctionMappings;
        private int score;
        private int chancesLeft;
        private const int StartingScore = 0;
        private const int StartingChances = 3;
        private bool playerVulnerable;
        private string playerMessage;
        private string playerMessageRow2;
        private int messageFadeOpacity;
        private const int MaxMessageFadeOpacity = 255;
        private const int MinMessageFadeOpacity = 0;
        private const int MessageFadeStep = 8;

        // Triggering button presses on keyboard events:
        private bool isLoaded;
        private bool upKeyPressed;
        private bool downKeyPressed;
        private bool leftKeyPressed;
        private bool rightKeyPressed;
        private bool spacebarPressed;
        private bool letterAKeyPressed;
        //private bool escapeKeyPressed;
        ButtonAutomationPeer northButtonPeer; // needed to simulate button clicks from keyboard strokes
        ButtonAutomationPeer southButtonPeer;
        ButtonAutomationPeer westButtonPeer;
        ButtonAutomationPeer eastButtonPeer;
        ButtonAutomationPeer actionButtonPeer;
        //ButtonAutomationPeer pauseButtonPeer;
        IInvokeProvider northProv; // needed to simulate button clicks from keyboard strokes
        IInvokeProvider southProv;
        IInvokeProvider westProv;
        IInvokeProvider eastProv;
        IInvokeProvider actionProv;
        //IInvokeProvider pauseProv;

        public MainGamePage()
        {
            isLoaded = false;
            playerVulnerable = true;
            this.InitializeComponent();
            gameStateMachine = new StateMachine.GameStateMachine();
            gameStateMachine.Subscribe(this);
            navMesh = new World.NavMeshes.NavMesh();
            locationContainer = new World.Locations.LocationContainer(this, navMesh);
            // Tile Arrangments must be loaded before the visualizer tries to use them in its own startup sequence:
            tileArrangements = new Visual.Arrangements.TileArrangements();
            visualizer = new Visual.Visualizer(
                locationContainer, tileArrangements,
                new Visual.TileGraphicArrangement());
            userInput = new Input.UserInput(this, this, this, navMesh);
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
            score = StartingScore;
            chancesLeft = StartingChances;
            transitionFunctionMappings = new Dictionary<GameStateCodes, TransitionFunc>
            {
                { GameStateCodes.StartingUp, StartingUpTransition },
                { GameStateCodes.PlayerReady, PlayerReadyTransition },
                { GameStateCodes.Playing, PlayingTransition },
                { GameStateCodes.PlayerDying, PlayerDyingTransition },
                { GameStateCodes.Paused, PausedTransition },
                { GameStateCodes.GameOver, GameOverTransition }
            };
            playerMessage = null;
            playerMessageRow2 = null;
        }

        private void PlayerMessage(string message, string message2)
        {
            playerMessage = message;
            playerMessageRow2 = message2;
            messageFadeOpacity = MaxMessageFadeOpacity;
        }

        public void Transition(GameStateCodes newStateCode)
        {
            if (transitionFunctionMappings.ContainsKey(newStateCode))
            {
                TransitionFunc transitionFunc = transitionFunctionMappings[newStateCode];
                transitionFunc();
            }
        }

        private void StartingUpTransition()
        {

        }

        private void PlayerReadyTransition()
        {
            locationContainer.ResetPlayer();
            visualizer.Blink(1000);
        }

        private void PlayingTransition()
        {
            visualizer.StopBlinking();
            playerVulnerable = true;
        }

        private void PlayerDyingTransition() // to do: pass in the string corresponding to how the player died
        {
            PlayerMessage("That bug bit you!", "Try again...");
            playerVulnerable = false;
            visualizer.Blink(1000);
        }

        private void PausedTransition()
        {

        }

        private void GameOverTransition()
        {
            PlayerMessage("GAME OVER...Press", "Action to try again");
            visualizer.StopBlinking();
        }

        // Spacebar and Enter-key prevention don't work yet...

        private void NorthButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameStateMachine.StateCode != GameStateCodes.GameOver)
            {
                if (!spacebarPressed)
                {
                    userInput.ReceiveNorth(locationContainer.PlayerLocationId);
                }
            }
        }

        private void WestButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameStateMachine.StateCode != GameStateCodes.GameOver)
            {
                if (!spacebarPressed)
                {
                    userInput.ReceiveWest(locationContainer.PlayerLocationId);
                }
            }
        }

        private void SouthButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameStateMachine.StateCode != GameStateCodes.GameOver)
            {
                if (!spacebarPressed)
                {
                    userInput.ReceiveSouth(locationContainer.PlayerLocationId);
                }
            }
        }

        private void EastButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameStateMachine.StateCode != GameStateCodes.GameOver)
            {
                if (!spacebarPressed)
                {
                    userInput.ReceiveEast(locationContainer.PlayerLocationId);
                }
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {

                if (gameStateMachine.StateCode != GameStateCodes.GameOver)
                {
                    userInput.ReceiveAction(locationContainer.PlayerLocationId);
                    int playerLocationId = locationContainer.PlayerLocationId;
                    if(locationContainer.IsBerryAt(playerLocationId))
                    {
                        locationContainer.RemoveBerry(playerLocationId);
                        score++;
                    }
                }
                else
                {
                    score = StartingScore;
                    chancesLeft = StartingChances;
                    locationContainer.ResetGame();
                    gameStateMachine.ResetGame();
                }
            }
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (!spacebarPressed)
            {
                userInput.ReceivePause();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            // Recommended from Win2D tutorial site:
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
            if (gameStateMachine.StateCode != GameStateCodes.GameOver)
            {
                int msElapsed = args.Timing.ElapsedTime.Milliseconds;
                int playerLocationId = locationContainer.PlayerLocationId;
                if (gameStateMachine.StateCode == StateMachine.GameStateCodes.Playing && playerVulnerable)
                {
                    if (locationContainer.IsBugAt(playerLocationId))
                    {
                        playerVulnerable = false;
                        chancesLeft--;
                        gameStateMachine.Die(chancesLeft, 1000);
                    }
                }
 
                gameStateMachine.Update(msElapsed);
                visualizer.Update(gameStateMachine.StateCode, msElapsed);
                locationContainer.Update(msElapsed);
                userInput.Update(msElapsed);
                if (playerMessage != null)
                {
                    messageFadeOpacity -= MessageFadeStep;
                    if (messageFadeOpacity <= MinMessageFadeOpacity)
                    {
                        messageFadeOpacity = MinMessageFadeOpacity;
                        playerMessage = null;
                        playerMessageRow2 = null;
                    }
                }
            }
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
                if (playerMessage != null)
                {
                    args.DrawingSession.DrawText(playerMessage,
                        new System.Numerics.Vector2(0f, 100f),
                        Color.FromArgb((byte)messageFadeOpacity, 0, 0, 0));
                }
                if (playerMessageRow2 != null)
                {
                    args.DrawingSession.DrawText(playerMessageRow2,
                        new System.Numerics.Vector2(0f, 120f),
                        Color.FromArgb((byte)messageFadeOpacity, 0, 0, 0));
                }

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

        public void PickupBerryAt(int destinationId)
        {
            visualizer.Pick();
            PlayerMessage("Great!", null);
        }

        public void PickupSunblockAt(int destinationId)
        {
            throw new System.NotImplementedException();
        }

     }
}
