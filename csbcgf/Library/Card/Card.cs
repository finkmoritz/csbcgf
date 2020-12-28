using System;
using System.Collections.Generic;
using csccgl;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ICard
    {
        public Card(List<ICardComponent> components)
        {
            Components = new List<ICardComponent>();
            Reactions = new List<IReaction>();

            components.ForEach(c => AddComponent(c));
        }

        public Card() : this(new List<ICardComponent>())
        {
        }

        public IPlayer Owner { get; set; }

        public List<ICardComponent> Components { get; }

        public ManaStat ManaStat { get; protected set; }

        public List<IReaction> Reactions { get; }

        public virtual void AddComponent(ICardComponent cardComponent)
        {
            if(ManaStat == null)
            {
                ManaStat = new ManaStat(0, ManaStat.GlobalMax);
            }
            ManaStat.Value += cardComponent.ManaStat.Value;

            cardComponent.Reactions.ForEach(r => AddReaction(r));

            Components.Add(cardComponent);
        }

        public virtual bool IsPlayable(IGame game)
        {
            return Owner == game.ActivePlayer
                && this.ManaStat.Value <= game.ActivePlayer.ManaStat.Value;
        }

        public virtual void RemoveComponent(ICardComponent cardComponent)
        {
            ManaStat.Value -= cardComponent.ManaStat.Value;
            cardComponent.Reactions.ForEach(r => RemoveReaction(r));
            Components.Remove(cardComponent);
        }

        public virtual void AddReaction(IReaction reaction)
        {
            Reactions.Add(reaction);
        }

        public virtual void RemoveReaction(IReaction reaction)
        {
            Reactions.Remove(reaction);
        }

        public virtual List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            Reactions.ForEach(r => reactions.AddRange(r.ReactTo(game, action)));
            return reactions;
        }
    }
}
