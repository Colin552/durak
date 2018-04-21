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
        public int NumberOfCards
        {
            get { return numOfCards; }
            set { numOfCards = value; }
        }
        /// <summary>
        /// Default constructor
        /// Set default cards to 36 ---  ADD FUNCTIONALITY FOR MORE CARDS LATER
        /// </summary>
        public Deck(int numCards = 36)
        {
            NumberOfCards = numCards;
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 6; rankVal < 14; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                }
                cards.Add(new Card((Suit)suitVal, Rank.Ace));
            }
            this.Shuffle();
        }

        /// <summary>
        /// Nondefault constructor. Allows aces to be set high.
        /// </summary>
        public Deck(bool isAceHigh) : this()
        {
            Card.isAceHigh = isAceHigh;
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

        /// <summary>
        /// Draws a card from the deck
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 35)
            {
                if ((cardNum == 35) && (LastCardDrawn != null))
                    LastCardDrawn(this, EventArgs.Empty);
                return cards[cardNum];
            }
            else
            {
                throw new CardOutOfRangeException((Cards)cards.Clone());
            } 
        }

        public Card GetTopCard()
        {
            Card returnedCard = null;
            System.Diagnostics.Debug.WriteLine("Cards left: " + cards.Count());
            if(cards.Count() > 0)
            { 
                returnedCard = cards[cards.Count - 1];
                cards.Remove(returnedCard);
            }
            else
                System.Diagnostics.Debug.WriteLine("No cards left in deck");
            return returnedCard;
        }
        /// <summary>
        /// MADE STATIC 36 NUMBER FOR CARDS SO NO EXCEPTIOSN FAM 100
        /// </summary>
        public void Shuffle()
        {
            Cards newDeck = new Cards();
            bool[] assigned = new bool[36];
            Random sourceGen = new Random();

            for (int i = 0; i < 36; i++)
            {
                int sourceCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    sourceCard = sourceGen.Next(36);
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
