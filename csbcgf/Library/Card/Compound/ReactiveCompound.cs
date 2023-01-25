using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class ReactiveCompound : Compound, IReactive
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        protected ReactiveCompound() {}

        public ReactiveCompound(List<ICardComponent> components)
            : this(components, new List<IReaction>())
        {
        }

        protected ReactiveCompound(List<ICardComponent> components, List<IReaction> reactions)
            : base(components)
        {
            this.reactions = reactions;
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

        public virtual void ReactTo(IGame game, IActionEvent actionEvent)
        {
            AllReactions().ForEach(r => r.ReactTo(game, actionEvent));
        }

        public abstract ICard? FindParentCard(IGameState gameState);

        public abstract IPlayer? FindParentPlayer(IGameState gameState);
    }
}
