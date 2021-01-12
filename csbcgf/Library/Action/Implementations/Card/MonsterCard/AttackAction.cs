using System;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class AttackAction : Action
    {
        [JsonProperty]
        public IMonsterCard Attacker;

        [JsonProperty]
        public ICharacter Target;

        [JsonConstructor]
        public AttackAction(IMonsterCard attacker, ICharacter target)
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
