using System;
using System.Collections.Generic;

namespace csbcgf
{
    [Serializable]
    public abstract class Card : ICard
    {
        public ManaStat ManaStat { get; }

        public IPlayer Owner { get; set; }

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

        public abstract bool IsPlayable(IGame game);

        public void AddReaction(IReaction reaction)
        {
            Reactions.Add(reaction);
        }

        public void RemoveReaction(IReaction reaction)
        {
            Reactions.Remove(reaction);
        }

        public List<IAction> ReactTo(IAction action)
        {
            List<IAction> actions = new List<IAction>();
            foreach(IReaction reaction in Reactions)
            {
                actions.AddRange(reaction.ReactTo(action));
            }
            return actions;
        }
    }
}
