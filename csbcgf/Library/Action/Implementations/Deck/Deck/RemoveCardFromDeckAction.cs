using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromDeckAction : Action
    {
        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        public readonly IDeck Deck;

        [JsonConstructor]
        public RemoveCardFromDeckAction(IDeck deck, ICard card = null, bool isAborted = false)
            : base(isAborted)
        {
            Deck = deck;
            Card = card;
        }

        public override void Execute(IGame game)
        {
            Card = Deck.Pop();
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !Deck.IsEmpty;
        }

        public override object Clone()
        {
            return new RemoveCardFromDeckAction(
                (IDeck)Deck.Clone(),
                (ICard)Card.Clone(),
                IsAborted
            );
        }
    }
}
