using System;
using System.Collections.Generic;
using System.Linq;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ICard
    {
        public Card(List<ICardComponent> components)
        {
            Components = new List<ICardComponent>();

            components.ForEach(c => AddComponent(c));
        }

        public Card() : this(new List<ICardComponent>())
        {
        }

        public IPlayer Owner { get; set; }

        public List<ICardComponent> Components { get; }

        public List<IReaction> Reactions {
            get
            {
                List<IReaction> reactions = new List<IReaction>();
                reactions.AddRange(this.reactions);
                Components.ForEach(c => reactions.AddRange(c.Reactions));
                return reactions;
            }
        }

        public int ManaValue {
            get => manaCostStat.Value + Components.Sum(c => c.ManaValue);
            set => manaCostStat.Value = value - Components.Sum(c => c.ManaValue);
        }

        public int ManaBaseValue {
            get => manaCostStat.BaseValue + Components.Sum(c => c.ManaBaseValue);
            set => manaCostStat.BaseValue = value - Components.Sum(c => c.ManaBaseValue);
        }

        protected List<IReaction> reactions = new List<IReaction>();

        protected ManaCostStat manaCostStat = new ManaCostStat(0, 0);

        public virtual void AddComponent(ICardComponent cardComponent)
        {
            cardComponent.ParentCard = this;
            Components.Add(cardComponent);
        }

        public virtual bool IsPlayable(IGame game)
        {
            return Owner == game.ActivePlayer
                && ManaValue <= game.ActivePlayer.ManaValue;
        }

        public virtual void RemoveComponent(ICardComponent cardComponent)
        {
            Components.Remove(cardComponent);
            cardComponent.ParentCard = null;
        }

        public virtual void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public virtual void RemoveReaction(IReaction reaction)
        {
            reactions.Remove(reaction);
        }

        public virtual List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            Reactions.ForEach(r => reactions.AddRange(r.ReactTo(game, action)));
            return reactions;
        }
    }
}
