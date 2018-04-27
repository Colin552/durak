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
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Durak
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : Window
    {
        private bool isPlaying = true;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private Uri muteURI = new Uri("pack://application:,,,/Durak;component/Resources/mute.png");
        private Uri playURI = new Uri("pack://application:,,,/Durak;component/Resources/play.png");
        /// <summary>
        /// 
        /// <see cref="https://www.youtube.com/watch?v=U06jlgpMtQs"/>
        /// </summary>
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

            mediaPlayer.Stop();

            MainWindow mainWindow = new MainWindow(numOfCards);
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Mutes or plays the glorious Russian anthem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MuteHandler(object sender, EventArgs e)
        {

            Console.WriteLine("Click");
            Image audioImage = sender as Image;

            BitmapImage newAudioImage = new BitmapImage();
            newAudioImage.BeginInit();
           
            if (isPlaying)
            {
                isPlaying = false;
                mediaPlayer.Pause();
                newAudioImage.UriSource = muteURI;
            }
            else
            {
                mediaPlayer.Play();
                isPlaying = true;
                newAudioImage.UriSource = playURI;
            }

            newAudioImage.EndInit();
            audioImage.Source = newAudioImage;
        }

        /// <summary>
        ///  Clears the statistics file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.IO.File.WriteAllText("../../Log/statistics.txt.", string.Empty);
        }
    }
}
