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
    }
}
