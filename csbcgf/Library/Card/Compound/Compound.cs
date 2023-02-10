using Newtonsoft.Json;
using System.Collections.Immutable;

namespace csbcgf
{
    public abstract class Compound : StatContainer, ICompound
    {
        [JsonProperty]
        protected List<ICardComponent> components = null!;

        protected Compound() { }

        public Compound(bool _ = true) : base(_)
        {
            this.components = new List<ICardComponent>();
        }

        [JsonIgnore]
        public IEnumerable<ICardComponent> Components
        {
            get => components.ToImmutableList();
        }

        public virtual void AddComponent(ICardComponent cardComponent)
        {
            components.Add(cardComponent);
        }

        public virtual bool RemoveComponent(ICardComponent cardComponent)
        {
            bool wasRemoved = components.Remove(cardComponent);
            if (wasRemoved)
            {
                cardComponent.ParentCard = null;
            }
            return wasRemoved;
        }
    }
}
