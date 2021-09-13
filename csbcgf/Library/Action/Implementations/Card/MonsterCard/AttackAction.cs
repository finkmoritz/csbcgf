using System;

namespace csbcgf
{
    public class AttackAction : Action
    {
        public IMonsterCard Attacker;

        public ICharacter Target;

        public AttackAction(IMonsterCard attacker, ICharacter target, bool isAborted = false)
            : base(isAborted)
        {
            Attacker = attacker;
            Target = target;
        }

        public override void Execute(IGame game)
        {
            game.Execute(new ModifyLifeStatAction(Target, -Attacker.AttackValue));
            game.Execute(new ModifyLifeStatAction(Attacker, -Target.AttackValue));
            game.Execute(new ModifyReadyToAttackAction(Attacker, false));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Attacker.IsReadyToAttack
                && Attacker.GetPotentialTargets(gameState).Contains(Target);
        }
    }
}
