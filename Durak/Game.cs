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
        public static Deck myDeck = new Deck();
        public static HumanPlayer humanPlayer = new HumanPlayer();
        public static ComputerPlayer computerPlayer = new ComputerPlayer();

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
            // ****** TO DO ******
            // Add cards to a player object
            // Add cards to an opponent object
            for (int i = 0; i < 6; i++)
            {
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                GUI.MoveCardImage(GUI.playerGrid, myImage, i, true);
                humanPlayer.myCards.Add(myCard);           
            }

            for (int i = 6; i < 12; i++)
            {
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                GUI.MoveCardImage(GUI.opponentGrid, myImage, i - 6, true);
                computerPlayer.myCards.Add(myCard);
            }
        }
    }
}
