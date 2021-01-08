using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetfulSpellCardComponent : ISpellCardComponent, ITargetful
    {
        /// <summary>
        /// Get all Actions to be performed by this component when the
        /// associated SpellCard is played.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        /// <returns>All Actions to be performed by this component when the
        /// associated SpellCard is played.</returns>
        List<IAction> GetActions(IGame gameState, ICharacter target);
    }
}
