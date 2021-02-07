using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyLifeStatAction : Core.Action
    {
        [JsonProperty]
        public IBcgLiving Living;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public BcgModifyLifeStatAction(IBcgLiving living, int delta, bool isAborted = false)
            : base(isAborted)
        {
            Living = living;
            Delta = delta;
        }

        public override object Clone()
        {
            return new BcgModifyLifeStatAction((IBcgLiving)Living.Clone(), Delta, IsAborted);
        }

        public override void Execute(IGame game)
        {
            Living.LifeValue += Delta;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return !(Living is ICardComponent)
                && Living.LifeValue > 0;
        }
    }
}
