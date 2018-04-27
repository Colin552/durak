/*
 * 
 * 
 * 
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Durak
{
    /// <summary>
    /// This class will contain the game loop and functions relating to playing the game
    /// </summary>
    public class Game
    {
        // Static variables for the deck, human and computer player
        private GUI myGUI;
        private Deck myDeck = new Deck();
        private HumanPlayer humanPlayer = new HumanPlayer("Runescape");
        private ComputerPlayer computerPlayer = new ComputerPlayer();
        public HumanPlayer HumanPlayer { get => humanPlayer; set => humanPlayer = value; }
        public ComputerPlayer ComputerPlayer { get => computerPlayer; set => computerPlayer = value; }
        public Deck MyDeck { get => myDeck; set => myDeck = value; }
        public GUI MyGUI { get => myGUI; set => myGUI = value; }
        public Card ComputerDecidedCard { get => computerDecidedCard; set => computerDecidedCard = value; }
        public Card CurrentCardInPlay { get => currentCardInPlay; set => currentCardInPlay = value; }

        public Suit trumpSuit;

        private Card computerDecidedCard;
        private Card currentCardInPlay;
        private Cards cardsInPlay = new Cards();
        private Player currentPlayer;
        private bool attackTurn = true;
        private Player lastPlayer;
        public Game(int numOfCards)
        {
            Deck newDeck = new Deck(numOfCards);
            MyDeck = newDeck;
        }

        /// <summary>
        /// Play - The main game loop
        /// </summary>
        public void Play()
        {
            myGUI.CurrentPlayer = humanPlayer;
            InitialDraw();
            SetTrump();
            currentPlayer = DetermineAttacker();
            humanPlayer.CanPlayCard = true;
            attackTurn = true;

            EndMove();
        }

        public void ComputerPlayerTurn()
        {
            // Set the current player to the computer 
            myGUI.CurrentPlayer = computerPlayer;

            // Allow the computer to make a decision
            ComputerDecidedCard = computerPlayer.MakeMove(trumpSuit, CurrentCardInPlay);
            // For each of the computers cards in hand, find which one matches the returned\
            // Choice for card to play and get its index
            if (ComputerDecidedCard != null)
            {
                // Remove the card from the grid
                myGUI.RemoveCardImage(myGUI.OpponentGrid, ComputerDecidedCard.myImage);
                // Move the card to the middle
                myGUI.MoveCardImage(myGUI.CenterGrid, ComputerDecidedCard.myImage, 0);
                CurrentCardInPlay = ComputerDecidedCard;
            }

            myGUI.CurrentPlayer = HumanPlayer;
            EndMove();

        }

        /// <summary>
        /// EndMove - Called when either the computer or player finishes his move
        /// </summary>
        public void EndMove()
        {
            //System.Diagnostics.Debug.WriteLine("\nNew Move");
            //System.Diagnostics.Debug.WriteLine("Attack turn: " + attackTurn);

            if (lastPlayer != null)
            {
                //System.Diagnostics.Debug.WriteLine(lastPlayer.Name + " played a card: " + lastPlayer.PlayedCard);
                if (attackTurn == false && lastPlayer.PlayedCard == false)
                {
                    EndTurn();
                }
            }

            if (CurrentCardInPlay != null)
            {
                cardsInPlay.Add(currentCardInPlay);
                System.Diagnostics.Debug.WriteLine("Card in play: " + CurrentCardInPlay.ToString());
            }

            if (currentPlayer == HumanPlayer)
            {
                humanPlayer.CanPlayCard = true;
                humanPlayer.PlayedCard = false;
                myGUI.OrderCards();
                currentPlayer = ComputerPlayer;
                lastPlayer = HumanPlayer;
                System.Diagnostics.Debug.WriteLine("Player's turn");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Computer's turn");
                currentPlayer = HumanPlayer;
                lastPlayer = ComputerPlayer;
                ComputerPlayerTurn();
            }
            attackTurn = false;
            
            if (myDeck.CardsRemaining() == 0)
            {
                myGUI.RemoveDeckImage();
                myGUI.SetDeckLabelText("NO CARDS LEFT!");
            }
            else
            {
                myGUI.SetDeckLabelText(myDeck.CardsRemaining().ToString());
            }
            Console.WriteLine("Computer card count: " + computerPlayer.Cards.Count);
            Console.WriteLine("Player card count: " + humanPlayer.Cards.Count);
        }

        public void EndTurn()
        {
            System.Diagnostics.Debug.WriteLine("ENDING TURN");
            System.Diagnostics.Debug.WriteLine("Player taking the cards: " + lastPlayer.Name);
            System.Diagnostics.Debug.WriteLine("Center Cards");

            

            foreach (Card card in cardsInPlay)
            {
                System.Diagnostics.Debug.WriteLine(card.ToString());
                lastPlayer.Cards.Add(card);
            }

            myGUI.RemoveRiver(cardsInPlay);
            if (lastPlayer == computerPlayer)
            {
                myGUI.MoveRiver(myGUI.OpponentGrid);
                UpdatePlayers(computerPlayer);
            }
            else
            {
                myGUI.MoveRiver(myGUI.PlayerGrid);
                UpdatePlayers(humanPlayer);
            }
            Draw(computerPlayer);
            Draw(humanPlayer);
            attackTurn = true;
            cardsInPlay.Clear();
            currentCardInPlay = null;
        }

        /// <summary>
        /// Calls from the GUI class so that the Player objects in the game get an updated hand
        /// When they play a card
        /// </summary>
        /// <param name="player"></param>
        public void UpdatePlayers(Player player)
        {
            if (player is HumanPlayer)
            {
                HumanPlayer.Cards = player.Cards;
            }
            else
                ComputerPlayer.Cards = player.Cards;
        }



        /// <summary>
        /// InitialDraw - Draws the initial 6 cards
        /// </summary>
        public void InitialDraw()
        {
            // Changed code from here previously
            // before it was 'Card myCard = myDeck.GetCard(i)'
            // Now is Card 'myCard = myDeck.GetTopCard()'
            Draw(HumanPlayer);

            for (int i = 6; i < 12; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                //myCard.SetFaceDown();
                myGUI.MoveCardImage(myGUI.OpponentGrid, myCard.myImage, i - 6, 0);
                ComputerPlayer.Cards.Add(myCard);
            }
        }

        /// <summary>
        /// Sets the trump suit for the game
        /// </summary>
        public void SetTrump()
        {
            Card trumpCard = MyDeck.GetTopCard();
            trumpSuit = trumpCard.suit;
            myGUI.PlaceTrumpCard(trumpCard);
            //System.Diagnostics.Debug.WriteLine("Trump Suit: " + trumpSuit);
        }

        /// <summary>
        /// Determines the attacking player
        /// </summary>
        /// <returns>The player who is attacking</returns>
        public Player DetermineAttacker()
        {
            //Temporary values set higher than a king.
            Rank humanLowestRank = (Rank)14;
            Rank computerLowestRank = (Rank)14;

            //Loops through the computer and human player's hands and finds their lowest rank of the trump suit
            for (int i = 1; humanPlayer.Cards.Count > i; i++)
            {
                if (humanPlayer.Cards[i - 1].rank < humanLowestRank && humanPlayer.Cards[i - 1].suit == trumpSuit)
                {
                    humanLowestRank = humanPlayer.Cards[i - 1].rank;
                }
            }

            for (int i = 1; humanPlayer.Cards.Count > i; i++)
            {
                if (computerPlayer.Cards[i - 1].rank < computerLowestRank && computerPlayer.Cards[i - 1].suit == trumpSuit)
                {
                    computerLowestRank = computerPlayer.Cards[i - 1].rank;
                }
            }

            // Returns the player with the lowest rank of the trump suit
            if (computerLowestRank > humanLowestRank)
            {
                MyGUI.SetLabelText("You are attacking");
                currentPlayer = HumanPlayer;
                return HumanPlayer;
            }
            else if (computerLowestRank < humanLowestRank)
            {
                MyGUI.SetLabelText("Computer is attacking");
                currentPlayer = ComputerPlayer;
                return ComputerPlayer;
            }
            else
            {
                MyGUI.SetLabelText("Tie, you are attacking");
                currentPlayer = HumanPlayer;
                return humanPlayer;
            }
        }

        /// <summary>
        /// Draws a new card from the deck
        /// Sends over the card and the Player object to the GUI class
        /// Attempted to be able to send all the 
        /// </summary>
        /// <param name="player"></param>
        public void Draw(HumanPlayer player)
        {
            myGUI.CurrentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                if (myCard != null)
                {
                    //System.Diagnostics.Debug.WriteLine("New Card: " + myCard);
                    myGUI.MoveCardImage(myGUI.PlayerGrid, myCard.myImage, i, 0);
                    player.Cards.Add(myCard);
                }

            }
            //System.Diagnostics.Debug.WriteLine("Cards: " + player.Cards.Count());
        }

        public void Draw(ComputerPlayer player)
        {
            myGUI.CurrentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                if (myCard != null)
                {
                    //myCard.SetFaceDown();
                    myGUI.CurrentCard = myCard;
                    myGUI.MoveCardImage(myGUI.OpponentGrid, myCard.myImage, i, 0);
                    player.Cards.Add(myCard);
                }
            }
        }



        /// <summary>
        /// Checks if one of the players has won
        /// </summary>
        /// <returns></returns>
        public bool CheckForWinner()
        {
            if (humanPlayer.Cards.Count == 0)
            {
                return true;
            }
            else if (computerPlayer.Cards.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Card_MouseMove - Event handler for the Card's drag and drop functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Card_MouseMove(object sender, MouseEventArgs e)
        {
            if (humanPlayer.CanPlayCard)
            {
                Image cardImage = sender as Image;
                if (cardImage != null && e.LeftButton == MouseButtonState.Pressed)
                {
                    string stringRank = cardImage.Name.Split('_')[0];
                    string stringSuit = cardImage.Name.Split('_')[2];
                    Rank cardRank = (Rank)Enum.Parse(typeof(Rank), stringRank);
                    Suit cardSuit = (Suit)Enum.Parse(typeof(Suit), stringSuit);
                    Card selectedCard = new Card(cardSuit, cardRank);


                    if (ValidMove(selectedCard))
                    {
                        CurrentCardInPlay = selectedCard;
                        DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
                    }
                }
            }
        }

        public bool ValidMove(Card cardToPlay)
        {
            bool isValid = true;

            //if (currentPlayer == computerPlayer)
            //{
            if (CurrentCardInPlay != null)
            {
                if (cardToPlay.suit == CurrentCardInPlay.suit || cardToPlay.suit == trumpSuit && currentCardInPlay.suit != trumpSuit)
                {
                    if (!(cardToPlay.suit == trumpSuit && currentCardInPlay.suit != trumpSuit))
                    {
                        if ((cardToPlay.rank < CurrentCardInPlay.rank))
                        {
                            isValid = false;
                        }
                    }

                }
                else
                {
                    isValid = false;
                }
            }
            //}
            return isValid;
        }

    }
}
