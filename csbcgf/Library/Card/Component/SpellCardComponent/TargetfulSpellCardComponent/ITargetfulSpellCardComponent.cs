namespace csbcgf
{
    public interface ITargetfulSpellCardComponent : ISpellCardComponent, ITargetful
    {
        /// <summary>
        /// Called when the spell card is cast. Execute Actions here.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        void Cast(IGame game, ICharacter target);
    }
}
