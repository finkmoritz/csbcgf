using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyAttackStatAction : Action
    {
        [JsonProperty]
        protected IStatContainer attacking = null!;

        [JsonProperty]
        protected int delta;

        protected ModifyAttackStatAction() { }

        public ModifyAttackStatAction(IStatContainer attacking, int delta, bool isAborted = false)
            : base(isAborted)
        {
            this.attacking = attacking;
            this.delta = delta;
        }

        [JsonIgnore]
        public IStatContainer Attacking
        {
            get => attacking;
        }

        [JsonIgnore]
        public int Delta
        {
            get => delta;
            set => delta = value;
        }

        public override void Execute(IGame game)
        {
            Attacking.AddStat(StatKeys.Attack, new Stat(Delta, 0));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return true;
        }
    }
}
