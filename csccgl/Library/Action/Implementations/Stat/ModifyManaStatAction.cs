using System;
namespace csbcgf
{
    public class ModifyManaStatAction : IAction
    {
        protected ManaStat manaStat;
        protected int delta;

        public ModifyManaStatAction(ManaStat manaStat, int delta)
        {
            this.manaStat = manaStat;
            this.delta = delta;
        }

        public void Execute(IGame game)
        {
            manaStat.Value += delta;
        }

        public bool IsExecutable(IGame game) => true;
    }
}
