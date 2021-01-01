using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyAttackStatAction : IAction
    {
        [JsonProperty]
        protected readonly AttackStat attackStat;

        [JsonProperty]
        protected readonly int delta;

        [JsonConstructor]
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
