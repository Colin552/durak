using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Input;
using System.Windows;

namespace Durak
{
    public class Card : ICloneable
    {
        public readonly Suit suit;
        public readonly Rank rank;
        public static bool isAceHigh = true;
        public System.Windows.Controls.Image myImage;
        public bool faceUp = true;
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
            if (faceUp)
            {
                myImage = new System.Windows.Controls.Image();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/" + CreateImageFileName() + ".png");
                bitmap.EndInit();
                myImage.Source = bitmap;
                myImage.Stretch = Stretch.Fill;
                myImage.Width = 100;
                myImage.Height = 154;
                myImage.Name = CreateImageFileName();
            }  
        }

        public void SetFaceDown()
        {
            faceUp = false;
            
            myImage = new System.Windows.Controls.Image();           
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/CardBack.png");
            bitmap.EndInit();
            myImage.Source = bitmap;
            myImage.Stretch = Stretch.Fill;
            myImage.Width = 100;
            myImage.Height = 154;
            myImage.Name = CreateImageFileName();
            
        }

        /// <summary>
        /// CreateImageFileName - Creates the file name for the BitmapImage's Uri source. The file names are set to correspond to each picture
        /// </summary>
        /// <returns>A string with the file name</returns>
        private string CreateImageFileName()
        {
            String resourceName = rank.ToString() + "_of_" + suit.ToString();

            return resourceName;
        }

        /// <summary>
        /// ToString - overrides the base object's ToString() method
        /// </summary>
        /// <returns>A string describing the card</returns>
        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
