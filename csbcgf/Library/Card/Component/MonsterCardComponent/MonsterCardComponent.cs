using System;
using System.Collections.Generic;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public class MonsterCardComponent : CardComponent, IMonsterCardComponent
    {
        public MonsterCardComponent(int mana, int attack, int life) : base(mana)
        {
            AttackStat = new AttackStat(attack);
            LifeStat = new LifeStat(life);
        }

        public AttackStat AttackStat { get; }

        public LifeStat LifeStat { get; }

        public HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            HashSet<ICharacter> potTargets = new HashSet<ICharacter>(
                (IEnumerable<ICharacter>)game.NonActivePlayer.Board.AllCards
            );
            potTargets.Add(game.NonActivePlayer);

            return potTargets;
        }
    }
}
