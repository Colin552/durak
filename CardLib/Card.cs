/*
 * 
 * 
 * 
 * 
 */
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace CardLib
{
    public class Card: ICloneable
    {
        #region Fields and Properties
        public Image myImage;
        /// <summary>
        /// 
        /// </summary>
        protected Suit mySuit;
        public Suit Suit
        {
            get { return mySuit; }
            set { mySuit = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected Rank myRank;
        public Rank Rank
        {
            get { return myRank; }
            set { myRank = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected int myValue;
        public int CardValue
        {
            get { return myValue; }
            set { myValue = value; }
        }
        protected bool faceUp = false;
        public bool FaceUp
        {
            get { return faceUp; }
            set { faceUp = value; }
        }
        #endregion

        /// <summary>
        /// Parameterized / Default constructor for the class
        /// </summary>
        /// <param name="rank"></param>
        /// <param name="suit"></param>
        public Card(Rank rank = Rank.Ace, Suit suit = Suit.Heart)
        {
            this.myRank = rank;
            this.mySuit = suit;
            this.myValue = (int)rank;
            SetCardImage();
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
        /// Clones the card
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// CreateImageFileName - Creates the file name for the BitmapImage's Uri source. The file names are set to correspond to each picture
        /// </summary>
        /// <returns>A string with the file name</returns>
        private string CreateImageFileName()
        {
            String resourceName =  myRank.ToString() + "_of_" + mySuit.ToString();

            return resourceName;
        }

        public override string ToString()
        {
            return "The " + myRank + " of " + mySuit + "s";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (this.CardValue == ((Card)obj).CardValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.myValue * 100 + (int)this.mySuit * 10 + ((this.FaceUp) ? 1 : 0);
        }

        #region Relational Operators
        public static bool operator ==(Card left, Card right)
        {
            return (left.CardValue == right.CardValue);
        }
        public static bool operator !=(Card left, Card right)
        {
            return (left.CardValue != right.CardValue);
        }
        public static bool operator <(Card left, Card right)
        {
            return (left.CardValue < right.CardValue);
        }
        public static bool operator <=(Card left, Card right)
        {
            return (left.CardValue <= right.CardValue);
        }
        public static bool operator >(Card left, Card right)
        {
            return (left.CardValue > right.CardValue);
        }
        public static bool operator >=(Card left, Card right)
        {
            return (left.CardValue >= right.CardValue);
        }
        #endregion
    }
}
