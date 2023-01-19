namespace csbcgf
{
    public abstract class TargetlessSpellCardComponent : CardComponent, ITargetlessSpellCardComponent
    {
        public TargetlessSpellCardComponent(ICard card, int mana) : base(card, mana)
        {
        }

        protected TargetlessSpellCardComponent(ICard card, ManaCostStat manaCostStat,
            List<IReaction> reactions)
            : base(card, manaCostStat, reactions)
        {
        }

        public abstract void Cast(IGame game);
    }
}
