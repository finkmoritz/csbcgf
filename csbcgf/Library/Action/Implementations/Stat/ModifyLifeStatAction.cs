using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyLifeStatAction : IAction
    {
        [JsonProperty]
        public ICharacter Character;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyLifeStatAction(ICharacter character, int delta)
        {
            Character = character;
            Delta = delta;
        }

        public void Execute(IGame game)
        {
            Character.LifeValue += Delta;
            if(!(Character is ICardComponent) && Character.LifeValue < 0)
            {
                Character.LifeValue = 0;
            }
            if(Character.LifeValue <= 0)
            {
                if (Character is IMonsterCard monsterCard)
                {
                    game.Execute(new List<IAction>
                    {
                        new RemoveCardFromBoardAction(monsterCard.Owner.Board, monsterCard),
                        new AddCardToGraveyardAction(monsterCard.Owner.Graveyard, monsterCard)
                    });
                } else if (Character is IPlayer player)
                {
                    game.Execute(new EndOfGameEvent());
                }
            }
            
        }

        public bool IsExecutable(IGame gameState)
        {
            return !(Character is ICardComponent)
                && Character.LifeValue > 0;
        }
    }
}
