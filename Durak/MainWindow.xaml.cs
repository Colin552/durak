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
            
            // Creates a deck and places the first 6 cards in the player's hand canvas.
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
    }
}
