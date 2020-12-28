using System;
using csbcgf;

namespace csccgl
{
    public interface IManaful
    {
        /// <summary>
        /// Costs to pay in order for the Card to be played.
        /// </summary>
        ManaStat ManaStat { get; }
    }
}
