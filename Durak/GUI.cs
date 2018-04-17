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
    /// <summary>
    /// This class contains all of the functions for interacting with the main window GUI.
    /// </summary>
    public class GUI
    {
        private Grid playerGrid;
        private Grid opponentGrid;
        private Grid centerGrid;
        private Grid windowGrid;
        private Player currentPlayer;
        private Card currentCard;
        private Game myGame;
        private Label gameInfoLabel;

        public Grid PlayerGrid
        {
            get { return playerGrid; }
            set { playerGrid = value; }
        }

        public Grid OpponentGrid
        {
            get { return opponentGrid; }
            set { opponentGrid = value; }
        }

        public Grid CenterGrid
        {
            get { return centerGrid; }
            set { centerGrid = value; }
        }

        public Grid WindowGrid
        {
            get { return windowGrid; }
            set { windowGrid = value; }
        }

        public Player CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        public Card CurrentCard
        {
            get { return currentCard; }
            set { currentCard = value; }
        }

        public Game MyGame
        {
            get { return myGame; }
            set { myGame = value; }
        }

        public Label GameInfoLabel
        {
            get { return gameInfoLabel; }
            set { gameInfoLabel = value; }
        }
        private int topMargin = -70;

        public void MoveCardImage(Grid toGrid, Image imageToMove, int gridColumn, int row)
        {
            // Check if the grid already contains the card
            if (!toGrid.Children.Contains(imageToMove))
            {
                toGrid.Children.Add(imageToMove);
                imageToMove.SetValue(Grid.ColumnProperty, gridColumn);

                // Put it on the top or bottow "row" 

                Thickness cardMargin = new Thickness(0, topMargin + (row * -topMargin), 0, 0);
                imageToMove.SetValue(Grid.MarginProperty, cardMargin);

                // Add or remove the event handler from the card's image depending on whether it is going to the Player's hand or not
                if (toGrid == playerGrid)
                {
                    imageToMove.MouseMove += Card_MouseMove;
                }
                else
                {
                    imageToMove.MouseMove -= Card_MouseMove;
                }
            }
        }

        /// <summary>
        /// RemoveCardImage - Removes a card from a grid
        /// </summary>
        /// <param name="removeFromGrid"></param>
        /// <param name="imageToRemove"></param>
        public void RemoveCardImage(Grid removeFromGrid, Image imageToRemove)
        {
            bool remove = false;
            foreach (Card card in currentPlayer.Cards)
            {
                if (imageToRemove == card.myImage)
                {
                    currentCard = card;
                    remove = true;
                }
            }
            if(remove)
            {
                removeFromGrid.Children.Remove(imageToRemove);
                currentPlayer.Cards.Remove(currentCard);
                //System.Diagnostics.Debug.WriteLine("Removed Card: " + currentCard.rank + " " + currentCard.suit);
                //System.Diagnostics.Debug.WriteLine("Total Cards in hand: " + currentPlayer.Cards.Count());
                myGame.UpdatePlayers(currentPlayer);
            }
            OrderCards();
        }
        /// <summary>
        /// Orders the cards in a players hand
        /// Typically used when drawing so cards will not go over top of one another
        /// </summary>
        public void OrderCards()
        {
            for(int i = 0; i < currentPlayer.Cards.Count(); i++)
            {
                if (currentPlayer is HumanPlayer)
                    playerGrid.Children.Remove(currentPlayer.Cards.ElementAt(i).myImage);
                else
                    opponentGrid.Children.Remove(currentPlayer.Cards.ElementAt(i).myImage);
                MoveCardImage(currentPlayer is HumanPlayer ? playerGrid : opponentGrid, currentPlayer.Cards.ElementAt(i).myImage, i, 0);
            }
        }

        /// <summary>
        /// Card_MouseMove - Event handler for the Card's drag and drop functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Card_MouseMove(object sender, MouseEventArgs e)
        {
            Image cardImage = sender as Image;
            if (cardImage != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Places the trump card in the top left corner of the window, rotates it 90 degrees and sets its margin
        /// </summary>
        /// <param name="trumpCard">The trump card</param>
        public void PlaceTrumpCard(Card trumpCard)
        {
            RotateTransform horizontalTransform = new RotateTransform(90);
            Thickness cardMargin = new Thickness(0, 60, -300, 0);
            
            Image trumpCardImage = trumpCard.myImage;
            trumpCardImage.RenderTransform = horizontalTransform;
            windowGrid.Children.Add(trumpCardImage);
            trumpCardImage.SetValue(Grid.MarginProperty, cardMargin);

            PlaceDeck();
        }

        /// <summary>
        /// Places the deck image in the top left corner above the trump card
        /// </summary>
        public void PlaceDeck()
        {
            Image deckImage = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/deck.png");
            bitmap.EndInit();
            deckImage.Source = bitmap;
            deckImage.Stretch = Stretch.Fill;
            deckImage.Width = 110;
            deckImage.Height = 170;
            deckImage.Name = "deck";

            Thickness deckMargin = new Thickness(0, 0, 0, 0);
            WindowGrid.Children.Add(deckImage);
            deckImage.SetValue(Grid.MarginProperty, deckMargin);

        }

        public void SetLabelText(String message)
        {
            gameInfoLabel.Content = message;
        }
    }
}
