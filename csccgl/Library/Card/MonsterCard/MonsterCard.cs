using System;

namespace csccgl
{
    [Serializable]
    public class MonsterCard : Card, IMonsterCard
    {
        public AttackStat AttackStat { get; }
        public LifeStat LifeStat { get; }

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="mana"></param>
        /// <param name="attack"></param>
        /// <param name="life"></param>
        public MonsterCard(int mana, int attack, int life) : base(mana)
        {
            AttackStat.Value = attack;
            LifeStat.Value = life;
        }
    }
}
