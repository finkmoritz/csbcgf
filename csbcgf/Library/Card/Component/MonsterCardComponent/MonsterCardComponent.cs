using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        public MonsterCardComponent(int mana, int attack, int life) : base(mana)
        {
            attackStat = new AttackStat(attack);
            lifeStat = new LifeStat(life);
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

        [JsonProperty]
        protected AttackStat attackStat;

        [JsonProperty]
        protected LifeStat lifeStat;

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            return new HashSet<ICharacter>(game.NonActivePlayer.Characters);
        }
    }
}
