using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class HearthstoneMonsterCard : MonsterCard
    {
        [JsonProperty]
        protected bool isReadyToAttack;

        protected HearthstoneMonsterCard()
        {
        }

        public HearthstoneMonsterCard(int mana, int attack, int life) : base(mana, attack, life)
        {
            this.isReadyToAttack = false;

            AddReaction(new SetReadyToAttackOnStartOfTurnEventReaction(this));
            AddReaction(new DieOnModifyLifeStatActionReaction(this));
        }

        [JsonIgnore]
        public bool IsReadyToAttack
        {
            get => isReadyToAttack;
            set => isReadyToAttack = value;
        }

        public void Attack(HearthstoneGame game, ICharacter target)
        {
            if (!IsReadyToAttack)
            {
                throw new CsbcgfException("Failed to attack with a MonsterCard " +
                    "that is not ready to attack!");
            }

            if (!GetPotentialTargets(game.State).Contains(target))
            {
                throw new CsbcgfException("Cannot attack a target character " +
                    "that is not specified in the list of potential targets!");
            }

            game.Execute(new AttackAction(this, target));
        }

        public override ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            ISet<ICharacter> potentialTargets = base.GetPotentialTargets(gameState);
            foreach (IPlayer player in gameState.NonActivePlayers)
            {
                potentialTargets.Add(player);
                foreach (ICharacter character in player.GetCardCollection(CardCollectionKeys.Board).Cards)
                {
                    potentialTargets.Add(character);
                }
            }
            return potentialTargets;
        }

        public override bool IsSummonable(IGameState gameState)
        {
            return base.IsSummonable(gameState)
                    && Owner != null
                    && Owner.GetCardCollection(CardCollectionKeys.Hand).Contains(this)
                    && !gameState.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsFull;
        }
    }
}
