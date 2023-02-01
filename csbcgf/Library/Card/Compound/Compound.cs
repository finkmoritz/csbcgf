using Newtonsoft.Json;

namespace csbcgf
{
    public abstract class Compound : ICompound
    {
        [JsonProperty]
        protected List<ICardComponent> components = null!;

        protected Compound() {}

        public Compound(bool initialize = true) {
            this.components = new List<ICardComponent>();
        }

        [JsonIgnore]
        public List<ICardComponent> Components {
             get => components;
        }

        public void AddComponent(ICardComponent cardComponent)
        {
            components.Add(cardComponent);
            cardComponent.ParentCard = this;
        }

        public bool RemoveComponent(ICardComponent cardComponent)
        {
            bool wasRemoved = components.Remove(cardComponent);
            if (wasRemoved) {
                cardComponent.ParentCard = null;
            }
            return wasRemoved;
        }
    }
}
