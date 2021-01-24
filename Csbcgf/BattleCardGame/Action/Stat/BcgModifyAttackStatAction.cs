using System;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgModifyAttackStatAction : Action
    {
        [JsonProperty]
        public IBcgAttacking Attacking;

        [JsonProperty]
        public int Delta;

        [JsonConstructor]
        public BcgModifyAttackStatAction(IBcgAttacking attacking, int delta, bool isAborted = false)
            : base(isAborted)
        {
            Attacking = attacking;
            Delta = delta;
        }

        public override object Clone()
        {
            return new BcgModifyAttackStatAction((IBcgAttacking)Attacking.Clone(), Delta, IsAborted);
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
