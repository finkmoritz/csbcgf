using System;

namespace Csbcgf.Core
{
    public interface IManaful : ICloneable
    {
        int ManaValue { get; set; }
        int ManaBaseValue { get; set; }
    }
}
