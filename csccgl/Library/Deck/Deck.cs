using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public abstract class Deck : IDeck
    {
        /// <summary>
        /// Abstract representation of a collection of Cards.
        /// </summary>
        public Deck()
        {
        }

        public abstract int Size { get; }
        public abstract List<ICard> AllCards { get; }
        public abstract bool Contains(ICard card);
        public abstract bool IsEmpty();
    }
}
