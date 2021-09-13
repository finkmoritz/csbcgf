using System;
using System.Collections.Generic;

namespace csbcgf
{
    public abstract class Compound : ICompound
    {
        public List<ICardComponent> Components { get; }

        public Compound(List<ICardComponent> components)
        {
            Components = components;
        }
    }
}
