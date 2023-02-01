using System.Collections.Immutable;
using Newtonsoft.Json;

namespace csbcgf
{
    public class CardComponent : ICardComponent
    {
        [JsonProperty]
        protected ManaCostStat manaCostStat = null!;

        [JsonProperty]
        protected List<IReaction> reactions = null!;

        [JsonProperty]
        protected ICompound? parentCard;

        protected CardComponent() { }

        public CardComponent(int mana)
            : this(new ManaCostStat(mana, mana))
        {
        }

        public CardComponent(int manaValue, int manaBaseValue)
            : this(new ManaCostStat(manaValue, manaBaseValue))
        {
        }

        protected CardComponent(ManaCostStat manaCostStat)
        {
            this.manaCostStat = manaCostStat;
            this.reactions = new List<IReaction>();
        }

        [JsonIgnore]
        public IEnumerable<IReaction> Reactions
        {
            get => reactions.ToImmutableList();
        }

        [JsonIgnore]
        public ICompound? ParentCard
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

        [JsonIgnore]
        public int ManaValue
        {
            get => manaCostStat.Value;
            set => manaCostStat.Value = value;
        }

        [JsonIgnore]
        public int ManaBaseValue
        {
            get => manaCostStat.BaseValue;
            set => manaCostStat.BaseValue = value;
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
