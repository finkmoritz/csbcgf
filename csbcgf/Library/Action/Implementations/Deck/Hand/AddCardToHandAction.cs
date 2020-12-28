using System;
namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : IAction
    {
        protected IHand hand;
        protected Func<ICard> getCard;

        public AddCardToHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            this.getCard = () => card;
        }

        public AddCardToHandAction(IHand hand, Func<ICard> getCard)
        {
            this.hand = hand;
            this.getCard = getCard;
        }

        public void Execute(IGame game)
        {
            hand.Add(getCard());
        }

        public bool IsExecutable(IGame game)
        {
            return getCard() != null && hand.Size < hand.MaxSize;
        }
    }
}
