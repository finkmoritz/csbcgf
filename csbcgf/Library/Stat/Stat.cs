using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Stat : IStat
    {
        public const int GlobalMin = -99;
        public const int GlobalMax = 99;

        [JsonProperty]
        protected int value;

        [JsonProperty]
        protected int baseValue;

        /// <summary>
        /// Represents a Card's property.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="baseValue"></param>
        [JsonConstructor]
        public Stat(int value, int baseValue)
        {
            this.baseValue = baseValue;
            this.value = value;
        }

        [JsonIgnore]
        public virtual int Value {
            get => value;
            set => this.value = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
        }

        [JsonIgnore]
        public virtual int BaseValue
        {
            get => baseValue;
            set => baseValue = Math.Max(GlobalMin, Math.Min(GlobalMax, value));
        }
    }
}
