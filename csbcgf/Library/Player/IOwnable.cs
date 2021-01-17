using System;

namespace csbcgf {

    public interface IOwnable : ICloneable
    {
        /// <summary>
        /// The Owner of this Object.
        /// </summary>
        IPlayer Owner { get; set; }
    }
}
