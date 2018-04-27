/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date: May 2018 - 4/27/2018
 * Description: Human Player Class
 */
using System;

namespace Durak
{
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="numCards"></param>
        public HumanPlayer(String name = "Player", int numCards = 36)
        {
            Name = name;
            Cards = new Cards();
        }

    }
}
