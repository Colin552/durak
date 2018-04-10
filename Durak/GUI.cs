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
        /// MoveCardImage - Moves a card to grid
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
                                        
            }
        }

        public static void RemoveCardImage(Grid removeFromGrid, Image imageToRemove)
        {
            removeFromGrid.Children.Remove(imageToRemove);
        }

    }
}
