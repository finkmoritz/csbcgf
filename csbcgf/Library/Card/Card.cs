using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ICard
    {
        public ManaStat ManaStat { get; protected set; }

        public virtual IPlayer Owner { get; set; }

        public virtual List<IReaction> Reactions { get; }

        /// <summary>
        /// Abstract class to represent a Card.
        /// </summary>
        /// <param name="mana">Initial value for the ManaStat.</param>
        public Card(int mana)
        {
            ManaStat = new ManaStat(mana, 99);
            Reactions = new List<IReaction>();
        }

        public virtual bool IsPlayable(IGame game)
        {
            return Owner == game.ActivePlayer
                && ManaStat.Value <= Owner.ManaStat.Value;
        }

        public void AddReaction(IReaction reaction)
        {
            Reactions.Add(reaction);
        }

        public void RemoveReaction(IReaction reaction)
        {
            Reactions.Remove(reaction);
        }

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> actions = new List<IAction>();
            foreach(IReaction reaction in Reactions)
            {
                actions.AddRange(reaction.ReactTo(game, action));
            }
            return actions;
        }
    }
}
