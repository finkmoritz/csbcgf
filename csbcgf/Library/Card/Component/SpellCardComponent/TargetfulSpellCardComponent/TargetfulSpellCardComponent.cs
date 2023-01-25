namespace csbcgf
{
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        protected TargetfulSpellCardComponent () {}

        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        protected TargetfulSpellCardComponent(ManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(manaCostStat, reactions)
        {
        }

        public abstract void Cast(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGameState gameState);
    }
}
