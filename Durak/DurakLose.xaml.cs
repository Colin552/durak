/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date : 4/27/2018
 * Description: The ending screen of when
 * a player loses in Durak
 * 
 */
using System.Windows;

namespace Durak
{
    /// <summary>
    /// Interaction logic for DurakLose.xaml
    /// </summary>
    public partial class DurakLose : Window
    {
        /// <summary>
        /// Losing screen
        /// </summary>
        /// <param name="player"></param>
        public DurakLose(Player player)
        {
            InitializeComponent();
            lblMessage.Content = player.Name + " IS THE DURAK";
        }

        /// <summary>
        /// Opens up the start form so the player can play again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            StartScreen startScreen = new StartScreen();
            startScreen.Show();
            this.Close();
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
