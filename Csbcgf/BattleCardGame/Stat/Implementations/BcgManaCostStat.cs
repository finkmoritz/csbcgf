using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame {

    [Serializable]
    public class BcgManaCostStat : BcgStat
    {
        /// <summary>
        /// Costs in Mana.
        /// </summary>
        [JsonConstructor]
        public BcgManaCostStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new BcgManaCostStat(Value, BaseValue);
        }
    }
}
