using System;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class ModifyLifeStatAction : IAction
    {
        protected ICharacter character;
        protected int delta;

        public ModifyLifeStatAction(ICharacter character, int delta)
        {
            this.character = character;
            this.delta = delta;
        }

        public void Execute(IGame game)
        {
            character.LifeValue += delta;
            if(!(character is ICardComponent) && character.LifeValue < 0)
            {
                character.LifeValue = 0;
            }
            if(character is ICard card && character.LifeValue <= 0)
            {
                game.Queue(new RemoveCardFromBoardAction(card.Owner.Board, (ICard)character));
                game.Queue(new AddCardToGraveyardAction(card.Owner.Graveyard, (ICard)character));
            }
        }

        public bool IsExecutable(IGame game)
        {
            return !(character is ICardComponent)
                && character.LifeValue > 0;
        }
    }
}
