using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromHandAction : IAction
    {
        [JsonProperty]
        protected readonly IHand hand;

        [JsonProperty]
        protected readonly ICard card;

        [JsonConstructor]
        public RemoveCardFromHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            hand.Remove(card);
        }

        public bool IsExecutable(IGame gameState)
        {
            return hand.Contains(card);
        }
    }
}
