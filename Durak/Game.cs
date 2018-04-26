﻿/*
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
        public bool EndOfTurn { get => endTurn; set => endTurn = value; }
        public Card ComputerDecidedCard { get => computerDecidedCard; set => computerDecidedCard = value; }
        public Card CurrentCardInPlay { get => currentCardInPlay; set => currentCardInPlay = value; }
        
        public Suit trumpSuit;
        private bool endTurn = true;
        private Card computerDecidedCard;
        private Card currentCardInPlay;

        public static Player attackingPlayer;
        private Player playersTurn;

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
            attackingPlayer = DetermineAttacker();
            humanPlayer.CanPlayCard = true;
            //myGUI.PlayGame is true once the 'Play' button is clicked
            /*while (myGUI.PlayGame && EndOfTurn)
            {
                myGUI.CurrentPlayer = humanPlayer;
                EndOfTurn = false;
                
                if (attackingPlayer is ComputerPlayer)
                {
                    ComputerPlayerTurn();
                }
                else
                {
                    //HumanPlayerTurn();
                }
            }*/
            
            while (MyGUI.PlayGame && playersTurn != humanPlayer)
            {
                Console.WriteLine("My Turn");
                if (playersTurn == ComputerPlayer)
                {
                    ComputerPlayerTurn();
                    ComputerPlayer.MakeMove(trumpSuit, CurrentCardInPlay);
                    playersTurn = HumanPlayer;
                }
            }
            
        }

        public void ComputerPlayerTurn()
        {
            // Set the current player to the computer 
            myGUI.CurrentPlayer = computerPlayer;

            

            // Allow the computer to make a decision
            ComputerDecidedCard = computerPlayer.MakeMove(trumpSuit,CurrentCardInPlay);
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
            
            myGUI.CurrentPlayer = humanPlayer;
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
                myGUI.MoveCardImage(myGUI.OpponentGrid, myCard.myImage, i - 6, 0);
                ComputerPlayer.Cards.Add(myCard);
                //System.Diagnostics.Debug.WriteLine("---------ADDED CARD: " + myCard.rank + " OF " + myCard.suit + "------------------");
            }
            //MessageBox.Show("Wait");
         
        }

        /// <summary>
        /// Sets the trump suit for the game
        /// </summary>
        public void SetTrump()
        {
            Card trumpCard = MyDeck.GetTopCard();
            trumpSuit = trumpCard.suit;
            myGUI.PlaceTrumpCard(trumpCard);
            //Console.WriteLine("Trump Suit: " + trumpSuit);
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

            //Console.WriteLine("Human Lowest Rank: " + humanLowestRank);
            //Console.WriteLine("Computer Lowest Rank: " + computerLowestRank);
            // Returns the player with the lowest rank of the trump suit
            if (computerLowestRank > humanLowestRank)
            {
                MyGUI.SetLabelText("You are attacking");
                playersTurn = HumanPlayer;
                return HumanPlayer;
            }
            else if (computerLowestRank < humanLowestRank)
            {
                MyGUI.SetLabelText("Computer is attacking");
                playersTurn = ComputerPlayer;
                return ComputerPlayer;
            }
            else
            {
                MyGUI.SetLabelText("Tie, you are attacking");
                playersTurn = HumanPlayer;
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
                if(myCard != null)
                {
                    //System.Diagnostics.Debug.WriteLine("New Card: " + myCard);
                    myGUI.MoveCardImage(myGUI.PlayerGrid, myCard.myImage, i, 0);
                    player.Cards.Add(myCard);
                }
                
            }
            myGUI.CurrentPlayer = player;
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
                    myCard.SetFaceDown();
                    myGUI.CurrentCard = myCard;
                    myGUI.MoveCardImage(myGUI.OpponentGrid, myCard.myImage, i - 6, 0);
                    player.Cards.Add(myCard);
                }
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
            //System.Diagnostics.Debug.WriteLine("Ended Turn Click");
            EndOfTurn = true;
            humanPlayer.CanPlayCard = true;
            playersTurn = ComputerPlayer;
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
                        DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
                        humanPlayer.CanPlayCard = false;
                    }               
                }

            }
        }

        public bool ValidMove(Card cardToPlay)
        {
            bool isValid = true;

            if (attackingPlayer == computerPlayer)
            {
                Console.WriteLine(CurrentCardInPlay.ToString());
                if (cardToPlay.suit == CurrentCardInPlay.suit || cardToPlay.suit == trumpSuit)
                {
                    Console.WriteLine("Valid suit");
                    if (!(cardToPlay.rank > CurrentCardInPlay.rank))
                    {
                        isValid = false;
                    }
                    else
                    {
                        Console.WriteLine("Valid rank");
                    }
                }
                else
                {
                    isValid = false;
                }
            }
            return isValid;
        }
    }
}
