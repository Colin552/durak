/*
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

namespace Durak
{
    public abstract class Player
    {
        private Cards myCards = new Cards();
        private bool isAttacking;
        private string myName;
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

        public abstract void Attack();
        public abstract void Defend();

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
