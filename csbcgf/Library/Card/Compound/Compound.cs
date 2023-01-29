using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class Compound : ICompound
    {
        [JsonProperty]
        protected List<ICardComponent> components = null!;

        protected Compound() {}

        public Compound(List<ICardComponent> components)
        {
            this.components = components;
        }

        [JsonIgnore]
        public List<ICardComponent> Components {
             get => components;
        }

        void ICompound.AddComponent(ICardComponent cardComponent)
        {
            components.Add(cardComponent);
            cardComponent.ParentCard = this;
        }

        bool ICompound.RemoveComponent(ICardComponent cardComponent)
        {
            bool wasRemoved = components.Remove(cardComponent);
            if (wasRemoved) {
                cardComponent.ParentCard = null;
            }
            return wasRemoved;
        }
    }
}
