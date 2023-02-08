using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class AttackAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstoneMonsterCard attacker = null!;

        [JsonProperty]
        protected ICharacter target = null!;

        protected AttackAction() { }

        public AttackAction(HearthstoneMonsterCard attacker, ICharacter target, bool isAborted = false)
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
        public ICharacter Target
        {
            get => target;
        }

        public override void Execute(IGame game)
        {
            game.ExecuteSimultaneously(new List<IAction> {
                new ModifyLifeStatAction(Target, -Attacker.AttackValue),
                new ModifyLifeStatAction(Attacker, -Target.AttackValue)
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
