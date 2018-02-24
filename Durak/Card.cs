using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace Durak
{
    public class Card
    {
        public readonly Suit suit;
        public readonly Rank rank;
        public Image myImage;

        /// <summary>
        /// Parameterized constructor for the Card class
        /// </summary>
        /// <param name="newSuit"></param>
        /// <param name="newRank"></param>
        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
            SetCardImage();
        }

        private Card()
        {
        }

        /// <summary>
        /// SetPicture - Sets the card's image.
        /// </summary>
        private void SetCardImage()
        {
            myImage = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/" + CreateImageFileName() + ".png");
            bitmap.EndInit();
            myImage.Source = bitmap;
            myImage.Stretch = Stretch.Fill;
            myImage.Width = 100;
            myImage.Height = 154;
            myImage.Name = CreateImageFileName();
            myImage.MouseMove += Card_MouseMove;
        }

        private void Card_MouseMove(object sender, MouseEventArgs e )
        {
            
            Image cardImage = sender as Image;
            if (cardImage != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
            }
            
        }

        /// <summary>
        /// CreateImageFileName - Creates the file name for the BitmapImage's Uri source. The file names are set to correspond to each picture
        /// </summary>
        /// <returns>A string with the file name</returns>
        private string CreateImageFileName()
        {
            String resourceName =  rank.ToString() + "_of_" + suit.ToString();

            return resourceName;
        }

        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }
    }
}
