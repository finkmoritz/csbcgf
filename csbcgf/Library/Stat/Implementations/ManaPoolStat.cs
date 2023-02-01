using Newtonsoft.Json;

namespace csbcgf
{
    public class ManaPoolStat : Stat
    {
        protected ManaPoolStat() { }

        /// <summary>
        /// Represents available mana.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue"></param>
        public ManaPoolStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        [JsonIgnore]
        public override int Value
        {
            get => base.Value;
            set => base.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public override int BaseValue
        {
            get => base.BaseValue;
            set => base.BaseValue = Math.Max(0, value);
        }
    }
}
