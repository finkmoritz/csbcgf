using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyAttackStatAction : Action
    {
        [JsonProperty]
        protected IAttacking attacking = null!;

        [JsonProperty]
        protected int delta;

        protected ModifyAttackStatAction() {}

        public ModifyAttackStatAction(IAttacking attacking, int delta, bool isAborted = false)
            : base(isAborted)
        {
            this.attacking = attacking;
            this.delta = delta;
        }

        [JsonIgnore]
        public IAttacking Attacking {
            get => attacking;
        }

        [JsonIgnore]
        public int Delta {
            get => delta;
            set => delta = value;
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
