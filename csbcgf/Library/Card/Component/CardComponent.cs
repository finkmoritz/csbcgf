using System;
using System.Collections.Generic;
using csbcgf;

namespace csccgl
{
    [Serializable]
    public abstract class CardComponent : ICardComponent
    {
        public CardComponent(int mana)
        {
            ManaStat = new ManaStat(mana, mana);
            Reactions = new List<IReaction>();
        }

        public ManaStat ManaStat { get;}

        public List<IReaction> Reactions { get; }

        public void AddReaction(IReaction reaction)
        {
            Reactions.Add(reaction);
        }

        public List<IAction> ReactTo(IGame game, IAction action)
        {
            List<IAction> reactions = new List<IAction>();
            Reactions.ForEach(r => reactions.AddRange(r.ReactTo(game, action)));
            return reactions;
        }

        public void RemoveReaction(IReaction reaction)
        {
            Reactions.Remove(reaction);
        }
    }
}
