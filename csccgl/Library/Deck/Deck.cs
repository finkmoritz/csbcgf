using System;

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

        public abstract bool Contains(ICard card);
        public abstract bool IsEmpty();
    }
}
