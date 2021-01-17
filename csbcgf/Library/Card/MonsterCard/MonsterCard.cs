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

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="components"></param>
        public MonsterCard(List<IMonsterCardComponent> components)
            : this(components, false)
        {
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

        public MonsterCard() : this(new List<IMonsterCardComponent>())
        {
        }

        public MonsterCard(List<IMonsterCardComponent> components, bool isReadyToAttack)
            : this(components, null, isReadyToAttack)
        {
        }

        public MonsterCard(
            List<IMonsterCardComponent> components,
            IPlayer owner,
            bool isReadyToAttack
            ) : this(components.ConvertAll(c => (ICardComponent)c), owner, isReadyToAttack)
        {
        }

        [JsonConstructor]
        public MonsterCard(
            List<ICardComponent> components,
            IPlayer owner,
            bool isReadyToAttack
            ) : base(components, owner)
        {
            IsReadyToAttack = isReadyToAttack;
            AddReaction(new SetReadyToAttackOnStartOfTurnEventReaction(this));
        }

        [JsonIgnore]
        public bool IsAlive => LifeValue > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => Math.Max(0, GetSum(c => c.AttackValue));
            set
            {
                Components.Add(new MonsterCardComponent(0, 0, value - GetSum(c => c.AttackValue), 0, 0, 0));
            }
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => Math.Max(0, GetSum(c => c.AttackBaseValue));
            set
            {
                Components.Add(new MonsterCardComponent(0, 0, 0, value - GetSum(c => c.AttackBaseValue), 0, 0));
            }
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => Math.Max(0, GetSum(c => c.LifeValue));
            set
            {
                Components.Add(new MonsterCardComponent(0, 0, 0, 0, value - GetSum(c => c.LifeValue), 0));
            }
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => Math.Max(0, GetSum(c => c.LifeBaseValue));
            set
            {
                Components.Add(new MonsterCardComponent(0, 0, 0, 0, 0, value - GetSum(c => c.LifeBaseValue)));
            }
        }

        private int GetSum(Func<IMonsterCardComponent, int> GetValue)
        {
            return Components.Where(c => c is IMonsterCardComponent).Sum(c => GetValue((IMonsterCardComponent)c));
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

            game.Execute(new AttackAction(this, target));
        }

        public virtual HashSet<ICharacter> GetPotentialTargets(IGameState gameState)
        {
            if (Components.Count == 0)
            {
                return new HashSet<ICharacter>();
            }

            //Compute the intersection of all potential targets
            HashSet<ICharacter> potentialTargets = ((ITargetful)Components[0]).GetPotentialTargets(gameState);
            foreach (ICardComponent component in Components)
            {
                potentialTargets.IntersectWith(((ITargetful)component).GetPotentialTargets(gameState));
            }
            return potentialTargets;
        }

        public bool IsSummonable(IGameState gameState)
        {
            IBoard board = gameState.ActivePlayer.Board;
            return base.IsCastable(gameState)
                    && board.AllCards.Count < board.MaxSize;
        }

        public override object Clone()
        {
            IMonsterCard clone = new MonsterCard(
                new List<IMonsterCardComponent>(),
                null, // otherwise circular dependencies
                IsReadyToAttack
            );
            foreach (ICardComponent c in Components)
            {
                ICardComponent cc = (ICardComponent)c.Clone();
                cc.ParentCard = clone;
                clone.AddComponent(cc);
            }
            return clone;
        }
    }
}
