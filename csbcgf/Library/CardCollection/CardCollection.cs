using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class CardCollection : List<ICard>, ICardCollection
    {
        /// <summary>
        /// Represents a collection of Cards.
        /// </summary>
        public CardCollection()
            : this(new List<ICard>())
        {
        }

        /// <summary>
        /// Represents a collection of Cards.
        /// </summary>
        [JsonConstructor]
        public CardCollection(List<ICard> cards)
            : base(cards)
        {
        }

        [JsonIgnore]
        public List<ICard> AllCards
        {
            get
            {
                return new List<ICard>(this);
            }
        }

        [JsonIgnore]
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        public object Clone()
        {
            List<ICard> cardsClone = new List<ICard>();
            foreach (ICard card in this)
            {
                cardsClone.Add((ICard)card.Clone());
            }
            return new CardCollection(cardsClone);
        }
    }
}
