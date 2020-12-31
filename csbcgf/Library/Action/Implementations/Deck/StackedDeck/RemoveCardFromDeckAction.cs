using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromDeckAction : IAction
    {
        [JsonProperty]
        public ICard Card;

        [JsonProperty]
        protected IStackedDeck deck;

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
