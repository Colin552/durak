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
        private Card currentCard = null;
        private Game myGame;
        private Label gameInfoLabel;
        private bool cardPlayed;
        private bool playGame;
        private bool turnPlayed = false;


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

        public bool CardPlayed
        {
            get { return cardPlayed; }
            set { cardPlayed = value; }
        }
        public bool PlayGame
        {
            get { return playGame; }
            set { playGame = value; }
        }
        public bool TurnPlayed
        {
            get { return turnPlayed; }
            set { turnPlayed = value; }
        }
        private int topMargin = -70;

        public void MoveCardImage(Grid toGrid, Image imageToMove, int gridColumn, int row = 0)
        {
            // Check if the grid already contains the card
            if (!toGrid.Children.Contains(imageToMove))
            {
                //System.Diagnostics.Debug.WriteLine("IMAGE: " +imageToMove.Name);
                toGrid.Children.Add(imageToMove);
                imageToMove.SetValue(Grid.ColumnProperty, gridColumn);

                // Put it on the top or bottow "row" 
                if (toGrid == CenterGrid)
                {
                    Thickness centerMargin = new Thickness(0, topMargin + ((CenterGrid.Children.Count -1) * - topMargin), 0, 0);                 
                    imageToMove.SetValue(Grid.MarginProperty, centerMargin);
                }
                else
                {
                    Thickness cardMargin = new Thickness(0, topMargin + (row * -topMargin), 0, 0);
                    imageToMove.SetValue(Grid.MarginProperty, cardMargin);
                }

                

                // Add or remove the event handler from the card's image depending on whether it is going to the Player's hand or not
                if (toGrid == playerGrid)
                {
                    imageToMove.MouseMove += myGame.Card_MouseMove;
                }
                else
                {
                    imageToMove.MouseMove -= myGame.Card_MouseMove;
                }
                CurrentCard = null;
            }
        }

        /// <summary>
        /// RemoveCardImage - Removes a card from a grid
        /// </summary>
        /// <param name="removeFromGrid"></param>
        /// <param name="imageToRemove"></param>
        public void RemoveCardImage(Grid removeFromGrid, Image imageToRemove)
        {
            TurnPlayed = false;
            bool remove = false;
            foreach (Card card in currentPlayer.Cards)
            {
                if (imageToRemove == card.myImage)
                {
                    CurrentCard = card;
                    remove = true;
                }
            }
            if(remove)
            {
                if(currentPlayer is ComputerPlayer && !CurrentCard.faceUp)
                {
                    CurrentCard.SetFaceUp();
                    imageToRemove = CurrentCard.myImage;
                }
                removeFromGrid.Children.Remove(imageToRemove);
                currentPlayer.Cards.Remove(CurrentCard);

                myGame.UpdatePlayers(currentPlayer);
            }
            cardPlayed = remove;
            OrderCards();
        }


        public void RemoveRiver(Cards riverImages)
        {
            CenterGrid.Children.Clear();
        }

        public void MoveRiver(Grid toGrid)
        {
            //foreach (Image cardImage in riverImages)
           // {
                //MoveCardImage(toGrid, cardImage, 4, 0);
            //}
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
                {
                    opponentGrid.Children.Remove(currentPlayer.Cards.ElementAt(i).myImage);
                    currentPlayer.Cards.ElementAt(i).SetFaceDown();
                }
                MoveCardImage(currentPlayer is HumanPlayer ? playerGrid : opponentGrid, currentPlayer.Cards.ElementAt(i).myImage, i, 0);
            }
            TurnPlayed = true;
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
