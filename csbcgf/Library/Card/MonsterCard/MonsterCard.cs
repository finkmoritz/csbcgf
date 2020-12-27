using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class MonsterCard : Card, IMonsterCard
    {
        public AttackStat AttackStat { get; }
        public LifeStat LifeStat { get; }

        public bool IsReadyToAttack { get; set; }

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

            IsReadyToAttack = false;
            Reactions.Add(new SetReadyToAttackOnStartOfTurnEventReaction(this));
        }

        public void Attack(IGame game, ICharacter targetCharacter)
        {
            game.Queue(new ModifyLifeStatAction(targetCharacter, -this.AttackStat.Value));
            game.Queue(new ModifyLifeStatAction(this, -targetCharacter.AttackStat.Value));
            game.Queue(new SetReadyToAttackAction(this, false));
            game.Process();
        }

        public virtual HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            return new HashSet<ICharacter>(
                (IEnumerable<ICharacter>)game.NonActivePlayer.Board.AllCards
            );
        }

        public bool IsAlive() => LifeStat.Value > 0;

        public override bool IsPlayable(IGame game)
        {
            IBoard board = game.ActivePlayer.Board;
            return base.IsPlayable(game)
                    && board.AllCards.Count < board.MaxSize;
        }
    }
}
