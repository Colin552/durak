using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            Name = "Computer";
            Cards = new Cards();
        }
        public override void Attack()
        {

        }
        public override void Defend()
        {

        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trump"></param>
        /// <returns></returns>
        public Card MakeMove(Suit trump)
        {
            Cards cardsToPlay = new Cards();
            Card highestCard = null;
            // Ye i know it doesn't work if there are no trump cards :thinkining:
            // Loops through each card in its hand and finds the highest value one for the trump suit passed
            foreach(Card card in Cards)
            {
                if (card.suit == trump)
                {
                    highestCard = card;
                    cardsToPlay.Add(card);
                }
            }
            foreach(Card card in cardsToPlay)
            {
                //System.Diagnostics.Debug.WriteLine("Cards of suit: " + highestCard.rank + " of " + highestCard.suit);
                if (highestCard.rank > card.rank && highestCard.suit == card.suit)
                    highestCard = card;
                else if(highestCard.rank > card.rank)
                    highestCard = card;
            }
            //System.Diagnostics.Debug.WriteLine("Computer player made a move");
            //System.Diagnostics.Debug.WriteLine("Highest card: " + highestCard.rank + " of " + highestCard.suit);
            return highestCard;
        }*/

        public Card MakeMove(Suit trump, Card cardInPlay = null)
        {
            Cards playableCards = new Cards();
            Card lowestPlayableCard = null;

            //If there is a card in play
            if (cardInPlay != null)
            {
                foreach (Card card in Cards)
                {
                    if (card.suit == cardInPlay.suit)
                    {
                        if (card.rank > cardInPlay.rank)
                        {
                            playableCards.Add(card);
                            if (lowestPlayableCard == null)
                            {
                                lowestPlayableCard = card;
                            }
                        }
                    }
                }
                if (playableCards.Count == 0)
                {
                    foreach (Card card in Cards)
                    {
                        if (card.suit == trump)
                        {
                            if (card.rank > cardInPlay.rank)
                            {
                                playableCards.Add(card);
                            }
                        }
                    }
                }
                if (playableCards.Count != 0)
                {
                    foreach (Card card in playableCards)
                    {
                        if (card.rank < lowestPlayableCard.rank)
                        {
                            lowestPlayableCard = card;
                        }
                    }
                }
                
            }
            else
            {
                //If the computer is attacking
                foreach (Card card in Cards)
                {
                    if (card.suit != trump)
                    {
                        playableCards.Add(card);
                        lowestPlayableCard = card;
                    }
                }
                foreach (Card card in playableCards)
                {
                    if (card.rank < lowestPlayableCard.rank)
                    {
                        lowestPlayableCard = card;
                    }
                }
            }

            if (lowestPlayableCard == null)
            {
                Console.WriteLine("Computer can not defend");
            }

            return lowestPlayableCard;
        }
    }
}
