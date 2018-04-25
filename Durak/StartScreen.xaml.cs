/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date: 4/25/2018
 * Description: This is the starting screen which allows
 * a user to select how many cards they would like to 
 * play with.
 * 
 */
using System.Windows;
using System.Windows.Media;
using System;
namespace Durak
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public StartScreen()
        {
            InitializeComponent();

            mediaPlayer.Open(new Uri("../../Resources/russia.mp3", UriKind.Relative));
            mediaPlayer.Play();
        }
        /// <summary>
        /// Once clicked, opens a new window so the
        /// user can play the game with the selected
        /// number of cards
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            int numOfCards = 0;

            if (opt20.IsChecked == true)
                numOfCards = 20;
            else if (opt36.IsChecked == true)
                numOfCards = 36;
            else if(opt52.IsChecked == true)
                numOfCards = 52;
            else
                numOfCards = 36;

            MainWindow mainWindow = new MainWindow(numOfCards);
            mainWindow.Show();
            this.Close();
        }
    }
}
