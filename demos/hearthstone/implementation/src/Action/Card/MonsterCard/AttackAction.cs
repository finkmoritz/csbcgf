using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class AttackAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstoneMonsterCard attacker = null!;

        [JsonProperty]
        protected IStatContainer target = null!;

        protected AttackAction() { }

        public AttackAction(HearthstoneMonsterCard attacker, IStatContainer target, bool isAborted = false)
            : base(isAborted)
        {
            this.attacker = attacker;
            this.target = target;
        }

        [JsonIgnore]
        public HearthstoneMonsterCard Attacker
        {
            get => attacker;
        }

        [JsonIgnore]
        public IStatContainer Target
        {
            get => target;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            game.ExecuteSimultaneously(new List<IAction> {
                new ModifyLifeStatAction<HearthstoneGameState>(Target, -Attacker.GetValue(StatKeys.Attack)),
                new ModifyLifeStatAction<HearthstoneGameState>(Attacker, -Target.GetValue(StatKeys.Attack))
            });
            game.Execute(new ModifyReadyToAttackAction(Attacker, false));
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return Attacker.IsReadyToAttack
                && Attacker.GetPotentialTargets(gameState).Contains(Target);
        }
    }
}
