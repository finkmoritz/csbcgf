namespace csccgl
{
    public interface IPlayer
    {
        /// <summary>
        /// Draw a Card from the Deck and add it to the Hand.
        /// </summary>
        /// <returns>The drawn Card or null if no Card was drawn.</returns>
        ICard DrawCard();
    }
}
