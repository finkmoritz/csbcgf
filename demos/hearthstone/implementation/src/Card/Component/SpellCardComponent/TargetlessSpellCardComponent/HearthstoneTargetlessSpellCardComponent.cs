namespace hearthstone
{
    public abstract class HearthstoneTargetlessSpellCardComponent : HearthstoneCardComponent
    {
        protected HearthstoneTargetlessSpellCardComponent() { }

        public HearthstoneTargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract void Cast(HearthstoneGame game);
    }
}
