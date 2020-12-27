using System;
using System.Collections.Generic;
using System.Linq;

namespace csbcgf
{
    public abstract class CompoundCard : Card, ICompoundCard
    {
        protected List<ICard> Components;

        public CompoundCard(List<ICard> components)
            : base(components.Sum(c => c.ManaStat.Value))
        {
            if(components.Count == 0)
            {
                throw new csbcgfException("Parameter components cannot be empty!");
            }
            this.Components = components;
        }

        public CompoundCard(ICard card) : this(new List<ICard> { card })
        {
        }

        override public List<IReaction> Reactions
        {
            get
            {
                List<IReaction> reactions = new List<IReaction>();
                Components.ForEach(c => reactions.AddRange(c.Reactions));
                return reactions;
            }
        }

        public void AddComponent(ICard card)
        {
            if(card is CompoundCard)
            {
                ((CompoundCard)card).Components.ForEach(c => AddComponent(c));
            }
            else
            {
                Components.Add(card);
            }
        }

        override public bool IsPlayable(IGame game)
        {
            return Components.TrueForAll(c => c.IsPlayable(game));
        }

        public void RemoveComponent(ICard card)
        {
            if (card is CompoundCard)
            {
                ((CompoundCard)card).Components.ForEach(c => RemoveComponent(c));
            }
            else
            {
                Components.Remove(card);
            }
        }
    }
}
