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
        public readonly IDeck deck;

        [JsonConstructor]
        public RemoveCardFromDeckAction(IDeck deck)
        {
            this.deck = deck;
        }

        public override void Execute(IGame game)
        {
            Card = deck.Pop();
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !deck.IsEmpty;
        }
    }
}
