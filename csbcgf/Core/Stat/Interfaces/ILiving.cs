using System;

namespace Csbcgf.Core
{
    public interface ILiving : ICloneable
    {
        int LifeValue { get; set; }
        int LifeBaseValue { get; set; }
    }
}
