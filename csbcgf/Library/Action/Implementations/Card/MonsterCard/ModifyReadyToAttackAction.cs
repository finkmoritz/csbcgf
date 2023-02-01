using Newtonsoft.Json;

namespace csbcgf
{
    public class ModifyReadyToAttackAction : Action
    {
        [JsonProperty]
        protected IMonsterCard monsterCard = null!;

        [JsonProperty]
        protected bool isReadyToAttack;

        protected ModifyReadyToAttackAction() { }

        public ModifyReadyToAttackAction(
            IMonsterCard monsterCard,
            bool isReadyToAttack,
            bool isAborted = false
            ) : base(isAborted)
        {
            this.monsterCard = monsterCard;
            this.isReadyToAttack = isReadyToAttack;
        }

        [JsonIgnore]
        public IMonsterCard MonsterCard
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
            return MonsterCard.IsReadyToAttack != IsReadyToAttack
                && gameState.CardsOnTheBoard.Contains(MonsterCard);
        }
    }
}
