using Newtonsoft.Json;

namespace csbcgf
{
    public class Stat : IStat
    {
        [JsonProperty]
        protected int value;

        [JsonProperty]
        protected int baseValue;

        [JsonProperty]
        protected int minValue;

        [JsonProperty]
        protected int maxValue;

        protected Stat() { }

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public Stat(int value, int baseValue, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            this.baseValue = baseValue;
            this.value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        [JsonIgnore]
        public virtual int Value
        {
            get => value;
            set => this.value = Math.Max(minValue, Math.Min(maxValue, value));
        }

        [JsonIgnore]
        public virtual int BaseValue
        {
            get => baseValue;
            set => baseValue = Math.Max(minValue, Math.Min(maxValue, value));
        }

        [JsonIgnore]
        public virtual int MinValue
        {
            get => minValue;
            set => this.minValue = value;
        }

        [JsonIgnore]
        public virtual int MaxValue
        {
            get => maxValue;
            set => this.maxValue = value;
        }
    }
}
