using System;

namespace csbcgf
{
    public interface IManaful : ICloneable
    {
        int ManaValue { get; set; }
        int ManaBaseValue { get; set; }
    }
}
