using System;
using System.Collections.Generic;

namespace Csbcgf.Core
{
    public interface ICompound : ICloneable
    {
        /// <summary>
        /// List of components that this Compound is made of.
        /// </summary>
        List<ICardComponent> Components { get; }
    }
}
