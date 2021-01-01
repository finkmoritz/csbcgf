using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : IAction
    {
        [JsonProperty]
        protected readonly IHand hand;

        [JsonProperty]
        protected readonly Func<ICard> GetCard;

        public AddCardToHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            GetCard = () => card;
        }

        [JsonConstructor]
        public AddCardToHandAction(IHand hand, Func<ICard> getCard)
        {
            this.hand = hand;
            this.GetCard = getCard;
        }

        [JsonIgnore]
        public ICard Card
        {
            get
            {
                return GetCard();
            }
        }

        public void Execute(IGame game)
        {
            hand.Add(Card);
        }

        public bool IsExecutable(IGame game)
        {
            return Card != null && hand.Size < hand.MaxSize;
        }
    }
}
