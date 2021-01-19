using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csbcgf
{
    [Serializable]
    public abstract class Compound : ICompound
    {
        public List<ICardComponent> Components { get; }

        [JsonConstructor]
        public Compound(List<ICardComponent> components)
        {
            Components = components;
        }

        public abstract object Clone();
    }
}
