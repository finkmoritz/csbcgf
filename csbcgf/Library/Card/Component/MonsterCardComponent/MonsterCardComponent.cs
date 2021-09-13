using System;
using System.Collections.Generic;

namespace csbcgf
{
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        protected AttackStat attackStat;

        protected LifeStat lifeStat;

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

        protected MonsterCardComponent(
            ManaCostStat manaCostStat,
            AttackStat attackStat,
            LifeStat lifeStat,
            List<IReaction> reactions
            ) : base(manaCostStat, reactions)
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

        public override object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new MonsterCardComponent(
                (ManaCostStat)manaCostStat.Clone(),
                (AttackStat)attackStat.Clone(),
                (LifeStat)lifeStat.Clone(),
                reactionsClone
            );
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
