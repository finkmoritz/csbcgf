using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class ModifyReadyToAttackAction : csbcgf.Action
    {
        [JsonProperty]
        protected IHearthstoneMonsterCard monsterCard = null!;

        [JsonProperty]
        protected bool isReadyToAttack;

        protected ModifyReadyToAttackAction() { }

        public ModifyReadyToAttackAction(
            IHearthstoneMonsterCard monsterCard,
            bool isReadyToAttack,
            bool isAborted = false
            ) : base(isAborted)
        {
            this.monsterCard = monsterCard;
            this.isReadyToAttack = isReadyToAttack;
        }

        [JsonIgnore]
        public IHearthstoneMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        [JsonIgnore]
        public bool IsReadyToAttack
        {
            get => isReadyToAttack;
        }

        public override void Execute(IGame game)
        {
            MonsterCard.IsReadyToAttack = IsReadyToAttack;
        }

        public override bool IsExecutable(IGameState gameState)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack;
        }
    }
}
