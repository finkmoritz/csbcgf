using System;

namespace csccgl
{
    [Serializable]
    public class Player : IPlayer
    {
        /// <summary>
        /// The Player's Deck of Cards.
        /// </summary>
        public readonly IStackedDeck Deck;

        /// <summary>
        /// The Player's Hand Cards.
        /// </summary>
        public readonly IHand Hand;

        /// <summary>
        /// The Player's Cards on the Board.
        /// </summary>
        public readonly IBoard Board;

        /// <summary>
        /// The Player's Cards that have been removed from the Game.
        /// </summary>
        public readonly IStackedDeck Graveyard;

        /// <summary>
        /// Represents a Player and all his/her associated Cards.
        /// </summary>
        /// <param name="deck"></param>
        public Player(IStackedDeck deck)
        {
            this.Deck = deck;
        }

        public ICard DrawCard()
        {
            ICard card = Deck.PopCard();
            Hand.Add(card);
            return card;
        }
    }
}
