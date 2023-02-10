using csbcgf;

namespace hearthstone
{
    public class HearthstoneCardComponent : CardComponent
    {
        protected HearthstoneCardComponent() { }

        public HearthstoneCardComponent(int mana)
            : base(true)
        {
            AddStat(StatKeys.Mana, new Stat(mana, mana));
        }
    }
}
