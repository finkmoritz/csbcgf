using System.Collections.Generic;

namespace csbcgf
{
    public interface ITargetfulSpellCard : ITargetful, ISpellCard
    {
        /// <summary>
        /// Retrieve all Actions to be performed when this spell card is played.
        /// Targetful spell card components will make use of the specified
        /// target character.
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="target"></param>
        /// <returns>All Actions to be performed when this spell card is played.</returns>
        List<IAction> GetActions(IGame gameState, ICharacter target);
    }
}
