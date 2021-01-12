using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : Action
    {
        [JsonProperty]
        public IManaful Manaful;

        [JsonProperty]
        public int DeltaValue;

        [JsonProperty]
        public int DeltaBaseValue;

        [JsonConstructor]
        public ModifyManaStatAction(IManaful manaful, int deltaValue, int deltaBaseValue)
        {
            Manaful = manaful;
            DeltaValue = deltaValue;
            DeltaBaseValue = deltaBaseValue;
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
