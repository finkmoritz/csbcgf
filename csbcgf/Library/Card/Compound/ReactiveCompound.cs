using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class ReactiveCompound : Compound, IReactive
    {
        public ReactiveCompound(List<ICardComponent> components)
            : base(components)
        {
        }

        [JsonIgnore]
        public List<IReaction> Reactions
        {
            get
            {
                List<IReaction> reactions = new List<IReaction>();
                reactions.AddRange(this.reactions);
                Components.ForEach(c => reactions.AddRange(c.Reactions));
                return reactions;
            }
        }

        protected List<IReaction> reactions = new List<IReaction>();

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
