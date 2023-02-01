using Newtonsoft.Json;

namespace csbcgf
{
    public class MonsterCard : Card, IMonsterCard
    {
        [JsonProperty]
        protected bool isReadyToAttack;

        protected MonsterCard()
        {
        }

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="mana"></param>
        /// <param name="attack"></param>
        /// <param name="life"></param>
        public MonsterCard(int mana, int attack, int life) : base(true)
        {
            this.isReadyToAttack = false;

            AddComponent(new MonsterCardComponent(mana, attack, life));
            AddReaction(new SetReadyToAttackOnStartOfTurnEventReaction(this));
        }

        [JsonIgnore]
        public bool IsReadyToAttack
        {
            get => isReadyToAttack;
            set => isReadyToAttack = value;
        }

        [JsonIgnore]
        public bool IsAlive => LifeValue > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => Math.Max(0, GetSum(c => c.AttackValue));
            set
            {
                AddComponent(new MonsterCardComponent(0, 0, value - GetSum(c => c.AttackValue), 0, 0, 0));
            }
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => Math.Max(0, GetSum(c => c.AttackBaseValue));
            set
            {
                AddComponent(new MonsterCardComponent(0, 0, 0, value - GetSum(c => c.AttackBaseValue), 0, 0));
            }
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => Math.Max(0, GetSum(c => c.LifeValue));
            set
            {
                AddComponent(new MonsterCardComponent(0, 0, 0, 0, value - GetSum(c => c.LifeValue), 0));
            }
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => Math.Max(0, GetSum(c => c.LifeBaseValue));
            set
            {
                AddComponent(new MonsterCardComponent(0, 0, 0, 0, 0, value - GetSum(c => c.LifeBaseValue)));
            }
        }

        private int GetSum(Func<IMonsterCardComponent, int> GetValue)
        {
            return Components.Where(c => c is IMonsterCardComponent).Sum(c => GetValue((IMonsterCardComponent)c));
        }

        public void Attack(IGame game, ICharacter target)
        {
            if (!IsReadyToAttack)
            {
                throw new CsbcgfException("Failed to attack with a MonsterCard " +
                    "that is not ready to attack!");
            }
            if (!GetPotentialTargets(game).Contains(target))
            {
                throw new CsbcgfException("Cannot attack a target character " +
                    "that is not specified in the list of potential targets!");
            }

            game.Execute(new AttackAction(this, target));
        }

        public virtual ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            if (Components.Count() == 0)
            {
                return new HashSet<ICharacter>();
            }

            //Compute the intersection of all potential targets
            ISet<ICharacter> potentialTargets = ((ITargetful)Components.First()).GetPotentialTargets(gameState);
            foreach (ICardComponent component in Components)
            {
                potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(gameState));
            }
            return potentialTargets;
        }

        public bool IsSummonable(IGameState gameState)
        {
            return base.IsCastable(gameState)
                    && !gameState.ActivePlayer.Board.IsFull;
        }
    }
}
