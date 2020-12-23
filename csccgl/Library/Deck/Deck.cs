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
    }
}
