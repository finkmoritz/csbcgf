using csbcgf;

namespace csbcgfdemo
{
    public class ArgentSquire : MonsterCard
    {
        protected ArgentSquire() { }

        public ArgentSquire(bool initialize = true) : base(1, 1, 1)
        {
            AddReaction(new DivineShield());
        }
    }
}
