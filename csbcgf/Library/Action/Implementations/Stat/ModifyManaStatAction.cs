using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
    {
        [JsonProperty]
        protected readonly IManaful manaful;

        [JsonProperty]
        protected readonly int deltaValue;

        [JsonProperty]
        protected readonly int deltaBaseValue;

        [JsonConstructor]
        public ModifyManaStatAction(IManaful manaful, int deltaValue, int deltaBaseValue)
        {
            this.manaful = manaful;
            this.deltaValue = deltaValue;
            this.deltaBaseValue = deltaBaseValue;
        }

        public void Execute(IGame game)
        {
            manaful.ManaBaseValue += deltaBaseValue;
            manaful.ManaValue += deltaValue;
        }

        public bool IsExecutable(IGame gameState)
        {
            return true;
        }
    }
}
