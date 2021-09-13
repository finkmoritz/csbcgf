using System;

namespace csbcgf
{
    public class ModifyAttackStatAction : Action
    {
        public IAttacking Attacking;

        public int Delta;

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
