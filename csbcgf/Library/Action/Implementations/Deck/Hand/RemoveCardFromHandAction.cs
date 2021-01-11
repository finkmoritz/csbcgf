using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromHandAction : IAction
    {
        [JsonProperty]
        public readonly IHand Hand;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public RemoveCardFromHandAction(IHand hand, ICard card)
        {
            Hand = hand;
            Card = card;
        }

        public void Execute(IGame game)
        {
            Hand.Remove(Card);
        }

        public bool IsExecutable(IGame game)
        {
            return Hand.Contains(Card);
        }
    }
}
