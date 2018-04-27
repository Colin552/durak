/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date: May 2018 - 4/27/2018
 * Description: Player Class
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    public abstract class Player
    {
        private bool playedCard = false;
        private bool canPlayCard;
        private Cards myCards = new Cards();
        private bool isAttacking;
        private string myName;
        public bool PlayedCard
        {
            get { return playedCard; }
            set { playedCard = value; }
        }
        public string Name
        {
            get { return myName; }
            set { myName = value; }
        }
        public Cards Cards
        {
            get { return myCards; }
            set { myCards = value; }
        }
        public bool Attacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }

        public bool CanPlayCard
        {
            get { return canPlayCard; }
            set { canPlayCard = value; }
        }
        /// <summary>
        /// Retreives a card from the hand
        /// </summary>
        /// <param name="mySuit"></param>
        /// <param name="myRank"></param>
        /// <returns></returns>
        public Card GetCard(Suit mySuit, Rank myRank)
        {
            Card aCard = new Card(mySuit, myRank);
            foreach (Card card in myCards)
            {
                if (card.suit == mySuit && card.rank == myRank)
                {
                    return card;
                }
            }
            return aCard;
        }
    }
}
