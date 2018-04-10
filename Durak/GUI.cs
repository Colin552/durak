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
    /// Static class contains all of the functions for interacting with the main window GUI.
    /// </summary>
    public static class GUI
    {

        public static Grid playerGrid;
        public static Grid opponentGrid;
        public static Grid centerGrid;

        private static int topMargin = -70;
        private static int bottomMargin = 50;

        /// <summary>
        /// MoveCardImage - Moves a card to a grid
        /// </summary>
        /// <param name="toGrid">The grid the card should be moved to</param>
        /// <param name="imageToMove">The card image to move</param>
        /// <param name="gridColumn">The column of the grid</param>
        /// <param name="isTopCard">True for top, false for bottom</param>
        public static void MoveCardImage(Grid toGrid, Image imageToMove, int gridColumn, bool isTopCard)
        {
            //Check if the grid already contains the card
            if (!toGrid.Children.Contains(imageToMove))
            {
                toGrid.Children.Add(imageToMove);
                imageToMove.SetValue(Grid.ColumnProperty, gridColumn);

                // Put it on the top or bottow "row" 
                if (isTopCard)
                {
                    Thickness cardMargin = new Thickness(0, topMargin, 0, 0);
                    imageToMove.SetValue(Grid.MarginProperty, cardMargin);
                }
                else
                {
                    Thickness cardMargin = new Thickness(0, bottomMargin, 0, 0);
                    imageToMove.SetValue(Grid.MarginProperty, cardMargin);
                }

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
        public static void RemoveCardImage(Grid removeFromGrid, Image imageToRemove)
        {
            removeFromGrid.Children.Remove(imageToRemove);
        }

        /// <summary>
        /// Card_MouseMove - Event handler for the Card's drag and drop functionality
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Card_MouseMove(object sender, MouseEventArgs e)
        {

            Image cardImage = sender as Image;
            if (cardImage != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(cardImage, cardImage, DragDropEffects.Move);
            }

        }
    }
}
