using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class MonsterCard : Card, IMonsterCard
    {
        public AttackStat AttackStat { get; protected set; }
        public LifeStat LifeStat { get; protected set; }

        public bool IsReadyToAttack { get; set; }
        public bool IsAlive => LifeStat.Value > 0;

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="components"></param>
        public MonsterCard(List<IMonsterCardComponent> components)
            : base(components.ConvertAll(c => (ICardComponent)c))
        {
            IsReadyToAttack = false;
            AddReaction(new SetReadyToAttackOnStartOfTurnEventReaction(this));
        }

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="mana"></param>
        /// <param name="attack"></param>
        /// <param name="life"></param>
        public MonsterCard(int mana, int attack, int life)
            : this(new List<IMonsterCardComponent> { new MonsterCardComponent(mana, attack, life) })
        {
        }

        public MonsterCard() : base()
        {
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

        public override bool IsPlayable(IGame game)
        {
            IBoard board = game.ActivePlayer.Board;
            return base.IsPlayable(game)
                    && board.AllCards.Count < board.MaxSize;
        }

        public override void AddComponent(ICardComponent cardComponent)
        {
            base.AddComponent(cardComponent);

            if(AttackStat == null)
            {
                AttackStat = new AttackStat(0);
            }
            if(LifeStat == null)
            {
                LifeStat = new LifeStat(0);
            }

            IMonsterCardComponent component = (IMonsterCardComponent)cardComponent;
            AttackStat.Value += component.AttackStat.Value;
            LifeStat.MaxValue += component.LifeStat.Value;
            LifeStat.Value += component.LifeStat.Value;
        }
    }
}
