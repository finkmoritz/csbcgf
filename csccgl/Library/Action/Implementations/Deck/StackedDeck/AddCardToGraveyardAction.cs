using System;
namespace csccgl
{
    public class AddCardToGraveyardAction : IAction
    {
        protected IStackedDeck graveyard;
        protected ICard card;

        public AddCardToGraveyardAction(IStackedDeck graveyard, ICard card)
        {
            this.graveyard = graveyard;
            this.card = card;
        }

        public void Execute(IGame game)
        {
            graveyard.Push(card);
        }

        public bool IsExecutable(IGame game)
        {
            return card != null && !graveyard.Contains(card);
        }
    }
}
