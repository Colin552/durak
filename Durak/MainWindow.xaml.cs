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
        public const int CARD_OFFSET = 110;
        public MainWindow()
        {
            InitializeComponent();
            
            // Creates a deck and places the first 6 card's graphic in the player's hand canvas.
            Deck myDeck = new Deck();
            for (int i = 0; i < 6; i++)
            {           
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                playerHandCanvas.Children.Add(myImage);
                Canvas.SetLeft(myImage, i * CARD_OFFSET);
                Console.WriteLine(i * CARD_OFFSET);
            }
        }

        //Changes the card offsets so that they do not overlap
        public void Resize()
        {
            for (int cardCounter = 0; cardCounter < centerCanvas.Children.Count; cardCounter++)
            {
                Image tempImage = (Image)centerCanvas.Children[cardCounter];

                double middle = (centerCanvas.ActualWidth - tempImage.ActualWidth) / 2;

                Canvas.SetLeft(tempImage, middle + CARD_OFFSET * cardCounter);         
            }
        }

        /// <summary>
        /// Card_Drop - Handler for the center canvas' drop event.
        /// </summary>
        /// <param name="sender">The center canvas</param>
        /// <param name="e">The card's image</param>
        private void Card_Drop(object sender, DragEventArgs e)
        {
            Image card = (Image)e.Data.GetData(typeof(Image));

            playerHandCanvas.Children.Remove(card);
            if (!centerCanvas.Children.Contains(card))
            {
                centerCanvas.Children.Add(card);
                Resize();
            }
           
        }
    }
}
