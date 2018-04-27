using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    public class Deck : ICloneable
    {
        public event EventHandler LastCardDrawn;
        private Cards cards = new Cards();
        private int numOfCards;
        public int NumOfCards { get => numOfCards; set => numOfCards = value; }

        /// <summary>
        /// Default and parameterized constructor which takes the number of cards
        /// the deck is to be played with
        /// </summary>
        /// <param name="numCards">The number of cards to be in the deck</param>
        public Deck(int numCards = 36)
        {
            NumOfCards = numCards;
            if (numCards == 36)
            {
                for (int suitVal = 0; suitVal < 4; suitVal++)
                {
                    for (int rankVal = 5; rankVal < 14; rankVal++)
                    {
                        cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                    }
                }
            }
            else if(numCards == 20)
            {
                for (int suitVal = 0; suitVal < 4; suitVal++)
                {
                    for (int rankVal = 9; rankVal < 14; rankVal++)
                    {
                        cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                    }
                }
            }
            else if(numCards == 52)
            {
                for (int suitVal = 0; suitVal < 4; suitVal++)
                {
                    for (int rankVal = 1; rankVal < 14; rankVal++)
                    {
                        cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                    }
                }
            }
            this.Shuffle();
        }

        /// <summary>
        /// Nondefault cosntructor. Takes a Cards object.
        /// </summary>
        /// <param name="newCards"></param>
        private Deck(Cards newCards)
        {
            cards = newCards;
        }

        /// <summary>
        /// Clones the deck of cards
        /// </summary>
        /// <returns>A deck of cards</returns>
        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }


        public int CardsRemaining()
        {
            return cards.Count;
        }

        /// <summary>
        /// Draws a card from the deck
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= NumOfCards -1)
            {
                if ((cardNum == NumOfCards - 1) && (LastCardDrawn != null))
                    LastCardDrawn(this, EventArgs.Empty);
                return cards[cardNum];
            }
            else
            {
                throw new CardOutOfRangeException((Cards)cards.Clone());
            } 
        }

        /// <summary>
        /// Gets the next card in the deck
        /// </summary>
        /// <returns>Null card object if no cards left, a card object if there are cards</returns>
        public Card GetTopCard()
        {
            Card returnedCard = null;
            //System.Diagnostics.Debug.WriteLine("Cards left: " + cards.Count());
            if (cards.Count() > 0)
            {
                returnedCard = cards[cards.Count - 1];
                cards.Remove(returnedCard);
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("No cards left in deck");
            }

            return returnedCard;
        }
        /// <summary>
        /// Shuffles the cards in the deck
        /// </summary>
        public void Shuffle()
        {
            Cards newDeck = new Cards();
            bool[] assigned = new bool[NumOfCards];
            Random sourceGen = new Random();

            // Loop through the number of cards in the deck
            // and shuffle the cards
            for (int i = 0; i < NumOfCards; i++)
            {
                int sourceCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    sourceCard = sourceGen.Next(NumOfCards);
                    if (assigned[sourceCard] == false)
                    {
                        foundCard = true;
                    }
                }
                assigned[sourceCard] = true;
                newDeck.Add(cards[sourceCard]);
            }
            newDeck.CopyTo(cards);
        }
    }
}
