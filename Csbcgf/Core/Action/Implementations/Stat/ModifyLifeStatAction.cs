using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class ModifyLifeStatAction : Action
    {
        [JsonProperty]
        public ILiving Living;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyLifeStatAction(ILiving living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            Living = living;
            Delta = delta;
        }

        public override object Clone()
        {
            return new ModifyLifeStatAction((ILiving)Living.Clone(), Delta, IsAborted);
        }

        public override void Execute(IGame game)
        {
            Living.LifeValue += Delta;
            /*if(Living.LifeValue <= 0)
            {
                if (Living is IMonsterCard monsterCard)
                {
                    game.Execute(new DieAction(monsterCard));
                }
                else if (Living is IPlayer)
                {
                    game.Execute(new EndOfGameEvent());
                }
            }*/ //TODO
            
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
