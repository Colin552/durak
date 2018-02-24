using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            
            // Creates a deck and places the first 6 card's graphic in the player's hand canvas.
            Deck myDeck = new Deck();
            for (int i = 0; i < 6; i++)
            {           
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                Console.WriteLine(myCard.myImage.Source);
                playerHandCanvas.Children.Add(myImage);
                Canvas.SetLeft(myImage, i * 110);        
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
            }
            
            Console.WriteLine(card.Name);
        }
    }
}
