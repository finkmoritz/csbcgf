using csbcgf;

namespace hearthstone
{
    public class ArgentSquire : HearthstoneMonsterCard
    {
        protected ArgentSquire() { }

        public ArgentSquire(bool _ = true) : base(1, 1, 1)
        {
            AddReaction(new DivineShield());
        }
    }
}
