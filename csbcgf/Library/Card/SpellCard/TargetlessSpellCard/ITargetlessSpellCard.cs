using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetlessSpellCard : ITargetless, ISpellCard
    {
        /// <summary>
        /// Retrieve all Actions to be performed when this spell card is played.
        /// </summary>
        /// <param name="gameState"></param>
        /// <returns>All Actions to be performed when this spell card is played.</returns>
        List<IAction> GetActions(IGame game);
    }
}
