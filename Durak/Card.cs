using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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
        /// <param name="newCardGraphic"></param>
        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }

        private Card()
        {
        }

        private void SetPicture()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("Resources/10_of_clubs.png", UriKind.Relative);
            bitmap.EndInit();
            myImage.Stretch = Stretch.Fill;
        }

        public override string ToString()
        {
            return "The " + rank + " of " + suit + "s";
        }
    }
}
