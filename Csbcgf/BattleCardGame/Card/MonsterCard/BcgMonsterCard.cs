using System;
using System.Collections.Generic;
using System.Linq;
using Csbcgf.Core;
using Newtonsoft.Json;

namespace Csbcgf.BattleCardGame
{
    [Serializable]
    public class BcgMonsterCard : BcgCard, IBcgMonsterCard
    {
        public bool IsReadyToAttack { get; set; }

        public BcgMonsterCard()
            : this(new List<IBcgMonsterCardComponent>())
        {
        }

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="mana"></param>
        /// <param name="attack"></param>
        /// <param name="life"></param>
        public BcgMonsterCard(int mana, int attack, int life)
            : this(new List<IBcgMonsterCardComponent> { new BcgMonsterCardComponent(mana, attack, life) })
        {
        }

        /// <summary>
        /// Represents a certain type of Card that is played
        /// onto the Player's Board.
        /// </summary>
        /// <param name="components"></param>
        public BcgMonsterCard(List<IBcgMonsterCardComponent> components)
            : this(components, false)
        {
        }

        public BcgMonsterCard(
            List<IBcgMonsterCardComponent> components,
            bool isReadyToAttack
            ) : this(components.ConvertAll(c => (ICardComponent)c), new List<IReaction>(), isReadyToAttack)
        {
        }

        [JsonConstructor]
        public BcgMonsterCard(
            List<ICardComponent> components,
            List<IReaction> reactions,
            bool isReadyToAttack
            ) : base(components, reactions)
        {
            IsReadyToAttack = isReadyToAttack;
        }

        [JsonIgnore]
        public bool IsAlive => LifeValue > 0;

        [JsonIgnore]
        public int AttackValue
        {
            get => Math.Max(0, GetSum(c => c.AttackValue));
            set
            {
                Components.Add(new BcgMonsterCardComponent(0, 0, value - GetSum(c => c.AttackValue), 0, 0, 0));
            }
        }

        [JsonIgnore]
        public int AttackBaseValue
        {
            get => Math.Max(0, GetSum(c => c.AttackBaseValue));
            set
            {
                Components.Add(new BcgMonsterCardComponent(0, 0, 0, value - GetSum(c => c.AttackBaseValue), 0, 0));
            }
        }

        [JsonIgnore]
        public int LifeValue
        {
            get => Math.Max(0, GetSum(c => c.LifeValue));
            set
            {
                Components.Add(new BcgMonsterCardComponent(0, 0, 0, 0, value - GetSum(c => c.LifeValue), 0));
            }
        }

        [JsonIgnore]
        public int LifeBaseValue
        {
            get => Math.Max(0, GetSum(c => c.LifeBaseValue));
            set
            {
                Components.Add(new BcgMonsterCardComponent(0, 0, 0, 0, 0, value - GetSum(c => c.LifeBaseValue)));
            }
        }

        private int GetSum(Func<IBcgMonsterCardComponent, int> GetValue)
        {
            return Components.Where(c => c is IBcgMonsterCardComponent).Sum(c => GetValue((IBcgMonsterCardComponent)c));
        }

        public virtual void Attack(IBcgGame game, IBcgCharacter target)
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

            game.Execute(new BcgAttackAction(this, target));
        }

        public virtual HashSet<IBcgCharacter> GetPotentialTargets(IBcgGameState gameState)
        {
            if (Components.Count == 0)
            {
                return new HashSet<IBcgCharacter>();
            }

            //Compute the intersection of all potential targets
            HashSet<IBcgCharacter> potentialTargets = ((IBcgTargetful)Components[0]).GetPotentialTargets(gameState);
            foreach (IBcgCardComponent component in Components)
            {
                potentialTargets.IntersectWith(((IBcgTargetful)component).GetPotentialTargets(gameState));
            }
            return potentialTargets;
        }

        public bool IsSummonable(IBcgGameState gameState)
        {
            return base.IsCastable(gameState);
        }

        public override object Clone()
        {
            List<ICardComponent> componentsClone = new List<ICardComponent>();
            Components.ForEach(c => componentsClone.Add((ICardComponent)c.Clone()));

            List<IReaction> reactionsClone = new List<IReaction>();
            Reactions.ForEach(r => reactionsClone.Add((IReaction)r.Clone()));

            return new BcgMonsterCard(
                componentsClone,
                reactionsClone,
                IsReadyToAttack
            );
        }
    }
}
