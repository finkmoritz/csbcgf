using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class ModifyReadyToAttackAction : csbcgf.Action<HearthstoneGameState>
    {
        [JsonProperty]
        protected HearthstoneMonsterCard monsterCard = null!;

        [JsonProperty]
        protected bool isReadyToAttack;

        protected ModifyReadyToAttackAction() { }

        public ModifyReadyToAttackAction(
            HearthstoneMonsterCard monsterCard,
            bool isReadyToAttack,
            bool isAborted = false
            ) : base(isAborted)
        {
            this.monsterCard = monsterCard;
            this.isReadyToAttack = isReadyToAttack;
        }

        [JsonIgnore]
        public HearthstoneMonsterCard MonsterCard
        {
            get => monsterCard;
        }

        [JsonIgnore]
        public bool IsReadyToAttack
        {
            get => isReadyToAttack;
        }

        public override void Execute(IGame<HearthstoneGameState> game)
        {
            MonsterCard.IsReadyToAttack = IsReadyToAttack;
        }

        public override bool IsExecutable(HearthstoneGameState gameState)
        {
            return MonsterCard.IsReadyToAttack != IsReadyToAttack;
        }
    }
}
