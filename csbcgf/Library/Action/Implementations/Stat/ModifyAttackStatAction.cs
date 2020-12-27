using System;
namespace csbcgf
{
    [Serializable]
    public class ModifyAttackStatAction : IAction
    {
        protected AttackStat attackStat;
        protected int delta;

        public ModifyAttackStatAction(AttackStat attackStat, int delta)
        {
            this.attackStat = attackStat;
            this.delta = delta;
        }

        public void Execute(IGame game)
        {
            attackStat.Value += delta;
        }

        public bool IsExecutable(IGame game) => true;
    }
}
