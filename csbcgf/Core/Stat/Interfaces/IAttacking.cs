using System;

namespace Csbcgf.Core
{
    public interface IAttacking : ITargetful, ICloneable
    {
        int AttackValue { get; set; }
        int AttackBaseValue { get; set; }
    }
}
