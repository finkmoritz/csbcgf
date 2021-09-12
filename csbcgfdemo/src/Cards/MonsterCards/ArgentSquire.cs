using System;
using csbcgf;

namespace csbcgfdemo
{
    public class ArgentSquire : MonsterCard
    {
        public ArgentSquire() : base(1, 1, 1)
        {
            Reactions.Add(new DivineShield());
        }
    }
}
