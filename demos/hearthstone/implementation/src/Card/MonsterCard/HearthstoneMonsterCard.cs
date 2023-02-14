using csbcgf;
using Newtonsoft.Json;

namespace hearthstone
{
    public class HearthstoneMonsterCard : Card
    {
        [JsonProperty]
        protected bool isReadyToAttack;

        protected HearthstoneMonsterCard()
        {
        }

        public HearthstoneMonsterCard(int mana, int attack, int life) : base(true)
        {
            this.isReadyToAttack = false;

            AddComponent(new HearthstoneMonsterCardComponent(mana, attack, life));

            AddReaction(new SetReadyToAttackOnStartOfTurnEventReaction(this));
            AddReaction(new DieOnModifyLifeStatActionReaction(this));
        }

        [JsonIgnore]
        public bool IsReadyToAttack
        {
            get => isReadyToAttack;
            set => isReadyToAttack = value;
        }

        public void Attack(HearthstoneGame game, IStatContainer target)
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

        public virtual ISet<IStatContainer> GetPotentialTargets(HearthstoneGameState gameState)
        {
            ISet<IStatContainer> potentialTargets = new HashSet<IStatContainer>();
            foreach (HearthstonePlayer player in gameState.NonActivePlayers)
            {
                potentialTargets.Add(player);
                foreach (HearthstoneMonsterCard card in player.GetCardCollection(CardCollectionKeys.Board).Cards)
                {
                    potentialTargets.Add(card);
                }
            }
            return potentialTargets;
        }

        public virtual bool IsSummonable(HearthstoneGameState gameState)
        {
            HearthstoneGameState state = gameState;
            return Owner != null
                    && Owner.GetCardCollection(CardCollectionKeys.Hand).Contains(this)
                    && Owner == state.ActivePlayer
                    && GetValue(StatKeys.Mana) <= state.ActivePlayer.GetValue(StatKeys.Mana)
                    && !state.ActivePlayer.GetCardCollection(CardCollectionKeys.Board).IsFull;
        }
    }
}
