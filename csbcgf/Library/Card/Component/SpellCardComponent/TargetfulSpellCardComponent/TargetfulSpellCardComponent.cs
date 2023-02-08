namespace csbcgf
{
    public abstract class TargetfulSpellCardComponent<T> : CardComponent, ITargetfulSpellCardComponent<T> where T : IGameState
    {
        protected TargetfulSpellCardComponent() { }

        public TargetfulSpellCardComponent(int mana) : base(mana)
        {
        }

        void ITargetfulSpellCardComponent.Cast(IGame game, ICharacter target)
        {
            if (game is IGame<T> g)
            {
                Cast(g, target);
            }
        }

        public abstract void Cast(IGame<T> game, ICharacter target);

        ISet<ICharacter> ITargetful.GetPotentialTargets(IGameState gameState)
        {
            if (gameState is T s)
            {
                return GetPotentialTargets(s);
            }
            else
            {
                return new HashSet<ICharacter>();
            }
        }

        public abstract ISet<ICharacter> GetPotentialTargets(T gameState);
    }
}
