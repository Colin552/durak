/* MainWindow.xaml.cs
 * 
 * @Author  Colin Strong
 * @Author  Calvin Lapp
 * @Author  Elizabeth Welch
 * @Author  Steven Hitchon
 * 
 * @Since   25-Feb-2018
 * 
 * Description:
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Durak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Set the GUI class's variables
            GUI.playerGrid = playerHandGrid;
            GUI.opponentGrid = opponentHandGrid;
            GUI.centerGrid = centerGrid;

            Game.Play();
        }

        /// <summary>
        /// Card_Drop - Handler for the center grid's drop event.
        /// </summary>
        /// <param name="sender">The player's canvas</param>
        /// <param name="e">The card's image</param>
        private void Card_Drop(object sender, DragEventArgs e)
        {
            Image card = (Image)e.Data.GetData(typeof(Image));

            GUI.RemoveCardImage(playerHandGrid, card);
            GUI.MoveCardImage(centerGrid, card, 0, true);     
        }
    }
}
