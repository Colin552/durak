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
            
            for (int i = 0; i < 6; i++)
            {
                Card myCard = myDeck.GetCard(i);
                Image myImage = myCard.myImage;
                GUI.MoveCardImage(GUI.playerGrid, myImage, i, 0);
                humanPlayer.myCards.Add(myCard);               
            }

            for (int i = 6; i < 12; i++)
            {
                Card myCard = myDeck.GetCard(i);
                myCard.SetFaceDown();
                Image myImage = myCard.myImage;
                GUI.MoveCardImage(GUI.opponentGrid, myImage, i - 6, 0);
                computerPlayer.myCards.Add(myCard);
            }
            
            
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
        }

        /// <summary>
        /// EndTurn - Called when the player ends his turn
        /// </summary>
        public static void EndTurn()
        {

        }

    }
}
