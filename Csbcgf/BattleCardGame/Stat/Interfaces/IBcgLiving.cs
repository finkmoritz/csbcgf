using System;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgLiving : ICloneable
    {
        int LifeValue { get; set; }
        int LifeBaseValue { get; set; }
    }
}
