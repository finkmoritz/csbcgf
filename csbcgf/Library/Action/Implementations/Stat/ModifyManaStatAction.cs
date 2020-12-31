﻿using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
    {
        [JsonProperty]
        protected IManaful manaful;

        [JsonProperty]
        protected int deltaValue;

        [JsonProperty]
        protected int deltaBaseValue;

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

        public bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
