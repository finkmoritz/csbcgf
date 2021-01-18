using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class ReactiveCompound : Compound, IReactive
    {
        [JsonProperty]
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
                List<IReaction> allReactions = new List<IReaction>();
                allReactions.AddRange(reactions);
                Components.ForEach(c => allReactions.AddRange(c.Reactions));
                return allReactions;
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

        public virtual void ReactTo(IGame game, IActionEvent actionEvent)
        {
            Reactions.ForEach(r => r.ReactTo(game, actionEvent));
        }

        public abstract object Clone();

        public abstract ICard FindParentCard(IGameState gameState);

        public abstract IPlayer FindParentPlayer(IGameState gameState);
    }
}
