using System;
using System.Collections.Generic;

namespace csbcgf
{
    public interface ICompound : ICloneable
    {
        /// <summary>
        /// List of components that this Compound is made of.
        /// </summary>
        List<ICardComponent> Components { get; }
    }
}
