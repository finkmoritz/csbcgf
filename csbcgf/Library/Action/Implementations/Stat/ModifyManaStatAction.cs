using System;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
    {
        protected IManaful manaful;
        protected int deltaValue;
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

        public bool IsExecutable(IGame game) => true;
    }
}
