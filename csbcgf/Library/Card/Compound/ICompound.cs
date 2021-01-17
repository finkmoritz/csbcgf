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

        /// <summary>
        /// Add a CardComponent to this Compound.
        /// </summary>
        /// <param name="cardComponent"></param>
        void AddComponent(ICardComponent cardComponent);

        /// <summary>
        /// Remove a CardCompound from this CompoundCard.
        /// </summary>
        /// <param name="cardComponent"></param>
        void RemoveComponent(ICardComponent cardComponent);
    }
}
