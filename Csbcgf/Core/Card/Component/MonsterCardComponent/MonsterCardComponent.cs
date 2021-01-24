using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    [Serializable]
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        [JsonProperty]
        protected BcgAttackStat attackStat;

        [JsonProperty]
        protected BcgLifeStat lifeStat;

        public MonsterCardComponent(int mana, int attack, int life)
            : this(mana, new BcgAttackStat(attack), new BcgLifeStat(life))
        {
        }

        public MonsterCardComponent(int manaValue, int manaBaseValue,
            int attackValue, int attackBaseValue, int lifeValue, int lifeBaseValue)
            : base(manaValue, manaBaseValue)
        {
            attackStat = new BcgAttackStat(attackValue, attackBaseValue);
            lifeStat = new BcgLifeStat(lifeValue, lifeBaseValue);
        }

        public MonsterCardComponent(int mana, BcgAttackStat attackStat, BcgLifeStat lifeStat)
            : base(mana)
        {
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonConstructor]
        protected MonsterCardComponent(
            BcgManaCostStat manaCostStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions
            ) : base(manaCostStat, reactions)
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

        public override object Clone()
        {
            List<IReaction> reactionsClone = new List<IReaction>();
            foreach (IReaction reaction in Reactions)
            {
                reactionsClone.Add((IReaction)reaction.Clone());
            }

            return new MonsterCardComponent(
                (BcgManaCostStat)manaCostStat.Clone(),
                (BcgAttackStat)attackStat.Clone(),
                (BcgLifeStat)lifeStat.Clone(),
                reactionsClone
            );
        }

        public virtual HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            throw new NotImplementedException("Please implement the " +
                "GetPotentialTargets method of this MonsterCardComponent!");
        }
        /*{
            HashSet<ICharacter> potentialTargets = new HashSet<ICharacter>();
            foreach (IPlayer player in gameState.NonActivePlayers)
            {
                player.Characters.ForEach(c => potentialTargets.Add(c));
            }
            return potentialTargets;
        }*/ //TODO
    }
}
