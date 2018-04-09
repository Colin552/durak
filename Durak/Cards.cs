using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Durak
{
    public class Cards : List<Card>, ICloneable
    {
        public object Clone()
        {
            Cards newCards = new Cards();
            foreach (Card sourceCard in this)
            {
                newCards.Add((Card)sourceCard.Clone());
            }
            return newCards;
        }

        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }
    }
}
