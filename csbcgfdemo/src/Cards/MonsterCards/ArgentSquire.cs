using System;
using csbcgf;

namespace csbcgfdemo
{
    public class ArgentSquire : MonsterCard
    {
        public ArgentSquire() : base(1, 1, 1)
        {
            AddReaction(new DivineShield(this));
        }
    }
}
