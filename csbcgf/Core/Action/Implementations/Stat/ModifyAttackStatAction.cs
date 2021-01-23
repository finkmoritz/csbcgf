using System;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class ModifyAttackStatAction : Action
    {
        [JsonProperty]
        public IAttacking Attacking;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public ModifyAttackStatAction(IAttacking attacking, int delta, bool isAborted = false)
            : base(isAborted)
        {
            Attacking = attacking;
            Delta = delta;
        }

        public override object Clone()
        {
            return new ModifyAttackStatAction((IAttacking)Attacking.Clone(), Delta, IsAborted);
        }

        public override void Execute(IGame game)
        {
            Attacking.AttackValue += Delta;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
