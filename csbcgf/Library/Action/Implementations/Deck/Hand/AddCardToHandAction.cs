using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : Action
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

        public override void Execute(IGame game)
        {
            Hand.Add(Card);
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Card != null && Hand.Size < Hand.MaxSize;
        }
    }
}
