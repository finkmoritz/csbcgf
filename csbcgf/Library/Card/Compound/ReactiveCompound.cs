using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class ReactiveCompound : Compound, IReactive
    {
        protected List<IReaction> reactions;

        public ReactiveCompound(List<ICardComponent> components)
            : this(components, new List<IReaction>())
        {
        }

        [JsonConstructor]
        protected ReactiveCompound(List<ICardComponent> components, List<IReaction> reactions)
            : base(components)
        {
            this.reactions = reactions;
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

        public virtual void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public virtual void RemoveReaction(IReaction reaction)
        {
            reactions.Remove(reaction);
        }

        public virtual List<IAction> ReactTo(IGame gameState, IActionEvent actionEvent)
        {
            List<IAction> reactions = new List<IAction>();
            Reactions.ForEach(r => reactions.AddRange(r.ReactTo(gameState, actionEvent)));
            return reactions;
        }
    }
}
