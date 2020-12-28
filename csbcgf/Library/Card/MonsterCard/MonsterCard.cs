using System;
using System.Collections.Generic;
using System.Linq;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public class MonsterCard : Card, IMonsterCard
    {
        public bool IsReadyToAttack { get; set; }
        public bool IsAlive => LifeValue > 0;

        public int AttackValue
        {
            get => attackStat.Value + Components.Sum(c=> ((IMonsterCardComponent)c).AttackValue);
            set => attackStat.Value = value - Components.Sum(c => ((IMonsterCardComponent)c).AttackValue);
        }

        public int AttackBaseValue
        {
            get => attackStat.BaseValue + Components.Sum(c => ((IMonsterCardComponent)c).AttackBaseValue);
            set => attackStat.BaseValue = value - Components.Sum(c => ((IMonsterCardComponent)c).AttackBaseValue);
        }

        public int LifeValue
        {
            get => lifeStat.Value + Components.Sum(c => ((IMonsterCardComponent)c).LifeValue);
            set => lifeStat.Value = value - Components.Sum(c => ((IMonsterCardComponent)c).LifeValue);
        }

        public int LifeBaseValue
        {
            get => lifeStat.BaseValue + Components.Sum(c => ((IMonsterCardComponent)c).LifeBaseValue);
            set => lifeStat.BaseValue = value - Components.Sum(c => ((IMonsterCardComponent)c).LifeBaseValue);
        }

        protected AttackStat attackStat = new AttackStat(0);

        protected LifeStat lifeStat = new LifeStat(0);

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
            game.Queue(new ModifyLifeStatAction(targetCharacter, -this.AttackValue));
            game.Queue(new ModifyLifeStatAction(this, -targetCharacter.AttackValue));
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
    }
}
