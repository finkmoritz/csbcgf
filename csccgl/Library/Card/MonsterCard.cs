using System;

namespace csccgl
{
    [Serializable]
    public class MonsterCard : Card
    {
        public AttackStat AttackStat { get; }
        public LifeStat LifeStat { get; }

        public MonsterCard(int mana, int attack, int life) : base(mana)
        {
            AttackStat.Value = attack;
            LifeStat.Value = life;
        }
    }
}
