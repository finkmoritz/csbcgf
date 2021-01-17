using System;

namespace csbcgf
{
    public interface IAttacking : ITargetful, ICloneable
    {
        int AttackValue { get; set; }
        int AttackBaseValue { get; set; }
    }
}
