namespace csbcgf
{
    public class TargetlessSpellCard : SpellCard, ITargetlessSpellCard
    {
        protected TargetlessSpellCard() : base() {}

        public TargetlessSpellCard(bool initialize = true) : base(initialize) {}

        public void Cast(IGame game)
        {
            foreach (ICardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent targetlessComponent)
                {
                    targetlessComponent.Cast(game);
                }
            }
        }
    }
}
