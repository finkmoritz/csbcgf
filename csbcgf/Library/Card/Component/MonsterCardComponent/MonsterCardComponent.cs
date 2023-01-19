namespace csbcgf
{
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        protected AttackStat attackStat;

        protected LifeStat lifeStat;

        public MonsterCardComponent(ICard card, int mana, int attack, int life)
            : this(card, mana, new AttackStat(attack), new LifeStat(life))
        {
        }

        public MonsterCardComponent(ICard card, int manaValue, int manaBaseValue,
            int attackValue, int attackBaseValue, int lifeValue, int lifeBaseValue)
            : base(card, manaValue, manaBaseValue)
        {
            attackStat = new AttackStat(attackValue, attackBaseValue);
            lifeStat = new LifeStat(lifeValue, lifeBaseValue);
        }

        public MonsterCardComponent(ICard card, int mana, AttackStat attackStat, LifeStat lifeStat)
            : base(card, mana)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        protected MonsterCardComponent(
            ICard card,
            ManaCostStat manaCostStat,
            AttackStat attackStat,
            LifeStat lifeStat,
            List<IReaction> reactions
            ) : base(card, manaCostStat, reactions)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = value;
        }

        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = value;
        }

        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = value;
        }

        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = value;
        }

        public HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            HashSet<ICharacter> potentialTargets = new HashSet<ICharacter>();
            foreach (IPlayer player in gameState.NonActivePlayers)
            {
                player.Characters.ForEach(c => potentialTargets.Add(c));
            }
            return potentialTargets;
        }
    }
}
