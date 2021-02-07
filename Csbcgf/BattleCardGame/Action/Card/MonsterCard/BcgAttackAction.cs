using System;
using Newtonsoft.Json;
using Csbcgf.Core;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgAttackAction : Core.Action
    {
        [JsonProperty]
        public IBcgMonsterCard Attacker;

        [JsonProperty]
        public IBcgCharacter Target;

        [JsonConstructor]
        public BcgAttackAction(IBcgMonsterCard attacker, IBcgCharacter target, bool isAborted = false)
            : base(isAborted)
        {
            Attacker = attacker;
            Target = target;
        }

        public override object Clone()
        {
            return new BcgAttackAction(
                (IBcgMonsterCard)Attacker.Clone(),
                (IBcgCharacter)Target.Clone(),
                IsAborted
            );
        }

        public override void Execute(IGame game)
        {
            game.Execute(new BcgModifyLifeStatAction(Target, -Attacker.AttackValue));
            game.Execute(new BcgModifyLifeStatAction(Attacker, -Target.AttackValue));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Attacker.IsReadyToAttack
                && Attacker.GetPotentialTargets((IBcgGameState)gameState).Contains(Target);
        }
    }
}
