using System;
namespace csccgl {
    public interface IOwnable
    {
        /// <summary>
        /// The Owner of this Object.
        /// </summary>
        IPlayer Owner { get; set; }
    }
}
