namespace csbcgf
{
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        protected TargetfulSpellCardComponent () {}

        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        public abstract void Cast(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGameState gameState);
    }
}
