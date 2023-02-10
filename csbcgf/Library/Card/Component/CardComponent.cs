using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class CardComponent : StatContainer, ICardComponent
    {
        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected ICard? parentCard;

        protected CardComponent() { }

        public CardComponent(bool _ = true) : base(_)
        {
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        [JsonIgnore]
        public ICard? ParentCard
        {
            get
            {
                return parentCard;
            }
            set
            {
                parentCard = value;
            }
        }

        public IEnumerable<IReaction> AllReactions()
        {
            return Reactions;
        }

        public void AddReaction(IReaction reaction)
        {
            reactions.Add(reaction);
        }

        public bool RemoveReaction(IReaction reaction)
        {
            return reactions.Remove(reaction);
        }
    }
}
