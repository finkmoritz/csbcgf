using System;
namespace csbcgf
{
    [Serializable]
    public class AddCardToHandAction : IAction
    {
        protected IHand hand;
        protected Func<ICard> cardGenerator;

        public AddCardToHandAction(IHand hand, ICard card)
        {
            this.hand = hand;
            this.cardGenerator = () => card;
        }

        public AddCardToHandAction(IHand hand, Func<ICard> cardGenerator)
        {
            this.hand = hand;
            this.cardGenerator = cardGenerator;
        }

        public void Execute(IGame game)
        {
            hand.Add(cardGenerator());
        }

        public bool IsExecutable(IGame game)
        {
            return cardGenerator() != null && hand.Size < hand.MaxSize;
        }
    }
}
