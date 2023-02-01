using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class ReactiveCompound : Compound, IReactive
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        protected ReactiveCompound() {}

        public ReactiveCompound(bool initialize = true) : base(initialize) {
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public List<IReaction> Reactions {
            get => reactions;
        }

        public List<IReaction> AllReactions()
        {
            List<IReaction> allReactions = new List<IReaction>(Reactions);
            Components.ForEach(c => allReactions.AddRange(c.AllReactions()));
            return allReactions;
        }

        public void AddReaction(IReaction reaction) {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction) {
            return reactions.Remove(reaction);
        }

        public virtual void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }
    }
}
