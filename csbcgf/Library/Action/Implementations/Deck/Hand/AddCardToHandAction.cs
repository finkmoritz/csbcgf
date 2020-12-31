using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : IAction
    {
        [JsonIgnore]
        public ICard Card
        {
            get
            {
                return GetCard();
            }
        }

        [JsonProperty]
        protected IHand hand;

        [JsonProperty]
        protected Func<ICard> GetCard;

        public AddCardToHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            GetCard = () => card;
        }

        public AddCardToHandAction(IHand hand, Func<ICard> getCard)
        {
            this.hand = hand;
            this.GetCard = getCard;
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
