using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class ModifyAttackStatAction : IAction
    {
        [JsonProperty]
        public IAttacking Attacking;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyAttackStatAction(IAttacking attacking, int delta)
        {
            Attacking = attacking;
            Delta = delta;
        }

        public void Execute(IGame game)
        {
            Attacking.AttackValue += Delta;
        }

        public bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
