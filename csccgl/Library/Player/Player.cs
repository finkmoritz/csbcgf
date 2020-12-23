using System;

namespace csccgl
{
    [Serializable]
    public class Player : IPlayer
    {
        public readonly IStackedDeck Deck;
        public readonly IHand Hand;
        public readonly IBoard Board;
        public readonly IStackedDeck Graveyard;

        public Player()
        {
        }

        public ICard DrawCard()
        {
            ICard card = Deck.PopCard();
            Hand.Add(card);
            return card;
        }
    }
}
