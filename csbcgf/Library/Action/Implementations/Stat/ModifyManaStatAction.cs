using System;
namespace csbcgf
{
    [Serializable]
    public class ModifyManaStatAction : IAction
    {
        protected ManaStat manaStat;
        protected int deltaValue;
        protected int deltaMaxValue;

        public ModifyManaStatAction(ManaStat manaStat, int deltaValue, int deltaMaxValue)
        {
            this.manaStat = manaStat;
            this.deltaValue = deltaValue;
            this.deltaMaxValue = deltaMaxValue;
        }

        public void Execute(IGame game)
        {
            manaStat.MaxValue += deltaMaxValue;
            manaStat.Value += deltaValue;
        }

        public bool IsExecutable(IGame game) => true;
    }
}
