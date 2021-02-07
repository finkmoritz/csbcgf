using System;
using System.Collections.Generic;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public abstract class BcgPlayer : Player, IBcgPlayer
    {
        [JsonProperty]
        protected BcgManaPoolStat manaPoolStat;

        [JsonProperty]
        protected BcgAttackStat attackStat;

        [JsonProperty]
        protected BcgLifeStat lifeStat;

        public BcgPlayer()
            : this(0, 0, 0)
        {
        }

        public BcgPlayer(int mana, int attack, int life)
            : this(
                  new BcgManaPoolStat(mana, mana),
                  new BcgAttackStat(attack),
                  new BcgLifeStat(life))
        {
        }

        public BcgPlayer(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat
            ) : this(
                manaPoolStat,
                attackStat,
                lifeStat,
                new List<IReaction>(),
                new Dictionary<string, ICardCollection>())
        {
        }

        [JsonConstructor]
        public BcgPlayer(
            BcgManaPoolStat manaPoolStat,
            BcgAttackStat attackStat,
            BcgLifeStat lifeStat,
            List<IReaction> reactions,
            Dictionary<string, ICardCollection> cardCollections
            ) : base(reactions, cardCollections)
        {
            this.manaPoolStat = manaPoolStat;
            this.attackStat = attackStat;
            this.lifeStat = lifeStat;
        }

        [JsonIgnore]
        public bool IsAlive => lifeStat.Value > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => attackStat.Value;
            set => attackStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackStat.BaseValue;
            set => attackStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeStat.Value;
            set => lifeStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeStat.BaseValue;
            set => lifeStat.BaseValue = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaValue
        {
            get => manaPoolStat.Value;
            set => manaPoolStat.Value = Math.Max(0, value);
        }

        [JsonIgnore]
        public int ManaBaseValue
        {
            get => manaPoolStat.BaseValue;
            set => manaPoolStat.BaseValue = Math.Max(0, value);
        }

        public abstract HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState);
    }
}
