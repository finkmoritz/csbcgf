using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromHandAction : IAction
    {
        [JsonProperty]
        protected IHand hand;

        [JsonProperty]
        protected ICard card;

        public RemoveCardFromHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            hand.Remove(card);
        }

        public bool IsExecutable(IGame game)
        {
            return hand.Contains(card);
        }
    }
}
