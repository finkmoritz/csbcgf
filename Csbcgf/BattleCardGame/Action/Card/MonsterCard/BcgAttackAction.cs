using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgAttackAction : Action
    {
        [JsonProperty]
        public IMonsterCard Attacker;

        [JsonProperty]
        public ICharacter Target;

        [JsonConstructor]
        public BcgAttackAction(IMonsterCard attacker, ICharacter target, bool isAborted = false)
            : base(isAborted)
        {
            Attacker = attacker;
            Target = target;
        }

        public override object Clone()
        {
            return new BcgAttackAction(
                (IMonsterCard)Attacker.Clone(),
                (ICharacter)Target.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            game.Execute(new BcgModifyLifeStatAction(Target, -Attacker.AttackValue));
            game.Execute(new BcgModifyLifeStatAction(Attacker, -Target.AttackValue));
            game.Execute(new BcgModifyReadyToAttackAction(Attacker, false));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Attacker.IsReadyToAttack
                && Attacker.GetPotentialTargets(gameState).Contains(Target);
        }
    }
}
