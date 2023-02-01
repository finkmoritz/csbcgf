namespace csbcgf
{
    public abstract class TargetlessSpellCardComponent : CardComponent, ITargetlessSpellCardComponent
    {
        protected TargetlessSpellCardComponent() {}
        
        public TargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract void Cast(IGame game);
    }
}
