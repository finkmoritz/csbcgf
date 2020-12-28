using System;
using csbcgf;

namespace csccgl
{
    public interface IAttacking : ITargetful
    {
        /// <summary>
        /// Damage that is dealt to an enemy in battle.
        /// </summary>
        AttackStat AttackStat { get; }
    }
}
