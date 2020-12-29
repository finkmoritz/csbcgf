using System;
namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromDeckAction : IAction
    {
        public ICard Card;

        protected IStackedDeck deck;

        public RemoveCardFromDeckAction(IStackedDeck deck)
        {
            this.deck = deck;
        }

        public void Execute(IGame game)
        {
            Card = deck.Pop();
        }

        public bool IsExecutable(IGame game)
        {
            return !deck.IsEmpty;
        }
    }
}
