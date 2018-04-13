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
    public static class Game
    {
        // Static variables for the deck, human and computer player
        private static Deck myDeck = new Deck();
        private static HumanPlayer humanPlayer = new HumanPlayer("Runescape");
        private static ComputerPlayer computerPlayer = new ComputerPlayer();
        public static HumanPlayer HumanPlayer { get => humanPlayer; set => humanPlayer = value; }
        public static ComputerPlayer ComputerPlayer { get => computerPlayer; set => computerPlayer = value; }
        public static Deck MyDeck { get => myDeck; set => myDeck = value; }

        /// <summary>
        /// Calls from the GUI class so that the Player objects in the game get an updated hand
        /// When they play a card
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayers(Player player)
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
        /// Play - The main game loop
        /// </summary>
        public static void Play()
        {
            InitialDraw();
        }

        /// <summary>
        /// InitialDraw - Draws the initial 6 cards
        /// </summary>
        public static void InitialDraw()
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
                GUI.MoveCardImage(GUI.opponentGrid, myCard.myImage, i - 6, 0);
                ComputerPlayer.Cards.Add(MyDeck.GetTopCard());
            }

            #region
            // Testing how many cards can be added to the player's hand
            /*
            for (int i = 0; i < 52; i++)
            {
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                
                humanPlayer.myCards.Add(myCard);
                if (i < 8)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i, 0);
                }
                else if (i < 16)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 8, 1);
                }
                else if (i < 24)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 16, 2);
                }
                else if (i < 32)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 24, 3);
                }
                else if (i < 40)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 32, 4);
                }
                else if (i < 48)
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 40, 5);
                }
                else
                {
                    GUI.MoveCardImage(GUI.playerGrid, myImage, i - 48, 6);
                }
            }*/
            #endregion
        }
        /// <summary>
        /// Draws a new card from the deck
        /// Sends over the card and the Player object to the GUI class
        /// Attempted to be able to send all the 
        /// </summary>
        /// <param name="player"></param>
        public static void Draw(HumanPlayer player)
        {
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                GUI.currentCard = myCard;
                System.Diagnostics.Debug.WriteLine("New Card: " + myCard);
                GUI.MoveCardImage(GUI.playerGrid, myCard.myImage, i, 0);
                player.Cards.Add(myCard);
            }
            GUI.currentPlayer = player;
            System.Diagnostics.Debug.WriteLine("Cards: " + player.Cards.Count());
        }

        public static void Draw(ComputerPlayer player)
        {
            GUI.currentPlayer = player;
            for (int i = player.Cards.Count(); i < 6; i++)
            {
                Card myCard = MyDeck.GetTopCard();
                myCard.SetFaceDown();
                GUI.currentCard = myCard;
                GUI.MoveCardImage(GUI.opponentGrid, myCard.myImage, i -6, 0);
                player.Cards.Add(myCard);
            }
        }

        /// <summary>
        /// EndTurn - Called when the player ends his turn
        /// </summary>
        public static void EndTurn()
        {
            // Draw(computerPlayer);    Big boy issues
            GUI.OrderCards();
            Draw(HumanPlayer);
            System.Diagnostics.Debug.WriteLine("Clicked");
        }

    }
}
