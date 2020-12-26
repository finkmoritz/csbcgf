using System;
namespace csccgl
{
    public class RemoveCardFromHandAction : IAction
    {
        protected IHand hand;
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

        public bool IsExecutable(IGame game) => hand.Contains(card);
    }
}
