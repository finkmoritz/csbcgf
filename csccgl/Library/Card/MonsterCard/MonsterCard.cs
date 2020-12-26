using System;
using System.Collections.Generic;

namespace csccgl
{
    [Serializable]
    public abstract class MonsterCard : Card, IMonsterCard
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
            AttackStat = new AttackStat(attack);
            LifeStat = new LifeStat(life);
        }

        public void Attack(IGame game, ICharacter targetCharacter)
        {
            game.Queue(new ModifyLifeStatAction(targetCharacter, -this.AttackStat.Value));
            game.Queue(new ModifyLifeStatAction(this, -targetCharacter.AttackStat.Value));
            game.Process();
        }

        public abstract HashSet<ICharacter> GetPotentialTargets(IGame game);

        public bool IsAlive() => LifeStat.Value > 0;
    }
}
