using System;
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
            if(character is ICard card && character.LifeValue == LifeStat.GlobalMin)
            {
                game.Queue(new RemoveCardFromBoardAction(card.Owner.Board, (ICard)character));
                game.Queue(new AddCardToGraveyardAction(card.Owner.Graveyard, (ICard)character));
            }
        }

        public bool IsExecutable(IGame game) => character.LifeValue > LifeStat.GlobalMin;
    }
}
