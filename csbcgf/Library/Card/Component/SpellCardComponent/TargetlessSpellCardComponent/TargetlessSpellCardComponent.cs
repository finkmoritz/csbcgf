namespace csbcgf
{
    public abstract class TargetlessSpellCardComponent<T> : CardComponent, ITargetlessSpellCardComponent<T> where T : IGameState
    {
        protected TargetlessSpellCardComponent() { }

        public TargetlessSpellCardComponent(int mana) : base(mana)
        {
        }

        void Cast(IGame game)
        {
            if (game is IGame<T> g)
            {
                Cast(g);
            }
        }

        public abstract void Cast(IGame<T> game);
    }
}
