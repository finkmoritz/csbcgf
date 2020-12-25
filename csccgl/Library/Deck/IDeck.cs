using System;

namespace csccgl
{
    public interface IDeck
    {
        /// <summary>
        /// Checks if this Deck contains any Cards.
        /// </summary>
        /// <returns>True if this Deck does not contain any Cards.</returns>
        bool IsEmpty();
    }
}
