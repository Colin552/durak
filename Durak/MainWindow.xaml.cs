/* MainWindow.xaml.cs
 * 
 * @Author  Colin Strong
 * @Author  Calvin Lapp
 * @Author  Elizabeth Welch
 * 
 * @Since   25-Feb-2018
 * 
 * Description:
 * 
 */

using System;
using System.Windows;
using System.Windows.Controls;

namespace Durak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        GUI gui = new GUI();
        public MainWindow(int numOfCards)
        {
            InitializeComponent();
            Game newGame = new Game(numOfCards);
            game = newGame;
            
            //Set the GUI class's variables
            gui.PlayerGrid = playerHandGrid;
            gui.OpponentGrid = opponentHandGrid;
            gui.CenterGrid = centerGrid;
            gui.WindowGrid = windowGrid;
            gui.MyGame = game;
            gui.GameInfoLabel = lblGameInfo;
            gui.LblCardsRemaining = lblDeckSize;
            game.MyGUI = gui;
        }

        /// <summary>
        /// Card_Drop - Handler for the center grid's drop event.
        /// </summary>
        /// <param name="sender">The player's canvas</param>
        /// <param name="e">The card's image</param>
        private void Card_Drop(object sender, DragEventArgs e)
        {
            Image card = (Image)e.Data.GetData(typeof(Image));
            gui.RemoveCardImage(playerHandGrid, card);
            gui.MoveCardImage(centerGrid, card, 0, gui.CenterGrid.Children.Count);
            game.HumanPlayer.CanPlayCard = false;
            game.HumanPlayer.PlayedCard = true;
            //humanPlayer.CanPlayCard = false;
            //humanPlayer.PlayedCard = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //game.EndTurn();
            game.EndMove();
            if (game.CheckForWinner())
                this.Close();
        }
        /// <summary>
        /// Once clicked, starts the game
        /// This was added to assist with things not happening right away and giving errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            game.MyGUI.PlayGame = true;
            btnPlay.Visibility = Visibility.Hidden;
            game.Play();
            btnEndTurn.IsEnabled = true;
        }
        /// <summary>
        /// Handles the exit button in the menu, closes the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Shows the rules window when clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuRules_Click(object sender, RoutedEventArgs e)
        {
            RulesForm rulesWindow = new RulesForm();
            rulesWindow.Show();
        }
        /// <summary>
        /// Opens a new game, closes the current game in progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuNew_Click(object sender, RoutedEventArgs e)
        {
            StartScreen startWindow = new StartScreen();
            startWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Opens the about form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }
    }
}
