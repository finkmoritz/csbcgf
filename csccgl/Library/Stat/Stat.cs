using System;

namespace csccgl
{
    [Serializable]
    public class Stat : IStat
    {
        protected int MinValue;
        protected int MaxValue;

        public Stat(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int Value {
            get => Value;
            set {
                Value = Math.Max(MinValue, Math.Min(MaxValue, value));
            }
        }
    }
}
