using System;

namespace csbcgf
{
    public interface ILiving : ICloneable
    {
        int LifeValue { get; set; }
        int LifeBaseValue { get; set; }
    }
}
