using Newtonsoft.Json;

namespace csbcgf
{
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        [JsonProperty]
        protected AttackStat attackStat = null!;

        [JsonProperty]
        protected LifeStat lifeStat = null!;

        protected MonsterCardComponent() { }

        public MonsterCardComponent(int mana, int attack, int life)
            : this(mana, new AttackStat(attack), new LifeStat(life))
        {
        }

        public MonsterCardComponent(int manaValue, int manaBaseValue,
            int attackValue, int attackBaseValue, int lifeValue, int lifeBaseValue)
            : base(manaValue, manaBaseValue)
        {
            attackStat = new AttackStat(attackValue, attackBaseValue);
            lifeStat = new LifeStat(lifeValue, lifeBaseValue);
        }

        public MonsterCardComponent(int mana, AttackStat attackStat, LifeStat lifeStat)
            : base(mana)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonIgnore]
        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = value;
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = value;
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = value;
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = value;
        }

        public ISet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            ISet<ICharacter> potentialTargets = new HashSet<ICharacter>();
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
    }
}
