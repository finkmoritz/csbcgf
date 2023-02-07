using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class AttackAction : csbcgf.Action
    {
        [JsonProperty]
        protected IHearthstoneMonsterCard attacker = null!;

        [JsonProperty]
        protected ICharacter target = null!;

        protected AttackAction() { }

        public AttackAction(IHearthstoneMonsterCard attacker, ICharacter target, bool isAborted = false)
            : base(isAborted)
        {
            this.attacker = attacker;
            this.target = target;
        }

        [JsonIgnore]
        public IHearthstoneMonsterCard Attacker
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
            game.ActionQueue.ExecuteSimultaneously(new List<IAction> {
                new ModifyLifeStatAction(Target, -Attacker.AttackValue),
                new ModifyLifeStatAction(Attacker, -Target.AttackValue)
            });
            game.ActionQueue.Execute(new ModifyReadyToAttackAction(Attacker, false));
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return Attacker.IsReadyToAttack
                && Attacker.GetPotentialTargets(gameState).Contains(Target);
        }
    }
}
