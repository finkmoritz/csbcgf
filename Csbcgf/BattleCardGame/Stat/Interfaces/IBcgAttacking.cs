using System;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgAttacking : IBcgTargetful, ICloneable
    {
        int AttackValue { get; set; }
        int AttackBaseValue { get; set; }
    }
}
