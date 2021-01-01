using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromDeckAction : IAction
    {
        [JsonProperty]
        public ICard Card { get; protected set; }

        [JsonProperty]
        protected readonly IStackedDeck deck;

        [JsonConstructor]
        public RemoveCardFromDeckAction(IStackedDeck deck)
        {
            this.deck = deck;
        }

        public void Execute(IGame game)
        {
            Card = deck.Pop();
        }

        public bool IsExecutable(IGame game)
        {
            return !deck.IsEmpty;
        }
    }
}
