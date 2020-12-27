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
            character.LifeStat.Value += delta;
            if(character is ICard && character.LifeStat.Value == LifeStat.GlobalMin)
            {
                game.Queue(new RemoveCardFromBoardAction(character.Owner.Board, (ICard)character));
                game.Queue(new AddCardToGraveyardAction(character.Owner.Graveyard, (ICard)character));
            }
        }

        public bool IsExecutable(IGame game) => character.LifeStat.Value > LifeStat.GlobalMin;
    }
}
