using System;

namespace csbcgf
{
    public interface IAttacking : ITargetful
    {
        int AttackValue { get; set; }
        int AttackBaseValue { get; set; }
    }
}
