using System;
namespace csbcgf
{
    public class RemoveCardFromDeckAction : IAction
    {
        protected IStackedDeck deck;
        public ICard Card { get; protected set; }

        public RemoveCardFromDeckAction(IStackedDeck deck)
        {
            this.deck = deck;
        }

        public void Execute(IGame game)
        {
            Card = deck.Pop();
        }

        public bool IsExecutable(IGame game) => !deck.IsEmpty();
    }
}
