using Newtonsoft.Json;
using System.Collections.Immutable;

namespace csbcgf
{
    public abstract class ReactiveCompound : Compound, IReactive
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        protected ReactiveCompound() { }

        public ReactiveCompound(bool _ = true) : base(_)
        {
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        public IEnumerable<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (ICardComponent cardComponent in Components)
            {
                allReactions.AddRange(cardComponent.AllReactions());
            }
            return allReactions.ToImmutableList();
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }

        public virtual void ReactBefore(IGame game, IAction action)
        {
            foreach (IReaction reaction in AllReactions())
            {
                reaction.ReactBefore(game, action);
            }
        }

        public virtual void ReactAfter(IGame game, IAction action)
        {
            foreach (IReaction reaction in AllReactions())
            {
                reaction.ReactAfter(game, action);
            }
        }

        public override int GetValue(string key)
        {
            return base.GetValue(key) + Components.Select(c => c.GetValue(key)).Sum();
        }

        public override int GetBaseValue(string key)
        {
            return base.GetBaseValue(key) + Components.Select(c => c.GetBaseValue(key)).Sum();
        }
    }
}
