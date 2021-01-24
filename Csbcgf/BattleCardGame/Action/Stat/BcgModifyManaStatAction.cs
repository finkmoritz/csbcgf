using System;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyManaStatAction : Core.Action
    {
        [JsonProperty]
        public IBcgManaful Manaful;

        [JsonProperty]
        public int DeltaValue;

        [JsonProperty]
        public int DeltaBaseValue;

        [JsonConstructor]
        public BcgModifyManaStatAction(
            IBcgManaful manaful,
            int deltaValue,
            int deltaBaseValue,
            bool isAborted = false
            ) : base(isAborted)
        {
            Manaful = manaful;
            DeltaValue = deltaValue;
            DeltaBaseValue = deltaBaseValue;
        }

        public override object Clone()
        {
            return new BcgModifyManaStatAction(
                (IBcgManaful)Manaful.Clone(),
                DeltaValue,
                DeltaBaseValue,
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            Manaful.ManaBaseValue += DeltaBaseValue;
            Manaful.ManaValue += DeltaValue;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
