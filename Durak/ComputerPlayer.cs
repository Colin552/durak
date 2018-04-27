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

                                if (lowestPlayableCard == null)
                                {
                                    lowestPlayableCard = card;
                                }
                            }
                        }
                    }
                }
                if (playableCards.Count != 0)
                {
                    foreach (Card card in playableCards)
                    {
                        if (lowestPlayableCard != null)
                        {
                            if (card.rank < lowestPlayableCard.rank)
                            {
                                lowestPlayableCard = card;
                            }
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
                if (playableCards.Count == 0)
                {
                    foreach (Card card in Cards)
                    {
                        if (card.suit == trump)
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
                }
            }

            if (lowestPlayableCard == null)
            {
                //Console.WriteLine("Computer can not defend");
                PlayedCard = false;
            }
            else
            {
                PlayedCard = true;
            }

            return lowestPlayableCard;
        }
    }
}
