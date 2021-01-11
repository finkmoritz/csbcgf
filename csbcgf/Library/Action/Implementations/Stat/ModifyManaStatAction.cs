using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
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

        public void Execute(IGame game)
        {
            Manaful.ManaBaseValue += DeltaBaseValue;
            Manaful.ManaValue += DeltaValue;
        }

        public bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
