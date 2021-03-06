﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace csbcgf
{
    [Serializable]
    public abstract class CompoundCard : Card, ICompoundCard
    {
        protected List<ICard> Components;

        public override IPlayer Owner
        {
            get => Components[0].Owner;
            set
            {
                Components.ForEach(c => c.Owner = value);
            }
        }

        public CompoundCard(List<ICard> components)
            : base(components.Sum(c => c.ManaStat.Value))
        {
            this.Components = components;
        }

        public CompoundCard(ICard card) : this(new List<ICard> { card })
        {
        }

        public virtual void AddComponent(ICard card)
        {
            if(card is CompoundCard)
            {
                ((CompoundCard)card).Components.ForEach(c => AddComponent(c));
            }
            else
            {
                card.Owner = Owner;
                card.Reactions.ForEach(r => Reactions.Add(r));
                Components.Add(card);
            }
        }

        public override bool IsCastable(IGame game)
        {
            return game.ActivePlayer.Hand.Contains(this)
                && Components.TrueForAll(c => c.IsCastable(game));
        }

        public void RemoveComponent(ICard card)
        {
            if (card is CompoundCard)
            {
                ((CompoundCard)card).Components.ForEach(c => RemoveComponent(c));
            }
            else
            {
                card.Reactions.ForEach(r => Reactions.Remove(r));
                Components.Remove(card);
            }
        }
    }
}
