﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        [JsonProperty]
        protected AttackStat attackStat;

        [JsonProperty]
        protected LifeStat lifeStat;

        public MonsterCardComponent(int mana, int attack, int life)
            : this(mana, new AttackStat(attack), new LifeStat(life))
        {
        }

        [JsonConstructor]
        protected MonsterCardComponent(int mana, AttackStat attackStat, LifeStat lifeStat)
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

        public HashSet<ICharacter> GetPotentialTargets(IGame gameState)
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
