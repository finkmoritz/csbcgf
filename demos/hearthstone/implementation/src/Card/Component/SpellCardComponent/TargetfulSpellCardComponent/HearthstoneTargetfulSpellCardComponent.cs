using csbcgf;

namespace hearthstone
{
    public abstract class HearthstoneTargetfulSpellCardComponent : HearthstoneCardComponent
    {
        protected HearthstoneTargetfulSpellCardComponent() { }

        public HearthstoneTargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract void Cast(HearthstoneGame game, IStatContainer target);

        public abstract ISet<IStatContainer> GetPotentialTargets(HearthstoneGameState gameState);
    }
}
