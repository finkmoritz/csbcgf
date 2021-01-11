using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : IAction
    {
        [JsonProperty]
        public readonly IHand Hand;

        [JsonProperty]
        public ICard Card;

        [JsonConstructor]
        public AddCardToHandAction(IHand hand, ICard card)
        {
            Hand = hand;
            Card = card;
        }

        public void Execute(IGame game)
        {
            Hand.Add(Card);
        }

        public bool IsExecutable(IGame game)
        {
            return Card != null && Hand.Size < Hand.MaxSize;
        }
    }
}
