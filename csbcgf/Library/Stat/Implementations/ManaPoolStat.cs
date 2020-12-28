﻿using System;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class ManaPoolStat : Stat
    {
        public ManaPoolStat(int value, int baseValue) : base(value, baseValue)
        {
        }

        public override int Value
        {
            get => base.Value;
            set => base.Value = Math.Max(0, value);
        }

        public override int BaseValue
        {
            get => base.BaseValue;
            set => base.BaseValue = Math.Max(0, value);
        }
    }
}