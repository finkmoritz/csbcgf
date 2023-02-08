namespace csbcgf
{
    public class TargetlessSpellCard<T> : SpellCard, ITargetlessSpellCard<T> where T : IGameState
    {
        protected TargetlessSpellCard() : base() { }

        public TargetlessSpellCard(bool _ = true) : base(_) { }

        public void Cast(IGame<T> game)
        {
            foreach (ICardComponent component in Components)
            {
                if (component is ITargetlessSpellCardComponent<T> targetlessComponent)
                {
                    targetlessComponent.Cast(game);
                }
            }
        }
    }
}
