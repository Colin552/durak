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
        /// Play - The main game loop
        /// </summary>
        public void Play()
        {
            InitialDraw();
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

    }
}
