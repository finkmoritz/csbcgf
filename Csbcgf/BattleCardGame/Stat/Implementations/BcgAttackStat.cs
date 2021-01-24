using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgAttackStat : BcgStat
    {
        /// <summary>
        /// Potential damage to be dealt.
        /// </summary>
        public BcgAttackStat(int value) : this(value, value)
        {
        }

        [JsonConstructor]
        public BcgAttackStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override object Clone()
        {
            return new BcgAttackStat(Value, BaseValue);
        }
    }
}
