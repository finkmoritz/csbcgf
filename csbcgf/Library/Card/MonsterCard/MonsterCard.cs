using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public class MonsterCard : Card, IMonsterCard
    {
        public bool IsReadyToAttack { get; set; }

        [JsonIgnore]
        public bool IsAlive => LifeValue > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => attackOffsetStat.Value + Components.Sum(c=> ((IMonsterCardComponent)c).AttackValue);
            set => attackOffsetStat.Value = value - Components.Sum(c => ((IMonsterCardComponent)c).AttackValue);
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => attackOffsetStat.BaseValue + Components.Sum(c => ((IMonsterCardComponent)c).AttackBaseValue);
            set => attackOffsetStat.BaseValue = value - Components.Sum(c => ((IMonsterCardComponent)c).AttackBaseValue);
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => lifeOffsetStat.Value + Components.Sum(c => ((IMonsterCardComponent)c).LifeValue);
            set => lifeOffsetStat.Value = value - Components.Sum(c => ((IMonsterCardComponent)c).LifeValue);
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => lifeOffsetStat.BaseValue + Components.Sum(c => ((IMonsterCardComponent)c).LifeBaseValue);
            set => lifeOffsetStat.BaseValue = value - Components.Sum(c => ((IMonsterCardComponent)c).LifeBaseValue);
        }

        [JsonProperty]
        protected AttackStat attackOffsetStat = new AttackStat(0);

        [JsonProperty]
        protected LifeStat lifeOffsetStat = new LifeStat(0);

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

        public void Attack(IGame game, ICharacter target)
        {
            if(!IsReadyToAttack)
            {
                throw new CsbcgfException("Failed to attack with a MonsterCard " +
                    "that is not ready to attack!");
            }
            if(!GetPotentialTargets(game).Contains(target))
            {
                throw new CsbcgfException("Cannot attack a target character " +
                    "that is not specified in the list of potential targets!");
            }

            game.Execute(new List<IAction>
            {
                new StartAttackEvent(this, target),
                new ModifyLifeStatAction(target, -AttackValue),
                new ModifyLifeStatAction(this, -target.AttackValue),
                new ModifyReadyToAttackAction(this, false),
                new EndAttackEvent(this, target)
            });
        }

        public virtual HashSet<ICharacter> GetPotentialTargets(IGame game)
        {
            if (Components.Count == 0)
            {
                return new HashSet<ICharacter>();
            }

            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = ((ITargetful)Components[0]).GetPotentialTargets(game);
            foreach (ICardComponent component in Components)
            {
                potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(game));
            }
            return potentialTargets;
        }

        public override bool IsPlayable(IGame game)
        {
            IBoard board = game.ActivePlayer.Board;
            return base.IsPlayable(game)
                    && board.AllCards.Count < board.MaxSize;
        }
    }
}
