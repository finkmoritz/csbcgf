using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyLifeStatAction : IAction
    {
        [JsonProperty]
        public ILiving Living;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyLifeStatAction(ILiving living, int delta)
        {
            Living = living;
            Delta = delta;
        }

        public void Execute(IGame game)
        {
            Living.LifeValue += Delta;
            if(!(Living is ICardComponent) && Living.LifeValue < 0)
            {
                Living.LifeValue = 0;
            }
            if(Living.LifeValue <= 0)
            {
                if (Living is IMonsterCard monsterCard)
                {
                    game.Execute(new RemoveCardFromBoardAction(monsterCard.Owner.Board, monsterCard));
                    game.Execute(new AddCardToGraveyardAction(monsterCard.Owner.Graveyard, monsterCard));
                } else if (Living is IPlayer)
                {
                    game.Execute(new EndOfGameEvent());
                }
            }
            
        }

        public bool IsExecutable(IGame game)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
