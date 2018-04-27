/*
 * Authors: Calvin Lapp, Colin Strong, Elizabeth Welch
 * Date: May 2018 - 4/27/2018
 * Description: Computer player class
 */
namespace Durak
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer()
        {
            Name = "Computer";
            Cards = new Cards();
        }

        /// <summary>
        /// Decides which move the computer should make.
        /// This decision is based off of the trump suit
        /// and the current card in play
        /// </summary>
        /// <param name="trump"></param>
        /// <param name="cardInPlay"></param>
        /// <returns></returns>
        public Card MakeMove(Suit trump, Card cardInPlay = null)
        {
            Cards playableCards = new Cards();
            Card lowestPlayableCard = null;

            //If there is a card in play and the computer is reacting
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
                            if (card.rank > cardInPlay.rank || cardInPlay.suit != trump)
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
            else //If the computer is attacking
            {
                
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
                            playableCards.Add(card);

                            if (lowestPlayableCard == null)
                            {
                                lowestPlayableCard = card;
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
