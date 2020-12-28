using System;
using csbcgf;

namespace csccgl
{
    public interface ILiving
    {
        /// <summary>
        /// Maximum damage that can be taken until this dies.
        /// </summary>
        LifeStat LifeStat { get; }
    }
}
