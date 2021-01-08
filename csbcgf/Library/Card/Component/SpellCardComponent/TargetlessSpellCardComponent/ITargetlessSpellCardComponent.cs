using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetlessSpellCardComponent : ISpellCardComponent, ITargetless
    {
        /// <summary>
        /// Get all Actions to be performed by this component when the
        /// associated SpellCard is played.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>All Actions to be performed by this component when the
        /// associated SpellCard is played.</returns>
        List<IAction> GetActions(IGame gameState);
    }
}
