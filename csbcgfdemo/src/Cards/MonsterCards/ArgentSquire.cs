using System;
using Csbcgf.Core;

namespace Csbcgf.Coredemo
{
    public class ArgentSquire : MonsterCard
    {
        public ArgentSquire() : base(1, 1, 1)
        {
            Reactions.Add(new DivineShield());
        }
    }
}
