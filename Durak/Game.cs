using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        public Suit trumpSuit;

        public Player attackingPlayer;

        /// <summary>
        /// Play - The main game loop
        /// </summary>
        public void Play()
        {
            InitialDraw();
            SetTrump();
            attackingPlayer = DetermineAttacker();

            //do { } while (!CheckForWinner());
        }

        /// <summary>
        /// Calls from the GUI class so that the Player objects in the game get an updated hand
        /// When they play a card
        /// </summary>
        /// <param name="player"></param>
        public void UpdatePlayers(Player player)
        {
            if(player is HumanPlayer)
            {
                HumanPlayer.Cards = player.Cards;
            }
            else
                ComputerPlayer.Cards = player.Cards;
            System.Diagnostics.Debug.WriteLine(player.Name + "'s remaining total cards after replacing: " + player.Cards.Count());
        }



        /// <summary>
        /// InitialDraw - Draws the initial 6 cards
        /// </summary>
        public void InitialDraw()
        {
            // Changed code from here previously
            // before it was 'Card myCard = myDeck.GetCard(i)'
            // Now is Card 'myCard = myDeck.GetTopCard()'
            for (int i = 0; i < 6; i++)
            {
                Draw(HumanPlayer);
            }

            for (int i = 6; i < 12; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                myCard.SetFaceDown();
                myGUI.MoveCardImage(myGUI.opponentGrid, myCard.myImage, i - 6, 0);
                ComputerPlayer.Cards.Add(MyDeck.GetTopCard());
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
            Console.WriteLine("Trump Suit: " + trumpSuit);
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
                if (humanPlayer.Cards[i - 1].rank < humanLowestRank && humanPlayer.Cards[i-1].suit == trumpSuit)
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

            Console.WriteLine("Human Lowest Rank: " + humanLowestRank);
            Console.WriteLine("Computer Lowest Rank: " + computerLowestRank);
            // Returns the player with the lowest rank of the trump suit
            if (computerLowestRank > humanLowestRank)
            {
                MyGUI.SetLabelText("You are attacking");
                return humanPlayer;
            }
            else if (computerLowestRank < humanLowestRank)
            {
                MyGUI.SetLabelText("Computer is attacking");
                return computerPlayer;
            }
            else
            {
                MyGUI.SetLabelText("Tie, you are attacking");
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
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                myGUI.currentCard = myCard;
                System.Diagnostics.Debug.WriteLine("New Card: " + myCard);
                myGUI.MoveCardImage(myGUI.playerGrid, myCard.myImage, i, 0);
                player.Cards.Add(myCard);
            }
            myGUI.currentPlayer = player;
            System.Diagnostics.Debug.WriteLine("Cards: " + player.Cards.Count());
        }

        public void Draw(ComputerPlayer player)
        {
            myGUI.currentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                myCard.SetFaceDown();
                myGUI.currentCard = myCard;
                myGUI.MoveCardImage(myGUI.opponentGrid, myCard.myImage, i - 6, 0);
                player.Cards.Add(myCard);
            }
        }

        /// <summary>
        /// EndTurn - Called when the player ends his turn
        /// </summary>
        public void EndTurn()
        {
            // Draw(computerPlayer);    Big boy issues
            myGUI.OrderCards();
            Draw(HumanPlayer);
            System.Diagnostics.Debug.WriteLine("Clicked");
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
    }
}
