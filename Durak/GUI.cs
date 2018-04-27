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
        private Label lblCardsRemaining;
        private Image deckImage = new Image();
        private Image discardPile = new Image();
        private bool discardPlaced = false;

        public Label LblCardsRemaining
        {
            get { return lblCardsRemaining; }
            set { lblCardsRemaining = value; }
        }

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

        public void MoveCardImage(Grid toGrid, Image imageToMove, int gridColumn = 0, int row = 0)
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

                    Console.WriteLine("Child count: " + centerGrid.Children.Count);
                    int tempColumn = 0;
                    Thickness tempThickness = new Thickness();
                    if (CenterGrid.Children.Count < 4)
                    {
                        tempColumn = 0;
                        tempThickness = new Thickness(0, topMargin + ((CenterGrid.Children.Count - 1) * -topMargin), 0, 0);
                    }
                    else if (CenterGrid.Children.Count < 8)
                    {

                        tempColumn = 1;
                        tempThickness = new Thickness(0, topMargin + ((CenterGrid.Children.Count - 4) * -topMargin), 0, 0);
                    }
                    else if (CenterGrid.Children.Count < 16)
                    {
                        tempColumn = 2;
                        tempThickness = new Thickness(0, topMargin + ((CenterGrid.Children.Count - 8) * -topMargin), 0, 0);
                    }
                    else
                    {
                        tempColumn = 3;
                        tempThickness = new Thickness(0, topMargin + ((CenterGrid.Children.Count - 16) * -topMargin), 0, 0);
                    }
                    imageToMove.SetValue(Grid.ColumnProperty, tempColumn);
                    imageToMove.SetValue(Grid.MarginProperty, tempThickness);
                }
                else
                {           
                    Thickness tempThickness = new Thickness();
                    int tempColumn = 0;

                    if (toGrid.Children.Count <= 8)
                    {
                        tempThickness = new Thickness(0, topMargin + (0 * -topMargin), 0, 0);
                        tempColumn = toGrid.Children.Count - 1;
                    }
                    else if (toGrid.Children.Count <= 16)
                    {
                        tempThickness = new Thickness(0, topMargin + (1 * -topMargin), 0, 0);
                        tempColumn = toGrid.Children.Count - 9;
                    }
                    else if (toGrid.Children.Count <= 24)
                    {
                        tempThickness = new Thickness(0, topMargin + (2 * -topMargin), 0, 0);
                        tempColumn = toGrid.Children.Count - 17;
                    }
                    else
                    {
                        tempThickness = new Thickness(0, topMargin + (3 * -topMargin), 0, 0);
                        tempColumn = toGrid.Children.Count - 25;
                    }
                    imageToMove.SetValue(Grid.ColumnProperty, tempColumn);
                    imageToMove.SetValue(Grid.MarginProperty, tempThickness);
                    
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
        /// Orders the cards in a players hand
        /// Typically used when drawing so cards will not go over top of one another
        /// </summary>
        public void OrderCards()
        {
            PlayerGrid.Children.Clear();
            OpponentGrid.Children.Clear();

            for (int i = 0; i < myGame.HumanPlayer.Cards.Count(); i++)
            {
                MoveCardImage(playerGrid, myGame.HumanPlayer.Cards.ElementAt(i).myImage);
            }

            for (int i = 0; i < myGame.ComputerPlayer.Cards.Count(); i++)
            {
                myGame.ComputerPlayer.Cards.ElementAt(i).SetFaceDown();
                MoveCardImage(opponentGrid, myGame.ComputerPlayer.Cards.ElementAt(i).myImage);
            }


            TurnPlayed = true;
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

        public void RemoveDeckImage()
        {
            if (WindowGrid.Children.Contains(deckImage))
            {
                WindowGrid.Children.Remove(deckImage);
            }
        }

        public void PlaceDiscardPile()
        {
            if (discardPlaced == false)
            {
                discardPlaced = true;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/Durak;component/Resources/deck.png");
                bitmap.EndInit();
                discardPile.Source = bitmap;
                discardPile.Stretch = Stretch.Fill;
                discardPile.Width = 110;
                discardPile.Height = 170;
                discardPile.Name = "discard";

                Thickness discardMargin = new Thickness(0, 0, 0, 0);
                WindowGrid.Children.Add(discardPile);
                discardPile.SetValue(Grid.ColumnProperty, 2);
                discardPile.SetValue(Grid.RowProperty, 1);
                discardPile.SetValue(Grid.MarginProperty, discardMargin);
            }
            
        }

        public void SetLabelText(String message)
        {
            gameInfoLabel.Content = message;
        }

        public void SetDeckLabelText(String message)
        {
            lblCardsRemaining.Content = message;
        }
    }
}
