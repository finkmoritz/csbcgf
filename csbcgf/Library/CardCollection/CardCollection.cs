using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public abstract class CardCollection : ICardCollection
    {
        /// <summary>
        /// Abstract representation of a collection of Cards.
        /// </summary>
        public CardCollection()
        {
        }

        public abstract int Size { get; }
        public abstract List<ICard> AllCards { get; }
        public abstract bool IsEmpty { get; }
        public abstract object Clone();
        public abstract bool Contains(ICard card);
    }
}
