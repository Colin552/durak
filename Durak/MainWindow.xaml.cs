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
        Game game = new Game();
        GUI gui = new GUI();
        public MainWindow()
        {
            InitializeComponent();

            
            //Set the GUI class's variables
            gui.PlayerGrid = playerHandGrid;
            gui.OpponentGrid = opponentHandGrid;
            gui.CenterGrid = centerGrid;
            gui.WindowGrid = windowGrid;
            gui.MyGame = game;
            gui.GameInfoLabel = lblGameInfo;
            game.MyGUI = gui;

            game.Play();
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
            gui.MoveCardImage(centerGrid, card, 0, 0);     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            game.EndTurn();
        }
    }
}
