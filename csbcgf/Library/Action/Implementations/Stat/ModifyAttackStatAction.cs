using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyAttackStatAction : IAction
    {
        [JsonProperty]
        protected AttackStat attackStat;

        [JsonProperty]
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

        public bool IsExecutable(IGame game)
        {
            return true;
        }
    }
}
