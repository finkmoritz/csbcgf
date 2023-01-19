namespace csbcgf
{
    public abstract class TargetfulSpellCardComponent : CardComponent, ITargetfulSpellCardComponent
    {
        public TargetfulSpellCardComponent(ICard card, int mana) : base(card, mana)
        {
        }

        protected TargetfulSpellCardComponent(ICard card, ManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(card, manaCostStat, reactions)
        {
        }

        public abstract void Cast(IGame game, ICharacter target);

        public abstract HashSet<ICharacter> GetPotentialTargets(IGameState gameState);
    }
}
