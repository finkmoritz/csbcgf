using System;
namespace csbcgf
{
    public class AddCardToHandAction : IAction
    {
        protected IHand hand;
        protected ICard card;

        public AddCardToHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            hand.Add(card);
        }

        public bool IsExecutable(IGame game)
        {
            return card != null && hand.Size < hand.MaxSize;
        }
    }
}
