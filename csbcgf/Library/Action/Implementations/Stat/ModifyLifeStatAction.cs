using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyLifeStatAction : Action
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

        public override void Execute(IGame game)
        {
            Living.LifeValue += Delta;
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

        public override bool IsExecutable(IGameState gameState)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
