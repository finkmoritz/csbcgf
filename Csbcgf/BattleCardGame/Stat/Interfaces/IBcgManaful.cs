using System;

namespace Csbcgf.BattleCardGame
{
    public interface IBcgManaful : ICloneable
    {
        int ManaValue { get; set; }
        int ManaBaseValue { get; set; }
    }
}
