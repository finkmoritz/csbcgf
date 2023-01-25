using Newtonsoft.Json;

namespace csbcgf
{
    public class AttackAction : Action
    {
        [JsonProperty]
        protected IMonsterCard attacker = null!;

        [JsonProperty]
        protected ICharacter target = null!;

        protected AttackAction() {}

        public AttackAction(IMonsterCard attacker, ICharacter target, bool isAborted = false)
            : base(isAborted)
        {
            this.attacker = attacker;
            this.target = target;
        }

        [JsonIgnore]
        public IMonsterCard Attacker {
            get => attacker;
        }

        [JsonIgnore]
        public ICharacter Target {
            get => target;
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
