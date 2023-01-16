namespace csbcgf
{
    public interface ITargetlessSpellCardComponent : ISpellCardComponent, ITargetless
    {
        /// <summary>
        /// Called when spell card is cast. Execute Actions here.
        /// </summary>
        /// <param name="gameState"></param>
        void Cast(IGame game);
    }
}
