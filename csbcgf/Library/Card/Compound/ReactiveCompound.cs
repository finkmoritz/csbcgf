using Newtonsoft.Json;
using System.Collections.Immutable;

namespace csbcgf
{
    public abstract class ReactiveCompound : Compound, IReactive
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        protected ReactiveCompound() { }

        public ReactiveCompound(bool initialize = true) : base(initialize)
        {
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            foreach (ICardComponent cardComponent in Components)
            {
                allReactions.AddRange(cardComponent.AllReactions());
            }
            return allReactions;
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }

        public virtual void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }
    }
}
