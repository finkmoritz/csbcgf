using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyLifeStatAction : IAction
    {
        [JsonProperty]
        protected readonly ICharacter character;

        [JsonProperty]
        protected readonly int delta;

        [JsonConstructor]
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
            if(character.LifeValue <= 0)
            {
                if (character is IMonsterCard monsterCard)
                {
                    game.Execute(new List<IAction>
                    {
                        new RemoveCardFromBoardAction(monsterCard.Owner.Board, monsterCard),
                        new AddCardToGraveyardAction(monsterCard.Owner.Graveyard, monsterCard)
                    });
                } else if (character is IPlayer player)
                {
                    game.Execute(new EndOfGameEvent());
                }
            }
            
        }

        public bool IsExecutable(IGame game)
        {
            return !(character is ICardComponent)
                && character.LifeValue > 0;
        }
    }
}
