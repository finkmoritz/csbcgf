using System.Collections.Generic;

namespace csbcgf
{
    public abstract class Compound
    {
        public List<ICardComponent> Components { get;}

        public Compound(List<ICardComponent> components)
        {
            Components = new List<ICardComponent>();
            components.ForEach(c => AddComponent(c));
        }

        public virtual void AddComponent(ICardComponent cardComponent)
        {
            Components.Add(cardComponent);
        }

        public virtual void RemoveComponent(ICardComponent cardComponent)
        {
            Components.Remove(cardComponent);
        }
    }
}
