using System;
namespace csbcgf
{
    [Serializable]
    public class RemoveCardFromDeckAction : IAction
    {
        protected IStackedDeck deck;
        public ICard card;

        public RemoveCardFromDeckAction(IStackedDeck deck)
        {
            this.deck = deck;
        }

        public void Execute(IGame game)
        {
            card = deck.Pop();
        }

        public bool IsExecutable(IGame game)
        {
            return !deck.IsEmpty;
        }
    }
}
